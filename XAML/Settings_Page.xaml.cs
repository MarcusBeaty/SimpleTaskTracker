using System;
using System.Collections.Generic;
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
    /// Interaction logic for Settings_Page.xaml
    /// </summary>
    public partial class Settings_Page : Page
    {
        public Settings_Page()
        {
            InitializeComponent();
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            switch (WarningsChk.IsChecked)
            {
                case true: Properties.Settings.Default.Warnings = true;
                    break;

                case false: Properties.Settings.Default.Warnings = false;
                    break;
            }


            switch (AutoDate.IsChecked)
            {
                case true: Properties.Settings.Default.AutoDate = true;
                    break;

                case false: Properties.Settings.Default.AutoDate = false;
                    break;
            }
            SaveSettings.IsEnabled = false;
        }

        private void WarningsChk_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded) SaveSettings.IsEnabled = true;
        }

        private void WarningsChk_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded) SaveSettings.IsEnabled = true;
        }

        private void AutoDate_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded) SaveSettings.IsEnabled = true;
        }

        private void AutoDate_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded) SaveSettings.IsEnabled = true;
        }

        private void AutoDateText_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsLoaded)
            {
                if (AutoDate.IsChecked == true)
                {
                    AutoDate.IsChecked = false;
                    SaveSettings.IsEnabled = true;
                }

                else
                {
                    AutoDate.IsChecked = true;
                    SaveSettings.IsEnabled = true;
                }
                
            }
        }

        private void WarningText_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsLoaded)
            {
                if (WarningsChk.IsChecked == true)
                {
                    WarningsChk.IsChecked = false;
                    SaveSettings.IsEnabled = true;
                }

                else
                {
                    WarningsChk.IsChecked = true;
                    SaveSettings.IsEnabled = true;
                }
            }
        }
    }
}
