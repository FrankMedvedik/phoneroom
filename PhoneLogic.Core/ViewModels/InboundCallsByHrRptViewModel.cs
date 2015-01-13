using PhoneLogic.Core.MVVM_Base_Types;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;
using System.Collections.ObjectModel;
using System;


namespace PhoneLogic.Core.ViewModels
{
    public class InboundCallsByHrRptViewModel : CollectionViewModelBase
    {

        public InboundCallsByHrRptViewModel()
        {
            //StartAutoRefresh(ApiRefreshFrequency.Rpts);
            GetRpt();
        }

        public async void GetRpt()
        {

            LoadDate = DateTime.Now;
            try
            {
                var jr = await RptSvc.GetInboundCallsByHour(StartRptDate, EndRptDate);
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



        private ObservableCollection<RptInboundCallByHour> _rpt = new ObservableCollection<RptInboundCallByHour>();

        public ObservableCollection<RptInboundCallByHour> Rpt
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
