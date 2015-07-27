using System;
using System.Collections.ObjectModel;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Areas.PhoneRooms
{
    public class JobCallActivityRptViewModel : CollectionViewModelBase
    {

        public JobCallActivityRptViewModel()
        {
            StartAutoRefresh(ApiRefreshFrequency.Rpts);
            GetRpt();
        }

        public async void GetRpt()
        {

            LoadDate = DateTime.Now;
            try
            {
                var jr = await RptSvc.GetJobActivityRpt(StartRptDate, EndRptDate);
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



        private ObservableCollection<RptJobActivity> _rpt = new ObservableCollection<RptJobActivity>();

        public ObservableCollection<RptJobActivity> Rpt
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
