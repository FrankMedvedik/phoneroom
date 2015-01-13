using System;
using System.Collections.ObjectModel;
using PhoneLogic.Core.MVVM_Base_Types;
using PhoneLogic.Model;


namespace PhoneLogic.Core.ViewModels
{
    public class RecruitersOnlineViewModel : CollectionViewModelBase
    {
        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetRecruiters(); 
        }

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