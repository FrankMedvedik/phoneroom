using PhoneLogic.Core.MVVM_Base_Types;
using System.Collections.ObjectModel;
using System;
using PhoneLogic.Core.Services;
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

                return ColorMapping.GetBackground(callCnt);
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

                return ColorMapping.GetForeground(callCnt);
            }
        }
        #endregion




    }
}
