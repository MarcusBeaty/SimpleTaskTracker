using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using SimpleTaskTracker.XAML;
using SimpleTaskTracker.Database;

namespace SimpleTaskTracker.XAML
{
    /// <summary>
    /// Interaction logic for stopwatch.xaml
    /// </summary>
    public partial class stopwatch : UserControl
    {
        public string _taskName { get; set; }
        public static bool recreate { get; set; }
        private bool tsRec = false;
        private ObservableCollection<Property> _collection;
        Stopwatch sw = new Stopwatch();
        DispatcherTimer dpTimer = new DispatcherTimer();
        private Tasks_Page _tsks;
        TimeSpan ts;
        string elapsedTime;
        int hours;
        int minutes;
        int seconds;

        public stopwatch(Tasks_Page tsks, ObservableCollection<Property> collection)
        {
            InitializeComponent();
            DataContext = this;
            this.Loaded += Stopwatch_Loaded;
            _tsks = tsks;
            _collection = collection;

            if (_tsks.isRecreated)
            {
                // Recreated Timespan activated 
                tsRec = true;
                _taskName = _tsks.ReTaskName;

                using (var db = new DataEntities())
                {
                    // using exist check for error: when clearing database but leaving tab open
                    bool exists = db.Properties.Any(x => x.Task == _taskName);

                    if (exists)
                    {
                        var property = db.Properties.Single(x => x.Task == _taskName);

                        // null handling 
                        if (property.Hours.HasValue)
                        {
                            hours = property.Hours.Value;
                        }

                        if (property.Minutes.HasValue)
                        {
                            minutes = property.Minutes.Value;
                        }

                        if (property.Seconds.HasValue)
                        {
                            seconds = property.Seconds.Value;
                        }
                        // end null handling

                        // catching HasValue exception
                        try
                        {
                            if (property.ClockIn.HasValue)
                            {
                                ClockIn.Visibility = Visibility.Hidden;
                                Resume.Visibility = Visibility.Visible;
                                Resume.IsEnabled = true;
                                ClockOut.IsEnabled = true;
                                ClockIn.IsEnabled = false;
                                StartBreak.IsEnabled = false;
                                EndBreak.IsEnabled = false;

                                if (property.ClockOut.HasValue)
                                {
                                    Resume.IsEnabled = false;
                                    EndBreak.IsEnabled = false;
                                    StartBreak.IsEnabled = false;
                                    ClockIn.IsEnabled = false;
                                    ClockOut.IsEnabled = false;
                                }
                            }
                        }

                        catch (InvalidCastException e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
            }

            else
            {
                _taskName = _tsks.TaskName;
                Property newTask = new Property()
                {
                    Task = _taskName
                };

                _collection.Add(newTask);
            }
            setTime();
            dpTimer.Tick += new EventHandler(Tick);
            dpTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        private void Stopwatch_Loaded(object sender, RoutedEventArgs e)
        {
            // Getting parent window and assigning closing event handler
            Window sw_Window = Window.GetWindow(this);
            sw_Window.Closing += (Exiting);
        }

        void setTime()
        {
            ts = new TimeSpan(hours, minutes, seconds).Add(sw.Elapsed);
            //Formating Display of time
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds,ts.Milliseconds);
            //Outputting to WPF Textbox
            Time.Text = elapsedTime;
        }

        void Tick(object sender, EventArgs e)
        {
            // If tab is being recreated, add database values for timespan to elapsed
            if (tsRec)
            {
                ts = new TimeSpan(hours, minutes, seconds).Add(sw.Elapsed);
            }

            else
            {
                ts = sw.Elapsed;
            }

            //Formating Display of time
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds,ts.Milliseconds);
            //Outputting Time to Textbox
            Time.Text = elapsedTime;
        }

        private async void ClockIn_Click(object sender, RoutedEventArgs e)
        {
            ClockOut.IsEnabled = true;
            ClockIn.IsEnabled = false;
            StartBreak.IsEnabled = true;
            
            // Assigning value to database
            using (var db = new DataEntities())
            {
               var _current = db.Properties.SingleOrDefault(x => x.Task == _taskName);
                _current.ClockIn = DateTime.Now;
                await db.SaveChangesAsync();
                Tasks_Page.loadItems();
            }
            // Starting StopWatch
            dpTimer.Start();
            sw.Start();
        }

        private async void ClockOut_Click(object sender, RoutedEventArgs e)
        {
            Resume.IsEnabled = false;
            EndBreak.IsEnabled = false;
            StartBreak.IsEnabled = false;
            ClockOut.IsEnabled = false;
         
            // Assigning value to database
            using (var db = new DataEntities())
            {
                var _current = db.Properties.SingleOrDefault(x => x.Task == _taskName);
                _current.ClockOut = DateTime.Now;
                _current.Hours = ts.Hours;
                _current.Minutes = ts.Minutes;
                _current.Seconds = ts.Seconds;
                _current.Total = Math.Round(ts.TotalHours, 4);
                
                await db.SaveChangesAsync();
                Tasks_Page.loadItems();
            }

            //Stoping StopWatch
            sw.Stop();
            dpTimer.Stop();
        }


        private async void Start_Break(object sender, RoutedEventArgs e)
        {
            StartBreak.IsEnabled = false;
            EndBreak.IsEnabled = true;
            //Stoping StopWatch and Dispatch Timer
            sw.Stop();
            dpTimer.Stop();

            using (var db = new DataEntities())
            {
                var _current = db.Properties.SingleOrDefault(x => x.Task == _taskName);
                _current.Hours = ts.Hours;
                _current.Minutes = ts.Minutes;
                _current.Seconds = ts.Seconds;
                _current.Total = Math.Round(ts.TotalHours, 4);
                await db.SaveChangesAsync();
                Tasks_Page.loadItems();
            }
        }

        private void End_Break(object sender, RoutedEventArgs e)
        {
            EndBreak.IsEnabled = false;
            StartBreak.IsEnabled = true;
            //Resuming StopWatch and DispatcherTimer
            dpTimer.Start();
            sw.Start();
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            Resume.IsEnabled = false;
            EndBreak.IsEnabled = false;
            StartBreak.IsEnabled = true;
            dpTimer.Start();
            sw.Start();
        }

        public async void Exiting(object sender, global::System.ComponentModel.CancelEventArgs e)
        {
            using (var db = new DataEntities())
            {
                var _current = db.Properties.SingleOrDefault(x => x.Task == _taskName);

                if(_current != null)
                {
                    _current.Hours = ts.Hours;
                    _current.Minutes = ts.Minutes;
                    _current.Seconds = ts.Seconds;
                    _current.Total = Math.Round(ts.TotalHours, 4);
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
