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
        private StringCollection list = Properties.Settings.Default.TabNames;
        public static ObservableCollection<Property> col = new ObservableCollection<Property>();
        public string TaskName { get; set; }
        public string ReTaskName { get; set; }
        public bool isRecreated { get; set; }
        private MainWindow _mw;

        public Tasks_Page(MainWindow mw)
        {
            InitializeComponent();

            _mw = mw;
            addTabItm.Content = new Task_Home(this);


            using (var db = new DataEntities())
            {
                bool exists = true;
                exists = db.Properties.Any();
                if (exists)
                {
                    loadItems();

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
                    loadItems();
                }
            }
  
        }

        public static void loadItems()
        {
            using (var db = new DataEntities())
            {
                col.Clear();
                // iterating through database and adding entries to obseravable collection
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
                // Creating header for Tab
                ReTaskName = list[i];
                isRecreated = true;
                var header = new TextBlock
                {
                    Text = list[i],
                    FontSize = 13,
                    Width = 120,
                    TextAlignment = TextAlignment.Left,
                    TextWrapping = TextWrapping.NoWrap,
                    Padding = new Thickness(2, 2, 10, 2)
                };

                // Loading StopWatch into Tab Content
                var content = new stopwatch(this, col);
                stopwatch.recreate = true;

                // Creating Tab
                var tab = new CloseableTabItem(this)
                {
                    TbName = list[i],
                    Content = content,
                    Uid = list[i]
                };
               
                tab.SetHeader(header);

                using (var db = new DataEntities())
                {
                    // Using exist check for error: when clearing database but leaving tab open
                    bool exists = db.Properties.Any(x => x.Task == ReTaskName);

                    if (exists)
                    {
                        var task = db.Properties.SingleOrDefault(x => x.Task == ReTaskName);
                        task.LastClosed = Properties.Settings.Default.LastClosed;
                        await db.SaveChangesAsync();
                        loadItems();
                    }
                }
                // Adding to TabControl
                // Inserting before (+) button
                tabCtrl.Items.Insert(tabCtrl.Items.Count - 1, tab);
            }
        }

        public async void OnPlusTabClick(object sender, RoutedEventArgs e)
        {
            isRecreated = false;
            _mw.Opacity = 0.3;
            NewTaskDialog dg = new NewTaskDialog(this) { Owner = _mw };
            dg.ShowDialog();

            // If user pressed "Create New Task"
            if (dg.DialogResult == true)
            {
                // Creating header for Tab
                var header = new TextBlock
                {
                    Text = TaskName,
                    FontSize = 13,
                    Width = 120,
                    TextAlignment = TextAlignment.Left,
                    TextWrapping = TextWrapping.NoWrap,
                    Padding = new Thickness(2, 2, 10, 2)
                };

                var content = new stopwatch(this, col);
                stopwatch.recreate = false;


                // Creating Tab
                var tab = new CloseableTabItem(this)
                {
                    TbName = TaskName,
                    Uid = TaskName,
                    Content = content
                };

                tab.SetHeader(header);

                // Adding to String Collection
                list.Add(TaskName);

                // Add to TabControl
                // inserting before (+) button
                tabCtrl.Items.Insert(tabCtrl.Items.Count - 1, tab);
                tab.Focus();
                Keyboard.ClearFocus();

                // Adding new entry to Database
                using (var db = new DataEntities())
                {
                    var newProp = new Property() { Task = TaskName };
                    db.Properties.Add(newProp);
                    await db.SaveChangesAsync();
                    loadItems();
                }
            }
        }
    }
}
