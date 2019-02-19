using Microsoft.Win32;
using SimpleTaskTracker_Data;
using SimpleTaskTracker_Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace SimpleTaskTracker.XAML
{
    /// <summary>
    /// Interaction logic for Logs_Page.xaml
    /// </summary>
    public partial class Logs_Page : Page
    {
        private Tasks_Page _tskpg;
        private ObservableCollectionService collectionService;
        private TaskService taskService;
        private CheckBox checkBoxHeader;
        private StringCollection list = Properties.Settings.Default.TabNames;

        public Logs_Page() { }

        public Logs_Page(Tasks_Page tsk_pg)
        {
            InitializeComponent();
            _tskpg = tsk_pg;

            taskService = new TaskService();
            collectionService = new ObservableCollectionService();

            dataGrid.ItemsSource = ObservableCollectionService.Collection;
            collectionService.Refresh();
        }

        public void Remove_Click(object sender, RoutedEventArgs e)
        {
            // Storing every item that is Selected ( value of 1 )
            var Selected = ObservableCollectionService.Collection.Where(x => x.Selected == 1);

            // Do nothing if nothing is selected
            if (Selected.Count() != 0)
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
                var selectedArr = Selected.ToArray();
                MarkSelected(selectedArr);
            }
        }

        private void MarkSelected(Task[] Selected)
        {
            // Storing every item that is Selected ( value of 1 )
            var selectedItems = Selected.Select(x=> x.Id);

            if (selectedItems.Count() == 1)
            {
                var Id = Convert.ToInt32(selectedItems.First());
                taskService.Delete(Id);
            }
            else
            {
                //var Ids = Convert.ToInt32(selectedItems);
                taskService.Delete(selectedItems);
            }

            foreach (var s in Selected)
            {
                RemoveTab(s.TaskName);
            }

            collectionService.Refresh();

            Delete_Btn.IsEnabled = false;
            checkBoxHeader.IsChecked = false;
            /*foreach (var x in Selected)
            {
                // Storing name of each selected item from observable collection
                var name = x.TaskName;

                // Selecting database row that corresponds with selected observable collection row
                var dbEntry = _taskService.Get(name);
                entriesToRemove.Add(dbEntry);
               
                
                // Setting it to "Selected" in database
                dbEntry.Selected = 1;
                _taskService.Update(dbEntry);*/

            // RemoveTab(name);
            //RemoveDatabaseEntries();
        }

        private async void RemoveDatabaseEntries()
        {
            // Code to remove entries that are in DataGrid selected
            using (var db = new AppDBContext())
            {
                var dbSelected = db.Tasks.Where(x => x.Selected == 1);
                db.Tasks.RemoveRange(dbSelected);
                await db.SaveChangesAsync();
                collectionService.Refresh();
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
            foreach (var prop in ObservableCollectionService.Collection)
            {
                prop.Selected = 1;  
            }
        }

        private void CheckBoxHeader_Unchecked(object sender, RoutedEventArgs e)
        {
            var Collection = ObservableCollectionService.Collection;

            foreach (var prop in Collection)
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
            var Collection = ObservableCollectionService.Collection;

            if (Collection.Any(x => x.Selected == 1))
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
            var Collection = ObservableCollectionService.Collection;

            if (Collection.Any(x => x.Selected == 1))
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

            foreach (Task itm in dataGridValues)
            {
                var rowValues = $"{itm.TaskName},{itm.ClockIn},{itm.ClockOut},{itm.Total},{itm.LastClosed}\n";
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

        private List<Task> ConvertData(string[] splitData)
        {
            var Entries = new List<Task>();

            for (var i = 5; i < splitData.Count() - 1; i += 5)
            {
                var csvEntry = new Task { TaskName = splitData[i] };

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

        private void DatabaseAdd(IEnumerable<Task> Entries)
        {
            taskService.Add(Entries);
            collectionService.Refresh();
        }

        private void DatabaseClear()
        {
            taskService.DeleteAll();
            collectionService.Refresh();
        }

        private void FilterReports_Click(object sender, RoutedEventArgs e)
        {
            var From = (DateTime)DatePicker_From.SelectedDate;
            var To = (DateTime)DatePicker_To.SelectedDate;
            collectionService.Filter(From,To);
        }

        private void DatePicker_From_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker_To.DisplayDateStart = DatePicker_From.SelectedDate;
        }

        private void DatePicker_To_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker_From.DisplayDateEnd = DatePicker_To.SelectedDate;
        }
    }
}
