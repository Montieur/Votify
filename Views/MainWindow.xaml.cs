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

namespace Votify.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private string Token;
        private List<Event> Events;
        private Models.User User;
        public MainWindow(string Token, Models.User User)
        {
            InitializeComponent();
            this.Token = Token;
            this.User = User;
            Models.GLOBALS.WindowUser = this;
            Events = Controller.GetEventFromResponse(Token);
            listBoxEvents_addEvents();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SliderSpeedSpeech.Value = Models.GLOBALS.synth.Rate;
            SliderVolumeSpeech.Value = Models.GLOBALS.synth.Volume;
            TextUsername.Text = User.UserName;
            CheckBoxHidePopup.IsChecked = Models.GLOBALS.popupSetting.hidePopup;
            CheckBoxMuteSpeech.IsChecked = Models.GLOBALS.popupSetting.mutePopup;
            Models.GLOBALS.TrayIcon.Click += (s, EventArgs) =>
            {
                if (this.Visibility == Visibility.Hidden && this == Models.GLOBALS.WindowUser)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                    this.Visibility = Visibility.Visible;
                }
            };
            foreach (InstalledVoice voice in Models.GLOBALS.synth.GetInstalledVoices())
            {
                ComboBoxVoices.Items.Add(voice.VoiceInfo.Name);
                if (Models.GLOBALS.synth.Voice.Name == voice.VoiceInfo.Name)
                {
                    ComboBoxVoices.SelectedValue = voice.VoiceInfo.Name;
                }
               
            }
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            Models.GLOBALS.SerializeSpeechSynthesizerObject();
            Models.GLOBALS.TrayIcon.Dispose();
            Application.Current.Shutdown();
        }

        private void ButtonEvents_Click(object sender, RoutedEventArgs e)
        {
            Events = Controller.GetEventFromResponse(Token);
            listBoxEvents_addEvents();
            EventsPane.Visibility = Visibility.Visible;
            SettingsPane.Visibility = Visibility.Collapsed;
            ButtonEvents.Style = FindResource("mButtonActive") as Style;
            ButtonSettings.Style = FindResource("mButton") as Style;
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
            EventsPane.Visibility = Visibility.Collapsed;
            SettingsPane.Visibility = Visibility.Visible;
            ButtonEvents.Style = FindResource("mButton") as Style;
            ButtonSettings.Style = FindResource("mButtonActive") as Style;
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
        private void SliderSpeedSpeech_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Models.GLOBALS.synth.Rate = (int)SliderSpeedSpeech.Value;
            Models.GLOBALS.SerializeSpeechSynthesizerObject();
        }

        private void SliderVolumeSpeech_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Models.GLOBALS.synth.Volume = (int)SliderVolumeSpeech.Value;
            Models.GLOBALS.SerializeSpeechSynthesizerObject();
        }

        private void ComboBoxVoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Models.GLOBALS.synth.SelectVoice(ComboBoxVoices.SelectedItem.ToString());
            Models.GLOBALS.SerializeSpeechSynthesizerObject();
            MessageBox.Show(Models.GLOBALS.synth.Voice.Name);
        }

        private void CheckBoxMuteSpeech_Click(object sender, RoutedEventArgs e)
        {
            Models.GLOBALS.popupSetting.mutePopup = (bool)CheckBoxMuteSpeech.IsChecked;
            Models.GLOBALS.SerializePopupSynthesizerObject();
        }

        private void CheckBoxHidePopup_Click(object sender, RoutedEventArgs e)
        {
            Models.GLOBALS.popupSetting.hidePopup = (bool)CheckBoxHidePopup.IsChecked;
            Models.GLOBALS.SerializePopupSynthesizerObject();
        }

        private void ListEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListEvents.SelectedIndex > -1)
                DescriptionArea.DataContext = Events[ListEvents.SelectedIndex];
        }

        private void ButtonPopupTest_Click(object sender, RoutedEventArgs e)
        {
            Popup Popup = new Popup(new Event(new DateTime().ToString(), new DateTime().AddMinutes(1).ToString(), "Przykładowy tytuł", "Przykładowy opis", -1));
            Popup.Show();
        }
    }
}
