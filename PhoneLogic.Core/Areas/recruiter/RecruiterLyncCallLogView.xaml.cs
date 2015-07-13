using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.Helpers;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.areas.recruiter
{
    public partial class CallView : UserControl
    {
        private CallViewModel _vm;
        private ToggleButton _selectedButton = new ToggleButton();

        //public CallView()
        //{
        //    _vm = new CallViewModel();
        //    DataContext = _vm;
        //    InitializeComponent();
        //    AudioPlayer.Visibility = Visibility.Collapsed;
        //    //AudioPlayer.AudioPlayback.MediaEnded += ResetPlaybackState;
        //}


        //private void btnGetLogsClick(object sender, RoutedEventArgs e)
        //{
        //    _vm.GetRecruiterLogs();
        //    AudioPlayer.Reset();
        //}
        
        //private void btnExport_Click(object sender, RoutedEventArgs e)
        //{
        //    RecruiterLogDG.Export();
        //}

        //public string RecruiterSip
        //{
        //    get { return (string)GetValue(RecruiterSipProperty); }
        //    set
        //    {
        //        AudioPlayer.Reset();
        //        AudioPlayer.Visibility = Visibility.Collapsed;
        //        SetValue(RecruiterSipProperty, value);
        //        _vm.RecruiterSip = value;

        //    }
        //}
        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty RecruiterSipProperty =
        //    DependencyProperty.Register("RecruiterSip", typeof(string), typeof(RecruiterRptView), new PropertyMetadata(""));

        //private void RecruiterLogDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //    // set up the audio playback
        //    if (_vm.RecruiterSip == null)
        //    {
        //        AudioPlayer.Reset();
        //        AudioPlayer.Visibility = Visibility.Collapsed;
        //    }
        //    else
        //    {
        //        if (_vm.SelectedRecruiter != null)
        //        {
        //            AudioPlayer.Reset();
        //            AudioPlayer.Title = String.Format("{0} - {1}", _vm.SelectedRecruiter.JobFormatted,
        //                _vm.SelectedRecruiter.PhoneFormatted);
        //            AudioPlayer.Url = ConditionalConfiguration.rootUrl + "ClientBin/LiveRecordings/" +
        //                              _vm.SelectedRecruiter.CallId + ".wma";
        //            AudioPlayer.Visibility = Visibility.Visible;
        //        }

        //    }
        //}

        private void RecruiterLogDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}