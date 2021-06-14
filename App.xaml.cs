using System.Windows;

namespace Votify
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Application_DispatcherUnhandledException" + e.Exception.Message);
            e.Handled = true;
        }
    }
}
