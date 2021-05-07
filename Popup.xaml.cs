using System;
using System.Media;
using System.Windows;
using System.Windows.Threading;
using System.Speech.Synthesis;
using System.Collections.Generic;

namespace VotifyTest
{
    /// <summary>
    /// Interaction logic for Popup.xaml
    /// </summary>
    public partial class Popup : Window
    {
        SpeechSynthesizer synth = new SpeechSynthesizer();
        double SCREEN_WIDTH = SystemParameters.PrimaryScreenWidth;
        double SCREEN_HEIGHT = SystemParameters.PrimaryScreenHeight;
        int TimeDisplayNotices;
        public Popup(int TimeDisplayNotices, string Theme, string Descroption)
        {
            InitializeComponent();
            SystemSounds.Beep.Play();
            synth.SetOutputToDefaultAudioDevice();
            this.TimeDisplayNotices = TimeDisplayNotices;
            textBlockTheme.Text = Theme;
            textBlockDescription.Text = Descroption;
            this.Top = SCREEN_HEIGHT; 
            this.Left = SCREEN_WIDTH - Width;
            MoveUp(10);
        }

        private void MoveUp(int step)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromTicks(10000);

            timer.Tick += (s, e) => {
                this.Top -= step;
                if(this.Top < SCREEN_HEIGHT - Height)
                {
                    Wait(TimeDisplayNotices, step);
                    timer.Stop();
                }
            };
            timer.Start();
            
        }

        private void Wait(int freezeTime, int step)
        {
            synth.Speak(generateSpeechText(textBlockTheme.Text,textBlockDescription.Text));
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
                    this.Close();
                } 

            };
            timer.Start();

        }
        private PromptBuilder generateSpeechText(string Thema,string Descroption)
        {
            PromptBuilder TextSpeech = new PromptBuilder(new System.Globalization.CultureInfo("pl-PL"));
            TextSpeech.StartParagraph();
            TextSpeech.StartSentence();
            TextSpeech.AppendText("Powiadomienie!");
            TextSpeech.EndSentence();
            TextSpeech.StartSentence();
            TextSpeech.AppendText(Thema);
            TextSpeech.EndSentence();
            TextSpeech.StartSentence();
            TextSpeech.AppendText(Descroption);
            TextSpeech.EndSentence();
            TextSpeech.EndParagraph();

            return TextSpeech;
        }

    }
}
