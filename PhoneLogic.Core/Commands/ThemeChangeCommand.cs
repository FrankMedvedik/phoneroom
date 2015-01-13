using System;
using System.Windows;
using System.Windows.Controls.Theming;
using System.Windows.Input;



namespace PhoneLogic.Core.Commands
{
    public class ThemeChangeCommand : ICommand
    {
        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var themeContainer =
                  (Theme)((FrameworkElement)Application.Current.RootVisual).FindName("ThemeContainer");

            var themeName = parameter as string;


            if (themeName == null)
            {
                themeContainer.ThemeUri = null;
            }
            else
            {
                themeContainer.ThemeUri =
                     new Uri("/System.Windows.Controls.Theming." + themeName +
                     ";component/Theme.xaml", UriKind.RelativeOrAbsolute);
            }

            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }

        #endregion
    }
}