using SimpleTaskTracker_Data;
using SimpleTaskTracker_Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Task = SimpleTaskTracker_Data.Task;

namespace SimpleTaskTracker.XAML
{
    /// <summary>
    /// Interaction logic for stopwatch.xaml
    /// </summary>
    public partial class Stopwatch : UserControl
    {
        public string _taskName { get; set; }
        MainWindow _mw;
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        DispatcherTimer dpTimer = new DispatcherTimer();
        TimeSpan ts;
        string elapsedTime;
        int hours;
        int minutes;
        int seconds;
        bool _newTab;
        private ObservableCollectionService _collectionService;
        private TaskService taskService;

        public Stopwatch(string TaskName, MainWindow mw, bool NewTab, IObservableCollectionService<Task> collectionService)
        {
            InitializeComponent();
            DataContext = this;
            _mw = mw;
            _taskName = TaskName;
            _newTab = NewTab;

            _collectionService = new ObservableCollectionService();
            taskService = new TaskService();

            this.Loaded += Stopwatch_Loaded;
            Startup();
        }

        private void Startup()
        {
            if (_newTab)
                NewTabSetup();
            else
                ExistingTabSetup();

            SetTime();
        }

        private void Stopwatch_Loaded(object sender, RoutedEventArgs e)
        {
            // Getting parent window and assigning closing event handler
            Window sw_Window = Window.GetWindow(this);
            sw_Window.Closing += (Exiting);
        }

        private void NewTabSetup()
        {
            // Using standard tick
            dpTimer.Tick += new EventHandler(Tick);
        }

        private void ExistingTabSetup()
        {
            // Using recreated tick
            dpTimer.Tick += new EventHandler(RecreatedTick);

            // using exist check for error: when clearing database but leaving tab open
            var Task = taskService.Get(_taskName);

            if (Task != null)
            {
                // null handling 
                if(Task.ClockIn != null)
                { 
                    hours = Task.Hours.Value;
                    minutes = Task.Minutes.Value;
                    seconds = Task.Seconds.Value;
                }

                // Recreated Tab Button Handling
                RecreatedButtonConfig(Task);
            }
        }

        private void RecreatedButtonConfig(Task Task)
        {
            if (Task.ClockIn.HasValue)
            {
                ClockIn.Visibility = Visibility.Hidden;
                Resume.Visibility = Visibility.Visible;
                Resume.IsEnabled = true;
                ClockOut.IsEnabled = false;
                ClockIn.IsEnabled = false;
                StartBreak.IsEnabled = false;
                EndBreak.IsEnabled = false;

                if (Task.ClockOut.HasValue)
                {
                    Resume.IsEnabled = false;
                    Edit_Btn.IsEnabled = false;
                    ClockOut.Visibility = Visibility.Visible;
                    Resume.Visibility = Visibility.Hidden;
                    EndBreak.IsEnabled = false;
                    StartBreak.IsEnabled = false;
                    ClockIn.IsEnabled = false;
                    ClockOut.IsEnabled = false;
                }
            }
        }

        void SetTime()
        {
            dpTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            ts = new TimeSpan(hours, minutes, seconds).Add(sw.Elapsed);

            //Formating Display of time
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

            //Outputting to WPF Textbox
            Time.Text = elapsedTime;
        }

        void Tick(object sender, EventArgs e)
        {
            ts = sw.Elapsed;

            //Formating Display of time
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

            //Outputting Time to Textbox
            Time.Text = elapsedTime;
        }

        void RecreatedTick(object sender, EventArgs e)
        {
            ts = new TimeSpan(hours, minutes, seconds).Add(sw.Elapsed);

            //Formating Display of time
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

            //Outputting Time to Textbox
            Time.Text = elapsedTime;
        }

        // Method to be called to prevent accidental hit of
        // Clock-Out button after Clocking In / Resuming 
        private async void Delay(object sender, RoutedEventArgs e)
        {
            await System.Threading.Tasks.Task.Delay(500);
            ClockOut.IsEnabled = true; 
        }

        private void ClockIn_Click(object sender, RoutedEventArgs e)
        {
            ClockOut.Visibility = Visibility.Visible;
            ClockIn.IsEnabled = false;
            ClockIn.Visibility = Visibility.Hidden;
            StartBreak.IsEnabled = true;
            StartBreak.Visibility = Visibility.Visible;

            // Assigning value to database
            var Task = taskService.Get(_taskName);
            Task.ClockIn = DateTime.Now;
            taskService.Update(Task);
            _collectionService.Refresh();
            // Starting StopWatch
            dpTimer.Start();
            sw.Start();

            Delay(sender, e);  
        }

        private void ClockOut_Click(object sender, RoutedEventArgs e)
        {
            if(Properties.Settings.Default.Warnings)
            {
                //Pausing Stopwatch while prompt is displayed
                sw.Stop();
                dpTimer.Stop();

                var Result = MessageBox.Show("Are you sure you would like to Clock-Out and finalize this Task?", "Simple Task Tracker", MessageBoxButton.YesNo);
                if (Result == MessageBoxResult.No)
                {
                    dpTimer.Start();
                    sw.Start();
                    return;
                }
            }

            Resume.IsEnabled = false;
            EndBreak.IsEnabled = false;
            StartBreak.IsEnabled = false;
            ClockOut.IsEnabled = false;
            Edit_Btn.IsEnabled = false;
            
            var Task = taskService.Get(_taskName);

            // Assigning value to database
            Task.ClockOut = DateTime.Now;
            Task.Hours = ts.Hours;
            Task.Minutes = ts.Minutes;
            Task.Seconds = ts.Seconds;
            Task.Total = Math.Round(ts.TotalHours, 4);

            taskService.Update(Task);
            _collectionService.Refresh();

            //Stoping StopWatch
            sw.Stop();
            dpTimer.Stop();
        }

        private void Start_Break(object sender, RoutedEventArgs e)
        {
            StartBreak.IsEnabled = false;
            StartBreak.Visibility = Visibility.Hidden;
            EndBreak.IsEnabled = true;
            EndBreak.Visibility = Visibility.Visible;
            //Stoping StopWatch and Dispatch Timer

            sw.Stop();
            dpTimer.Stop();

            var Task = taskService.Get(_taskName);
            Task.Hours = ts.Hours;
            Task.Minutes = ts.Minutes;
            Task.Seconds = ts.Seconds;
            Task.Total = Math.Round(ts.TotalHours, 4);

            taskService.Update(Task);
            _collectionService.Refresh();
        }

        private void End_Break(object sender, RoutedEventArgs e)
        {
            EndBreak.IsEnabled = false;
            EndBreak.Visibility = Visibility.Hidden;
            StartBreak.IsEnabled = true;
            StartBreak.Visibility = Visibility.Visible;
            //Resuming StopWatch and DispatcherTimer
            dpTimer.Start();
            sw.Start();
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            Resume.IsEnabled = false;
            Resume.Visibility = Visibility.Hidden;
            ClockOut.Visibility = Visibility.Visible;
            EndBreak.IsEnabled = false;
            StartBreak.IsEnabled = true;

            dpTimer.Start();
            sw.Start();

            Delay(sender, e);
        }

        public void Exiting(object sender, global::System.ComponentModel.CancelEventArgs exeception)
        {
            try
            {
                var Task = taskService.Get(_taskName);
                if (Task != null)
                {
                    Task.Hours = ts.Hours;
                    Task.Minutes = ts.Minutes;
                    Task.Seconds = ts.Seconds;
                    Task.Total = Math.Round(ts.TotalHours, 4);
                    taskService.Update(Task);
                }
            }
            catch(InvalidOperationException exception)
            {
                Debug.WriteLine("Error: " + exception);
            }
        }

        private void EditTaskName_Click(object sender, RoutedEventArgs e)
        {
            _mw.Opacity = 0.3;
            var dg = new RenameTaskDialog(SW_Name.Text) { Owner = _mw };
            dg.ShowDialog();

            // If user pressed "Rename New Task"
            if (dg.DialogResult is true)
            {
                RenameTask(dg.Input);
            }
        }

        private void RenameTask(string NewName)
        {
            // Changing corresponding Task Name in DB to new Task Name
            var Task = taskService.Get(_taskName);
            Task.TaskName = NewName;
            taskService.Update(Task);
            // Changing corresponding Task Name in Observable Collection to new Task Name
            UpdateObservableCollection(NewName);

            // Changing corresponding Task Name in Tab List to new Task Name
            UpdateTabList(NewName);

            // Updating parent elements to reflect new Task Name
            UpdateParentElements(NewName);

            // Updates the current Tab : Needs to happen last due to previous TaskName being used in other calls
            UpdateCurrentTab(NewName);
        }

        private void UpdateCurrentTab(string NewName)
        {
            SW_Name.Text = NewName;
            _taskName = NewName;
        }

        private void UpdateParentElements(string NewName)
        {
            var parent = this.Parent as CloseableTabItem;
            parent.TbName = NewName;
            parent.Uid = NewName;
            parent.SetHeader(NewName);
        }

        private void UpdateTabList(string NewName)
        {
            var propInList = Tasks_Page.list;
            // Getting index so that Tab order can be saved
            var indexOfTab = propInList.IndexOf(_taskName);
            propInList.Insert(indexOfTab, NewName);
            propInList.Remove(_taskName);
        }

        private void UpdateObservableCollection(string NewName)
        {
            var propInCollection = ObservableCollectionService.Collection.First(n => n.TaskName == _taskName);
            propInCollection.TaskName = NewName;
            _collectionService.Refresh();
        }

        private void SW_Name_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (SW_Name.ActualHeight <= 28)
            {
                SW_Name.Margin = new Thickness(0, 0, 4, 15);
            }
                

            if(SW_Name.ActualHeight > 28 && SW_Name.ActualHeight < 47)
            {
                SW_Name.Margin = new Thickness(0, 0, 4, 35);
                SW_Name.FontSize = 20;
            }
                

            if (SW_Name.ActualHeight > 47)
            {
                SW_Name.Margin = new Thickness(0, 0, 4, 50);
                SW_Name.FontSize = 18;
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.T && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                var test = Parent as Tasks_Page;
                test.OnPlusTabClick(sender, e);
            }
        }
    }
}
