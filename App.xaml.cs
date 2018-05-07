using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using SimpleTaskTracker.Database;


namespace SimpleTaskTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {   
            AppDomain.CurrentDomain.SetData("DataDirectory", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        }
    }
}
