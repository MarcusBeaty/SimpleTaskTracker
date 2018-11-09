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
using System.Windows.Shapes;

namespace SimpleTaskTracker.XAML
{
    /// <summary>
    /// Interaction logic for IncorrectFormat.xaml
    /// </summary>
    public partial class IncorrectFormat : Window
    {
        private MainWindow _mw;

        public IncorrectFormat()
        {
            InitializeComponent();
        }

        public IncorrectFormat(MainWindow mw)
        {
            InitializeComponent();
            _mw = mw;
            Owner = _mw;
            _mw.Opacity = 0.3;
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            // Close window
            _mw.Opacity = 1;
            Close();
        }

        private void IncorrectDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _mw.Opacity = 1;
        }
    }
}
