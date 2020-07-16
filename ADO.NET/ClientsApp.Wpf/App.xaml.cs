using ClientsApp.Wpf.Core;
using ClientsApp.Wpf.ViewModels;
using ClientsApp.Wpf.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ClientsApp.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private DisconnectedDbProvider provider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var connectionString = "Data Source=localhost;Initial Catalog=ClientsDB;Integrated Security=True";

            ShutdownMode = ShutdownMode.OnLastWindowClose;

            provider = new DisconnectedDbProvider(connectionString);
            var mainViewModel = new MainWindowViewModel(provider);

            var mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };

            mainWindow.Show();

            mainWindow.Closing += OnClose;
        }

        private void OnClose(object sender, CancelEventArgs e)
        {
            var closeDialog = new CloseDialogWindow();
            closeDialog.DataContext = new CloseDialogWIndowViewModel(provider, () => closeDialog.Close());
            closeDialog.Show();
        }
    }
}
