using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Lopushok
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Models.LopushokEntities DB { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                DB = new Models.LopushokEntities();
                DB.Database.Connection.Open();
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к базе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown();
            }

            MainWindow = new Windows.ProductsWindow();
            MainWindow.Show();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show("Возникла ошибка:\n" + e.Exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
