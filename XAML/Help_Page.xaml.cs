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
        private Help_Documentation doc;
        private Help_ReleaseNotes rn;

        public Help_Page(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            doc = new Help_Documentation();
            rn = new Help_ReleaseNotes();

            About.Content = new Help_About();
            ReleaseNotes.Content = rn;
            Documentation.Content = doc;
        }

        private void TabCtrl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tabCtrl = (TabControl)sender;
            var tabItem = tabCtrl.SelectedValue as TabItem;
            if (tabItem.Header.ToString() == "Documentation")
                doc.ScrollTop();

            if (tabItem.Header.ToString() == "Release notes")
                rn.ScrollTop();

            _mainWindow.TitlePage.Text = tabItem.Header.ToString();
        }

      
    }
}
