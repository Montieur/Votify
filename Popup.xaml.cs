using System;
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

        public Popup()
        {
            InitializeComponent();
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
                    Wait(3, step);
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
