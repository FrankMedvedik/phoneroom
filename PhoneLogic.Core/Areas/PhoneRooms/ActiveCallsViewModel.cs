using System;
using System.Collections.ObjectModel;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Areas.PhoneRooms
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
                if (_activeCalls != value)
                {
                    _activeCalls = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("Background");
                    NotifyPropertyChanged("Foreground");
                }
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

        private ActiveCallDetail _selectedActiveCall;
        public ActiveCallDetail SelectedActiveCall
        {
            get
            {
                return _selectedActiveCall;
            }
            set
            {
                _selectedActiveCall = value;
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
        #region DisplayColors




        public string TheBackground
        {
            get
            {
                var callCnt = 0;
                try
                {
                    callCnt = ActiveCalls.Count;
                }
                catch (Exception e)
                {
                    callCnt = 0;
                }

                return ColorMappingSvc.GetBackground(callCnt);
            }
        }

        public string TheForeground
        {
            get {
                var callCnt = 0;
                try
                {
                    callCnt = ActiveCalls.Count;
                }
                catch (Exception e)
                {
                    callCnt = 0;
                }

                return ColorMappingSvc.GetForeground(callCnt);
            }
        }
        #endregion




    }
}
