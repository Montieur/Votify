using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
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
    /// Logika interakcji dla klasy WindowTestDeveloper.xaml
    /// </summary>
    public partial class WindowTestDeveloper : Window
    {
        private System.Windows.Threading.DispatcherTimer timerSynch;
        private System.Windows.Threading.DispatcherTimer timerDisplayPopup;
        private System.Windows.Threading.DispatcherTimer timmerWhatIsTime;
        int TimeStatus = 0;
        private string Token;
        private List<Event> Events;
        private Models.User User;
        public WindowTestDeveloper(string Token, Models.User User)
        {
            InitializeComponent();
            this.Token = Token;
            this.User = User;
            InitTimerSynch();
            InitTimerDisplayPopup();
        }
        private void InitWhatIsTime()
        {
            timmerWhatIsTime = new System.Windows.Threading.DispatcherTimer();
            timmerWhatIsTime.Interval = TimeSpan.FromSeconds(1);
            timmerWhatIsTime.Tick += (s, e) => {
                TimeStatus++;

                labelWhatIsTime.Content = "Sync: " + (30-(TimeStatus % 30)).ToString() + " Popup: " + (60-(TimeStatus % 60)).ToString();
            };
            timmerWhatIsTime.Start();
        }
        private void InitTimerDisplayPopup()
        {
            timerDisplayPopup = new System.Windows.Threading.DispatcherTimer();
            timerDisplayPopup.Interval = TimeSpan.FromSeconds(60);
            timerDisplayPopup.Tick += (s, e) => {
                foreach (Event ev in Events)
                {
                    if (ev.Date.Start.Hour == DateTime.Now.Hour && ev.Date.Start.Minute == DateTime.Now.Minute)
                    {
                        Popup Popup = new Popup(ev);
                        Popup.Show();
                    }
                }
                TimeStatus = 0;
            };
            timerDisplayPopup.Start();
        }
        private void InitTimerSynch()
        {
            timerSynch = new System.Windows.Threading.DispatcherTimer();
            InitWhatIsTime();
            timerSynch.Interval = TimeSpan.FromSeconds(30);
            timerSynch.Tick += (s, e) => {
                    Events = Controller.GetEventFromResponse(Token);
                    listBoxEvents_addEvents();
            };
            timerSynch.Start();
        }
        private void buttonInstantPopup_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(Models.GLOBALS.synth.Volume.ToString());
            Event TempEvent = new Event(new DateTime().ToString(), new DateTime().AddMinutes(1).ToString(), textBoxTitle.Text, textBoxDescription.Text, -1);
            Popup Popup = new Popup(TempEvent);
            Popup.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Models.GLOBALS.SerializeSpeechSynthesizerObject();
            Application.Current.Shutdown();
        }

        private void buttonEnforceGetEvents_Click(object sender, RoutedEventArgs e)
        {
            Events = Controller.GetEventFromResponse(Token);
            listBoxEvents_addEvents();
        }

        private void listBoxEvents_addEvents()
        {
            listBoxEvents.Items.Clear();
            foreach (Event ev in Events)
            {
                listBoxEvents.Items.Add(ev.ToString());
            }
        }

        private string getUnixDate(DateTime dateTime)
        {
            return ((int)(dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds)).ToString();
        }

        private void buttonCreateEvent_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show();
            var Event = Controller.CreateEvents(Token, textBoxTitle.Text, textBoxDescription.Text, getUnixDate(DateTime.UtcNow.AddMinutes(1)), getUnixDate(DateTime.UtcNow.AddMinutes(2)));

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
