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
                        for (var i = 0; i < list.Count; i++)
                        {
                            CreateNewTab(list[i],false);
                        }
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

        public void SetLastClosed(string TabName)
        {
            // Using exist check for error: when clearing database but leaving tab open
            using (var db = new DataEntities())
            {
                bool exists = db.Properties.Any(x => x.Task == TabName);

                if (exists)
                {
                    var task = db.Properties.First(x => x.Task == TabName);
                    task.LastClosed = Properties.Settings.Default.LastClosed;
                    db.SaveChanges();
                    RefreshObservableCollection();
                }
            }
        }
      
        public void OnPlusTabClick(object sender, RoutedEventArgs e)
        {
            _mw.Opacity = 0.3;
            NewTaskDialog dg = new NewTaskDialog() { Owner = _mw };
            dg.ShowDialog();

            
            if (dg.DialogResult == true)
            {
                // Create new Tab
                var taskName = dg.TaskName;
                CreateNewTab(taskName,true);
            }
        }

        private void CreateNewTab(string TabName, bool IsANewTask)
        {
            // Pass arguments to Stopwatch class
            var Stopwatch = new Stopwatch(TabName, _mw, IsANewTask);

            // Creating TabItem
            var Tab = new CloseableTabItem(this)
            {
                TbName = TabName,
                Uid = TabName,
                Content = Stopwatch
            };

            // Creating header for Tab
            Tab.SetHeader(TabName);

            // Adding to TabControl : Inserting TB before (+) button
            tabCtrl.Items.Insert(tabCtrl.Items.Count - 1, Tab);

            if (!IsANewTask)
            {
                var firstTab = (TabItem)tabCtrl.Items[0];
                firstTab.Focus();
                SetLastClosed(TabName);
                return;
            }

            Keyboard.ClearFocus();
            Tab.Focus();

            var newProperty = new Property() { Task = TabName };
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

        private void TabCtrl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabCtrl.Items.Count > 1)
            {
                _mw.TitleGroup.Visibility = Visibility.Hidden;
            }
            else if(_mw.TitlePage.Text == "Tasks")
            {
                _mw.TitlePage.Text = "Tasks";
                _mw.TitleGroup.Visibility = Visibility.Visible;
            }
        }
    }
}

