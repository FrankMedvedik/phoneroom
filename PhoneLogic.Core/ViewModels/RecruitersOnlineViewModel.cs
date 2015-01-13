using System;
using System.Collections.ObjectModel;
using PhoneLogic.Core.MVVM_Base_Types;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;


namespace PhoneLogic.Core.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class RecruitersOnlineViewModel : CollectionViewModelBase
    {
        
        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetRecruiters(); 
        }

        /// <summary>
        /// Initializes a new instance of the RecruiteresOnline class.
        /// </summary>
        public RecruitersOnlineViewModel()
        {
            StartAutoRefresh(ApiRefreshFrequency.Rpts);
            GetRecruiters(); 
        }

        private ObservableCollection<Recruiter> _recruitersOnline = new ObservableCollection<Recruiter>();

        public ObservableCollection<Recruiter> RecruitersOnline
        {
            get { return _recruitersOnline; }
            set
            {
                _recruitersOnline = value;
                NotifyPropertyChanged();
            }
        }


        private async void GetRecruiters()
        {
            try
            {
                var ro = await LyncSvc.GetOnlineRecruiters();
                var r = new ObservableCollection<Recruiter>();
                if (ro.Count > 0)
                {
                    ShowGridData = true;
                    foreach (var c in ro)
                    {
                        r.Add(new Recruiter()
                        {
                            //sip = c,
                            DisplayName = "displayName",
                            Description = "Job Title",
                            EmailAddress = "f@y.com",
                            InboundCallCnt = DateTime.Now.Millisecond,
                            OutboundCallCnt = DateTime.Now.Millisecond,
                            TotalIdleTime = DateTime.Now.Second,
                            AvgIdleTime = DateTime.Now.Second,
                            MaxIdleTime = DateTime.Now.Second 
                        });
                    }
                    RecruitersOnline = r;
                }
                else
                    ShowGridData = false;
                LoadedOk = true;
            }
            catch (Exception e)
            {
                LoadFailed(e);
            }
        }
    }
}