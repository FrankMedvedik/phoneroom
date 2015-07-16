using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.CallRpt.Model;
using PhoneLogic.Core.areas.CallsRpts.Models;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.areas.CallsRpts
{
    public class JobRecruitersViewModel : CollectionViewModelBase
    {
        public JobRecruitersViewModel()
        {
            Messenger.Default.Register<NotificationMessage<CallRptDateRange>>(this, message =>
            {
                if (message.Notification == Notifications.CallRptDateRangeChanged)
                {
                    CallRptDateRange = message.Content;
                        GetRecruiters();
                }
            });

            Messenger.Default.Register<NotificationMessage<string>>(this, message =>
            {
                if (message.Notification == Notifications.CallRptJobNumSet)
                {
                    _callRptJobNum= message.Content;
                      GetRecruiters();
                }

                if (message.Notification == Notifications.Refresh)
                {
                        GetRecruiters();
                }
            });

            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (message.Notification == Notifications.CallRptJobNumCleared)
                {
                    CallRptJobNum = null;
                }
            });


        }

        private bool _canRefresh;
        public Boolean CanRefresh
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }


        #region reporting variables
        public  CallRptDateRange CallRptDateRange = new CallRptDateRange();
        #endregion

        #region CallSummaries

        private ObservableCollection<ByRecruitersForJob> _jobRecruiters = new ObservableCollection<ByRecruitersForJob>();

        public string CallRptJobNum
        {
            get { return _callRptJobNum; }
            set { 
                _callRptJobNum = value; 
                  NotifyPropertyChanged();
            }
        }


        private string _headingText;
        public string HeadingText
        {
            get
            {
                return _headingText;
            }
            set
            {
                _headingText = value;
                NotifyPropertyChanged();
            }
        }


        public ObservableCollection<ByRecruitersForJob> JobRecruiters
        {
            get
            {
                return _jobRecruiters;
            }
            set
            {
                _jobRecruiters = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("JobHeadingText");

            }
        }

        private ByRecruitersForJob  _selectedRecruiter;
        public ByRecruitersForJob SelectedRecruiter
        {
            get { return _selectedRecruiter; }
            set {
                _selectedRecruiter = value;
                NotifyPropertyChanged();
                if (value != null)
                    Messenger.Default.Send(new NotificationMessage<string>(this, SelectedRecruiter.recruiterSip,Notifications.CallRptRecruiterSet));
                else
                    Messenger.Default.Send(new NotificationMessage(this, Notifications.CallRptRecruiterCleared));
            }
        }
        private string _callRptJobNum;
        protected override void RefreshAll (object sender, EventArgs e)
        {
            if (CanRefresh)
            {
                var sr = SelectedRecruiter;
                GetRecruiters();
                if (sr != null)
                {
                    SelectedRecruiter = JobRecruiters.First(x => x.recruiterSip == sr.recruiterSip);
                }
            }
        }

        public async void GetRecruiters()
        {
            ShowGridData = false;
            HeadingText = "";
            if ((CallRptDateRange != null) && (_callRptJobNum != null))
            {
                HeadingText = "Loading...";
                try
                {
                    var ro = await LyncCallLogSvc.GetLynRecruitersForJob(_callRptJobNum, CallRptDateRange.StartRptDate,
                                CallRptDateRange.EndRptDate);
                    if (ro.Count > 0)
                    {
                        ShowGridData = true;
                        JobRecruiters = new ObservableCollection<ByRecruitersForJob>(ro);
                        HeadingText = String.Format("Job {0}-{1}", CallRptJobNum.Substring(0, 4), CallRptJobNum.Substring(4, 4));
                    }
                    LoadedOk = true;
                }
                catch (Exception e)
                {
                    LoadFailed(e);
                }
            }
        }

        #endregion
    }
    }