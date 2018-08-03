using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech;
using System.Speech.AudioFormat;
using System.Speech.Recognition;
using System.Speech.Recognition.SrgsGrammar;
using System.Speech.Synthesis;
using System.Speech.Synthesis.TtsEngine;

namespace player
{
    class Recognizer
    {
        SpeechRecognitionEngine engine;

        public Recognizer(EventHandler<SpeechRecognizedEventArgs> func)
        {
            //creates the new engine
            this.engine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            this.engine.LoadGrammar(new DictationGrammar());
            if (func == null)
                func = new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

            this.engine.SpeechRecognized += func;

            this.engine.SetInputToDefaultAudioDevice();


        }
        public void AsyncRecognize()
        {
            this.engine.RecognizeAsync();
        }


        private static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Recognized text: " + e.Result.Text);
        }

    }
}
