using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using Microsoft.Lync.Model.Extensibility;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Views
{
    public partial class QuotableQuoteView : UserControl
    {
        private QuotableQuoteViewModel _vm;

        public QuotableQuoteView()
        {
            _vm = new QuotableQuoteViewModel();
            DataContext = _vm;
            InitializeComponent();
        }
    }
}