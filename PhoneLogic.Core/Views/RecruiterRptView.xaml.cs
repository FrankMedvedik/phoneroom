using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Views
{
    public partial class RecruiterRptView : UserControl
    {
        private RecruiterViewModel _vm;
        private ToggleButton _selectedButton = new ToggleButton();

        public RecruiterRptView()
        {
            _vm = new RecruiterViewModel();
            DataContext = _vm;
            InitializeComponent();
            AudioPlayer.Visibility = Visibility.Collapsed;
            AudioPlayer.AudioPlayback.MediaEnded += ResetPlaybackState;
        }


        private void btnGetLogsClick(object sender, System.Windows.RoutedEventArgs e)
        {
            _vm.GetRecruiterLogs();
        }


        public string RecruiterSip
        {
            get { return (string)GetValue(RecruiterSipProperty); }
            set
            {
                SetValue(RecruiterSipProperty, value);
                _vm.RecruiterSip = value;
            }
        }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RecruiterSipProperty =
            DependencyProperty.Register("RecruiterSip", typeof(string), typeof(RecruiterRptView), new PropertyMetadata(""));

        private void tbtnPlay_Checked(object sender, RoutedEventArgs e)
        {
            AudioPlayer.Visibility = Visibility.Visible;
            _selectedButton = (sender as ToggleButton);
            _selectedButton.Content = "Stop";
            AudioPlayer.Play();

        }

        private void tbtnPlay_Unchecked(object sender, RoutedEventArgs e)
        {
            _selectedButton = (sender as ToggleButton);
            _selectedButton.Content = "Play";
            AudioPlayer.Stop();
            AudioPlayer.Visibility = Visibility.Collapsed;
        }

        private void ResetPlaybackState(object o, RoutedEventArgs e)
        {
            _selectedButton.Content = "Play";
            AudioPlayer.Visibility = Visibility.Collapsed;
        }

        private void RecruiterLogDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            // set up the audio playback
            if (_vm.RecruiterSip == null) return;
            AudioPlayer.PlaybackFileName = ConditionalConfiguration.rootUrl + "ClientBin/LiveRecordings/" + _vm.SelectedRecruiterLog.callId + ".wma";
            AudioPlayer.Visibility = Visibility.Visible;
        }

    }
}