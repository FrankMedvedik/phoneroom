using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Lync.Model;
using Newtonsoft.Json;
using PhoneLogic.CallRpt.Model;
using PhoneLogic.Core.Areas.CallsRpts.Models;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;
using Silverlight.Base.MVVMBaseTypes;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    public class CallsViewModel : CollectionViewModelBase
    {
        public CallsViewModel()
        {
        }

        private Boolean _canCall = true;

        public Boolean CanCall
        {
            get { return _canCall; }
            set
            {
                _canCall = value;
                NotifyPropertyChanged();
            }
        }


        private String _actionMsg;

        public String ActionMsg
        {
            get { return _actionMsg; }
            set
            {
                _actionMsg = value;
                NotifyPropertyChanged();
            }
        }

        private bool _canRefresh = true;

        public Boolean CanRefresh
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }

        #region reporting variables

        public ReportDateRange ReportDateRange = new ReportDateRange();

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

        public DateTime? LastCallStartTime
        {
            get { return _calls.Select(x => x.CallStartTime).Max(); }
        }

        protected override void RefreshAll(object sender, EventArgs e)
        {
            if (CanRefresh)
            {
                var s = SelectedCall;
                GetCalls();
                //SelectedCall = null;
                if ((s != null) && (Calls.First(x => x.CallId == s.CallId) != null))
                {
                    SelectedCall = Calls.First(x => x.CallId == s.CallId);
                }
                else
                    SelectedCall = null;
            }
        }

        public async void GetCalls()
        {
            ShowGridData = false;
            HeadingText = "";
            var ro = new List<Call>();
            try
            {
                if ((ReportDateRange != null) && (SelectedJobNum != null) && (SelectedRecruiter != null))
                {
                    HeadingText = "Loading...";
                    ro = await LyncCallLogSvc.GetLyncCallLog(SelectedJobNum, SelectedRecruiter,
                        ReportDateRange.StartRptDate, ReportDateRange.EndRptDate);
                }
                else if ((ReportDateRange != null) && (SelectedRecruiter != null))
                {
                    //HeadingText = "Loading...";
                    //ro = await LyncCallLogSvc.GetCalls(SelectedRecruiter, ReportDateRange.StartRptDate,
                    //    ReportDateRange.EndRptDate);
                    throw new Exception("There is a prblem we should not ask for calls without a job!!!");
                }

                ShowGridData = true;
                Calls = new ObservableCollection<Call>(ro.OrderByDescending(x => x.CallStartTime));
                if (Calls.Count > 0)
                {
                    //if its a recruiter show the heading without their name
                    //string sip = Calls.First().RecruiterSIP;
                    //if (sip == LyncClient.GetClient().Uri)
                    HeadingText = String.Format("Job {0} has {1} calls",
                        StringFormatSvc.JobAndTaskFormatted(SelectedJobNum), Calls.Count());
                    //else
                    //{
                    //if its a not a recruiter show the heading with name 
                    //Contact r = LyncClient.GetClient().ContactManager.GetContactByUri(sip);
                    //HeadingText = String.Format("{0} has {1} Calls for Job {2}-{3}",
                    //    r.GetContactInformation(ContactInformationType.DisplayName).ToString() != ""
                    //        ? r.GetContactInformation(ContactInformationType.DisplayName).ToString()
                    //        : "Inbound Respondent",
                    //    Calls.Count(), SelectedJobNum.Substring(0, 4), SelectedJobNum.Substring(4, 4));
                }
                else
                {
                    HeadingText = "No calls";
                    ShowGridData = false;
                }
                LoadedOk = true;
            }
            catch (Exception e)
            {
                LoadFailed(e);
                HeadingText = e.Message;
            }
        }
    }
}