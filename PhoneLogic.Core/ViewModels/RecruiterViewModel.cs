using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using PhoneLogic.Core.MVVM_Base_Types;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;


namespace PhoneLogic.Core.ViewModels
{
    public class RecruiterViewModel : CollectionViewModelBase
    {
        public RecruiterViewModel()
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