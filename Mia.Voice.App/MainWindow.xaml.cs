using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mia.Voice.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VoiceListener listener = new VoiceListener();


        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            VoiceListenerParameters parameters = new VoiceListenerParameters();

            parameters.Terms = new List<VoiceTerm>()
            {
                new VoiceTerm() { Type = TermType.Command, Value = "OK" },
                new VoiceTerm() { Type = TermType.Number, Value = "Eins" },
                new VoiceTerm() { Type = TermType.Number, Value = "2" },
                new VoiceTerm() { Type = TermType.Text, Value = "Einlagern" },
            };

            //parameters.Tolerance = 50;
            listener.Initialize(parameters);
            listener.TermReceived += Listener_TermReceived;




        }

        private void Listener_TermReceived(VoiceTerm term)
        {
            if (term.Type == TermType.Command)
            {
                //...
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listener.StartListening();
        }
    }
}
