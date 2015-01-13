using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace PhoneLogic.Core.Views
{
    [ContentProperty("CustomContent")]
    public partial class CollectionFrame : UserControl
    {
        public CollectionFrame()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Title Property
        /// </summary>
        public string PortletTitle
        {
            get
            {
                return (string)GetValue(PortletTitleProperty);
            }
            set
            {
                SetValue(PortletTitleProperty, value);
            }
        }

        /// <summary>
        /// PortletIcon Property
        /// </summary>
        public object PortletIcon
        {
            get
            {
                return (FrameworkElement)GetValue(PortletIconProperty);
            }
            set
            {
                SetValue(PortletIconProperty, value);
            }
        }

        /// <summary>
        /// CustomContent Property
        /// </summary>
        public object CustomContent
        {
            get
            {
                return (FrameworkElement)GetValue(CustomContentProperty);
            }
            set
            {
                SetValue(CustomContentProperty, value);
            }
        }

        /// <summary>
        /// Title Dependency Property.
        /// </summary>
        public static readonly DependencyProperty PortletTitleProperty =
            DependencyProperty.Register("Title",
                                        typeof(string),
                                        typeof(CollectionFrame),
                                        new PropertyMetadata("Title"));

        /// <summary>
        /// PortletIcon Dependency Property.
        /// </summary>
        public static readonly DependencyProperty PortletIconProperty =
            DependencyProperty.Register("PortletIcon",
                                        typeof(object),
                                        typeof(CollectionFrame),
                                        new PropertyMetadata(null));


        /// <summary>
        /// CustomContent Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CustomContentProperty =
            DependencyProperty.Register("CustomContent",
                                        typeof(object),
                                        typeof(CollectionFrame),
                                        new PropertyMetadata(null));


    }
}
