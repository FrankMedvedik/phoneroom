using System;
using System.Collections.Generic;
using Microsoft.Lync.Model;
namespace PhoneLogic.Core.Services
{
    public class LyncSearchSvc
    {

        
        public static List<String> SearchForContacts(string searchKey)
        {
                List<String> contacts = new List<string>();
             
                LyncClient.GetClient().ContactManager.BeginSearch(searchKey,
                (ar) =>
                {
                    var searchResults = LyncClient.GetClient().ContactManager.EndSearch(ar);
                    foreach (Contact contact in searchResults.Contacts)
                    {
                        contacts.Add(contact.GetContactInformation(ContactInformationType.DisplayName).ToString());
                    }
                },
                null);
            return contacts;
        }
    }
}
