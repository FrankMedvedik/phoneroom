﻿using System.Windows.Controls;

namespace PhoneLogic.Core.Areas.PhoneRooms
{
    public partial class ActiveCallsView : UserControl
    {
        private readonly ActiveCallsViewModel _vm;

        public ActiveCallsView()
        {
            InitializeComponent();
            _vm = new ActiveCallsViewModel();
            DataContext = _vm;
        }
    }
}