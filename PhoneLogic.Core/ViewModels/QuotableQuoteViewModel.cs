using PhoneLogic.Core.MVVM_Base_Types;
using System.Collections.ObjectModel;
using System;
using PhoneLogic.Core.Services;
using QueueSummary = PhoneLogic.Model.QueueSummary;
using PhoneLogic.Model;

namespace PhoneLogic.Core.ViewModels
{
    public class QuotableQuoteViewModel : CollectionViewModelBase
    {

        #region QuotableQuotes

        ObservableCollection<QuotableQuote> _quotableQuotes;

        public ObservableCollection<QuotableQuote> QuotableQuotes
        {
            get { return _quotableQuotes; }
            set
            {
                if (_quotableQuotes != value)
                {
                    _quotableQuotes = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        // sets up the 
        public QuotableQuoteViewModel()
        {
            StartAutoRefresh(36000);
            RefreshAll();

        }

        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetQuotableQuotes();
        }

        
        public async void GetQuotableQuotes()
        {
            try
            {
                LoadDate = DateTime.Now;
                var cq = await QuoteSrv.GetQuote();
                QuotableQuotes.Clear();
                QuotableQuotes.Add(cq);
                ShowGridData = true;
                LoadedOk = true;
            }
            catch (Exception e)
            {
                LoadFailed(e);
            }
        }
 


    }
}
