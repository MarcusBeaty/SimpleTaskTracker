using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SimpleTaskTracker_Data;
using SimpleTaskTracker_Services;

namespace SimpleTaskTracker.XAML
{
    /// <summary>
    /// Interaction logic for NewTaskDialog.xaml
    /// </summary>
    public partial class NewTaskDialog : Window
    {
        public string TaskName { get; set; }
        private readonly TaskService taskService;

        public NewTaskDialog()
        {
            InitializeComponent();
            taskService = new TaskService();
            taskEntry.Focus();

            PopulatePresets();  
        }


        private void PopulatePresets()
        {
            var Presets = Properties.Settings.Default.Presets;

            if(Presets != null)
            {
                foreach (var p in Properties.Settings.Default.Presets)
                {
                    taskEntry.Items.Add(p);
                }
            }
           
        }

        private void Submit(object sender, RoutedEventArgs e)
        {        
            string input = taskEntry.Text;

            // Checking database for existing name
            DateTime currentDate = DateTime.Now;
            var date = currentDate.ToString("M-d-yyyy");

            // If AutoDate setting is checked
            if (Properties.Settings.Default.AutoDate)
            {
                currentDate = DateTime.Now;
                date = currentDate.ToString("M-d-yyyy");
                TaskName = input + ($" | {date}");
            }

            else
            {
                TaskName = input;
            }

            foreach (var task in taskService.List())
            {
                var name = task.TaskName;

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

            // If input is empty
            if (string.IsNullOrWhiteSpace(input))
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

        private void TaskEntry_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox TxtBox = (TextBox)taskEntry.Template.FindName("PART_EditableTextBox", taskEntry);
            TxtBox.Focus();
        }
    }
}
