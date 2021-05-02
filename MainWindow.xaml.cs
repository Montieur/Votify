using System;
using System.Windows;
using System.Speech.Synthesis;
using System.Collections.Generic;

namespace VotifyTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var installedVoices = synth.GetInstalledVoices();

            foreach(var voice in installedVoices)
            {
                voicesBox.Items.Add(voice.VoiceInfo.Name);
            }
            voicesBox.SelectedIndex = 0;
        }

        SpeechSynthesizer synth = new SpeechSynthesizer();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            synth.SetOutputToDefaultAudioDevice();
            synth.Speak(spokenText.Text);
        }

        private void popupButton_Click(object sender, RoutedEventArgs e)
        {
            Popup popup = new Popup();
            popup.Show();
        }

    }
}