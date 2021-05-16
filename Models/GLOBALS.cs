using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace VotifyTest.Models
{
    static class GLOBALS
    {
        public static SpeechSynthesizer synth = new SpeechSynthesizer();

        private static SpeechSynthesizer initSpeechSynthesizer()
        {
            SpeechSynthesizer _synth = new SpeechSynthesizer();
            _synth.SetOutputToDefaultAudioDevice();
            return null;
        }

    }
}
