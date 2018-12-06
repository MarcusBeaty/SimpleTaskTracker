using SimpleTaskTracker.Database;
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
    /// Interaction logic for Tasks_Page.xaml
    /// </summary>
    public partial class Tasks_Page : Page
    {
        public static StringCollection list = Properties.Settings.Default.TabNames;
        public static ObservableCollection<Property> col = new ObservableCollection<Property>();
        public string TaskName { get; set; }
        public string ReTaskName { get; set; }
        private MainWindow _mw;

        public Tasks_Page(MainWindow mw)
        {
            InitializeComponent();

            _mw = mw;

            addTabItm.Content = new Task_Home(this);

            CheckForExistingTabs();
        }

        private void CheckForExistingTabs()
        {
            using (var db = new DataEntities())
            { 
                bool exists = db.Properties.Any();

                if (exists)
                {
                    RefreshObservableCollection();

                    if (list.Count != 0)
                    {
                        // No need to use await here
                        RecreateTabs();
                    }
                }

                else
                {
                    list.Clear();
                    Properties.Settings.Default.AutoDate = false;
                    Properties.Settings.Default.Warnings = true;
                    RefreshObservableCollection();
                }
            }
        }

        public static void RefreshObservableCollection()
        {
            // This method is called whenever there is a modification to the database || Populating DataGrid from Recreated Tabs
            using (var db = new DataEntities())
            {
                col.Clear();
                // Iterating through database and adding entries to obseravable collection
                foreach (var itm in db.Properties)
                {
                    col.Add(itm);
                }
            }
        }

        private void TabItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OnPlusTabClick(sender, e);
        }

        public async Task RecreateTabs()
        {
            for (var i = 0; i < list.Count; i++)
            {
                ReTaskName = list[i];

                // Loading StopWatch into Tab Content
                var content = new Stopwatch(this, _mw, false);

                // Creating Tab
                var tab = new CloseableTabItem(this)
                {
                    TbName = list[i],
                    Content = content,
                    Uid = list[i]
                };

                // Creating header for Tab
                tab.SetHeader(list[i]);

                using (var db = new DataEntities())
                {
                    // Using exist check for error: when clearing database but leaving tab open
                    bool exists = db.Properties.Any(x => x.Task == ReTaskName);

                    if (exists)
                    {
                        var task = db.Properties.SingleOrDefault(x => x.Task == ReTaskName);
                        task.LastClosed = Properties.Settings.Default.LastClosed;
                        await db.SaveChangesAsync();
                        RefreshObservableCollection();
                    }
                }
                // Adding to TabControl
                // Inserting before (+) button
                tabCtrl.Items.Insert(tabCtrl.Items.Count - 1, tab);

                // Focusing first tab - i < list.Count - 1 to focus last tab
                if (i == 0)
                    tab.Focus();
            }
        }

        public void OnPlusTabClick(object sender, RoutedEventArgs e)
        {
            _mw.Opacity = 0.3;
            NewTaskDialog dg = new NewTaskDialog(this) { Owner = _mw };
            dg.ShowDialog();

            
            if (dg.DialogResult == true)
            {
                // Create new Tab
                CreateNewTab();
            }
        }

        private void CreateNewTab()
        {
            // Pass arguments to Stopwatch class
            var content = new Stopwatch(this, _mw, true);

            // Creating TabItem
            var tab = new CloseableTabItem(this)
            {
                TbName = TaskName,
                Uid = TaskName,
                Content = content
            };

            // Creating header for Tab
            tab.SetHeader(TaskName);

            // Adding to TabControl : Inserting TB before (+) button
            tabCtrl.Items.Insert(tabCtrl.Items.Count - 1, tab);
            Keyboard.ClearFocus();
            tab.Focus();

            var newProperty = new Property() { Task = TaskName };
            PopulateCollections(newProperty);
        }

        private void PopulateCollections(Property property)
        {
            AddToTabCollection(property.Task);
            AddDatabaseEntry(property);
        }

        private async void AddDatabaseEntry(Property property)
        {
            // Adding new entry to Database
            using (var db = new DataEntities())
            {
                db.Properties.Add(property);
                int save = await db.SaveChangesAsync();
                RefreshObservableCollection();
            }
        }

        public void AddToTabCollection(string tabName)
        {
            list.Add(tabName);
        }


        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.N && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                OnPlusTabClick(sender, e);
            }
        }
    }
}
