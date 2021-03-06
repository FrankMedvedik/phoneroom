﻿using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneLogic.Core.Services
{
    public static class DefaultButtonSvc
    {
        public static DependencyProperty DefaultButtonProperty =
            DependencyProperty.RegisterAttached("DefaultButton",
                typeof(Button),
                typeof(DefaultButtonSvc),
                new PropertyMetadata(null, DefaultButtonChanged));

        private static void DefaultButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = d as UIElement;
            var button = e.NewValue as Button;
            if (uiElement != null && button != null)
            {
                uiElement.KeyUp += (sender, arg) =>
                {
                    var peer = new ButtonAutomationPeer(button);

                    if (arg.Key == Key.Enter)
                    {
                        peer.SetFocus();
                        uiElement.Dispatcher.BeginInvoke(delegate
                        {
                            var invokeProv =
                                peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                            if (invokeProv != null)
                                invokeProv.Invoke();
                        });
                    }
                };
            }
        }

        public static Button GetDefaultButton(UIElement obj)
        {
            return (Button) obj.GetValue(DefaultButtonProperty);
        }

        public static void SetDefaultButton(DependencyObject obj, Button button)
        {
            obj.SetValue(DefaultButtonProperty, button);
        }
    }
}