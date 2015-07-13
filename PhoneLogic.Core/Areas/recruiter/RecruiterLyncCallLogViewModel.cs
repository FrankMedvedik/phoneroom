using System;
using System.Collections.ObjectModel;
using System.Linq;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Core.areas.recruiter
{
    public class CallViewModel : CollectionViewModelBase
    {
        public CallViewModel()
        {
    
        }

        #region reporting variables

        private DateTime _startRptDate = DateTime.Now.AddDays(-1);
        public DateTime StartRptDate
        {
            get { return _startRptDate; }
            set
            {
                _startRptDate = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime _endRptDate = DateTime.Now;
        public DateTime EndRptDate
        {
            get { return _endRptDate; }
            set
            {
                _endRptDate = value;
                NotifyPropertyChanged();
            }
        }


        #endregion


        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetRecruiterLogs();
        }


        #region Recruiter

        private String _recruiterSip = "sip:kmolina@reckner.com";
        public String RecruiterSip
        {
            get
            {
                return _recruiterSip;
            }
            set
            {
                _recruiterSip = value;
                ShowSelectedRecruiter = (_recruiterSip != null);
                NotifyPropertyChanged();
                GetRecruiterLogs();
            }
        }
        #endregion

        #region Calls

        private ObservableCollection<Call> _recruiterLogs = new ObservableCollection<Call>();

        public ObservableCollection<Call> RecruiterLogs
        {
            get
            {
                return _recruiterLogs;
            }
            set
            {
                _recruiterLogs = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("RecruiterJobTotals");
            }
        }


        public ObservableCollection<RecruiterLogJobSummary> RecruiterJobTotals
        {
            get
            {
                var result = RecruiterLogs.GroupBy(l => l.JobNumber)
                .Select( j => new RecruiterLogJobSummary
                    {
                        JobNumber = j.First().JobNumber,
                        recruiterCallDuration = new TimeSpan(),
                         totalCallDuration = new TimeSpan()

                        //recruiterCallDuration = new TimeSpan(j.Sum(c => (c.CallEndTime - c.CallStartTime)),
                        //totalCallDuration  = new TimeSpan(j.Sum(c => c.tsTotalCallDuration.Ticks)),
                    }).ToList<RecruiterLogJobSummary>().OrderBy(e => e.jobFormatted);

                    var x = new ObservableCollection<RecruiterLogJobSummary>(result);
                    return x ;
            }
        }


        private Boolean _showLogData = false;
        public Boolean ShowLogData
        {
            get
            {
                return _showLogData;
            }
            set
            {
                _showLogData = value;
                NotifyPropertyChanged();
            }
        }
        private Boolean _showSelectedRecruiter = false;
        public Boolean ShowSelectedRecruiter
        {
            get
            {
                return _showSelectedRecruiter;
            }
            set
            {
                _showSelectedRecruiter = value;
                NotifyPropertyChanged();
            }
        }

        private Call  _selectedRecruiter;
        public Call SelectedRecruiter
        { 
            get
            {
                return _selectedRecruiter;
            }
            set
            {
                _selectedRecruiter = value;
                NotifyPropertyChanged();
            }
        }
        public async void GetRecruiterLogs()
        {
            if (RecruiterSip == null)
            {
                ShowLogData = false;
                LoadedOk = true;
            }
            else
                try
                {
                    var ro = await LyncCallLogSvc.GetCall(RecruiterSip, StartRptDate, EndRptDate);
                    var r = new ObservableCollection<Call>(ro.OrderBy(e => e.CallStartTime));
                    if (ro.Count > 0)
                    {
                        ShowLogData = true;
                        RecruiterLogs = r;
                    }
                    else
                        ShowLogData = false;
                    LoadedOk = true;
                }
                catch (Exception e)
                {
                    LoadFailed(e);
                }
        }
        #endregion
    }
}