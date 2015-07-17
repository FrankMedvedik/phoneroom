using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.CallRpt.Model;
using PhoneLogic.Core.areas.CallsRpts.Models;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;
using Silverlight.Base.MVVMBaseTypes;

namespace PhoneLogic.Core.areas.CallsRpts
{
    public class CallsViewModel : CollectionViewModelBase
    {
        public CallsViewModel()
        {

        }


        private bool _canRefresh = true;

        public Boolean CanRefresh
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }


        #region reporting variables

        public CallRptDateRange CallRptDateRange = new CallRptDateRange();

        #endregion


        private string _selectedRecruiter = null;

        public string SelectedRecruiter
        {
            get { return _selectedRecruiter; }
            set
            {
                _selectedRecruiter = value;
                NotifyPropertyChanged();
            }
        }

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

        private string _selectedJobNum = null;

        public string SelectedJobNum
        {
            get { return _selectedJobNum; }
            set
            {
                _selectedJobNum = value;
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

        private ObservableCollection<Call> _calls;
        public ObservableCollection<Call> Calls
        {
            get { return _calls; }
            set
            {
                _calls = value;
                NotifyPropertyChanged();
            }
        }


        protected override void RefreshAll(object sender, EventArgs e)
        {
            if (CanRefresh)
            {
                //var s = SelectedCall;
                GetCalls();
                //if ((s != null) && (Calls.First(x => x.CallId == s.CallId) != null))
                //{
                //    SelectedCall = Calls.First(x => x.CallId == s.CallId);
                //}
                //else
                //    SelectedCall = null;
            }
        }

        public async void GetCalls()
        {
            ShowGridData = false;
            HeadingText = "";
            try
            {

                if ((CallRptDateRange != null) && (SelectedJobNum != null) && (SelectedRecruiter != null))
                {
                    HeadingText = "Loading...";
                    var ro = await LyncCallLogSvc.GetLyncCallLog(SelectedJobNum, SelectedRecruiter,
                                CallRptDateRange.StartRptDate, CallRptDateRange.EndRptDate);
                    ShowGridData = true;
                    Calls = new ObservableCollection<Call>(ro);
                    HeadingText = String.Format("Job {0}-{1}  Recruiter {2}", SelectedJobNum.Substring(0, 4),
                        SelectedJobNum.Substring(4, 4), Calls.First().DisplayName);
                    LoadedOk = true;
                }
                else if ((CallRptDateRange != null) && (SelectedRecruiter != null))
                {
                    HeadingText = "Loading...";
                    var ro = await LyncCallLogSvc.GetCalls(SelectedRecruiter, CallRptDateRange.StartRptDate,
                    CallRptDateRange.EndRptDate);
                    ShowGridData = true;
                    Calls = new ObservableCollection<Call>(ro);
                    HeadingText = String.Format("{0} has {1} Calls", Calls.First().DisplayName != "" ? Calls.First().DisplayName: "Inbound Respondent", Calls.Count());
                    LoadedOk = true;
                }
            }
            catch (Exception e)
            {
                LoadFailed(e);
            }
        }
    }


}


