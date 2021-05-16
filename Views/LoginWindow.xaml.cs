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

namespace VotifyTest
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        readonly bool debug = false;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
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
    }
}
