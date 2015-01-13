using System.Collections.ObjectModel;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Services
{
    public static class LyncStatusRangeSvc
    {
        public static ObservableCollection<LyncStatusRange> GetAll()
        {
            return new ObservableCollection<LyncStatusRange>
            {
                new LyncStatusRange {Name = "Available", MinValue = 3000, MaxValue = 5999},
                new LyncStatusRange {Name = "Online", MinValue = 1, MaxValue = 8999},
                new LyncStatusRange {Name = "All", MinValue = 0, MaxValue = 15501}
            };
        }

        public static LyncStatusRange GetDefault()
        {
           return new LyncStatusRange {Name = "All", MinValue = 0, MaxValue = 15501};
        }
    }
}
