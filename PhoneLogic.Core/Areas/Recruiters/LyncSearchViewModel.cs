using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using PhoneLogic.Core.ViewModels;
using Silverlight.Base.MVVMBaseTypes;

namespace PhoneLogic.Core.Areas.Recruiters
{
    public class lsContact : ViewModelBase
    {
        private Contact _contact;

        public lsContact()
        {
            //Contact.ContactInformationChanged += _Contact_OnInformationChanged;
        }

        public Contact Contact
        {
            get { return _contact; }
            set
            {
                _contact = value;
                NotifyPropertyChanged();
            }
        }

        private Boolean _canCall;

        public Boolean CanCall
        {
            get { return _canCall; }
            set
            {
                _canCall = value;
                NotifyPropertyChanged();
            }
        }

        void _Contact_OnInformationChanged(Object source, ContactInformationChangedEventArgs data)
        {
            if (data.ChangedContactInformation.Contains(ContactInformationType.Availability))
            {
                CanCall = ((ContactAvailability) _contact.GetContactInformation(ContactInformationType.Availability) ==
                           ContactAvailability.Free);
            }
        }
    }

    public class LyncSearchViewModel : CollectionViewModelBase
    {
        private ObservableCollection<lsContact> _contacts = new ObservableCollection<lsContact>();

        public ObservableCollection<lsContact> Contacts
        {
            get { return _contacts; }
            set
            {
                _contacts = value;
                NotifyPropertyChanged();
                ShowGridData = (Contacts.Count > 0);
            }
        }

        private lsContact _selectedContact;

        public lsContact SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                NotifyPropertyChanged();
            }
        }

        private Boolean _showGridData;

        public Boolean ShowGridData
        {
            get { return _showGridData; }
            set
            {
                _showGridData = value;
                NotifyPropertyChanged();
            }
        }

        public void SearchForContacts(string searchKey)
        {
            LyncClient.GetClient().ContactManager.BeginSearch(searchKey,
                (ar) =>
                {
                    var contacts = new ObservableCollection<lsContact>();
                    var searchResults = LyncClient.GetClient().ContactManager.EndSearch(ar);
                    foreach (Contact contact in searchResults.Contacts)
                    {
                        contacts.Add(new lsContact {Contact = contact});
                    }
                    Contacts = contacts;
                },
                null);
        }

        internal void ClearSearch()
        {
            Contacts = new ObservableCollection<lsContact>();
        }

        protected override void RefreshAll(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}