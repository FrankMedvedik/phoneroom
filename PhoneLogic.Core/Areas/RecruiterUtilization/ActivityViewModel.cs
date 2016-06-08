using System.Collections.Generic;
using System.Collections.ObjectModel;
using PhoneLogic.Model;
using Silverlight.Base.MVVMBaseTypes;

namespace PhoneLogic.Core.Areas.RecruiterUtilization
{
    public class ActivityViewModel : ViewModelBase
    {
        private string _headingText;

        public string HeadingText
        {
            get { return _headingText; }
            set
            {
                _headingText = value;
                NotifyPropertyChanged();
            }
        }

        private string _sip;

        public string sip
        {
            get { return _sip; }
            set
            {
                _sip = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<RecruiterActivity> _recruiterActivities;

        public ObservableCollection<RecruiterActivity> RecruiterActivities
        {
            get { return _recruiterActivities; }
            set
            {
                _recruiterActivities = value;
                NotifyPropertyChanged();
            }
        }
    }
}