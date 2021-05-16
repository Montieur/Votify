using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace VotifyTest.Models
{
    static class GLOBALS
    {
        public static SpeechSynthesizer synth = initSpeechSynthesizer();
        public static NotifyIcon TrayIcon = InitNotifyIcon();
        public static Window WindowUser;
        public static NotifyIcon InitNotifyIcon()
        {
            NotifyIcon TrayIcon = new NotifyIcon();
            TrayIcon.Icon = SystemIcons.Exclamation;
            TrayIcon.Text = "Votify";
            TrayIcon.BalloonTipTitle = "Votify";
            TrayIcon.BalloonTipText = "Od teraz Votify będzie pracowąć w tle..";
            TrayIcon.Visible = true;
  

            return TrayIcon;
        }
  

        private static SpeechSynthesizer initSpeechSynthesizer()
        {
            SpeechSynthesizer _synth;
            if (DeSerializeSpeechSynthesizerObject() != null)
            {
                _synth = DeSerializeSpeechSynthesizerObject();
            }
            else
            {
                _synth = new SpeechSynthesizer();
                
            }
            _synth.SetOutputToDefaultAudioDevice();

           
            return _synth;
        }

        public static void SerializeSpeechSynthesizerObject()
        {
            try
            {
                string ObjectSerialized = JsonConvert.SerializeObject(synth);
                if(!File.Exists(@"config/"))
                {
                    System.IO.Directory.CreateDirectory(@"config/");
                }
                FileStream FileStream = new FileStream(@"config/synth.json", FileMode.OpenOrCreate, FileAccess.Write,FileShare.None);
                StreamWriter StreamWiter = new StreamWriter(FileStream);
                StreamWiter.WriteLine(ObjectSerialized);
                StreamWiter.Flush();
                StreamWiter.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static SpeechSynthesizer DeSerializeSpeechSynthesizerObject()
        {
            try
            {
                if (File.Exists(@"config/synth.json"))
                {
                    try
                    {
                        FileStream FileStream = new FileStream(@"config/synth.json", FileMode.Open, FileAccess.Read);
                        StreamReader StreamReader = new StreamReader(FileStream);
                        var ObjectDeSerialized = StreamReader.ReadToEnd();
                        StreamReader.Close();
                        return JsonConvert.DeserializeObject<SpeechSynthesizer>(ObjectDeSerialized);
                    }
                    catch(Exception e)
                    {
                        
                        Console.WriteLine(e.Message);
                        return null;
                    }

                }
                else
                {
                    return null;
                }
             
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }
}
