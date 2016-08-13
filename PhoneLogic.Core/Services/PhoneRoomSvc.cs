using System.Collections.ObjectModel;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Services
{
    public static class PhoneRoomSvc
    {
        public static ObservableCollection<PhoneRoom> GetAll()
        {
            return new ObservableCollection<PhoneRoom>
            {
                new PhoneRoom {Code = "CHL", Name = "Chalfont"},
                new PhoneRoom {Code = "MKE", Name = "Milwaukee"},
                new PhoneRoom {Code = "All", Name = "All"}
            };
        }

        public static PhoneRoom GetDefault()
        {
            return new PhoneRoom {Code = "All", Name = "All"};
        }
    }
}