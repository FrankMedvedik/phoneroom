using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PhoneLogic.Core.Views
{
    public partial class AudioPlaybackView : UserControl
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private TimeSpan _playbackDuration = new TimeSpan();
        public TimeSpan PlaybackDuration { get { return _playbackDuration; }
            set
            {
                _playbackDuration = value;
                NotifyPropertyChanged();
            }
        }
        
        public AudioPlaybackView()
        {
            _timer.Interval = TimeSpan.FromSeconds(0.1);
            _timer.Tick += timer_Tick;
            InitializeComponent();
            //PlaybackControls.Visibility = Visibility.Collapsed;


        }

        private void timer_Tick(object sender, EventArgs e)
        {
            _playbackDuration = AudioPlayback.Position;
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
                AudioPlayback.Stop();
                AudioPlayback.Source = new Uri(PlaybackFileName, UriKind.Absolute);
                AudioPlayback.Stop();
                SliderPosition.Value = 0;
                AudioPlayback.Stop();
                playControlsPanel.Visibility = Visibility.Visible;
            }
        }

        
        private void tbtnListen_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            (sender as ToggleButton).Content = "Stop";
            AudioPlayback.Play();
            _timer.Start();
        }

        private void tbtnListen_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ResetPlayback();
        }
        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            PlaybackDuration = AudioPlayback.NaturalDuration.TimeSpan;
            SliderPosition.Maximum = AudioPlayback.NaturalDuration.TimeSpan.TotalSeconds;
            AudioPlayback.Position = TimeSpan.FromSeconds(0);
        }

        private void ResetPlayback()
        {
            AudioPlayback.Position = TimeSpan.FromSeconds(0);
            AudioPlayback.Stop();
            SliderPosition.Value = 0;
            tbtnListen.Content = "Listen";
        }

        private void sliderPosition_ValueChanged(object sender, RoutedEventArgs e)
        {
            AudioPlayback.Pause();
            AudioPlayback.Position = TimeSpan.FromSeconds(SliderPosition.Value);
            AudioPlayback.Play();
        }

        private void media_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(AudioPlayback.CurrentState.ToString());
            switch (AudioPlayback.CurrentState)
            {
                case MediaElementState.Playing:
                    tbtnListen.Content = "Stop";
                    break;
                default:
                    ResetPlayback();
                    break;
            }
        }
        #endregion
        #region Property changed
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            var e = PropertyChanged;
            if (e != null) e(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

    }
}
