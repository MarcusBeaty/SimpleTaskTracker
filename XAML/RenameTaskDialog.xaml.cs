using SimpleTaskTracker.Database;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for RenameTaskDialog.xaml
    /// </summary>
    public partial class RenameTaskDialog : Window
    {
        public string Input { get; set; }

        public RenameTaskDialog(string TabName)
        {
            InitializeComponent();
            //taskEntry.Text = TabName;
            taskEntry.CaretIndex = TabName.Count();
            taskEntry.Focus();
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            // Store user input
            var initialInput = taskEntry.Text;

            var currentDate = DateTime.Now;
            var date = currentDate.ToString("M-d-y");

            // If AutoDate setting is checked - Date is appended to Name
            if (Properties.Settings.Default.AutoDate)
            {
                currentDate = DateTime.Now;
                date = currentDate.ToString("M-d-y");
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
            using (var db = new DataEntities())
            {
                foreach (var prop in db.Properties)
                {
                    // Storing each DB Task Name
                    var dbName = prop.Task;

                    if (name == dbName)
                    {
                        MessageBox.Show("The entered Task Name is already in use, please enter a valid Task Name.", "Simple Task Tracker", MessageBoxButton.OK);
                        //taskEntry.Clear();
                        taskEntry.Focus();
                        return false;
                    }
                }
            }

            // If input is empty
            if (name == "")
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
    }
}
