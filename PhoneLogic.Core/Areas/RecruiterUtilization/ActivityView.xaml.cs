using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Lync.Model;
using PhoneLogic.Core.Areas.CallsRpts;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Behaviors;
using PhoneLogic.Core.Helpers;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Areas.RecruiterUtilization
{
    public partial class ActivityView : UserControl
    {
        private ActivityViewModel _vm = null;

        public ActivityView()
        {
            InitializeComponent();
            _vm = new ActivityViewModel();
            DataContext = _vm;
            ExcelBehavior.EnableForGrid(ActivitiesDG);
        }


        public string sip
        {
            get { return _vm.sip; }
            set
            {
                SetValue(sipProperty, value);
                _vm.sip = value;
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty sipProperty =
            DependencyProperty.Register("sip", typeof(string), typeof(ActivityView), new PropertyMetadata(string.Empty));

        public List<RecruiterActivity> RecruiterActivities
        {
            get { return _vm.RecruiterActivities.ToList(); }
            set
            {
                SetValue(RecruiterActivitiesProperty, value);
                _vm.RecruiterActivities = new ObservableCollection<RecruiterActivity>(value);
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RecruiterActivitiesProperty =
            DependencyProperty.Register("RecruiterActivities", typeof(List<RecruiterActivity>), typeof(ActivityView),
                new PropertyMetadata(new List<RecruiterActivity>()));

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                ActivitiesDG.Export("RecruiterActivities");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving file - " + ex.Message);
            }
        }
    }
}