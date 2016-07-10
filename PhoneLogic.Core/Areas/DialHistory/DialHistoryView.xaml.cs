using System;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.Services;
using PhoneLogic.ViewContracts.MVVMMessenger;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Areas.DialHistory
{
    /// <summary>
    /// Description for CallsView.
    /// </summary>
    public partial class DialHistoryView : UserControl
    {
        private DialHistoryViewModel _vm = null;

        public DialHistoryView()
        {
            InitializeComponent();
            _vm = new DialHistoryViewModel();
            DataContext = _vm;
            //Messenger.Default.Register<NotificationMessage<Call>>(this, message =>
            //{
            //    if (message.Notification == Notifications.DialHistoryCallChanged)
            //    {
            //        SetupAudioPlayback();
            //    }
            //});
            AudioPlayer.Visibility = Visibility.Collapsed;
        }

        public void Refresh()
        {
            _vm.RefreshAll();
        }

        //public string SelectedCallerId
        //{
        //    get { return _vm.PhoneNumber; }
        //    set
        //    {
        //        SetValue(SelectedCallerIdProperty, value);
        //        _vm.PhoneNumber = value;
        //    }
        //}

        private void SetupAudioPlayback()
        {
            AudioPlayer.Visibility = Visibility.Collapsed;
            if (_vm.PhoneNumber != null)
            {
                AudioPlayer.Url = ConditionalConfiguration.rootUrl + "ClientBin/LiveRecordings/" +
                                  _vm.SelectedCall.CallId + ".wma";
                AudioPlayer.Reset();
                AudioPlayer.Title = String.Format("{0} - {1}", _vm.SelectedCall.JobFormatted,
                    _vm.SelectedCall.PhoneFormatted);
                AudioPlayer.Visibility = Visibility.Visible;
            }
        }

     public void GetCalls(string CallerId)
        {
            PhoneNumber = CallerId;
            _vm.GetCalls();
        }

     
        public bool ShowData
        {
            get { return _vm.ShowGridData; }
            set
            {
                _vm.ShowGridData = value;
                _vm.HeadingText = "";
            }
        }

        // Using a DependencyProperty as the backing store for boolean ShowData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowDataProperty =
            DependencyProperty.Register("ShowData", typeof(bool), typeof(DialHistoryView), new PropertyMetadata(false));


        public bool ShowInput
        {
            get { return _vm.ShowInput; }
            set
            {
                _vm.ShowInput = value;
                _vm.HeadingText = "";
            }
        }

        public string PhoneNumber
        {
            get { return _vm.PhoneNumber; }
            set { _vm.PhoneNumber = value; }
        }

        public DateTime StartDate
        {
            get { return _vm.StartDate; }
            set { _vm.StartDate = value; }
        }

        public DateTime EndDate
        {
            get { return _vm.EndDate; }
            set { _vm.EndDate = value; }
        }

        public bool AllCalls
        {
            get { return _vm.AllCalls; }
            set { _vm.AllCalls = value; }
        }

        public static readonly DependencyProperty ShowInputProperty =
            DependencyProperty.Register("ShowInput", typeof(bool), typeof(DialHistoryView), new PropertyMetadata(false));

        public void ResizeGrid()
        {
            //tbGridSize.Text = String.Format("Width : {0}  Height : {1} ", BrowserInfoSvc.ClientWidth,
            //    BrowserInfoSvc.ClientHeight);
            if (BrowserInfoSvc.ClientWidth < UserInterfaceTimings.ResizeBoundryWidth)
            {
                CallsDG.Columns[4].Visibility = Visibility.Collapsed;
                CallsDG.Columns[5].Visibility = Visibility.Collapsed;
                CallsDG.Columns[6].Visibility = Visibility.Collapsed;
                CallsDG.Columns[7].Visibility = Visibility.Collapsed;
                CallsDG.Columns[8].Visibility = Visibility.Collapsed;
                CallsDG.Columns[9].Visibility = Visibility.Collapsed;
            }
            if (BrowserInfoSvc.ClientWidth >= UserInterfaceTimings.ResizeBoundryWidth)
            {
                CallsDG.Columns[4].Visibility = Visibility.Visible;
                CallsDG.Columns[5].Visibility = Visibility.Visible;
                CallsDG.Columns[6].Visibility = Visibility.Visible;
                CallsDG.Columns[7].Visibility = Visibility.Visible;
                CallsDG.Columns[8].Visibility = Visibility.Visible;
                CallsDG.Columns[9].Visibility = Visibility.Visible;
            }
        }

        private void CallsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetupAudioPlayback();
        }
    }
}