using System;
using System.Collections.ObjectModel;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Core.areas.phoneroom
{
    public class TodaysCallLogRptViewModel : CollectionViewModelBase
    {

        public TodaysCallLogRptViewModel()
        {
            StartAutoRefresh(ApiRefreshFrequency.Rpts);
            GetRpt();
        }

        public async void GetRpt()
        {

            LoadDate = DateTime.Now;
            try
            {
                var jr = await RptSvc.GetTodaysCallLogSummary();
                Rpt.Clear();
                if (jr.Count > 0)
                {
                    foreach (var j in jr)
                        Rpt.Add(j);
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

        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetRpt();
        }

        #region Rpt


        private ObservableCollection<RptTodayCallLogSummary> _rpt = new ObservableCollection<RptTodayCallLogSummary>();

        public ObservableCollection<RptTodayCallLogSummary> Rpt
        {
            get { return _rpt; }
            set
            {
                _rpt = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
    }
}
