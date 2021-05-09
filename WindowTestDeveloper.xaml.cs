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
    /// Logika interakcji dla klasy WindowTestDeveloper.xaml
    /// </summary>
    public partial class WindowTestDeveloper : Window
    {
        private System.Windows.Threading.DispatcherTimer timerSynch;
        private System.Windows.Threading.DispatcherTimer timerDisplayPopup;
        private System.Windows.Threading.DispatcherTimer timmerWhatIsTime;
        int TimeStatus = 1;
        private string Token;
        private List<Event> Events;
        public WindowTestDeveloper(string Token)
        {
            InitializeComponent();
            this.Token = Token;
            InitTimerSynch();
            InitTimerDisplayPopup();
            InitWhatIsTime();



        }
        private void InitWhatIsTime()
        {
            timmerWhatIsTime = new System.Windows.Threading.DispatcherTimer();
            timmerWhatIsTime.Interval = TimeSpan.FromSeconds(1);
            timmerWhatIsTime.Tick += (s, e) => {
                TimeStatus++;

                labelWhatIsTime.Content = "Sync: " + (30-(TimeStatus % 30)).ToString() + " Popup: " + (10-(TimeStatus % 10)).ToString();
            };
            timmerWhatIsTime.Start();
        }
        private void InitTimerDisplayPopup()
        {
            timerDisplayPopup = new System.Windows.Threading.DispatcherTimer();
            timerDisplayPopup.Interval = TimeSpan.FromSeconds(10);
            timerDisplayPopup.Tick += (s, e) => {
                foreach (Event ev in Events)
                {
                    if (ev.date.start.Hour == DateTime.Now.Hour && ev.date.start.Minute == DateTime.Now.Minute)
                    {
                        Popup Popup = new Popup(ev.title, ev.description);
                        Popup.Show();
                    }
                       
                }
                
            };
            timerDisplayPopup.Start();
        }
        private void InitTimerSynch()
        {
            timerSynch = new System.Windows.Threading.DispatcherTimer();
            timerSynch.Interval = TimeSpan.FromSeconds(30);
            timerSynch.Tick += (s, e) => {
                    Events = Controller.GetEventFromResponse(Token);
                    listBoxEvents_addEvents();
            };
            timerSynch.Start();
        }
        private void buttonInstantPopup_Click(object sender, RoutedEventArgs e)
        {
            Popup Popup = new Popup(textBoxTitle.Text, textBoxDescription.Text);
            Popup.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
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
    }
}
