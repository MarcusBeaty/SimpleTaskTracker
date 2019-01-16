using SimpleTaskTracker_Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SimpleTaskTracker_Services;

namespace SimpleTaskTracker.XAML
{
    /// <summary>
    /// Interaction logic for RenameTaskDialog.xaml
    /// </summary>
    public partial class RenameTaskDialog : Window
    {
        public string Input { get; set; }
        private readonly TaskService taskService;

        public RenameTaskDialog(string TabName)
        {
            InitializeComponent();
            taskService = new TaskService();
            //taskEntry.CaretIndex = TabName.Count();
            taskEntry.Focus();
            PopulatePresets();
        }

        private void PopulatePresets()
        {
            var Presets = Properties.Settings.Default.Presets;

            if (Presets != null)
            {
                foreach (var p in Properties.Settings.Default.Presets)
                {
                    taskEntry.Items.Add(p);
                }
            }
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            // Store user input
            var initialInput = taskEntry.Text;

            var currentDate = DateTime.Now;
            var date = currentDate.ToString("M-d-yyyy");

            // If AutoDate setting is checked - Date is appended to Name
            if (Properties.Settings.Default.AutoDate)
            {
                currentDate = DateTime.Now;
                date = currentDate.ToString("M-d-yyyy");
                initialInput += ($" | {date}");
            }
            
            // Validation Checks
            if(ValidateName(initialInput))
            {
                Input = initialInput;
                DialogResult = true;
                this.Close();
            }
        }

        private bool ValidateName(string name)
        {
            // Checking database for existing name
            var Tasks = taskService.List();
            foreach(var Task in Tasks)
            {
                var dbName = Task.TaskName;
                if (name == dbName)
                {
                    MessageBox.Show("The entered Task Name is already in use, please enter a valid Task Name.", "Simple Task Tracker", MessageBoxButton.OK);
                    //taskEntry.Clear();
                    taskEntry.Focus();
                    return false;
                }
            }

            // If input is empty
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Task Name is required, please enter a valid Task Name.", "Simple Task Tracker", MessageBoxButton.OK);
                //taskEntry.Clear();
                taskEntry.Focus();
                return false;
            }

            // If input char length is longer than 90
            else if (name.Length > 90)
            {
                MessageBox.Show("Character limit of 90 exceeded, please try again.", "Simple Task Tracker", MessageBoxButton.OK);
                //taskEntry.Clear();
                taskEntry.Focus();
                return false;
            }
            return true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            Owner.Opacity = 1;
        }

        private void TaskEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                Rename_Click(sender, e);
            }

            if(e.Key == Key.Escape)
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
