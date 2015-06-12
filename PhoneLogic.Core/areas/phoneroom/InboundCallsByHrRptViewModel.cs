using System;
using System.Collections.ObjectModel;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Core.areas.phoneroom
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
                Rpt = new ObservableCollection<RptInboundCallByHour>(jr);
                GetAllRptsSeries();
                ShowGridData = (jr.Count > 0); 
                LoadedOk = true;

            }
            catch (Exception e)
            {
                LoadFailed(e);

            }
        }

        //public List<DataPoint> Series1
        //{
        //    get
        //    {
        //        var a = ChartSeriesCollection[0].Elements; 
        //        return a;
        //    }
        //}

        private ObservableCollection<RptSeries> _chartSeriesCollection = new ObservableCollection<RptSeries>();
        public ObservableCollection<RptSeries> ChartSeriesCollection 
        {
            get { return _chartSeriesCollection;; }
            set
            {
                _chartSeriesCollection = value;
                NotifyPropertyChanged();
            }
        }

        private void GetAllRptsSeries()
        {
            var rs = new ObservableCollection<RptSeries>();
            foreach (var r in Rpt)
            {
                var a = new RptSeries(){ Name = r.WorkDate };
                a.Elements.Add(new DataPoint()
                {
                    IndependentValue = "<9am",
                    DependentValue = r.Before_9am.GetValueOrDefault()
                });
                a.Elements.Add(new DataPoint()
                {
                    IndependentValue = "9",
                    DependentValue = r.C9_am.GetValueOrDefault()
                });
                a.Elements.Add(new DataPoint()
                {
                    IndependentValue = "10",
                    DependentValue = r.C10_am.GetValueOrDefault()
                });
                a.Elements.Add(new DataPoint()
                {
                    IndependentValue = "11",
                    DependentValue = r.C11_am.GetValueOrDefault()
                });
                a.Elements.Add(new DataPoint()
                {
                    IndependentValue = "12pm",
                    DependentValue = r.C12_pm.GetValueOrDefault()
                });
                a.Elements.Add(new DataPoint()
                {
                    IndependentValue = "1",
                    DependentValue = r.C1_pm.GetValueOrDefault()
                });
                a.Elements.Add(new DataPoint()
                {
                    IndependentValue = "2",
                    DependentValue = r.C2_pm.GetValueOrDefault()
                });
                a.Elements.Add(new DataPoint()
                {
                    IndependentValue = "3",
                    DependentValue = r.C3_pm.GetValueOrDefault()
                });
                a.Elements.Add(new DataPoint()
                {
                    IndependentValue = "4",
                    DependentValue = r.C4_pm.GetValueOrDefault()
                });
                a.Elements.Add(new DataPoint()
                {
                    IndependentValue = "5",
                    DependentValue = r.C5_pm.GetValueOrDefault()
                });
                a.Elements.Add(new DataPoint()
                {
                    IndependentValue = "6",
                    DependentValue = r.C6_pm.GetValueOrDefault()
                });
                a.Elements.Add(new DataPoint()
                {
                    IndependentValue = "7",
                    DependentValue = r.C7_pm.GetValueOrDefault()
                });
                a.Elements.Add(new DataPoint()
                {
                    IndependentValue = "8",
                    DependentValue = r.C8_pm.GetValueOrDefault()
                });
                a.Elements.Add(new DataPoint()
                {
                    IndependentValue = "9",
                    DependentValue = r.C9_pm.GetValueOrDefault()
                });
                a.Elements.Add(new DataPoint()
                {
                    IndependentValue = "10",
                    DependentValue = r.C10_pm.GetValueOrDefault()
                });
                a.Elements.Add(new DataPoint()
                {
                    IndependentValue = ">10pm",
                    DependentValue = r.after_10pm.GetValueOrDefault()
                });

                rs.Add(a);
            }

            ChartSeriesCollection = rs;
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
