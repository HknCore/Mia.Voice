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
using System.Diagnostics;
using NLog;

namespace Mia.Voice
{
    public class VoiceListener
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        static CultureInfo ci = new CultureInfo("de-DE");
        static SpeechRecognitionEngine sre = new SpeechRecognitionEngine(ci);
        public VoiceListenerParameters parameters = new VoiceListenerParameters();
        public List<Tuple<TermType, String>> ListType;
        public List<Tuple<TermType, String>> ListRange;
        public Dictionary<string, int> textNumber;
        private float RealTolerance;
        TermType type;

        public void Initialize(VoiceListenerParameters parameters)
        {
            try
            {
                sre.SetInputToDefaultAudioDevice();
                sre.SpeechRecognized += SomethingHeard;
                Grammar g_HelloGoodbye = GetGrammar(parameters);
                sre.LoadGrammarAsync(g_HelloGoodbye);
                GetTolerance(parameters);
                logger.Info("Program started..");
            }
            catch (DivideByZeroException ex)
            {
                logger.Error("Initializing problems..", ex);
            }
        }
        public void StartListening()
        {
            try
            {
                sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                logger.ErrorException("Can't be run twice", ex);
            }
        }

        public void StopListening()
        {
            sre.RecognizeAsyncCancel();
        }

        private void SomethingHeard(object sender,
          SpeechRecognizedEventArgs e)
        {        
            string txt = e.Result.Text;
            float conf = e.Result.Confidence;
            if (conf < RealTolerance) return;
            try
            {
                foreach (var lst in ListType)
                {
                    if (lst.Item2.Equals(txt))
                    {
                        type = lst.Item1;                    
                    }
                }
            }
            catch (DivideByZeroException ex)
            {
                logger.Error("Something went wrong", ex);
            }

            logger.Info("Listening... You said:" + "\"" + txt + "|" + type + "\"");

            this.RaiseTermReceived(new VoiceTerm() { Type = type, Value = txt });
        }

        public delegate void ReceivedHandler(VoiceTerm term);
        public event ReceivedHandler TermReceived;

        protected virtual void RaiseTermReceived(VoiceTerm term)
        {
            if (TermReceived != null)
                TermReceived(term);
        }

        private Grammar GetGrammar(VoiceListenerParameters parameters)
        {

            ListType = new List<Tuple<TermType, String>>();
            Choices ch_Dict = new Choices();
            foreach (VoiceTerm Value in parameters.Terms)
            {
                if (Value.Value.Equals("Range"))
                {
                    int[] arr = Enumerable.Range(0, 10).ToArray();
                    foreach (int y in arr)
                    {
                        String newint = y.ToString();
                        ch_Dict.Add(newint);
                        ListType.Add(Tuple.Create(TermType.Number, newint));
                    }
                }
                else
                {
                    string varadd = Value.Value;
                    ch_Dict.Add(Value.Value);
                    ListType.Add(Tuple.Create(Value.Type, varadd));
                }
            }

            GrammarBuilder gb_result = new GrammarBuilder(ch_Dict);
            Grammar g_result = new Grammar(gb_result);
            return g_result;
        }

        private void GetTolerance(VoiceListenerParameters parameters)
        {
            RealTolerance = parameters.Tolerance / 100;
        }

    }
}
