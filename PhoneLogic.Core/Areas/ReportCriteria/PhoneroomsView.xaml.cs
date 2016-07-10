using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Areas.ReportCriteria
{
    /// <summary>
    /// Description for PhoneroomsView.
    /// </summary>
    public partial class PhoneroomsView : UserControl
    {
        private PhoneroomsViewModel _vm = null;

        /// <summary>
        /// Initializes a new instance of the PhoneroomsView class.
        /// </summary>
        public PhoneroomsView()
        {
            InitializeComponent();
            _vm = new PhoneroomsViewModel();
            DataContext = _vm;
        }


        public string SelectedPhoneroom
        {
            get { return _vm.SelectedPhoneRoomName; }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedPhoneroomProperty =
            DependencyProperty.Register("SelectedPhoneroom", typeof(string), typeof(PhoneroomsView),
                new PropertyMetadata(""));


        public List<Recruiter> PhoneroomRecruiters
        {
            get { return _vm.FilteredRecruiters.ToList(); }
        }


        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PhoneroomRecruitersProperty =
            DependencyProperty.Register("PhoneroomRecruiters", typeof(List<Recruiter>), typeof(PhoneroomsView),
                new PropertyMetadata(new List<Recruiter>()));


        public List<PhoneLogicTask> PhoneroomJobs
        {
            get { return _vm.FilteredJobs.ToList(); }
        }


        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PhoneroomJobsProperty =
            DependencyProperty.Register("PhoneroomJobs", typeof(List<PhoneLogicTask>), typeof(PhoneroomsView),
                new PropertyMetadata(new List<PhoneLogicTask>()));

        
    }
}