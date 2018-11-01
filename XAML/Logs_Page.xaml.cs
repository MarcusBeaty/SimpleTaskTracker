using SimpleTaskTracker.Database;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Logs_Page.xaml
    /// </summary>
    public partial class Logs_Page : Page
    {
        private Tasks_Page _tskpg;
        private StringCollection list = Properties.Settings.Default.TabNames;

        public Logs_Page() { }

        public Logs_Page(Tasks_Page tsk_pg)
        {
            InitializeComponent();
            _tskpg = tsk_pg;
            dataGrid.ItemsSource = Tasks_Page.col;
        }



        public async void remove_Selected(object sender, RoutedEventArgs e)
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



                //

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

                        _tskpg.tabCtrl.Items.Remove(nameOfTab);
                        list.Remove(name);
                    }

                    // code to remove entries with selected
                    var dbSelected = db.Properties.Where(x => x.Selected == 1);
                    db.Properties.RemoveRange(dbSelected);
                    await db.SaveChangesAsync();
                    Tasks_Page.LoadItems();
                }
            }
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var prop in Tasks_Page.col)
            {
                prop.Selected = 1;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var prop in Tasks_Page.col)
            {
                prop.Selected = 0;
            }
        }
    }
}
