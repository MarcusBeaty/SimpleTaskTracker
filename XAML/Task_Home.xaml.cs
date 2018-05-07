using SimpleTaskTracker.XAML;
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
    /// Interaction logic for Task_Home.xaml
    /// </summary>
    public partial class Task_Home : UserControl
    {
        private Tasks_Page _tsks;

        public Task_Home()
        {
            InitializeComponent();
        }

        public Task_Home(Tasks_Page tsks)
        {
            InitializeComponent();
            _tsks = tsks;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _tsks.OnPlusTabClick(sender,e);
        }
    }
}
