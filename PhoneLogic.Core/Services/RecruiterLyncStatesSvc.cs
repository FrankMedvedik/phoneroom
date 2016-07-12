using System;
using System.Collections.ObjectModel;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Services
{
    public static class RecruiterLyncStatesSvc
    {

        public static String AvailableRecruiters = "Available";
        public static String ActiveRecruiters = "Active";
        public static String AllRecruiters = "All";
        public static ObservableCollection<String> GetAll()
        {
            return new ObservableCollection<String>
            {
                AvailableRecruiters,
                ActiveRecruiters,
                AllRecruiters
            };
        }

        public static String GetDefault()
        {
            return ActiveRecruiters;
        }
    }
}
