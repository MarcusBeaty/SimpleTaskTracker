﻿using System;
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

namespace SimpleTaskTracker
{
    /// <summary>
    /// Interaction logic for TabCloseButton.xaml
    /// </summary>
    public partial class TabCloseButton : UserControl
    {
        public event EventHandler Click;
      
        public TabCloseButton()
        {
            InitializeComponent();  
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            if (Click != null)
            {
                
                Click(sender, e);
            }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            close_stroke.Stroke = Brushes.Red;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            close_stroke.Stroke = Brushes.Gray;
        }
    }
}
