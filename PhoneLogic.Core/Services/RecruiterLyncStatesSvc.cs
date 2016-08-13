using System.Collections.ObjectModel;

namespace PhoneLogic.Core.Services
{
    public static class RecruiterLyncStatesSvc
    {
        public static string AvailableRecruiters = "Available";
        public static string ActiveRecruiters = "Active";
        public static string AllRecruiters = "All";

        public static ObservableCollection<string> GetAll()
        {
            return new ObservableCollection<string>
            {
                AvailableRecruiters,
                ActiveRecruiters,
                AllRecruiters
            };
        }

        public static string GetDefault()
        {
            return ActiveRecruiters;
        }
    }
}