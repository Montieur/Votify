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
    /// Logika interakcji dla klasy WindowTestPopup.xaml
    /// </summary>
    public partial class WindowTestPopup : Window
    {
        public WindowTestPopup()
        {
            InitializeComponent();
        }

        private void buttonTestujPowiadomienie_Click(object sender, RoutedEventArgs e)
        {
            //var response = Controller.PostAsync();
            //Popup popup = new Popup(Controller.ResponseToString(response), textBoxDescription.Text);
            //popup.Show();
        }
    }
}
