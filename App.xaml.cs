using System;
using System.Windows;
using SimpleTaskTracker_Services;
using SimpleTaskTracker_Data;
using Unity;

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
