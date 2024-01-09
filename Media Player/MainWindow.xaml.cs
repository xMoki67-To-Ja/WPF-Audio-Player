using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Media_Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timerTick);
        }
        void timerTick(object sender, EventArgs e)
        {
            if (mediaPlayer != null && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                txtCzas.Text = mediaPlayer.Position.ToString(@"mm\:ss");
                TimeSpan ts = mediaPlayer.NaturalDuration.TimeSpan;
                pbGra.Maximum = 100;
                pbGra.Value = ((double)mediaPlayer.Position.TotalMilliseconds /
                    ts.TotalMilliseconds * 100);
            }
        }
        private void btnWybierz_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "MP3 Files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                mediaPlayer.Open(new Uri(dialog.FileName));
                txtUtwor.Text = String.Format("Utwór: {0}", dialog.FileName);
                btnPlay.IsEnabled = true;
                btnPause.IsEnabled = true;
                btnStop.IsEnabled = true;
                timer.Start();
            }
        }
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
        }
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
        }
        private void radio_Checked(object sender, RoutedEventArgs e)
        {
            var radio = sender as RadioButton;
            string kolor = (radio.Content.ToString() == "Niebieski") ? "LightSkyBlue" : "LightGreen";
            pbGra.Foreground = (SolidColorBrush)new
                BrushConverter().ConvertFromString(kolor);
        }
    }
}