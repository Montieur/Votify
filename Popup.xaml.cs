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
        int i = 0;
        bool moveUp = true;

        DispatcherTimer timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromTicks(10000)
        };

        public Popup()
        {
            InitializeComponent();
            Top = SCREEN_HEIGHT;
            Left = SCREEN_WIDTH - Width;
            timer.Tick += Timer_Tick;
            timer.Start();
            //moveAnimation();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (moveUp)
            {
                i += 10;
                Top -= 10;
                if (i >= Height) moveUp = false;
            }
            else
            {
                i -= 10;
                Top += 10;
                if (i <= 0) moveUp = true;
            }   
        }

    }
}
