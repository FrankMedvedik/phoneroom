using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.areas.CallsRpts;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    /// <summary>
    /// Description for JobsView.
    /// </summary>
    public partial class JobsView : UserControl
    {
        private JobsViewModel _vm = null;

        public JobsView()
        {
            InitializeComponent();
            _vm = new JobsViewModel();
            DataContext = _vm;
            
        }

        private void JobsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}