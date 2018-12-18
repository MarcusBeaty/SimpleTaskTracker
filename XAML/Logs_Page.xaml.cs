using Microsoft.Win32;
using SimpleTaskTracker.Database;
using System;
using System.Collections.Specialized;
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



        public async void Remove_Selected(object sender, RoutedEventArgs e)
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

                using (var db = new DataEntities())
                {
                    // Storing every item that is Selected ( value of 1 )
                    //selected = Tasks_Page.col.Where(x => x.Selected == 1);

                    // Creating a temp array to iterate from that doesn't change in size 
                    var selectedArr = selected.ToArray();

                    foreach (var x in selectedArr)
                    {
                        // Storing name of each selected item from observable collection
                        var name = x.Task;

                        // Selecting database row that corresponds with selected observable collection row
                        var dbEntry = db.Properties.SingleOrDefault(i => i.Task == name);

                        // Setting it to "Selected" in database
                        dbEntry.Selected = 1;

                        // Saving and repopulating
                        await db.SaveChangesAsync();

                        // Name of the Tab to remove, removing from tab items and from re-create list
                       
                        var nameOfTab = _tskpg.tabCtrl.Items.OfType<TabItem>()
                            .SingleOrDefault(n => n.Uid == name);

                        var total = _tskpg.tabCtrl.Items.Count;
                        var thisIndex = _tskpg.tabCtrl.SelectedIndex;
                        var newIndex = (thisIndex - 1);


                        if (newIndex != -1)
                        {
                            _tskpg.tabCtrl.SelectedIndex = newIndex;
                        }

                        _tskpg.tabCtrl.Items.Remove(nameOfTab);
                        list.Remove(name);
                    }

                    // code to remove entries with selected
                    var dbSelected = db.Properties.Where(x => x.Selected == 1);
                    db.Properties.RemoveRange(dbSelected);
                    await db.SaveChangesAsync();
                    Tasks_Page.RefreshObservableCollection();
                    Delete_Btn.IsEnabled = false;
                    checkBoxHeader.IsChecked = false;
                }
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

        private string GetSpreadsheetData()
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
                FileName = "STT_Reports", // Default file name
                DefaultExt = ".csv", // Default file extension
                Filter = "CSV (Comma delimited) (.csv)|*.csv" // Filter files by extension
            };

            // Save file dialog box
            var Data = GetSpreadsheetData();
            var result = saveFileDialog.ShowDialog();

            if (result is true)
            {
                // Saving spreadsheet to user's desired location
                string filename = saveFileDialog.FileName;
                File.WriteAllText(filename, Data);
            }
        }
    }
}
