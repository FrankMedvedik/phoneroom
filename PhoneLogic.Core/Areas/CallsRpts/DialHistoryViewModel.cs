using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    public class DialHistoryViewModel : CollectionViewModelBase
    {
        private Call _selectedCall;
        public Call SelectedCall
        {
            get { return _selectedCall; }
            set
            {
                _selectedCall = value;
                NotifyPropertyChanged();
            }
        }

        private string _selectedCallerId= null;
        public string SelectedCallerId
        {
            get { return _selectedCallerId; }
            set
            {
                _selectedCallerId = value;
                NotifyPropertyChanged();
            }
        }

        private string _headingText;
        public string HeadingText
        {
            get { return _headingText; }
            set
            {
                _headingText = value;
                NotifyPropertyChanged();
            }
        }


        private ObservableCollection<Call> _calls = new ObservableCollection<Call>();

        public ObservableCollection<Call> Calls
        {
            get { return _calls; }
            set
            {
                _calls = value;
                NotifyPropertyChanged();
            }
        }
        private bool _showInput = true;

        public bool ShowInput
        {
            get { return _showInput; }
            set { _showInput = value; NotifyPropertyChanged(); }
        }

   

        protected override void RefreshAll(object sender, EventArgs e)
        {
                GetCalls();
        }

        public async void GetCalls()
        {
            ShowGridData = false;
            HeadingText = "";
            var ro = new List<Call>();
            try
            {
                if (SelectedCallerId != null)
                {
                    HeadingText = "Loading...";
                    ro = await LyncCallLogSvc.GetCalls(PhoneNumberSvc.GetNumbers(SelectedCallerId));
                    Calls = new ObservableCollection<Call>(ro.OrderByDescending(x => x.CallStartTime));
                    //var z = Calls.GroupBy(x => x.JobFormatted)
                    //   .Select(x => new { Date = x.Key, Values = x.Distinct().Count() });
                    if (Calls.Count > 0)
                    {
                        HeadingText = String.Format("Phone Number {0} has {1} calls", SelectedCallerId, Calls.Count());
                        ShowGridData = true;
                    }
                    else
                    {
                        HeadingText = "No calls";
                        ShowGridData = false;
                    }
                    LoadedOk = true;
                    
                }
            }
            catch (Exception e)
            {
                LoadFailed(e);
                HeadingText = e.Message;
            }
        }
    }
}


