using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace PhoneLogic.Core.Views
{
    public partial class AudioPlaybackView : UserControl
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        public AudioPlaybackView()
        {
            _timer.Interval = TimeSpan.FromSeconds(0.1);
            _timer.Tick += timer_Tick;
            InitializeComponent();
            TbPlaybackDuration.Text = "00:00";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            TbPlaybackDuration.Text = AudioPlayback.Position.TotalSeconds.ToString();
            SliderPositionBackground.Value = AudioPlayback.Position.TotalSeconds;
        }

        #region AudioPlayback controls

        private string _playbackFileName = "";
        public string PlaybackFileName
        {
            get { return _playbackFileName; }
            set
            {
                _playbackFileName = value;
                AudioPlayback.Source = new Uri(PlaybackFileName, UriKind.Absolute);
                ResetPlayback();
            }
        }


        public void Play()
        {
            AudioPlayback.Play();
            _timer.Start();
        }

        public void Stop()
        {
            ResetPlayback();
        }
        //private void tbtnListen_Checked(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    (sender as ToggleButton).Content = "Stop";
        //    AudioPlayback.Play();
        //}

        //private void tbtnListen_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    (sender as ToggleButton).Content = "Play";
        //    AudioPlayback.Stop();
        //    ResetPlayback();
        //}

        private void ResetPlayback()
        {
            AudioPlayback.Position = TimeSpan.FromSeconds(0);
            TbPlaybackDuration.Text = AudioPlayback.NaturalDuration.TimeSpan.ToString();
            SliderPosition.Value = 0;
            SliderPosition.Maximum = AudioPlayback.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void sliderPosition_ValueChanged(object sender, RoutedEventArgs e)
        {
            AudioPlayback.Pause();
            AudioPlayback.Position = TimeSpan.FromSeconds(SliderPosition.Value);
            AudioPlayback.Play();
        }

//        private void media_CurrentStateChanged(object sender, RoutedEventArgs e)
//        {
//            //MessageBox.Show(AudioPlayback.CurrentState.ToString());
//            switch (AudioPlayback.CurrentState)
//            {
//                //case MediaElementState.Playing:
//                //    break;
//                case MediaElementState.Stopped:
////                    tbtnListen.Content = "Stop";
//                    ResetPlayback();
//                    PlaybackStopped();
//                    break;
//                default:
//                    //ResetPlayback();
//                    break;
//            }
//        }
     
        //public delegate void PlaybackStoppedEventHandler();
        //public event PlaybackStoppedEventHandler PlaybackStopped;
        
        #endregion

       
    }
}
