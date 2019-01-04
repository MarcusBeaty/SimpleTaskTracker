using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

namespace SimpleTaskTracker.XAML
{
    /// <summary>
    /// Interaction logic for Settings_Page.xaml
    /// </summary>
    public partial class Settings_Page : Page
    {
        public Settings_Page()
        {
            InitializeComponent();
            PopulateListBox();
        }

        private void PopulateListBox()
        {
            foreach (var x in Properties.Settings.Default.Presets)
            {
                ListBox.Items.Add(x);
            }
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            switch (WarningsChk.IsChecked)
            {
                case true: Properties.Settings.Default.Warnings = true;
                    break;

                case false: Properties.Settings.Default.Warnings = false;
                    break;
            }


            switch (AutoDate.IsChecked)
            {
                case true: Properties.Settings.Default.AutoDate = true;
                    break;

                case false: Properties.Settings.Default.AutoDate = false;
                    break;
            }
            //SaveSettings.IsEnabled = false;
        }

        private void WarningsChk_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded) SaveSettings_Click(sender, e);
        }

        private void WarningsChk_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded) SaveSettings_Click(sender, e);
        }

        private void AutoDate_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded) SaveSettings_Click(sender, e);
        }

        private void AutoDate_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded) SaveSettings_Click(sender, e);
        }

        private void AutoDateText_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsLoaded)
            {
                if (AutoDate.IsChecked == true)
                {
                    AutoDate.IsChecked = false;
                    SaveSettings_Click(sender, e);
                    //SaveSettings.IsEnabled = true;
                }

                else
                {
                    AutoDate.IsChecked = true;
                    SaveSettings_Click(sender, e);
                    //SaveSettings.IsEnabled = true;
                }

            }
        }

        private void WarningText_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsLoaded)
            {
                if (WarningsChk.IsChecked == true)
                {
                    WarningsChk.IsChecked = false;
                    SaveSettings_Click(sender, e);
                    //SaveSettings.IsEnabled = true;
                }

                else
                {
                    WarningsChk.IsChecked = true;
                    SaveSettings_Click(sender, e);
                    //SaveSettings.IsEnabled = true;
                }
            }
        }

        private void AddTaskPreset_Click(object sender, RoutedEventArgs e)
        {
            var Input = ListBoxInput.Text;

            if (ValidationCheck(Input))
            {
                Properties.Settings.Default.Presets.Add(Input);
                ListBox.Items.Add(Input);
                ListBoxInput.Clear();
                ListBox.Items.Refresh();
            }
            
        }

        private bool ValidationCheck(string presetName)
        {
            if (string.IsNullOrWhiteSpace(presetName))
            {
                MessageBox.Show("Task Name is required, please enter a valid Task Name.", "Simple Task Tracker", MessageBoxButton.OK);
                ListBoxInput.Clear();
                return false;
            }

            // If input char length is longer than 90
            else if (presetName.Length > 90)
            {
                MessageBox.Show("Character limit of 90 exceeded, please try again.", "Simple Task Tracker", MessageBoxButton.OK);
                return false;
            }

            else if (Properties.Settings.Default.Presets.Contains(presetName))
            {
                MessageBox.Show("The entered Task Name is already in use, please enter a valid Task Name.", "Simple Task Tracker", MessageBoxButton.OK);
                return false;
            }
            return true;
        }

        private void RemoveTaskPreset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Selected = ListBox.SelectedItem;
                Properties.Settings.Default.Presets.Remove(Selected.ToString());
                ListBox.Items.Remove(Selected);
                ListBox.Items.Refresh();
                ListBox.UnselectAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox.SelectedItem is null)
                RemoveTaskPreset.IsEnabled = false;
            else
                RemoveTaskPreset.IsEnabled = true;
        }

        private void ListBoxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ListBoxInput.Text == string.Empty)
                AddTaskPreset.IsEnabled = false;
            else
                AddTaskPreset.IsEnabled = true;
        }

        private void ListBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                AddTaskPreset_Click(sender, e);
            }
        }
    }
}
