using SimpleTaskTracker_Data;
using SimpleTaskTracker_Services;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleTaskTracker.XAML
{
    /// <summary>
    /// Interaction logic for Tasks_Page.xaml
    /// </summary>
    public partial class Tasks_Page : Page
    {
        public static StringCollection list = Properties.Settings.Default.TabNames;
        private MainWindow _mw;
        private TaskService _taskService;
        private ObservableCollectionService _collectionService;

        public Tasks_Page(MainWindow mw, ITaskService<Task> taskService, IObservableCollectionService<Task> collectionService)
        {
            InitializeComponent();

            _mw = mw;

            _taskService = new TaskService();
            _collectionService = new ObservableCollectionService();
            // Use DI to give a new instance of TaskService to ITaskService

            addTabItm.Content = new Task_Home(this);

            CheckForExistingTabs();
        }


        private void CheckForExistingTabs()
        {
            if (_taskService.List().Any())
            {
                _collectionService.Refresh();

                if (list.Count != 0)
                {
                    for (var i = 0; i < list.Count; i++)
                    {
                        CreateNewTab(list[i], false);
                    }
                }
                else
                {
                    list.Clear();
                    _collectionService.Refresh();
                }
            }
        }

        private void TabItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OnPlusTabClick(sender, e);
        }

        private void SetLastClosed(string TabName)
        {

            // Using exist check for error: when clearing database but leaving tab open
            bool Exists = _taskService.List().Any();

            if (Exists)
            {
                var Task = _taskService.Get(TabName);
                Task.LastClosed = Properties.Settings.Default.LastClosed;
                _taskService.Update(Task);
                _collectionService.Refresh();
            }
        }

        public void OnPlusTabClick(object sender, RoutedEventArgs e)
        {
            _mw.Opacity = 0.3;
            NewTaskDialog dg = new NewTaskDialog() { Owner = _mw };
            dg.ShowDialog();

            if (dg.DialogResult == true) CreateNewTab(dg.TaskName, true);
        }

        private void CreateNewTab(string TabName, bool NewTask)
        {
            // Pass arguments to Stopwatch class
            var Stopwatch = new Stopwatch(TabName, _mw, NewTask, _collectionService);

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
            var tabTotal = tabCtrl.Items.Count;
            tabCtrl.Items.Insert(tabCtrl.Items.Count - 1, Tab);

            if (!NewTask)
            {
                tabCtrl.SelectedIndex = tabTotal - 1;
                SetLastClosed(TabName);
                return;
            }

            Keyboard.ClearFocus();
            Tab.Focus();

            var newProperty = new Task() { TaskName = TabName };
            _taskService.Add(newProperty);
            _collectionService.Refresh();
            PopulateCollections(newProperty);
        }

        private void PopulateCollections(Task property)
        {
            AddToTabCollection(property.TaskName);
        }

        private void AddToTabCollection(string TabName)
        {
            list.Add(TabName);
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.T && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                OnPlusTabClick(sender, e);
            }
        }

        private void TabCtrl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabCtrl.Items.Count > 1 && _mw.TitlePage.Text == "Tasks")
            {
                _mw.TitleGroup.Visibility = Visibility.Hidden;
            }
            else if (_mw.TitlePage.Text == "Tasks")
            {
                _mw.TitlePage.Text = "Tasks";
                _mw.TitleGroup.Visibility = Visibility.Visible;
            }
        }

        public void TempHeaderFix()
        {
            _mw.TitlePage.Text = "Tasks";
            _mw.TitleGroup.Visibility = Visibility.Visible;
        }
    }
}


