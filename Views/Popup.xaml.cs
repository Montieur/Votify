﻿using System;
using System.Media;
using System.Windows;
using System.Windows.Threading;
using System.Speech.Synthesis;
using System.Collections.Generic;

namespace VotifyTest
{
    public partial class Popup : Window
    {
        private double SCREEN_WIDTH = SystemParameters.PrimaryScreenWidth;
        private double SCREEN_HEIGHT = SystemParameters.PrimaryScreenHeight;
        private double TASKBAR_HEIGHT = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;

        private Event Event;

        public string EventTitle { get; set; }
        public string EventDescription { get; set; }

        public Popup(Event Event)
        {
            InitializeComponent();

            this.Width = SCREEN_WIDTH / 5;
            this.Height = SCREEN_HEIGHT / 4;

            this.Event = Event;
            this.DataContext = Event;
            this.Top = SCREEN_HEIGHT; 
            this.Left = SCREEN_WIDTH - Width;
            this.Topmost = true;
           
        }
        private void Popup_Loaded(object sender, RoutedEventArgs e)
        {
            MoveUp(5);
        }

        private void MoveUp(int step)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);

            timer.Tick += (s, e) => {
                this.Top -= step;
                if(this.Top < SCREEN_HEIGHT - Height - TASKBAR_HEIGHT)
                {
                    Wait(step);
                    timer.Stop();
                }
            };
            timer.Start();
            
        }


        private void Wait(int step)
        {
            Models.GLOBALS.synth.SpeakAsync(generateSpeechText(Event));
            DispatcherTimer timer = new DispatcherTimer();
            //Co sekunda sprawdzanie czy tekst został zakończony
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => {
                if (Models.GLOBALS.synth.GetCurrentlySpokenPrompt() == null)
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
            timer.Interval = TimeSpan.FromMilliseconds(1);
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
