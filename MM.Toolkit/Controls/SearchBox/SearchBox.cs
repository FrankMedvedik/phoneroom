using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Reflection;
using System.Globalization;
using System.Windows.Data;

namespace MM.Toolkit
{
    //[TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "MouseOver", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Focused", GroupName = "CommonStates")]
    public class SearchBox : Control, INotifyPropertyChanged
    {
        #region -: Events :-

        public event RoutedEventHandler SearchStarted;
        public event RoutedEventHandler SelectedItemChanged;

        #endregion

        #region -: Properties :-

        private Boolean loaded = false;
        private TextBox searchtextbox = null;
        private ListBox resultlistbox = null;
        private Popup popup = null;
        private DispatcherTimer timer;
        private DataTemplate defaultcontenttemplate = null;
        private DataTemplate tempnullcontenttemplate = null;

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);
                NotifyPropertyChanged("ItemsSource");
            }
        }

        public Object SelectedItem
        {
            get { return (Object)GetValue(SelectedItemProperty); }
            set
            {
                SetValue(SelectedItemProperty, value);
                NotifyPropertyChanged("SelectedItem");
            }
        }

        private Object InternalSelectedItem
        {
            get { return (Object)GetValue(InternalSelectedItemProperty); }
            set
            {
                SetValue(InternalSelectedItemProperty, value);
                NotifyPropertyChanged("InternalSelectedItem");
            }
        }

        public Boolean PopupIsVisible
        {
            get { return (Boolean)GetValue(PopupIsVisibleProperty); }
            set
            {
                SetValue(PopupIsVisibleProperty, value);
                NotifyPropertyChanged("PopupIsVisible");
            }
        }

        public Int32 PopupWidth
        {
            get { return (Int32)GetValue(PopupWidthProperty); }
            set
            {
                SetValue(PopupWidthProperty, value);
                NotifyPropertyChanged("PopupWidth");
            }
        }

        public Int32 PopupHeight
        {
            get { return (Int32)GetValue(PopupHeightProperty); }
            set
            {
                SetValue(PopupHeightProperty, value);
                NotifyPropertyChanged("PopupHeight");
            }
        }

        public Boolean CloseOnSelection
        {
            get { return (Boolean)GetValue(CloseOnSelectionProperty); }
            set
            {
                SetValue(CloseOnSelectionProperty, value);
                NotifyPropertyChanged("CloseOnSelection");
            }
        }

        public Object ClearContent
        {
            get { return (Object)GetValue(ClearContentProperty); }
            set
            {
                SetValue(ClearContentProperty, value);
                NotifyPropertyChanged("ClearContent");
            }
        }

        public String SearchLabelText
        {
            get { return (String)GetValue(SearchLabelTextProperty); }
            set
            {
                SetValue(SearchLabelTextProperty, value);
                NotifyPropertyChanged("SearchLabelText");
            }
        }

        public Boolean IsReadOnly
        {
            get { return (Boolean)GetValue(IsReadOnlyProperty); }
            set
            {
                SetValue(IsReadOnlyProperty, value);
                NotifyPropertyChanged("IsReadOnly");
            }
        }

        public Boolean IsFocused
        {
            get { return (Boolean)GetValue(IsFocusedProperty); }
            private set
            {
                SetValue(IsFocusedProperty, value);
                NotifyPropertyChanged("IsFocused");
            }
        }

        public Boolean IsHovering
        {
            get { return (Boolean)GetValue(IsHoveringProperty); }
            private set
            {
                SetValue(IsHoveringProperty, value);
                NotifyPropertyChanged("IsHovering");
            }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set
            {
                SetValue(ItemTemplateProperty, value);
                NotifyPropertyChanged("ItemTemplate");
            }
        }

        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set
            {
                SetValue(ContentTemplateProperty, value);
                NotifyPropertyChanged("ContentTemplate");
            }
        }

        public DataTemplate NullContentTemplate
        {
            get { return (DataTemplate)GetValue(NullContentTemplateProperty); }
            set
            {
                SetValue(NullContentTemplateProperty, value);
                NotifyPropertyChanged("NullContentTemplate");
            }
        }

        public FrameworkElement ExtendedFilter
        {
            get { return (FrameworkElement)GetValue(ExtendedFilterProperty); }
            set
            {
                SetValue(ExtendedFilterProperty, value);
                NotifyPropertyChanged("ExtendedFilter");
            }
        }

        public Object CommandParameter
        {
            get { return (Object)GetValue(CommandParameterProperty); }
            set
            {
                SetValue(CommandParameterProperty, value);
                NotifyPropertyChanged("CommandParameter");
            }
        }

        public String SearchText
        {
            get { return (String)GetValue(SearchTextProperty); }
            set
            {
                SetValue(SearchTextProperty, value);
                NotifyPropertyChanged("SearchText");
            }
        }

        public Boolean ClearContentButtonIsVisible
        {
            get { return (Boolean)GetValue(ClearContentButtonIsVisibleProperty); }
            set
            {
                SetValue(ClearContentButtonIsVisibleProperty, value);
                NotifyPropertyChanged("ClearContentButtonIsVisible");
            }
        }

        public Boolean IsSearching
        {
            get { return (Boolean)GetValue(IsSearchingProperty); }
            private set
            {
                SetValue(IsSearchingProperty, value);
                NotifyPropertyChanged("IsSearching");
            }
        }

        public IValueConverter TextConverter
        {
            get { return (IValueConverter)GetValue(TextConverterProperty); }
            set
            {
                SetValue(TextConverterProperty, value);
                NotifyPropertyChanged("TextConverter");
            }
        }

        public String TextPropertyName
        {
            get { return (String)GetValue(TextPropertyNameProperty); }
            set
            {
                SetValue(TextPropertyNameProperty, value);
                NotifyPropertyChanged("TextPropertyName");
            }
        }

        #endregion

        #region -: Properties, Commands :-

        private DelegateCommand PopupClickCommand
        {
            get { return (DelegateCommand)GetValue(PopupClickCommandProperty); }
            set
            {
                SetValue(PopupClickCommandProperty, value);
                NotifyPropertyChanged("PopupClickCommand");
            }
        }

        private DelegateCommand ClearClickCommand
        {
            get { return (DelegateCommand)GetValue(ClearClickCommandProperty); }
            set
            {
                SetValue(ClearClickCommandProperty, value);
                NotifyPropertyChanged("ClearClickCommand");
            }
        }

        public DelegateCommand SearchStartedCommand
        {
            get { return (DelegateCommand)GetValue(SearchStartedCommandProperty); }
            set
            {
                SetValue(SearchStartedCommandProperty, value);
                NotifyPropertyChanged("SearchStartedCommand");
            }
        }

        public DelegateCommand SearchCompletedCommand
        {
            get { return (DelegateCommand)GetValue(SearchCompletedCommandProperty); }
            private set
            {
                SetValue(SearchCompletedCommandProperty, value);
                NotifyPropertyChanged("SearchCompletedCommand");
            }
        }

        public DelegateCommand SelectedItemChangedCommand
        {
            get { return (DelegateCommand)GetValue(SelectedItemChangedCommandProperty); }
            private set
            {
                SetValue(SelectedItemChangedCommandProperty, value);
                NotifyPropertyChanged("SelectedItemChangedCommand");
            }
        }

        #endregion

        #region -: Dependency Properties :-

        // Public
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(SearchBox), new PropertyMetadata(null));
        public static readonly DependencyProperty InternalSelectedItemProperty = DependencyProperty.Register("InternalSelectedItem", typeof(Object), typeof(SearchBox), new PropertyMetadata(null, OnInternalSelectedItemPropertyChanged));
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(Object), typeof(SearchBox), new PropertyMetadata(null, OnSelectedItemPropertyChanged));
        public static readonly DependencyProperty PopupIsVisibleProperty = DependencyProperty.Register("PopupIsVisible", typeof(Boolean), typeof(SearchBox), new PropertyMetadata(false));
        public static readonly DependencyProperty PopupWidthProperty = DependencyProperty.Register("PopupWidth", typeof(Int32), typeof(SearchBox), new PropertyMetadata(200));
        public static readonly DependencyProperty PopupHeightProperty = DependencyProperty.Register("PopupHeight", typeof(Int32), typeof(SearchBox), new PropertyMetadata(200));
        public static readonly DependencyProperty CloseOnSelectionProperty = DependencyProperty.Register("CloseOnSelection", typeof(Boolean), typeof(SearchBox), new PropertyMetadata(true));
        public static readonly DependencyProperty ClearContentProperty = DependencyProperty.Register("ClearContent", typeof(Object), typeof(SearchBox), new PropertyMetadata("Clear result"));
        public static readonly DependencyProperty SearchLabelTextProperty = DependencyProperty.Register("SearchLabelText", typeof(String), typeof(SearchBox), new PropertyMetadata("Search"));
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(Boolean), typeof(SearchBox), new PropertyMetadata(false));
        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.Register("IsFocused", typeof(Boolean), typeof(SearchBox), new PropertyMetadata(false));
        public static readonly DependencyProperty IsHoveringProperty = DependencyProperty.Register("IsHovering", typeof(Boolean), typeof(SearchBox), new PropertyMetadata(false));
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(SearchBox), new PropertyMetadata(null));
        public static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.Register("ContentTemplate", typeof(DataTemplate), typeof(SearchBox), new PropertyMetadata(null));
        public static readonly DependencyProperty NullContentTemplateProperty = DependencyProperty.Register("NullContentTemplate", typeof(DataTemplate), typeof(SearchBox), new PropertyMetadata(null));
        public static readonly DependencyProperty ExtendedFilterProperty = DependencyProperty.RegisterAttached("ExtendedFilter", typeof(FrameworkElement), typeof(SearchBox), new PropertyMetadata(null));
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(Object), typeof(SearchBox), new PropertyMetadata(null));
        public static readonly DependencyProperty SearchTextProperty = DependencyProperty.Register("SearchText", typeof(String), typeof(SearchBox), new PropertyMetadata(null));
        public static readonly DependencyProperty IsSearchingProperty = DependencyProperty.Register("IsSearching", typeof(Boolean), typeof(SearchBox), new PropertyMetadata(false));
        public static readonly DependencyProperty ClearContentButtonIsVisibleProperty = DependencyProperty.Register("ClearContentButtonIsVisible", typeof(Boolean), typeof(SearchBox), new PropertyMetadata(true));
        public static readonly DependencyProperty TextConverterProperty = DependencyProperty.Register("TextConverter", typeof(IValueConverter), typeof(SearchBox), new PropertyMetadata(null));
        public static readonly DependencyProperty TextPropertyNameProperty = DependencyProperty.Register("TextPropertyName", typeof(String), typeof(SearchBox), new PropertyMetadata(null));
        
        // Commands
        private static readonly DependencyProperty PopupClickCommandProperty = DependencyProperty.Register("PopupClickCommand", typeof(DelegateCommand), typeof(SearchBox), new PropertyMetadata(null));
        private static readonly DependencyProperty ClearClickCommandProperty = DependencyProperty.Register("ClearClickCommand", typeof(DelegateCommand), typeof(SearchBox), new PropertyMetadata(null));

        public static readonly DependencyProperty SearchStartedCommandProperty = DependencyProperty.Register("SearchStartedCommand", typeof(DelegateCommand), typeof(SearchBox), new PropertyMetadata(null));
        public static readonly DependencyProperty SearchCompletedCommandProperty = DependencyProperty.Register("SearchCompletedCommand", typeof(DelegateCommand), typeof(SearchBox), new PropertyMetadata(null));
        public static readonly DependencyProperty SelectedItemChangedCommandProperty = DependencyProperty.Register("SelectedItemChangedCommand", typeof(DelegateCommand), typeof(SearchBox), new PropertyMetadata(null));
        
        #endregion

        #region -: Dependency Property Methods :-

        private static void OnSelectedItemPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            SearchBox ctl = sender as SearchBox;

            ctl.doSelectedItemChanged();
            ctl = null;
        }

        private static void OnInternalSelectedItemPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            SearchBox ctl = sender as SearchBox;

            ctl.doInternalSelectedItemChanged();
            ctl = null;
        }

        #endregion

        #region -: Constructors & Destructor :-
        
        public SearchBox()
        {
            this.Name = "SearchBox_" + Guid.NewGuid().ToString("N");

            setContentTemplate();

            this.Loaded += new RoutedEventHandler(SearchBox_Loaded);
            this.IsEnabledChanged += new DependencyPropertyChangedEventHandler(SearchBox_IsEnabledChanged);

            this.DefaultStyleKey = typeof(SearchBox);

            initCommands();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            timer.Tick += onTimerTick;
        }

        #endregion

        #region -: Control Overrides :-

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            popup = this.GetTemplateChild("PART_Popup") as Popup;

            if (popup != null)
            {
                popup.Opened += new EventHandler(popup_Opened);
                popup.Closed += new EventHandler(popup_Closed);

                if (popup.Child != null)
                {
                    foreach (UIElement obj in popup.Child.GetAllVisualChildren<UIElement>())
                        obj.LostFocus += new RoutedEventHandler(PopupChild_LostFocus);
                }

                resultlistbox = popup.FindName("PART_ListBox") as ListBox;
                if (resultlistbox != null)
                {
                    resultlistbox.KeyUp += new KeyEventHandler(resultlistbox_KeyUp);
                    resultlistbox.MouseLeftButtonUp += new MouseButtonEventHandler(resultlistbox_MouseLeftButtonUp);
                }

                searchtextbox = popup.FindName("PART_SearchTextBox") as TextBox;
                if (searchtextbox != null)
                    searchtextbox.KeyUp += new KeyEventHandler(searchtextbox_KeyUp);
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (!e.Handled && base.Focus())
                e.Handled = true;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.IsHovering = true;
            doUpdateStates();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.IsHovering = false;
            doUpdateStates();
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            this.IsFocused = true;
            doUpdateStates();
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            checkFocus();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            Object focusedelt = null;

            base.OnKeyDown(e);

            focusedelt = FocusManager.GetFocusedElement();

            if ((focusedelt == searchtextbox) || (focusedelt == resultlistbox))
                return;

            if ((e.Key == Key.C) && ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control))
            {
                if (this.SelectedItem != null)
                    tryCopyText(this.SelectedItem);

                return;
            }

            if (!this.IsEnabled || this.IsReadOnly || (e.Key == Key.Tab) || (e.Key == Key.Ctrl) || (e.Key == Key.Shift) || (e.Key == Key.Alt))
                return;

            if (e.Key == Key.Escape)
                this.PopupIsVisible = false;
            else if ((e.Key == Key.Back) && !this.PopupIsVisible)
                doClearClick();
            else
            {
                this.PopupIsVisible = true;

                if (searchtextbox != null)
                {
                    if ((e.Key == Key.Enter) || (e.Key == Key.Down) || (e.Key == Key.Up) || (e.Key == Key.Left) || (e.Key == Key.Right))
                        searchtextbox.Text = String.Empty;
                    else
                        searchtextbox.Text = e.Key.ToString(); // viewtextbox.Text;

                    searchtextbox.SelectAll();
                    searchtextbox.Focus();
                }
            }
        }

        #endregion

        #region -: Control Events :-

        private void SearchBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (loaded)
                return;

            // Loading this command last, becuase it needs to be created last
            this.SearchCompletedCommand = DelegateCommand.Create(doSearchCompleted);

            checkNullContentTemplate();

            loaded = true;
        }

        private void SearchBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.PopupClickCommand != null)
                this.PopupClickCommand.FireCanExecuteChanged();
        }

        private void popup_Opened(object sender, EventArgs e)
        {
            popup.UpdateLayout();

            if (searchtextbox != null)
            {
                searchtextbox.SelectAll();
                searchtextbox.Focus();
            }
        }

        private void popup_Closed(object sender, EventArgs e)
        {
            this.UpdateLayout();
            this.Focus();
        }

        private void PopupChild_LostFocus(object sender, RoutedEventArgs e)
        {
            checkFocus();
        }

        private void resultlistbox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            setSelectedItem();
        }

        private void resultlistbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                setSelectedItem();
            else if (e.Key == Key.Escape)
                doPopupClick();

            e.Handled = true;
        }

        private void searchtextbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                onTimerTick(timer, new EventArgs());
                e.Handled = true;
                return;
            }
            else if (e.Key == Key.Escape)
            {
                doPopupClick();
                e.Handled = true;
                return;
            }
            else if (e.Key == Key.Down)
            {
                if (resultlistbox != null)
                {
                    resultlistbox.Focus();
                    e.Handled = true;
                    return;
                }
            }

            // Restarting timer
            if (timer.IsEnabled)
                timer.Stop();

            timer.Start();
        }

        #endregion

        #region -: Private Methods :-

        private void initCommands()
        {
            this.PopupClickCommand = DelegateCommand.Create(doPopupClick, checkPopupCanExecute);
            this.ClearClickCommand = DelegateCommand.Create(doClearClick, checkClearCanExecute);
        }

        private void doPopupClick()
        {
            this.PopupIsVisible = !this.PopupIsVisible;
        }

        private void doClearClick()
        {
            this.SelectedItem = null;
            this.InternalSelectedItem = null;
            this.PopupIsVisible = false;
        }

        private Boolean checkPopupCanExecute()
        {
            return !this.IsReadOnly;
        }

        private Boolean checkClearCanExecute()
        {
            return true;
        }

        private void startSearch()
        {
            this.IsSearching = true;
            this.InternalSelectedItem = null;

            if ((this.SearchStartedCommand != null) && this.SearchStartedCommand.CanExecute(this.CommandParameter) || (this.SearchStarted != null))
            {
                if ((this.SearchStartedCommand != null) && this.SearchStartedCommand.CanExecute(this.CommandParameter))
                    this.SearchStartedCommand.Execute(this.CommandParameter);

                if (this.SearchStarted != null)
                    this.SearchStarted(this, new RoutedEventArgs());
            }
            else
                this.IsSearching = false;
        }

        private void doInternalSelectedItemChanged()
        {
            // nothing yet
        }

        private void doSelectedItemChanged()
        {
            if (this.IsSearching)
                return;

            if (this.CloseOnSelection)
                this.PopupIsVisible = false;

            if (this.InternalSelectedItem != this.SelectedItem)
                this.InternalSelectedItem = this.SelectedItem;

            // Setting template for Null Content if given
            checkNullContentTemplate();

            //Firing and SelectedItem changed event and executing SelectedItem changed command
            if (this.SelectedItemChanged != null)
                this.SelectedItemChanged(this, new RoutedEventArgs());

            if ((this.SelectedItemChangedCommand != null) && this.SelectedItemChangedCommand.CanExecute(this.CommandParameter))
                this.SelectedItemChangedCommand.Execute(this.CommandParameter);
        }

        private void doSearchCompleted()
        {
            Int32 count = 0;
            Object selobj = null;

            this.IsSearching = false;

            foreach (Object obj in this.ItemsSource)
            {
                if (count > 1)
                    break;

                selobj = obj;
                count++;
            }

            if (count == 1)
                this.SelectedItem = selobj;
        }

        private void onTimerTick(object sender, EventArgs e)
        {
            timer.Stop();
            startSearch();
        }

        private void setSelectedItem()
        {
            this.SelectedItem = this.InternalSelectedItem;
        }

        private void setContentTemplate()
        {
            if ((defaultcontenttemplate == null) || (this.ContentTemplate == defaultcontenttemplate))
            {
                defaultcontenttemplate = getDefaultContentTemplate();
                this.ContentTemplate = defaultcontenttemplate;
            }
        }

        private DataTemplate getDefaultContentTemplate()
        {
            DataTemplate retVal = null;
            String markup = String.Empty;

            markup = "<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:local=\"clr-namespace:MM.Toolkit;assembly=MM.Toolkit\">";
            markup += "<Grid>";
            markup += "<TextBlock Text=\"{Binding RelativeSource={RelativeSource Mode=TemplatedParent}, Path=Content, Mode=OneWay}\" />";
            markup += "</Grid>";
            markup += "</DataTemplate>";

            retVal = (DataTemplate)XamlReader.Load(markup);

            return retVal;
        }

        private void checkFocus()
        {
            Boolean childhascontrol = false;

            if (FocusManager.GetFocusedElement() != null)
            {
                childhascontrol = Common.FindVisualChild(this, FocusManager.GetFocusedElement() as UIElement);

                if (!childhascontrol && (popup != null))
                    childhascontrol = Common.FindVisualChild(popup, FocusManager.GetFocusedElement() as UIElement);
            }

            this.IsFocused = childhascontrol;

            if (!this.IsFocused)
                this.PopupIsVisible = false;

            doUpdateStates();
        }


        private void doUpdateStates()
        {
            if (this.IsHovering)
                VisualStateManager.GoToState(this, "MouseOver", true);
            else
            {
                if (this.IsFocused)
                    VisualStateManager.GoToState(this, "Focused", true);
                else
                    VisualStateManager.GoToState(this, "Normal", true);
            }
        }

        private void checkNullContentTemplate()
        {
            if (this.SelectedItem == null)
            {
                if (this.NullContentTemplate != null)
                {
                    tempnullcontenttemplate = this.ContentTemplate;
                    this.ContentTemplate = this.NullContentTemplate;
                }
            }
            else
            {
                if (tempnullcontenttemplate != null)
                {
                    this.ContentTemplate = tempnullcontenttemplate;
                    tempnullcontenttemplate = null;
                }
            }
        }

        private void tryCopyText(Object item)
        {
            try
            {
                Clipboard.SetText(getTextFromItem(item));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error copying text!\r\n" + ex.Message);
            }
        }

        private String getTextFromItem(Object item)
        {
            String retVal = null;
            PropertyInfo prop = null;

            if (item == null)
                return retVal;

            if (!String.IsNullOrEmpty(this.TextPropertyName))
            {
                prop = item.GetType().GetProperty(this.TextPropertyName);

                if (prop != null)
                {
                    if (this.TextConverter != null)
                        retVal = this.TextConverter.Convert(prop.GetValue(item, null), typeof(String), null, CultureInfo.CurrentCulture).ToString();
                    else
                        retVal = prop.GetValue(item, null).ToString();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("System.Reflection Error: 'TextPropertyName' property '" + this.TextPropertyName + "' was not found on class '" + item.GetType().Name + "'; . Target element is '" + this.GetType().FullName + "'..");
                    retVal = item.ToString();
                }
            }
            else
            {
                if (this.TextConverter != null)
                    retVal = this.TextConverter.Convert(item, typeof(String), null, CultureInfo.CurrentCulture).ToString();
                else
                    retVal = item.ToString();
            }

            return retVal;
        }

        #endregion

        #region -: Public Methods :-

        public void SearchCompleted()
        {
            doSearchCompleted();
        }

        #endregion

        #region -: INotifyPropertyChanged Members :-

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String aPropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(aPropertyName));
        }

        #endregion
    }
}
