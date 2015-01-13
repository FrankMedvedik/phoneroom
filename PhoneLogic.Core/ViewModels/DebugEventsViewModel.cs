using System.Collections.ObjectModel;

namespace PhoneLogic.Core.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class DebugEventsViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the DebugEventsViewModel class.
        /// </summary>
        public DebugEventsViewModel()
        {
        }

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