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

        public LoginWindow()
        {
            
            Width = SystemParameters.PrimaryScreenWidth / 4;
            Height = SystemParameters.PrimaryScreenHeight / 3;
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            var response = Controller.Login(LoginBox.Text, PasswordBox.Password);

            if (response != null)
            {
                this.Hide();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Wprowadzono niepoprawne dane logowania!");
            }
            

        }
    }
}
