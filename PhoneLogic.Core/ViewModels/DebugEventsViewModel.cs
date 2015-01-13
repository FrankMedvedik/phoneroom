using System.Collections.ObjectModel;

namespace PhoneLogic.Core.ViewModels
{
    public class DebugEventsViewModel : ViewModelBase
    {
        private ObservableCollection<string> _debugEvents = new ObservableCollection<string>();

        public ObservableCollection<string> DebugEvents
        {
            get { return _debugEvents; }
            set
            {
                _debugEvents = value;
                this.NotifyPropertyChanged();
            }
        }

    }
}