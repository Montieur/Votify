using System;
using System.Media;
using System.Windows;
using System.Windows.Threading;

namespace VotifyTest
{
    /// <summary>
    /// Interaction logic for Popup.xaml
    /// </summary>
    public partial class Popup : Window
    {
        double SCREEN_WIDTH = SystemParameters.PrimaryScreenWidth;
        double SCREEN_HEIGHT = SystemParameters.PrimaryScreenHeight;
        int TimeDisplayNotices;
        public Popup(int TimeDisplayNotices, string Theme, string Descroption)
        {
            InitializeComponent();
            SystemSounds.Beep.Play();
            this.TimeDisplayNotices = TimeDisplayNotices;
            textBlockTheme.Text = Theme;
            textBlockDescription.Text = Descroption;
            Top = SCREEN_HEIGHT;
            Left = SCREEN_WIDTH - Width;
            MoveUp(10);
        }

        private void MoveUp(int step)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromTicks(10000);

            timer.Tick += (s, e) => {
                Top -= step;
                if(Top < SCREEN_HEIGHT - Height)
                {
                    Wait(TimeDisplayNotices, step);
                    timer.Stop();
                }
            };
            timer.Start();
            
        }

        private void Wait(int freezeTime, int step)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(freezeTime);
            timer.Tick += (s, e) => {
                MoveDown(step);
                timer.Stop();
            };
            timer.Start();
        }


        private void MoveDown(int step)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromTicks(10000);
            timer.Tick += (s, e) => {
                Top += step;
                if (Top > SCREEN_HEIGHT)
                {
                    timer.Stop();
                    Close();
                } 

            };
            timer.Start();
        }

    }
}
