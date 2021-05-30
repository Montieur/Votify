using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace Votify
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        readonly bool debug = !true;

        public LoginWindow()
        {
            InitializeComponent();
            Models.GLOBALS.WindowUser = this;
           
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if(Controller.TestNet())
            {
                Models.DataFromAuthorization response = Controller.Login(LoginBox.Text, PasswordBox.Password);

                if (response != null)
                {
                    this.Hide();
                    if (debug)
                    {
                        WindowTestDeveloper WindowTestDeveloper = new WindowTestDeveloper(response.Token, response.User);
                        WindowTestDeveloper.Show();
                    }
                    else
                    {
                        Views.MainWindow mainWindow = new Views.MainWindow(response.Token, response.User);
                        mainWindow.Show();
                    }

                }
                else
                {
                    MessageBox.Show("Wprowadzono niepoprawne dane logowania!");
                }
            }
            else
            {
                MessageBox.Show("Sprawdź połączenia z internetem!","Brak połączenia z internetem",MessageBoxButton.OK,MessageBoxImage.Error,MessageBoxResult.OK);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Models.GLOBALS.TrayIcon.Click += (s, EventArgs) =>
            {
                if (this.Visibility == Visibility.Hidden && this == Models.GLOBALS.WindowUser)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                    this.Visibility = Visibility.Visible;
                }
            };
        }


        private void Window_StateChanged(object sender, EventArgs e)
        {
            Models.GLOBALS.WindowUser = this;
            if (this.WindowState == WindowState.Minimized && this == Models.GLOBALS.WindowUser)
            { 
                Models.GLOBALS.TrayIcon.Visible = true;
                Models.GLOBALS.TrayIcon.ShowBalloonTip(1);
                this.Hide();
            }
            else
            {
                Models.GLOBALS.TrayIcon.Visible = false;
                this.Show();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Models.GLOBALS.TrayIcon.Dispose();
            Application.Current.Shutdown();
        }
    }
}
