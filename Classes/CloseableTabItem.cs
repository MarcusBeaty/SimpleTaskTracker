using SimpleTaskTracker.XAML;
using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleTaskTracker
{
    class CloseableTabItem : TabItem
    {
        private Tasks_Page _tp;
        public StringCollection list = Properties.Settings.Default.TabNames;
        public string TbName { get; set; }

        public CloseableTabItem(Tasks_Page tp)
        {
            _tp = tp;
            Focusable = true;
            KeyDown += OnKeyDown;
            FocusVisualStyle = null;
            this.SetResourceReference(StyleProperty, typeof(TabItem));
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.N && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                _tp.OnPlusTabClick(sender, e);
            }

            if (e.Key == Key.W && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                CloseTb(sender,e);
            }
        }

        public void SetHeader(string name)
        {
            var header = new TextBlock
            {
                Text = name,
                TextTrimming = TextTrimming.CharacterEllipsis,
                TextWrapping = TextWrapping.NoWrap,
                TextAlignment = TextAlignment.Left,
                FontSize = 13,
                Width = 120,
                Padding = new Thickness(2,2,10,2)
            };

            // Container for header controls
            var dockPanel = new DockPanel
            {
                ToolTip = TbName
            };

            dockPanel.Children.Add(header);
            Height = 30;
            Width = 160;
            
            // Close button to remove the tab
            var closeButton = new TabCloseButton();
            closeButton.Click += CloseTb;

            dockPanel.Children.Add(closeButton);
            // Set the header
            Header = dockPanel;
        }

        public void CloseTb(object sender, EventArgs e)
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
            var newIndex = (thisIndex - 1);

            list.Remove(TbName);

            tabControl.Items.Remove(this);

            if (newIndex != -1)
            {
                tabControl.SelectedIndex = newIndex;
               
            }
            else
            {
                _tp.TempHeaderFix();
            }
        }
    }
}
