using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mia.Voice
{
    interface Interface
    {
        //Windows-Standard Mikrofon wird verwendet (=sichergehen , dass ein Mikrofon in den "Aufnahmeoptionen" von Windows ausgewählt ist)
        //SomethingHeard wird gehandelt
        //Grammar wird erstellt (=Wörter die angegeben worden sind werden initialisiert) danach wird das Grammar geladen
        //Die Toleranz wird von der Applikation "Gegetted" - Wert von 1-100 (=100 wird nichtsmehr erkannt / je höher desto genauer)
        void Initialize(VoiceListenerParameters parameters);

        //Recognizer wird gestartet
        void StartListening();

        //Recognizer wird gecancelt
        void StopListening();
    }
}
