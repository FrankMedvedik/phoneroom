using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace MM.Toolkit
{
    //[TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "MouseOver", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Focused", GroupName = "CommonStates")]
    public class DateTimeBox : Control, INotifyPropertyChanged
    {
        #region -: Events :-

        public RoutedEventHandler SelectedDateTimeChanged;

        #endregion

        #region -: Variables :-

        private Boolean loaded = false;
        private Boolean focused = false;
        private Popup calendarpopup = null;
        private Popup timepopup = null;
        private TextBox datetextbox = null;
        private TextBox timetextbox = null;
        private TextBox lasttextbox = null;
        private Int32 numvisible = 3;

        #endregion

        #region -: Properties :-

        public DateTime? SelectedDateTime
        {
            get { return (DateTime?)GetValue(SelectedDateTimeProperty); }
            set
            {
                SetValue(SelectedDateTimeProperty, value);
                NotifyPropertyChanged("SelectedDateTime");
            }
        }

        public ObservableCollection<DateTimeFormatHolder> Times
        {
            get { return (ObservableCollection<DateTimeFormatHolder>)GetValue(TimesProperty); }
            set
            {
                SetValue(TimesProperty, value);
                NotifyPropertyChanged("Times");
            }
        }

        public DateTimeFormatHolder SelectedTime
        {
            get { return (DateTimeFormatHolder)GetValue(SelectedTimeProperty); }
            set
            {
                SetValue(SelectedTimeProperty, value);
                NotifyPropertyChanged("SelectedTime");
            }
        }

        public String DateFormat
        {
            get { return (String)GetValue(DateFormatProperty); }
            set
            {
                SetValue(DateFormatProperty, value);
                NotifyPropertyChanged("DateFormat");
            }
        }

        public String TimeFormat
        {
            get { return (String)GetValue(TimeFormatProperty); }
            set
            {
                SetValue(TimeFormatProperty, value);
                NotifyPropertyChanged("TimeFormat");
            }
        }

        public Int32 MinuteSpan
        {
            get { return (Int32)GetValue(MinuteSpanProperty); }
            set
            {
                SetValue(MinuteSpanProperty, value);
                NotifyPropertyChanged("MinuteSpan");
            }
        }

        public Boolean IsTodayHighlighted
        {
            get { return (Boolean)GetValue(IsTodayHighlightedProperty); }
            set
            {
                SetValue(IsTodayHighlightedProperty, value);
                NotifyPropertyChanged("IsTodayHighlighted");
            }
        }

        public DayOfWeek FirstDayOfWeek
        {
            get { return (DayOfWeek)GetValue(FirstDayOfWeekProperty); }
            set
            {
                SetValue(FirstDayOfWeekProperty, value);
                NotifyPropertyChanged("FirstDayOfWeek");
            }
        }

        public Boolean CalendarPopupIsOpen
        {
            get { return (Boolean)GetValue(CalendarPopupIsOpenProperty); }
            set
            {
                SetValue(CalendarPopupIsOpenProperty, value);
                NotifyPropertyChanged("CalendarPopupIsOpen");
            }
        }

        public Boolean TimePopupIsOpen
        {
            get { return (Boolean)GetValue(TimePopupIsOpenProperty); }
            set
            {
                SetValue(TimePopupIsOpenProperty, value);
                NotifyPropertyChanged("TimePopupIsOpen");
            }
        }

        public Boolean DateButtonIsVisible
        {
            get { return (Boolean)GetValue(DateButtonIsVisibleProperty); }
            set
            {
                SetValue(DateButtonIsVisibleProperty, value);
                NotifyPropertyChanged("DateButtonIsVisible");
            }
        }

        public Boolean TimeButtonIsVisible
        {
            get { return (Boolean)GetValue(TimeButtonIsVisibleProperty); }
            set
            {
                SetValue(TimeButtonIsVisibleProperty, value);
                NotifyPropertyChanged("TimeButtonIsVisible");
            }
        }

        public Boolean UpDownButtonsIsVisible
        {
            get { return (Boolean)GetValue(UpDownButtonsIsVisibleProperty); }
            set
            {
                SetValue(UpDownButtonsIsVisibleProperty, value);
                NotifyPropertyChanged("UpDownButtonsIsVisible");
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

        #endregion

        #region -: Properties, Private :-

        private DateTime? SelectedCalendarDateTime
        {
            get { return (DateTime?)GetValue(SelectedCalendarDateTimeProperty); }
            set
            {
                SetValue(SelectedCalendarDateTimeProperty, value);
                NotifyPropertyChanged("SelectedCalendarDateTime");
            }
        }

        #endregion

        #region -: Properties, Commands :-

        private DelegateCommand ShowHideCalendarCommand
        {
            get { return (DelegateCommand)GetValue(ShowHideCalendarCommandProperty); }
            set
            {
                SetValue(ShowHideCalendarCommandProperty, value);
                NotifyPropertyChanged("ShowHideCalendarCommand");
            }
        }

        private DelegateCommand ShowHideTimeCommand
        {
            get { return (DelegateCommand)GetValue(ShowHideTimeCommandProperty); }
            set
            {
                SetValue(ShowHideTimeCommandProperty, value);
                NotifyPropertyChanged("ShowHideTimeCommand");
            }
        }

        private DelegateCommand UpCommand
        {
            get { return (DelegateCommand)GetValue(UpCommandProperty); }
            set
            {
                SetValue(UpCommandProperty, value);
                NotifyPropertyChanged("UpCommand");
            }
        }

        private DelegateCommand DownCommand
        {
            get { return (DelegateCommand)GetValue(DownCommandProperty); }
            set
            {
                SetValue(DownCommandProperty, value);
                NotifyPropertyChanged("DownCommand");
            }
        }

        #endregion

        #region -: Dependency Properties :-

        private static readonly DependencyProperty SelectedCalendarDateTimeProperty = DependencyProperty.Register("SelectedCalendarDateTime", typeof(DateTime?), typeof(DateTimeBox), new PropertyMetadata(null, OnSelectedCalendarDateTimePropertyChanged));

        public static readonly DependencyProperty SelectedDateTimeProperty = DependencyProperty.Register("SelectedDateTime", typeof(DateTime?), typeof(DateTimeBox), new PropertyMetadata(null, OnSelectedDateTimePropertyChanged));
        public static readonly DependencyProperty TimesProperty = DependencyProperty.Register("Times", typeof(ObservableCollection<DateTimeFormatHolder>), typeof(DateTimeBox), new PropertyMetadata(null));
        public static readonly DependencyProperty SelectedTimeProperty = DependencyProperty.Register("SelectedTime", typeof(DateTimeFormatHolder), typeof(DateTimeBox), new PropertyMetadata(null, OnSelectedTimePropertyChanged));
        public static readonly DependencyProperty DateFormatProperty = DependencyProperty.Register("DateFormat", typeof(String), typeof(DateTimeBox), new PropertyMetadata("yyyy.MM.dd", OnDateFormatPropertyChanged));
        public static readonly DependencyProperty TimeFormatProperty = DependencyProperty.Register("TimeFormat", typeof(String), typeof(DateTimeBox), new PropertyMetadata("HH:mm", OnTimeFormatPropertyChanged));
        public static readonly DependencyProperty MinuteSpanProperty = DependencyProperty.Register("MinuteSpan", typeof(Int32), typeof(DateTimeBox), new PropertyMetadata(1));
        public static readonly DependencyProperty IsTodayHighlightedProperty = DependencyProperty.Register("IsTodayHighlighted", typeof(Boolean), typeof(DateTimeBox), new PropertyMetadata(true));
        public static readonly DependencyProperty FirstDayOfWeekProperty = DependencyProperty.Register("FirstDayOfWeek", typeof(DayOfWeek), typeof(DateTimeBox), new PropertyMetadata(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek));
        public static readonly DependencyProperty CalendarPopupIsOpenProperty = DependencyProperty.Register("CalendarPopupIsOpen", typeof(Boolean), typeof(DateTimeBox), new PropertyMetadata(false, OnCalendarPopupIsOpenPropertyChanged));
        public static readonly DependencyProperty TimePopupIsOpenProperty = DependencyProperty.Register("TimePopupIsOpen", typeof(Boolean), typeof(DateTimeBox), new PropertyMetadata(false, OnTimePopupIsOpenPropertyChanged));
        public static readonly DependencyProperty DateButtonIsVisibleProperty = DependencyProperty.Register("DateButtonIsVisible", typeof(Boolean), typeof(DateTimeBox), new PropertyMetadata(true, OnButtonIsVisiblePropertyChanged));
        public static readonly DependencyProperty TimeButtonIsVisibleProperty = DependencyProperty.Register("TimeButtonIsVisible", typeof(Boolean), typeof(DateTimeBox), new PropertyMetadata(true, OnButtonIsVisiblePropertyChanged));
        public static readonly DependencyProperty UpDownButtonsIsVisibleProperty = DependencyProperty.Register("UpDownButtonsIsVisible", typeof(Boolean), typeof(DateTimeBox), new PropertyMetadata(true, OnButtonIsVisiblePropertyChanged));
        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.Register("IsFocused", typeof(Boolean), typeof(DateTimeBox), new PropertyMetadata(false));
        public static readonly DependencyProperty IsHoveringProperty = DependencyProperty.Register("IsHovering", typeof(Boolean), typeof(DateTimeBox), new PropertyMetadata(false));
        
        // Commands
        private static readonly DependencyProperty ShowHideCalendarCommandProperty = DependencyProperty.Register("ShowHideCalendarCommand", typeof(DelegateCommand), typeof(DateTimeBox), new PropertyMetadata(null));
        private static readonly DependencyProperty ShowHideTimeCommandProperty = DependencyProperty.Register("ShowHideTimeCommand", typeof(DelegateCommand), typeof(DateTimeBox), new PropertyMetadata(null));
        private static readonly DependencyProperty UpCommandProperty = DependencyProperty.Register("UpCommand", typeof(DelegateCommand), typeof(DateTimeBox), new PropertyMetadata(null));
        private static readonly DependencyProperty DownCommandProperty = DependencyProperty.Register("DownCommand", typeof(DelegateCommand), typeof(DateTimeBox), new PropertyMetadata(null));

        #endregion

        #region -: Dependency Property Methods :-

        private static void OnSelectedCalendarDateTimePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            DateTimeBox ctl = sender as DateTimeBox;

            ctl.doSelectedCalendarDateTimeChanged();
            ctl = null;
        }

        private static void OnSelectedDateTimePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            DateTimeBox ctl = sender as DateTimeBox;

            ctl.doSelectedDateTimeChanged();
            ctl = null;
        }

        private static void OnSelectedTimePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            DateTimeBox ctl = sender as DateTimeBox;

            ctl.doSelectedTimeChanged();
            ctl = null;
        }

        private static void OnDateFormatPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            DateTimeBox ctl = sender as DateTimeBox;

            ctl.doDateFormatChanged();
            ctl = null;
        }

        private static void OnTimeFormatPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            DateTimeBox ctl = sender as DateTimeBox;

            ctl.doTimeFormatChanged();
            ctl = null;
        }

        private static void OnCalendarPopupIsOpenPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            DateTimeBox ctl = sender as DateTimeBox;

            ctl.doCalendarPopupIsOpenChanged();
            ctl = null;
        }

        private static void OnTimePopupIsOpenPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            DateTimeBox ctl = sender as DateTimeBox;

            ctl.doTimePopupIsOpenChanged();
            ctl = null;
        }

        private static void OnButtonIsVisiblePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            DateTimeBox ctl = sender as DateTimeBox;

            ctl.setLayout();
            ctl = null;
        }

        #endregion

        #region -: Constructors & Destructor :-

        public DateTimeBox()
        {
            this.DefaultStyleKey = typeof(DateTimeBox);

            if (System.ComponentModel.DesignerProperties.IsInDesignTool)
                return;

            this.Loaded += new RoutedEventHandler(DateTimeBox_Loaded);
            this.Unloaded += new RoutedEventHandler(DateTimeBox_Unloaded);
            this.GotFocus += new RoutedEventHandler(DateTimeBox_GotFocus);
            this.IsEnabledChanged += new DependencyPropertyChangedEventHandler(DateTimeBox_IsEnabledChanged);

            // Initiating control
            this.Times = new ObservableCollection<DateTimeFormatHolder>();

            initCommands();
        }

        private void DateTimeBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.ShowHideCalendarCommand != null)
                this.ShowHideCalendarCommand.FireCanExecuteChanged();

            if (this.ShowHideTimeCommand != null)
                this.ShowHideTimeCommand.FireCanExecuteChanged();
        }

        #endregion

        #region -: Control Overrides :-
        
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            calendarpopup = this.GetTemplateChild("PART_CalendarPopup") as Popup;
            timepopup = this.GetTemplateChild("PART_TimePopup") as Popup;

            datetextbox = this.GetTemplateChild("PART_DateTextBox") as TextBox;
            if (datetextbox != null)
            {
                datetextbox.GotFocus += new RoutedEventHandler(datetextbox_GotFocus);
                datetextbox.LostFocus += new RoutedEventHandler(datetextbox_LostFocus);
                datetextbox.KeyDown += new KeyEventHandler(datetextbox_KeyDown);
            }

            timetextbox = this.GetTemplateChild("PART_TimeTextBox") as TextBox;
            if (timetextbox != null)
            {
                timetextbox.GotFocus += new RoutedEventHandler(timetextbox_GotFocus);
                timetextbox.LostFocus += new RoutedEventHandler(timetextbox_LostFocus);
                timetextbox.KeyDown += new KeyEventHandler(timetextbox_KeyDown);
            }

            doSelectedDateTimeChanged();
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
            base.OnKeyDown(e);

            if (e.Key == Key.Escape)
            {
                this.CalendarPopupIsOpen = false;
                this.TimePopupIsOpen = false;
                e.Handled = true;
            }
        }

        #endregion

        #region -: Control Events :-

        private void DateTimeBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (loaded)
                return;

            loadTimes();

            if (this.MinuteSpan < 1)
                this.MinuteSpan = 1;

            //this.UpdateLayout();
            //this.Focus();

            loaded = true;
        }

        private void DateTimeBox_Unloaded(object sender, RoutedEventArgs e)
        {
            loaded = false;
        }

        private void DateTimeBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (focused)
                return;

            if (datetextbox != null)
                datetextbox.Focus();

            focused = true;
        }

        private void timetextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            timetextbox.SelectAll();
            lasttextbox = timetextbox;
        }

        private void datetextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            DateTime? seldate = null;
            DateTime outdate = default(DateTime);

            if (!String.IsNullOrWhiteSpace(datetextbox.Text))
            {
                if (String.IsNullOrWhiteSpace(timetextbox.Text))
                {
                    if (DateTime.TryParse(datetextbox.Text, out outdate))
                        seldate = outdate;
                    else
                        timetextbox.Text = DateTime.Now.ToString(this.TimeFormat);
                }

                if (!seldate.HasValue)
                {
                    if (DateTime.TryParse(datetextbox.Text + " " + timetextbox.Text, out outdate))
                        seldate = outdate;
                }

                if (seldate.HasValue)
                {
                    this.SelectedDateTime = seldate.Value;
                }
                else
                {
                    this.CalendarPopupIsOpen = true;
                    this.SelectedCalendarDateTime = DateTime.Now;
                    datetextbox.Text = this.SelectedCalendarDateTime.Value.ToString(this.DateFormat);
                    timetextbox.Text = DateTime.Now.ToString(this.TimeFormat);
                }
            }
            else
                this.SelectedDateTime = null;
        }

        private void datetextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Up) || (e.Key == Key.Down))
            {
                doMoveDate(e.Key == Key.Down);
                e.Handled = true;
            }
            else if ((e.Key == Key.Right) && (datetextbox.SelectionStart == datetextbox.Text.Length))
                timetextbox.Focus();
        }

        private void datetextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            datetextbox.SelectAll();
            lasttextbox = datetextbox;
        }

        private void timetextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            DateTime date = default(DateTime);

            if (!String.IsNullOrWhiteSpace(timetextbox.Text))
            {
                if (timetextbox.Text.Length == 2)
                    timetextbox.Text += ":00";

                if (!String.IsNullOrWhiteSpace(datetextbox.Text))
                    datetextbox.Text = DateTime.Now.ToString(this.DateFormat);

                if (DateTime.TryParse(datetextbox.Text + " " + timetextbox.Text, out date))
                {
                    this.SelectedDateTime = date;
                }
                else
                {
                    timetextbox.Text = DateTime.Now.ToString(this.TimeFormat);
                    timetextbox.Focus();
                    timetextbox_LostFocus(sender, e);
                    //this.TimePopupIsOpen = true;
                }
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(datetextbox.Text))
                {
                    timetextbox.Text = DateTime.Now.ToString(this.TimeFormat);

                    if (DateTime.TryParse(datetextbox.Text + " " + timetextbox.Text, out date))
                    {
                        this.SelectedDateTime = date;
                    }
                    else
                    {
                        timetextbox.Text = DateTime.Now.ToString(this.TimeFormat);
                        timetextbox.Focus();
                        timetextbox_LostFocus(sender, e);
                        //this.TimePopupIsOpen = true;
                    }
                }
                else
                    this.SelectedDateTime = null;
            }
        }

        private void timetextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Up) || (e.Key == Key.Down))
            {
                doMoveTime(e.Key == Key.Down);
                e.Handled = true;
            }
            else if ((e.Key == Key.Left) && (timetextbox.SelectionStart == 0))
                datetextbox.Focus();
        }

        #endregion

        #region -: Private Methods :-

        private void initCommands()
        {
            this.ShowHideCalendarCommand = DelegateCommand.Create(doShowHideCalendar, checkShowHideCalendarCanExecute);
            this.ShowHideTimeCommand = DelegateCommand.Create(doShowHideTime, checkShowHideTimeCanExecute);
            this.UpCommand = DelegateCommand.Create(doUp, checkUpCanExecute);
            this.DownCommand = DelegateCommand.Create(doDown, checkDownCanExecute);
        }

        private void loadTimes()
        {
            DateTime time = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
            // Example time formats available : t, T, h, H, hh, HH, mm, ss

            if (this.Times == null)
                this.Times = new ObservableCollection<DateTimeFormatHolder>();

            this.Times.Clear();

            for (Int32 hour = 0; hour < 24; hour++)
            {
                this.Times.Add(new DateTimeFormatHolder() { DateTime = time, Format = this.TimeFormat });
                time = time.AddMinutes(30);
                this.Times.Add(new DateTimeFormatHolder() { DateTime = time, Format = this.TimeFormat });
                time = time.AddMinutes(30);
            }
        }

        private void checkFocus()
        {
            Boolean childhascontrol = false;

            if (FocusManager.GetFocusedElement() != null)
            {
                childhascontrol = Common.FindVisualChild(this, FocusManager.GetFocusedElement() as UIElement);

                if (!childhascontrol && (calendarpopup != null))
                    childhascontrol = Common.FindVisualChild(calendarpopup, FocusManager.GetFocusedElement() as UIElement);

                if (!childhascontrol && (timepopup != null))
                    childhascontrol = Common.FindVisualChild(timepopup, FocusManager.GetFocusedElement() as UIElement);
            }

            this.IsFocused = childhascontrol;

            if (!this.IsFocused)
            {
                this.CalendarPopupIsOpen = false;
                this.TimePopupIsOpen = false;
            }

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

        private void doSelectedDateTimeChanged()
        {
            if (timetextbox != null)
            {
                if (this.SelectedDateTime.HasValue)
                    timetextbox.Text = this.SelectedDateTime.Value.ToString(this.TimeFormat);
                else
                    timetextbox.Text = String.Empty;
            }

            if (datetextbox != null)
            {
                if (this.SelectedDateTime.HasValue)
                    datetextbox.Text = this.SelectedDateTime.Value.ToString(this.DateFormat);
                else
                    datetextbox.Text = String.Empty;
            }

            setLayout();

            this.CalendarPopupIsOpen = false;
            this.TimePopupIsOpen = false;

            if (this.SelectedDateTimeChanged != null)
                this.SelectedDateTimeChanged(this, new RoutedEventArgs());
        }

        private void doSelectedCalendarDateTimeChanged()
        {
            Int32 year = DateTime.Now.Year;
            Int32 month = DateTime.Now.Month;
            Int32 day = DateTime.Now.Day;

            if (this.SelectedCalendarDateTime.HasValue)
            {
                year = this.SelectedCalendarDateTime.Value.Year;
                month = this.SelectedCalendarDateTime.Value.Month;
                day = this.SelectedCalendarDateTime.Value.Day;

                if (this.SelectedDateTime.HasValue)
                    this.SelectedDateTime = new DateTime(year, month, day, this.SelectedDateTime.Value.Hour, this.SelectedDateTime.Value.Minute, this.SelectedDateTime.Value.Second);
                else
                    this.SelectedDateTime = new DateTime(year, month, day, 0, 0, 0);
            }
            else
                this.SelectedDateTime = null;
        }

        private void doSelectedTimeChanged()
        {
            Int32 hour = 0;
            Int32 minute = 0;
            Int32 second = 0;

            if (this.SelectedTime != null)
            {
                hour = this.SelectedTime.DateTime.Hour;
                minute = this.SelectedTime.DateTime.Minute;
                second = this.SelectedTime.DateTime.Second;
            }

            if (this.SelectedDateTime.HasValue)
                this.SelectedDateTime = new DateTime(this.SelectedDateTime.Value.Year, this.SelectedDateTime.Value.Month, this.SelectedDateTime.Value.Day, hour, minute, second);
            else
                this.SelectedDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, second);
        }

        private void doDateFormatChanged()
        {
            doSelectedDateTimeChanged();
        }

        private void doTimeFormatChanged()
        {
            loadTimes();
            doSelectedDateTimeChanged();
        }

        private void doTimePopupIsOpenChanged()
        {
            if (this.TimePopupIsOpen)
                this.CalendarPopupIsOpen = false;
        }

        private void doCalendarPopupIsOpenChanged()
        {
            if (this.CalendarPopupIsOpen)
                this.TimePopupIsOpen = false;
        }

        private void doShowHideCalendar()
        {
            this.CalendarPopupIsOpen = !this.CalendarPopupIsOpen;
        }
        private Boolean checkShowHideCalendarCanExecute()
        {
            return this.IsEnabled;
        }

        private void doShowHideTime()
        {
            this.TimePopupIsOpen = !this.TimePopupIsOpen;
        }
        private Boolean checkShowHideTimeCanExecute()
        {
            return this.IsEnabled;
        }

        private void doUp()
        {
            if (lasttextbox == null)
                lasttextbox = timetextbox;

            if (lasttextbox == datetextbox)
                doMoveDate(false);
            else
                doMoveTime(false);
        }
        private Boolean checkUpCanExecute()
        {
            return this.IsEnabled;
        }

        private void doDown()
        {
            if (lasttextbox == null)
                lasttextbox = timetextbox;

            if (lasttextbox == datetextbox)
                doMoveDate(true);
            else
                doMoveTime(true);
        }
        private Boolean checkDownCanExecute()
        {
            return this.IsEnabled;
        }

        private void doMoveDate(Boolean down)
        {
            String format = null;
            Char formatchar = default(Char);
            Int32 incvalue = 1;
            DateTime outdate = default(DateTime);
            DateTime? seldate = null;
            Int32 selindex = 0;
            List<Char> formatchars = new List<Char>() { 'y', 'Y', 'M', 'd', 'D' };

            if (!String.IsNullOrWhiteSpace(datetextbox.Text))
            {
                if (down)
                    incvalue = -1;

                if (String.IsNullOrWhiteSpace(timetextbox.Text))
                    timetextbox.Text = DateTime.Now.ToString(this.TimeFormat);

                if (DateTime.TryParse(datetextbox.Text + " " + timetextbox.Text, out outdate))
                    seldate = outdate;

                if (seldate.HasValue)
                {
                    //if (this.TimeFormat.Equals("t", StringComparison.Ordinal))
                    //    format = "HH:mm";
                    //else if (this.TimeFormat.Equals("T", StringComparison.Ordinal))
                    //    format = "HH:mm:ss";
                    //else
                    format = this.DateFormat;

                    selindex = datetextbox.SelectionStart;

                    if (format.Length < selindex)
                        selindex = 0;
                    else if (format.Length == selindex)
                        selindex = format.Length - 1;

                    formatchar = format[selindex];

                    if (!formatchars.Contains(formatchar))
                        selindex -= 1;

                    if (formatchar.Equals('Y') || formatchar.Equals('y'))
                        seldate = seldate.Value.AddYears(incvalue);
                    else if (formatchar.Equals('M'))
                        seldate = seldate.Value.AddMonths(incvalue);
                    else if (formatchar.Equals('D') || formatchar.Equals('d'))
                        seldate = seldate.Value.AddDays(incvalue);

                    datetextbox.Text = seldate.Value.ToString(this.DateFormat);
                    datetextbox.SelectionStart = selindex;
                }
            }
            else
                datetextbox.Text = DateTime.Now.ToString(this.DateFormat);
        }

        private void doMoveTime(Boolean down)
        {
            String format = null;
            Char formatchar = default(Char);
            Int32 incvalue = 1;
            DateTime outdate = default(DateTime);
            DateTime? seldate = null;
            Int32 selindex = 0;
            List<Char> formatchars = new List<Char>() { 'H', 'h', 'm', 's', 'f' };

            if (!String.IsNullOrWhiteSpace(timetextbox.Text))
            {
                if (down)
                    incvalue = -1;

                if (String.IsNullOrWhiteSpace(datetextbox.Text))
                    datetextbox.Text = DateTime.Now.ToString(this.DateFormat);

                if (DateTime.TryParse(datetextbox.Text + " " + timetextbox.Text, out outdate))
                    seldate = outdate;

                if (seldate.HasValue)
                {
                    if (this.TimeFormat.Equals("t", StringComparison.Ordinal))
                        format = "HH:mm";
                    else if (this.TimeFormat.Equals("T", StringComparison.Ordinal))
                        format = "HH:mm:ss";
                    else
                        format = this.TimeFormat;

                    selindex = timetextbox.SelectionStart;

                    if (format.Length < selindex)
                        selindex = 0;
                    else if (format.Length == selindex)
                        selindex = format.Length - 1;

                    formatchar = format[selindex];

                    if (!formatchars.Contains(formatchar))
                        selindex -= 1;

                    if (formatchar.Equals('H') || formatchar.Equals('h'))
                        seldate = seldate.Value.AddHours(incvalue);
                    else if (formatchar.Equals('m'))
                        seldate = seldate.Value.AddMinutes(incvalue * this.MinuteSpan);
                    else if (formatchar.Equals('s'))
                        seldate = seldate.Value.AddSeconds(incvalue);
                    else if (formatchar.Equals('f'))
                        seldate = seldate.Value.AddMilliseconds(incvalue * 100);

                    timetextbox.Text = seldate.Value.ToString(this.TimeFormat);
                    timetextbox.SelectionStart = selindex;
                }
            }
            else
                timetextbox.Text = DateTime.Now.ToString(this.TimeFormat);
        }

        private void setLayout()
        {
            Double width = 0;
            Double height = 0;
            Int32 numvisible = 0;

            if (Double.IsNaN(this.Width))
                width = this.ActualWidth;
            else
                width = this.Width;

            if (Double.IsNaN(this.Height))
                height = this.ActualHeight;
            else
                height = this.Height;

            if (this.DateButtonIsVisible)
                numvisible += 1;

            if (this.TimeButtonIsVisible)
                numvisible += 1;

            if (this.UpDownButtonsIsVisible)
                numvisible += 1;

            if (timetextbox != null)
            {
                if (this.SelectedDateTime.HasValue)
                    timetextbox.Width = Double.NaN;
                else
                    timetextbox.Width = timetextbox.MinWidth;
            }

            if (datetextbox != null)
            {
                if (this.SelectedDateTime.HasValue)
                    datetextbox.Width = Double.NaN;
                else
                    datetextbox.Width = width - (numvisible * height) - 2;
            }
        }

        #endregion

        #region -: Public Methods :-

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