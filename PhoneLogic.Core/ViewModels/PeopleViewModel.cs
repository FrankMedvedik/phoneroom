using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;

namespace PhoneLogic.Core.ViewModels
{
    public class PeopleViewModel : CollectionViewModelBase
    {
        public void LookupPhone(string phoneNum)
        {
            if (phoneNum != null)
            {
                var pn = Regex.Replace(phoneNum, "[^0-9.]", "");
                if (String.IsNullOrEmpty(pn)) return;
                LoadPeople(pn);
            }
        }

        private ObservableCollection<person> _people = new ObservableCollection<person>();

        public ObservableCollection<person> People
        {
            get { return _people; }
            set
            {
                _people = value;
                NotifyPropertyChanged();
            }
        }

        protected override void RefreshAll(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private person _selectedPerson;

        public person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                NotifyPropertyChanged();
            }
        }

        public async void LoadPeople(string phoneNum)
        {
            try
            {
                var d = await PeopleSvc.GetPeople(phoneNum);
                var r = new ObservableCollection<person>();

                foreach (var p in d)
                    r.Add(p);
                People = r;
                DataStatusHeadingMsg = "Possible Matches";
                ShowGridData = true;
                LoadedOk = true;
            }
            catch (Exception e)
            {
                DataStatusMsg = "No Matches Found";
                LoadFailed(e);
            }
        }
    }
}