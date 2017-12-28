﻿using System;
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

namespace Mia.Voice
{
    public class VoiceListener
    {

        static CultureInfo ci = new CultureInfo("de-DE");
        static SpeechRecognitionEngine sre = new SpeechRecognitionEngine(ci);
        public VoiceListenerParameters parameters = new VoiceListenerParameters();
        public List<Tuple<TermType, String>> ListType;
        public Dictionary<string, int> textNumber;
        private float RealTolerance;
        TermType type;

        public void Initialize(VoiceListenerParameters parameters)
        {
            sre.SetInputToDefaultAudioDevice();
            Numbers();
            sre.SpeechRecognized += SomethingHeard;
            Grammar g_HelloGoodbye = GetGrammar(parameters);
            sre.LoadGrammarAsync(g_HelloGoodbye);
            GetTolerance(parameters);


        }

        public void StartListening()
        {
            sre.RecognizeAsync(RecognizeMode.Multiple);
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

            foreach (var lst in ListType)
            {

                if (lst.Item2.Equals(txt))
                {
                    type = lst.Item1;             
                }
            }
            
            this.RaiseTermReceived(new VoiceTerm() { Type = type, Value = txt });

            //System.Diagnostics.Debug.WriteLine(type);
            //System.Diagnostics.Debug.WriteLine(txt);

        }
          
        public void Numbers()
        {
            textNumber = new Dictionary<string, int>();
            textNumber.Add("eins", 1);
            textNumber.Add("zwei", 2);
            textNumber.Add("drei", 3);
            textNumber.Add("vier", 4);
            textNumber.Add("fünf", 5);
            textNumber.Add("sechs", 6);
            textNumber.Add("sieben", 7);
            textNumber.Add("acht", 8);
            textNumber.Add("neun", 9);
            textNumber.Add("zehn", 10);
            textNumber.Add("elf", 11);
            textNumber.Add("zwölf", 12);
            textNumber.Add("dreizehn", 13);
            textNumber.Add("vierzehn", 14);
            textNumber.Add("fünfzehn", 15);
            textNumber.Add("sechszehn", 16);
            textNumber.Add("siebzehn", 17);
            textNumber.Add("achtzehn", 18);
            textNumber.Add("neunzehn", 19);
            textNumber.Add("zwanzig", 20);
            textNumber.Add("einundzwanzig", 21);
            textNumber.Add("zweiundzwanzig", 22);
            textNumber.Add("dreiundzwanzig", 23);
            textNumber.Add("vierundzwanzig", 24);
            textNumber.Add("fünfundzwanzig", 25);
            textNumber.Add("sechsundzwanzig", 26);
            textNumber.Add("siebenundzwanzig", 27);
            textNumber.Add("achtundzwanzig", 28);
            textNumber.Add("neunundzwanzig", 29);
            textNumber.Add("dreißig", 30);
            textNumber.Add("einunddreißig", 31);
            textNumber.Add("zweiunddreißig", 32);
            textNumber.Add("dreiunddreißig", 33);
            textNumber.Add("vierunddreißig", 34);
            textNumber.Add("fünfunddreißig", 35);
            textNumber.Add("sechsunddreißig", 36);
            textNumber.Add("siebenunddreißig", 37);
            textNumber.Add("achtunddreißig", 38);
            textNumber.Add("neununddreißig", 39);
            textNumber.Add("vierzig", 40);
            textNumber.Add("einundvierzig", 41);
            textNumber.Add("zweiundvierzig", 42);
            textNumber.Add("dreiundvierzig", 43);
            textNumber.Add("vierundvierzig", 44);
            textNumber.Add("fünfundvierzig", 45);
            textNumber.Add("sechsundvierzig", 46);
            textNumber.Add("siebenundvierzig", 47);
            textNumber.Add("achtundvierzig", 48);
            textNumber.Add("neunundvierzig", 49);
            textNumber.Add("fünfzig", 50);
            textNumber.Add("einundfünfzig", 51);
            textNumber.Add("zweiundfünfzig", 52);
            textNumber.Add("dreiundfünfzig", 53);
            textNumber.Add("viersundfünfzig", 54);
            textNumber.Add("Fünfundfünfzig", 55);
            textNumber.Add("sechsundfünfzig", 56);
            textNumber.Add("siebenundfünfzig", 57);
            textNumber.Add("achtundfünfzig", 58);
            textNumber.Add("neunundfünfzig", 59);
            textNumber.Add("sechzig", 60);
            textNumber.Add("einundsechzig", 61);
            textNumber.Add("zweiundsechzig", 62);
            textNumber.Add("dreiundsechzig", 63);
            textNumber.Add("viersundsechzig", 64);
            textNumber.Add("fünfundsechzig", 65);
            textNumber.Add("sechsundsechzig", 66);
            textNumber.Add("siebenundsechzig", 67);
            textNumber.Add("achtundsechzig", 68);
            textNumber.Add("neunundsechzig", 69);
            textNumber.Add("siebzig", 70);
            textNumber.Add("einundsiebzig", 71);
            textNumber.Add("zweiundsiebzig", 72);
            textNumber.Add("dreiundsiebzig", 73);
            textNumber.Add("vierundsiebzig", 74);
            textNumber.Add("fünfundsiebzig", 75);
            textNumber.Add("sechsundsiebzig", 76);
            textNumber.Add("siebenundsiebzig", 77);
            textNumber.Add("achtundsiebzig", 78);
            textNumber.Add("neunundsiebzig", 79);
            textNumber.Add("achtzig", 80);
            textNumber.Add("einundachtzig", 81);
            textNumber.Add("zweiundachtzig", 82);
            textNumber.Add("dreiundachtzig", 83);
            textNumber.Add("vierundachtzig", 84);
            textNumber.Add("fünfundachtzig", 85);
            textNumber.Add("sechsundachtzig", 86);
            textNumber.Add("siebenundachtzig", 87);
            textNumber.Add("achtundachtzig", 88);
            textNumber.Add("neunundachtzig", 89);
            textNumber.Add("Neunzig", 90);
            textNumber.Add("einundneunzig", 91);
            textNumber.Add("Zweiundneunzig", 92);
            textNumber.Add("dreiundneunzig", 93);
            textNumber.Add("vierundneunzig", 94);
            textNumber.Add("fünfundneunzig", 95);
            textNumber.Add("sechsundneunzig", 96);
            textNumber.Add("siebenundneunzig", 97);
            textNumber.Add("achtundneunzig", 98);
            textNumber.Add("neunundneunzig", 99);
            textNumber.Add("hundert", 100);
        }

        public delegate void ReceivedHandler(VoiceTerm term);
        public event ReceivedHandler TermReceived;

        protected virtual void RaiseTermReceived(VoiceTerm term)
        {
            if (TermReceived != null)
                TermReceived(term);
        }


        public Grammar GetGrammar(VoiceListenerParameters parameters)
        {

            ListType = new List<Tuple<TermType, String>>();
            Choices ch_Dict = new Choices();

            foreach (VoiceTerm Value in parameters.Terms)
            {
                string testterm = Value.Value;
                int ValueS;

                if (textNumber.ContainsKey(testterm))
                {
                    int value = textNumber[testterm];
                    ValueS = value;
                    ch_Dict.Add(ValueS.ToString());

                }

                else
                {
                    ch_Dict.Add(Value.Value);
                }

                ListType.Add(Tuple.Create(Value.Type, testterm));
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
