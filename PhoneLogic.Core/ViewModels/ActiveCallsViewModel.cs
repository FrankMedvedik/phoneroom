using PhoneLogic.Core.MVVM_Base_Types;
using System.Collections.ObjectModel;
using System;
using QueueSummary = PhoneLogic.Model.QueueSummary;
using PhoneLogic.Model;

namespace PhoneLogic.Core.ViewModels
{
    public class ActiveCallsViewModel : CollectionViewModelBase
    {

        #region ActiveCalls

        ObservableCollection<ActiveCallDetail> _activeCalls;

        public ObservableCollection<ActiveCallDetail> ActiveCalls
        {
            get { return _activeCalls; }
            set
            {
                _activeCalls = value;
                NotifyPropertyChanged();
            }
        }

        #endregion


        // sets up the 
        public ActiveCallsViewModel()
        {
            StartAutoRefresh(ApiRefreshFrequency.LyncApi);
            RefreshAll();

        }

        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetActiveCalls();
        }


        #region SelectedActiveCall

        private Recruiter _selectedActiveCall;
        public Recruiter SelectedActiveCall
        {
            get
            {
                return _selectedActiveCall;
            }
            set
            {
                _selectedActiveCall = value;
                ShowSelectedActiveCall = (_selectedActiveCall != null);
                NotifyPropertyChanged();
            }
        }
        
        private Boolean _showSelectedActiveCall = false;
        public Boolean ShowSelectedActiveCall
        {
            get
            {
                return _showSelectedActiveCall;
            }
            set
            {
                _showSelectedActiveCall = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        public async void GetActiveCalls()
        {
            try
            {
                LoadDate = DateTime.Now;
                var cq = await LyncSvc.GetActiveCallsDetail();
                if (cq.Count > 0)
                {
                    ActiveCalls = cq;
                    ShowGridData = true;
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
