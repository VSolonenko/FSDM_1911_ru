using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;

            var viewModel = new MainWindowViewModel();
            
            var mainWindow = new MainWindow
            {
                DataContext = viewModel
            };

            mainWindow.Show();
            viewModel.Fill(new AsyncSample());
        }
    }
}
