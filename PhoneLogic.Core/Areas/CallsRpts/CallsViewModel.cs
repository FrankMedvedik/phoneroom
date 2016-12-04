using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Lync.Model;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    public class CallsViewModel : CollectionViewModelBase
    {
        public CallsViewModel()
        {
            Messenger.Default.Register<NotificationMessage<PhoneLogicTask>>(this, message =>
            {
                if (message.Notification == Notifications.MySelectedPhoneLogicTaskChanged)
                {
                    SelectedJobNum = message.Content.JobNum + ":0" + message.Content.TaskID;
                    RefreshAll();
                }
            });
        }
        private string _actionMsg;


        private ObservableCollection<Call> _calls = new ObservableCollection<Call>();

        private bool _canCall = true;

        private string _headingText;

        private Call _selectedCall;

        private string _selectedJobNum;

        private string _selectedRecruiter;

        #region reporting variables

        public ReportDateRange ReportDateRange = new ReportDateRange();

        #endregion

        public bool CanCall
        {
            get { return _canCall; }
            set
            {
                _canCall = value;
                NotifyPropertyChanged();
            }
        }

        public string ActionMsg
        {
            get { return _actionMsg; }
            set
            {
                _actionMsg = value;
                NotifyPropertyChanged();
            }
        }

        public bool CanRefresh { get; set; } = true;

        public string SelectedRecruiter
        {
            get { return _selectedRecruiter; }
            set
            {
                _selectedRecruiter = value;
                NotifyPropertyChanged();
            }
        }

        public Call SelectedCall
        {
            get { return _selectedCall; }
            set
            {
                _selectedCall = value;
                NotifyPropertyChanged();
            }
        }

        public string SelectedJobNum
        {
            get { return _selectedJobNum; }
            set
            {
                _selectedJobNum = value;
                NotifyPropertyChanged();
            }
        }

        public string HeadingText
        {
            get { return _headingText; }
            set
            {
                _headingText = value;
                NotifyPropertyChanged();
            }
        }

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
            SelectedRecruiter = LyncClient.GetClient().Self.Contact.Uri;
            ReportDateRange = new ReportDateRange()
            {
                StartRptDate = Convert.ToDateTime("7/01/2015"),
                EndRptDate = DateTime.Now
            };
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
                    throw new Exception("There is a prblem we should not ask for calls without a job!!!");
                }

                ShowGridData = true;
                Calls = new ObservableCollection<Call>(ro.OrderByDescending(x => x.CallStartTime));
                if (Calls.Count > 0)
                {
                    HeadingText = string.Format("Job {0} has {1} calls",
                        StringFormatSvc.JobAndTaskFormatted(SelectedJobNum), Calls.Count());
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