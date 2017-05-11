using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Media.Imaging;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace busqueda11
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SpeechSynthesizer synthesizer;

        public MainPage()
        {
            this.InitializeComponent();
            txbCodigo.Focus(FocusState.Programmatic);
            InicializaHabla();
         }

        private async void BtnEmpieza_Click(object sender, RoutedEventArgs e)
        {
            await Dime("Hola, Cris; soy Raspi. Joc Moz ha venido a tu casa y ha escondido tus regalos; si los quieres, vas a tener que buscarlos. Pero tenemos suerte: tiene un plano que indica dónde están, y se ha dejado olvidado el código de barras que muestra el plano. Podremos ver el plano cuando escaneemos su código secreto aquí, debajo de su foto. Para encontrar el código, busca a Leidi Bag. Para encontrarla, la pista es la siguiente: debajo de donde lees cuando anochece, te espera Leidi Bag. Mucha suerte, Cris.");
            txbCodigo.Focus(FocusState.Programmatic);
        }

        private void TxbCodigo_KeyUp(object sender, KeyRoutedEventArgs e)
        {            
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                ClaveCorrecta();
            }
        }

        private void InicializaHabla()
        {
            //lanza el habla
            synthesizer = new SpeechSynthesizer();

            VoiceInformation voiceInfo =
         (
           from voice in SpeechSynthesizer.AllVoices
           where voice.Gender == VoiceGender.Female
           select voice
         ).FirstOrDefault() ?? SpeechSynthesizer.DefaultVoice;

            synthesizer.Voice = voiceInfo;
        }

        private async Task Dime(String szTexto)
        {
            try
            {
                // crear el flujo desde el texto
                SpeechSynthesisStream synthesisStream = await synthesizer.SynthesizeTextToStreamAsync(szTexto);

                // ...y lo dice
                media.AutoPlay = true;
                media.SetSource(synthesisStream, synthesisStream.ContentType);
                media.Play();
            }
            catch (Exception e)
            {
                var msg = new Windows.UI.Popups.MessageDialog(e.Message, "Error hablando:");
                await msg.ShowAsync();
            }
        }

        private async void ClaveCorrecta()
        {            
            this.foto.Source = new BitmapImage(new Uri("ms-appx:///Assets//plano.png"));          

            await Dime("¡¡¡¡¡Muchas felicidades!!!!!"); 
            
        }
    }
}
