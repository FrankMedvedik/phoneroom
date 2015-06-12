using System;
using System.Collections.ObjectModel;
using System.Linq;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Core.areas.recruiter
{
    public class RecruiterRptViewModel : CollectionViewModelBase
    {
        public RecruiterRptViewModel()
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

        private String _recruiterSip;
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

        #region RecruiterLogs

        private ObservableCollection<RecruiterLog> _recruiterLogs = new ObservableCollection<RecruiterLog>();

        public ObservableCollection<RecruiterLog> RecruiterLogs
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
                        recruiterCallDuration = new TimeSpan(j.Sum(c => c.tsrecruiterCallDuration.Ticks)),
                        totalCallDuration  = new TimeSpan(j.Sum(c => c.tstotalCallDuration.Ticks)),
                    }).ToList<RecruiterLogJobSummary>();

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

        private RecruiterLog  _selectedRecruiterLog;
        public RecruiterLog SelectedRecruiterLog
        { 
            get
            {
                return _selectedRecruiterLog;
            }
            set
            {
                _selectedRecruiterLog = value;
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
                    var ro = await RecruiterSvc.GetRecruiterLog(RecruiterSip, StartRptDate, EndRptDate);
                    var r = new ObservableCollection<RecruiterLog>(ro);
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