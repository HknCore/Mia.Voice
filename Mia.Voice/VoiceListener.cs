using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Speech.Recognition;
using Microsoft.Speech;
using System.Globalization;
using System.Windows.Forms;

namespace Mia.Voice
{
    public class VoiceListener
    {
        
        static CultureInfo ci = new CultureInfo("de-DE");
        static SpeechRecognitionEngine sre = new SpeechRecognitionEngine(ci);
        public VoiceListenerParameters parameters = new VoiceListenerParameters();
        private bool Listener = false;

        public void Initialize(VoiceListenerParameters parameters)
        {
            sre.SetInputToDefaultAudioDevice();
            sre.SpeechRecognized += SomethingHeard;
            Grammar g_HelloGoodbye = GetGrammar(parameters);
            sre.LoadGrammarAsync(g_HelloGoodbye);
            //sre.RecognizeAsync(RecognizeMode.Multiple);
        }


        public void StartListening()
        {
            Listener = true;

            // Dummy
            if (Listener == true)
            {
                sre.RecognizeAsync(RecognizeMode.Multiple);
            } else
            {
                StopListening();
            }
        }


        public void StopListening()
        {
            sre.RecognizeAsyncCancel();
        }


        private void SomethingHeard(object sender,
          SpeechRecognizedEventArgs e)
        {
            this.RaiseTermReceived(new VoiceTerm() { Type = TermType.Command, Value = "OK" });
            string txt = e.Result.Text;
            float conf = e.Result.Confidence;
            if (conf < 0.65) return;


            if (parameters.Terms.ToString().Contains(txt)){
                System.Diagnostics.Debug.WriteLine(txt);

            }

        }



        public delegate void ReceivedHandler(VoiceTerm term);
        public event ReceivedHandler TermReceived;

        protected virtual void RaiseTermReceived(VoiceTerm term)
        {
            if (TermReceived != null)
                TermReceived(term);
        }



        static Grammar GetGrammar(VoiceListenerParameters parameters)
        {
            VoiceTerm example = new VoiceTerm();

            Choices ch_Dict = new Choices();

            foreach (var Value in parameters.Terms)
            {
                System.Diagnostics.Debug.WriteLine(example.GetType());
                ch_Dict.Add(Value.ToString());
                //ch_Dict.Add(example.Value);
            }


            

            GrammarBuilder gb_result =
            new GrammarBuilder(ch_Dict);
            Grammar g_result = new Grammar(gb_result);
            return g_result;
        }

    }
}
