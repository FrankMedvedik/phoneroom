using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Lync.Model;
using PhoneLogic.Core.MVVMMessenger;

namespace PhoneLogic.MyCalls
{
    public partial class MainPage : UserControl
    {
         
        public MainPage()
        {
            InitializeComponent();
            MyCalls.RecruiterSip = LyncClient.GetClient().Self.Contact.Uri;

            Messenger.Default.Register<AppColors>
          (
              this, SetColors

          );
        }


        private void SetColors(AppColors ac)
        {
            LayoutRoot.Background = ac.TheBackground;

        }

    }
}
