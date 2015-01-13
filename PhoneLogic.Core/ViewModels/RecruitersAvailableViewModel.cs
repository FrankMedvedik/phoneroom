using System;
using System.Collections.ObjectModel;
using PhoneLogic.Core.MVVM_Base_Types;
//using PhoneLogicSilverlightClient.PLSrv;
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
    public class RecruitersAvailableViewModel : CollectionViewModelBase
    {
        /* test data */
      
        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetRecruiters();
        }

        /// <summary>
        /// Initializes a new instance of the RecruiteresOnline class.
        /// </summary>
        public RecruitersAvailableViewModel()
        {
            StartAutoRefresh(ApiRefreshFrequency.Rpts);
            GetRecruiters();
        }

        private ObservableCollection<Recruiter> _recruiters = new ObservableCollection<Recruiter>();

        public ObservableCollection<Recruiter> RecruitersAvailable
        {
            get { return _recruiters; }
            set
            {
                _recruiters = value;
                NotifyPropertyChanged();
            }
        }
     
        private async void GetRecruiters()
        {
            try
            {
                var ro = await LyncSvc.GetAvailableRecruiters();
                var r = new ObservableCollection<Recruiter>();
                if (ro.Count > 0)
                {
                    ShowGridData = true;
                    foreach (var c in ro)
                    {
                        r.Add(new Recruiter()
                        {
                            //sip = c
                            //?????????????????????
                        });
                    }
                    RecruitersAvailable = r;
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