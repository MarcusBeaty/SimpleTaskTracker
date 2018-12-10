using SimpleTaskTracker.Database;
using SimpleTaskTracker.XAML;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using System.Windows.Shapes;

namespace SimpleTaskTracker.XAML
{
    /// <summary>
    /// Interaction logic for NewTaskDialog.xaml
    /// </summary>
    public partial class NewTaskDialog : Window
    {
        private Tasks_Page _tsks;

        public NewTaskDialog() { }

        public NewTaskDialog(Tasks_Page tsks)
        {
            InitializeComponent();
            
            _tsks = tsks;
            taskEntry.Focus();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {        
            string input = taskEntry.Text;

            // Checking database for existing name
            using (var db = new DataEntities())
            {               
                DateTime currentDate = DateTime.Now;
                var date = currentDate.ToString("M-d-yyyy");

                // If AutoDate setting is checked
                if (Properties.Settings.Default.AutoDate)
                {
                    currentDate = DateTime.Now;
                    date = currentDate.ToString("M-d-yyyy");
                    _tsks.TaskName = input + ($" | {date}");
                }

                else
                {
                    _tsks.TaskName = input;
                }

                foreach (var prop in db.Properties)
                {
                    var name = prop.Task;

                    // If input name exists
                    if (Properties.Settings.Default.AutoDate)
                    {
                        if ((input + ($" | {date}")) == name)
                        {
                            MessageBox.Show("The entered Task Name is already in use, please enter a valid Task Name.", "Simple Task Tracker", MessageBoxButton.OK);
                            //taskEntry.Clear();
                            taskEntry.Focus();
                            return;
                        }
                    }

                    else
                    {
                        if (input == name)
                        {
                            MessageBox.Show("The entered Task Name is already in use, please enter a valid Task Name.", "Simple Task Tracker", MessageBoxButton.OK);
                            //taskEntry.Clear();
                            taskEntry.Focus();
                            return;
                        }
                    }
                    
                }
            }
                // If input is empty
                if (input == "")
                {
                    MessageBox.Show("Task Name is required, please enter a valid Task Name.", "Simple Task Tracker", MessageBoxButton.OK);
                    //taskEntry.Clear();
                    taskEntry.Focus();
                    return;
                }

                // If input char length is longer than 90
                else if (input.Length > 90)
                {
                    MessageBox.Show("Character limit of 90 exceeded, please try again.", "Simple Task Tracker", MessageBoxButton.OK);
                    //taskEntry.Clear();
                    taskEntry.Focus();
                    return;
                }
            DialogResult = true;
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            Owner.Opacity = 1;
        }

        private void TaskEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter || e.Key == Key.Return)
            {
                Submit(sender,e);
            }

            if (e.Key == Key.Escape)
            {
                this.Close();
                Owner.Opacity = 1;
            }
        }
    }
}
