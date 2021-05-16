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
            Models.GLOBALS.WindowUser = this;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonEvents_Click(object sender, RoutedEventArgs e)
        {
            Events = Controller.GetEventFromResponse(Token);
            listBoxEvents_addEvents();
            ListEvents.Visibility = Visibility.Visible;
            SettingsPane.Visibility = Visibility.Collapsed;
        }

        private void listBoxEvents_addEvents()
        {
            ListEvents.Items.Clear();
            foreach (Event ev in Events)
            {
                ListEvents.Items.Add(ev);
            }
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            ListEvents.Visibility = Visibility.Collapsed;
            SettingsPane.Visibility = Visibility.Visible;

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
    }
}
