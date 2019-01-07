﻿using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace SimpleTaskTracker.XAML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private StringCollection list = Properties.Settings.Default.TabNames;
        private Tasks_Page tk;
        private Logs_Page lgs;
        private Settings_Page set;
        private Help_Page hlp;

        public MainWindow()
        {

#if DEBUG
            System.Diagnostics.PresentationTraceSources.DataBindingSource.Switch.Level = System.Diagnostics.SourceLevels.Error;
#endif
            InitializeComponent();
            tk = new Tasks_Page(this);
            lgs = new Logs_Page(tk);
            set = new Settings_Page();
            hlp = new Help_Page(this);
            Main.Content = tk;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(list.Count == 0)
            {
                list.Clear();
            }

            // if tabs were open store application shutdown time
            else
            {
                Properties.Settings.Default.LastClosed = DateTime.Now;
            }

            Properties.Settings.Default.Save();
        }

        private void Tasks_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = tk;

            if (tk.tabCtrl.Items.Count > 1)
            {
                TitleGroup.Visibility = Visibility.Hidden;
            }
            else
            {
                TitlePage.Text = "Tasks";
                TitleGroup.Visibility = Visibility.Visible;
            }

            var bc = new BrushConverter();
            // tasks_panel.Visibility = Visibility.Visible;
           // tasks_panel.Background = (Brush)bc.ConvertFrom("#FF33363B");
            tasks_button.Background = (Brush)bc.ConvertFrom("#2e3135");
            reports_button.ClearValue(BackgroundProperty);
            settings_button.ClearValue(BackgroundProperty);
            help_button.ClearValue(BackgroundProperty);
            sel_panel.SetValue(Grid.RowProperty, 1);
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = lgs;
            TitleGroup.Visibility = Visibility.Visible;
            TitlePage.Text = "Reports";

            var bc = new BrushConverter();
            //  tasks_panel.Visibility = Visibility.Hidden;
           // tasks_panel.Background = (Brush)bc.ConvertFrom("#FF33363B");
            tasks_button.ClearValue(BackgroundProperty);
            reports_button.Background = (Brush)bc.ConvertFrom("#2e3135");
            settings_button.ClearValue(BackgroundProperty);
            help_button.ClearValue(BackgroundProperty);
            sel_panel.SetValue(Grid.RowProperty, 2);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = set;
            TitleGroup.Visibility = Visibility.Visible;
            TitlePage.Text = "Settings";

            var bc = new BrushConverter();
            // tasks_panel.Visibility = Visibility.Hidden;
            //tasks_panel.Background = (Brush)bc.ConvertFrom("#FF33363B");
            tasks_button.ClearValue(BackgroundProperty);
            reports_button.ClearValue(BackgroundProperty);
            help_button.ClearValue(BackgroundProperty);
            settings_button.Background = (Brush)bc.ConvertFrom("#2e3135");
            sel_panel.SetValue(Grid.RowProperty, 3);
        }

        private void Help_button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = hlp;
            TitleGroup.Visibility = Visibility.Visible;

            var Tab = hlp.tabCtrl.SelectedValue as TabItem;

            if (Tab is null)
                TitlePage.Text = "Documentation";
            else 
                TitlePage.Text = Tab.Header.ToString();

            var bc = new BrushConverter();
            // tasks_panel.Visibility = Visibility.Hidden;
            //tasks_panel.Background = (Brush)bc.ConvertFrom("#FF33363B");
            tasks_button.ClearValue(BackgroundProperty);
            reports_button.ClearValue(BackgroundProperty);
            settings_button.ClearValue(BackgroundProperty);
            help_button.Background = (Brush)bc.ConvertFrom("#2e3135");
            sel_panel.SetValue(Grid.RowProperty, 5);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(Main.Content == tk)
            {
                if (e.Key == Key.T && Keyboard.IsKeyDown(Key.LeftCtrl))
                {
                    tk.OnPlusTabClick(sender, e);
                }
            }
        }
    }
}
