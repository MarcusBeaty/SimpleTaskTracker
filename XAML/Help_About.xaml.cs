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
    /// Interaction logic for Help_About.xaml
    /// </summary>
    public partial class Help_About : UserControl
    {
        public Help_About()
        {
            InitializeComponent();
        }

        private void Donate_Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.paypal.me/MarcusBeaty");
        }

        private void Help_Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://drive.google.com/file/d/1x0L-HiZkMyExT4Y2T9BVTw7xTtv31Kq1/view?usp=sharing");
        }
    }
}
