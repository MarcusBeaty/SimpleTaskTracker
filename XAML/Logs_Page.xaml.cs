using Microsoft.Win32;
using SimpleTaskTracker.Database;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SimpleTaskTracker.XAML
{
    /// <summary>
    /// Interaction logic for Logs_Page.xaml
    /// </summary>
    public partial class Logs_Page : Page
    {
        private Tasks_Page _tskpg;
        private CheckBox checkBoxHeader;
        private StringCollection list = Properties.Settings.Default.TabNames;

        public Logs_Page() { }

        public Logs_Page(Tasks_Page tsk_pg)
        {
            InitializeComponent();
            _tskpg = tsk_pg;
            dataGrid.ItemsSource = Tasks_Page.col;
        }

        public void Remove_Click(object sender, RoutedEventArgs e)
        {
            // Storing every item that is Selected ( value of 1 )
            var selected = Tasks_Page.col.Where(x => x.Selected == 1);

            // Do nothing if nothing is selected
            if (selected.Count() != 0)
            {
                // If user has the Warning Setting enabled
                if (Properties.Settings.Default.Warnings)
                {
                    MessageBoxResult UserSelection = MessageBox.Show("Are you sure you would like to remove the entry/entries ?", "Simple Task Tracker", MessageBoxButton.YesNo);
                    switch (UserSelection)
                    {
                        case MessageBoxResult.Yes:
                            break;

                        case MessageBoxResult.No:
                            return;
                    }
                }
                // Creating a temp array to iterate from that doesn't change in size 
                var selectedArr = selected.ToArray();
                MarkSelected(selectedArr);
            }
        }

        private async void MarkSelected(Property[] Selected)
        {
            using (var db = new DataEntities())
            {
                // Storing every item that is Selected ( value of 1 )
                foreach (var x in Selected)
                {
                    // Storing name of each selected item from observable collection
                    var name = x.Task;

                    // Selecting database row that corresponds with selected observable collection row
                    var dbEntry = db.Properties.SingleOrDefault(i => i.Task == name);

                    // Setting it to "Selected" in database
                    dbEntry.Selected = 1;

                    // Saving and repopulating
                    await db.SaveChangesAsync();

                    RemoveTab(name);
                }
                RemoveDatabaseEntries();
            }
        }

        private async void RemoveDatabaseEntries()
        {
            // Code to remove entries that are in DataGrid selected
            using (var db = new DataEntities())
            {
                var dbSelected = db.Properties.Where(x => x.Selected == 1);
                db.Properties.RemoveRange(dbSelected);
                await db.SaveChangesAsync();
                Tasks_Page.RefreshObservableCollection();
                Delete_Btn.IsEnabled = false;
                checkBoxHeader.IsChecked = false;
            }
        }

        private void RemoveTab(string TabName)
        {
            // Name of the Tab to remove, removing from tab items and from re-create list
            try
            {
                var nameOfTab = _tskpg.tabCtrl.Items.OfType<TabItem>()
               .First(n => n.Uid == TabName);

                var total = _tskpg.tabCtrl.Items.Count;
                var thisIndex = _tskpg.tabCtrl.SelectedIndex;
                var newIndex = (thisIndex - 1);

                if (newIndex != -1)
                {
                    _tskpg.tabCtrl.SelectedIndex = newIndex;
                }

                _tskpg.tabCtrl.Items.Remove(nameOfTab);
                list.Remove(TabName);
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private void CheckBoxHeader_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var prop in Tasks_Page.col)
            {
                prop.Selected = 1;
            }
        }

        private void CheckBoxHeader_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var prop in Tasks_Page.col)
            {
                prop.Selected = 0;
            }
        }

        private void CheckBoxHeader_Loaded(object sender, RoutedEventArgs e)
        {
            checkBoxHeader = (CheckBox)sender;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (Tasks_Page.col.Any(x => x.Selected == 1))
            {
                Delete_Btn.IsEnabled = true;
            }

            else
            {
                Delete_Btn.IsEnabled = false;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Tasks_Page.col.Any(x => x.Selected == 1))
            {
                Delete_Btn.IsEnabled = true;
            }

            else
            {
                Delete_Btn.IsEnabled = false;
            }
        }

        private string GetReports()
        {
            var Data = string.Empty;
            var Header = "Task,Clock-In,Clock-Out,Total(Hours),Last Closed\n";
            var dataGridValues = dataGrid.Items;

            Data += Header;

            foreach (Property itm in dataGridValues)
            {
                var rowValues = $"{itm.Task},{itm.ClockIn},{itm.ClockOut},{itm.Total},{itm.LastClosed}\n";
                Data += rowValues;
            }
            return Data;
        }


        private void SaveSpreadsheet(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            var saveFileDialog = new SaveFileDialog
            {
                FileName = "STT_Reports",
                DefaultExt = ".csv",
                Filter = "CSV (Comma delimited) (.csv)|*.csv"
            };

            // Save file dialog box
            var Data = GetReports();
            var result = saveFileDialog.ShowDialog();

            if (result is true)
            {
                // Saving spreadsheet to user's desired location
                var filename = saveFileDialog.FileName;
                File.WriteAllText(filename, Data);
            }
        }

        private void OpenSpreadsheet(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "CSV(Comma delimited)(.csv) | *.csv",
                Title = "Select a Spreadsheet"
            };

            var result = openFileDialog.ShowDialog();
            if (result is true)
            {
                var Data = File.ReadAllText(openFileDialog.FileName);
                LoadSpreadsheet(Data);
            }
        }

        private void LoadSpreadsheet(string Data)
        {
            // Ignore First Line(5 elements), split data by comma
            var splitData = Data.Split(new string[] { "\n", "," }, StringSplitOptions.None);

            try
            {
                var Entries = ConvertData(splitData);
                DatabaseClear();
                ResetTabBar();
                DatabaseAdd(Entries);
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
                MessageBox.Show("Load failed. Spreadsheet format is incorrect.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetTabBar()
        {
            // Code to add Default Tab Home Page
            var tabCount = _tskpg.tabCtrl.Items.Count;
            var main = (TabItem)_tskpg.tabCtrl.Items.GetItemAt(tabCount - 1);
            _tskpg.tabCtrl.Items.Clear();
            Properties.Settings.Default.TabNames.Clear();
            _tskpg.tabCtrl.Items.Add(main);
            main.Content = new Task_Home(_tskpg);
            _tskpg.tabCtrl.Items.Refresh();
        }

        private List<Property> ConvertData(string[] splitData)
        {
            var Entries = new List<Property>();

            for (var i = 5; i < splitData.Count() - 1; i += 5)
            {
                var csvEntry = new Property { Task = splitData[i] };

                if (splitData[i + 1] != string.Empty)
                    csvEntry.ClockIn = Convert.ToDateTime(splitData[i + 1]);

                if (splitData[i + 2] != string.Empty)
                    csvEntry.ClockOut = Convert.ToDateTime(splitData[i + 2]);

                if (splitData[i + 3] != string.Empty)
                    csvEntry.Total = Convert.ToDouble(splitData[i + 3]);

                if (splitData[i + 4] != string.Empty)
                    csvEntry.LastClosed = Convert.ToDateTime(splitData[i + 4]);

                Entries.Add(csvEntry);
            }
            return Entries;
        }

        private void DatabaseAdd(IEnumerable<Property> Entries)
        {
            using (var db = new DataEntities())
            {
                db.Properties.AddRange(Entries);
                db.SaveChanges();
                Tasks_Page.RefreshObservableCollection();
            }
        }

        private void DatabaseClear()
        {
            using (var db = new DataEntities())
            {
                var Existing = db.Properties.Select(entries => entries);
                db.Properties.RemoveRange(Existing);
                db.SaveChanges();
                Tasks_Page.RefreshObservableCollection();
            }
        }
    }
}
