﻿using System.Windows.Controls;

namespace PhoneLogic.Core.Areas.PhoneRooms
{
    public partial class AllRecruitersRptView : UserControl
    {
        private AllRecruitersViewModel _vm = null;
        public AllRecruitersRptView()
        {
            InitializeComponent();
            _vm = new AllRecruitersViewModel();
            DataContext = _vm;
        }

      

        //private void RecruiterDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
            
        //    if (_vm.SelectedRecruiter != null)
        //    {
        //        RecDtl.RecruiterSip = _vm.SelectedRecruiter.sip;
        //        _vm.GetRecruiterLogs();
        //    }
        //    else
        //        RecDtl.RecruiterSip = null;

        //}

        private void RecruiterDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}