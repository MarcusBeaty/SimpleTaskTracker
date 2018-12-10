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
    /// Interaction logic for About_Page.xaml
    /// </summary>
    public partial class Help_Page : Page
    {
        private readonly MainWindow _mainWindow;

        public Help_Page(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            About.Content = new Help_About();
            ReleaseNotes.Content = new Help_ReleaseNotes();
            Documentation.Content = new Help_Documentation();
        }

        private void TabCtrl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tabCtrl = (TabControl)sender;
            var pgName = tabCtrl.SelectedValue as TabItem;

            _mainWindow.TitlePage.Text = pgName.Header.ToString();
        }
    }
}
