using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace PhoneLogic.Core.Behaviors
{
    public class DefaultButtonBehavior : Behavior<System.Windows.Controls.Control>
    {

        #region Methods

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.KeyDown += PerformKeyDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.KeyDown -= PerformKeyDown;
        }

        private void PerformKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrEmpty(this.ButtonName))
            {
                var myRoot = VisualTreeHelper.GetParent(sender as System.Windows.Controls.Control);
                //var root = App.Current.RootVisual;
                var b = ((FrameworkElement)myRoot).FindName(this.ButtonName);
                if (b is Button)
                {
                    Button button = b as Button;
                    if (button != null && button.IsEnabled)
                    {
                        if (sender is TextBox)
                        {
                            TextBox textBox = sender as TextBox;
                            ButtonAutomationPeer peer = new ButtonAutomationPeer(button);
                            IInvokeProvider invokeProvider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                            if (invokeProvider != null)
                            {
                                invokeProvider.Invoke();
                            }
                        }
                        else if (sender is PasswordBox)
                        {
                            PasswordBox pwdBox = sender as PasswordBox;
                            ButtonAutomationPeer peer = new ButtonAutomationPeer(button);
                            IInvokeProvider invokeProvider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                            if (invokeProvider != null)
                            {
                                invokeProvider.Invoke();
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Properties

        public string ButtonName { get; set; }

        #endregion

    }
}
