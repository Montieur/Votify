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

namespace VotifyTest.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        int TimeStatus = 0;
        private string Token;
        private List<Event> Events;
        private Models.User User;
        public MainWindow(string Token, Models.User User)
        {
            InitializeComponent();
            this.Token = Token;
            this.User = User;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonEvents_Click(object sender, RoutedEventArgs e)
        {
            Events = Controller.GetEventFromResponse(Token);
            listBoxEvents_addEvents();
        }

        private void listBoxEvents_addEvents()
        {
            ListEvents.Items.Clear();
            foreach (Event ev in Events)
            {
                ListEvents.Items.Add(ev);
            }
        }

    }
}
