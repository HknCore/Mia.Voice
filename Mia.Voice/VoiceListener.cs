using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mia.Voice
{
    public class VoiceListener
    {

        public void Initialize(VoiceListenerParameters parameters)
        {
        }


        public void StartListening()
        {
            // Dummy
            this.SomethingHeard();
        }


        public void StopListening()
        {
        }


        private void SomethingHeard()
        {
            this.RaiseTermReceived(new VoiceTerm() { Type = TermType.Command, Value = "OK" });
        }



        public delegate void ReceivedHandler(VoiceTerm term);
        public event ReceivedHandler TermReceived;

        protected virtual void RaiseTermReceived(VoiceTerm term)
        {
            if (TermReceived != null)
                TermReceived(term);
        }




    }
}
