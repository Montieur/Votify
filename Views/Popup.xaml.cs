using System;
using System.Media;
using System.Windows;
using System.Windows.Threading;
using System.Speech.Synthesis;
using System.Collections.Generic;

namespace VotifyTest
{
    public partial class Popup : Window
    {
        private SpeechSynthesizer synth = Models.GLOBALS.synth;
        private double SCREEN_WIDTH = SystemParameters.PrimaryScreenWidth;
        private double SCREEN_HEIGHT = SystemParameters.PrimaryScreenHeight;
        private Event Event;
        public Popup(Event Event)
        {
            InitializeComponent();
            this.Event = Event;
            this.Top = SCREEN_HEIGHT; 
            this.Left = SCREEN_WIDTH - Width;
           
        }
        private void Popup_Loaded(object sender, RoutedEventArgs e)
        {
            textBlockTheme.Text = Event.Title;
            textBlockDescription.Text = Event.Description;
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
                    Wait(step);
                    timer.Stop();
                }
            };
            timer.Start();
            
        }

        private void Wait(int step)
        {
            synth.SpeakAsync(generateSpeechText(Event));
            DispatcherTimer timer = new DispatcherTimer();
            //Co sekunda sprawdzanie czy tekst został zakończony
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => {
                if (synth.GetCurrentlySpokenPrompt() == null)
                {
                    MoveDown(step);
                    timer.Stop();
                }
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
        private PromptBuilder generateSpeechText(Event _Event)
        {
            PromptBuilder TextSpeech = new PromptBuilder(new System.Globalization.CultureInfo("pl-PL"));
            TextSpeech.StartParagraph();
            TextSpeech.StartSentence();
            TextSpeech.AppendText("Powiadomienie!");
            TextSpeech.EndSentence();
            TextSpeech.StartSentence();
            TextSpeech.AppendText(Event.Title);
            TextSpeech.EndSentence();
            TextSpeech.StartSentence();
            TextSpeech.AppendText(Event.Description);
            TextSpeech.EndSentence();
            TextSpeech.EndParagraph();

            return TextSpeech;
        }

    }
}
