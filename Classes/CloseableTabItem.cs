using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SimpleTaskTracker;
using System.Windows.Media;
using System.Windows.Input;
using SimpleTaskTracker.XAML;

namespace SimpleTaskTracker
{
    class CloseableTabItem : TabItem
    {
        private Tasks_Page _tp;

        public CloseableTabItem(Tasks_Page tp)
        {
            _tp = tp;
            this.SetResourceReference(StyleProperty, typeof(TabItem));
        }

        public StringCollection list = Properties.Settings.Default.TabNames;

        public string TbName { get; set; }
        public void SetHeader(TextBlock header)
        {
            
            // Container for header controls
            var dockPanel = new DockPanel();
            dockPanel.Children.Add(header);
            Height = 30;
            Width = 160;
            
            // Close button to remove the tab
            var closeButton = new TabCloseButton();
            var f = (this);
            closeButton.Click +=
                (sender, e) =>
                {
                    // If user has the Warning Setting enabled

                    if (Properties.Settings.Default.Warnings)
                    {
                        MessageBoxResult UserSelection = MessageBox.Show("Are you sure you would like to remove this tab?", "Simple Task Tracker", MessageBoxButton.YesNo);
                        switch (UserSelection)
                        {
                            case MessageBoxResult.Yes:
                                break;

                            case MessageBoxResult.No:
                                return;
                        }
                    }

                    var tabControl = (TabControl)Parent;
                    var total = tabControl.Items.Count;
                    var thisIndex = tabControl.SelectedIndex;

                    list.Remove(TbName);

                    tabControl.Items.Remove(this);

                    var newIndex = (thisIndex - 1);

                    if (newIndex != -1)
                    {
                        tabControl.SelectedIndex = newIndex;
                    }
                };


            //new
            //if (_tp.tabCtrl.Items.Count > 4)
           // {
              //  foreach (TabItem t in _tp.tabCtrl.Items)
             //   {
            //        if (t.Name != "addTabItm") t.Width = 128;
                    
                  //  header.Width = 


              //  }
         //   }
            //
            dockPanel.ToolTip = TbName;
            dockPanel.Children.Add(closeButton);
            // Set the header
            Header = dockPanel;
        }
    }
}
