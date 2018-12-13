using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using SimpleTaskTracker.Database;

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

        public Stopwatch(string TaskName, MainWindow mw, bool NewTab)
        {
            InitializeComponent();
            DataContext = this;
            _mw = mw;
            _taskName = TaskName;
            _newTab = NewTab;
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

            using (var db = new DataEntities())
            {
                // using exist check for error: when clearing database but leaving tab open
                bool exists = db.Properties.Any(x => x.Task == _taskName);

                if (exists)
                {
                    var Property = db.Properties.First(x => x.Task == _taskName);
                    
                    // null handling 
                    if(Property.ClockIn != null)
                    { 
                        hours = Property.Hours.Value;
                        minutes = Property.Minutes.Value;
                        seconds = Property.Seconds.Value;
                    }

                    // Recreated Tab Button Handling
                    RecreatedButtonConfig(Property);
                }
            }
        }

        private void RecreatedButtonConfig(Property property)
        {
            if (property.ClockIn.HasValue)
            {
                ClockIn.Visibility = Visibility.Hidden;
                Resume.Visibility = Visibility.Visible;
                Resume.IsEnabled = true;
                ClockOut.IsEnabled = false;
                ClockIn.IsEnabled = false;
                StartBreak.IsEnabled = false;
                EndBreak.IsEnabled = false;

                if (property.ClockOut.HasValue)
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
            await Task.Delay(500);
            ClockOut.IsEnabled = true; 
        }

        private async void ClockIn_Click(object sender, RoutedEventArgs e)
        {
            ClockOut.Visibility = Visibility.Visible;
            ClockIn.IsEnabled = false;
            ClockIn.Visibility = Visibility.Hidden;
            StartBreak.IsEnabled = true;
            StartBreak.Visibility = Visibility.Visible;

            // Assigning value to database
            using (var db = new DataEntities())
            {
                var _current = db.Properties.First(x => x.Task == _taskName);
                _current.ClockIn = DateTime.Now;
                await db.SaveChangesAsync();
                Tasks_Page.RefreshObservableCollection();
            }
            // Starting StopWatch
            dpTimer.Start();
            sw.Start();

            Delay(sender, e);  
        }

        private async void ClockOut_Click(object sender, RoutedEventArgs e)
        {
            Resume.IsEnabled = false;
            EndBreak.IsEnabled = false;
            StartBreak.IsEnabled = false;
            ClockOut.IsEnabled = false;
            Edit_Btn.IsEnabled = false;

            // Assigning value to database
            using (var db = new DataEntities())
            {
                var _current = db.Properties.First(x => x.Task == _taskName);
                _current.ClockOut = DateTime.Now;
                _current.Hours = ts.Hours;
                _current.Minutes = ts.Minutes;
                _current.Seconds = ts.Seconds;
                _current.Total = Math.Round(ts.TotalHours, 4);

                await db.SaveChangesAsync();
                Tasks_Page.RefreshObservableCollection();
            }

            //Stoping StopWatch
            sw.Stop();
            dpTimer.Stop();
        }


        private async void Start_Break(object sender, RoutedEventArgs e)
        {
            StartBreak.IsEnabled = false;
            StartBreak.Visibility = Visibility.Hidden;
            EndBreak.IsEnabled = true;
            EndBreak.Visibility = Visibility.Visible;
            //Stoping StopWatch and Dispatch Timer

            sw.Stop();
            dpTimer.Stop();

            using (var db = new DataEntities())
            {
                var _current = db.Properties.First(x => x.Task == _taskName);
                _current.Hours = ts.Hours;
                _current.Minutes = ts.Minutes;
                _current.Seconds = ts.Seconds;
                _current.Total = Math.Round(ts.TotalHours, 4);
                await db.SaveChangesAsync();
                Tasks_Page.RefreshObservableCollection();
            }
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

        public async void Exiting(object sender, global::System.ComponentModel.CancelEventArgs exeception)
        {
            using (var db = new DataEntities())
            {
                try
                {
                    var _current = db.Properties.FirstOrDefault(x => x.Task == _taskName);

                    if (_current != null)
                    {
                        _current.Hours = ts.Hours;
                        _current.Minutes = ts.Minutes;
                        _current.Seconds = ts.Seconds;
                        _current.Total = Math.Round(ts.TotalHours, 4);
                        await db.SaveChangesAsync();
                    }
                }

                catch(InvalidOperationException exception)
                {
                    Debug.WriteLine("Error: " + exception);
                    throw;
                }
                
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

        private async void RenameTask(string NewName)
        {
            // Changing corresponding Task Name in DB to new Task Name
            using (var db = new DataEntities())
            {
                var propInDb = db.Properties.First(n => n.Task == _taskName);
                propInDb.Task = NewName;
                var save = await db.SaveChangesAsync();
            }

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
            var propInCollection = Tasks_Page.col.First(n => n.Task == _taskName);
            propInCollection.Task = NewName;
            Tasks_Page.RefreshObservableCollection();
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
            if (e.Key == Key.N && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                var test = Parent as Tasks_Page;
                test.OnPlusTabClick(sender, e);
            }
        }
    }
}
