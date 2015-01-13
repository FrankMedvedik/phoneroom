using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using Microsoft.Lync.Controls;
using PhoneLogic.Core.MVVM_Base_Types;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;


namespace PhoneLogic.Core.ViewModels
{
    public class RecruitersViewModel : CollectionViewModelBase
    {
        public RecruitersViewModel()
        {
            GetRecruiters();
            PhoneRooms = PhoneRoomSvc.GetAll();
            SelectedPhoneRoomName = PhoneRoomSvc.GetDefault();
            LyncStatusRanges = LyncStatusRangeSvc.GetAll();
            SelectedLyncStatusRange = LyncStatusRangeSvc.GetDefault();
        }


        #region reporting variables

        private DateTime _startRptDate = DateTime.Now.AddDays(-1);
        public DateTime StartRptDate
        {
            get { return _startRptDate; }
            set
            {
                _startRptDate = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime _endRptDate = DateTime.Now;
        public DateTime EndRptDate
        {
            get { return _endRptDate; }
            set
            {
                _endRptDate = value;
                NotifyPropertyChanged();
            }
        }

        private LyncStatusRange _selectedLyncStatusRange;
        public LyncStatusRange SelectedLyncStatusRange
        {
            get { return _selectedLyncStatusRange; }
            set
            {
                _selectedLyncStatusRange = value;
                NotifyPropertyChanged();
                RefreshFilteredData();
            }
        }

        private string _selectedPhoneRoomName;
        public string SelectedPhoneRoomName
        {
            get { return _selectedPhoneRoomName; }
            set
            {
                _selectedPhoneRoomName = value;
                NotifyPropertyChanged();
                RefreshFilteredData();
            }
        }

        private ObservableCollection<PhoneRoom> _phoneRooms = new ObservableCollection<PhoneRoom>();
        public ObservableCollection<PhoneRoom> PhoneRooms
        {
            get { return _phoneRooms; }
            set
            {
                _phoneRooms = value;
                NotifyPropertyChanged();
            }
        }


        private ObservableCollection<LyncStatusRange> _lyncStatusRanges = new ObservableCollection<LyncStatusRange>();
        public ObservableCollection<LyncStatusRange> LyncStatusRanges
        {
            get { return _lyncStatusRanges; }
            set
            {
                _lyncStatusRanges = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Filters
        private ObservableCollection<Recruiter> _filteredRecruiters = new ObservableCollection<Recruiter>();
        public ObservableCollection<Recruiter> FilteredRecruiters
        {
            get { return _filteredRecruiters; }
            set
            {
                _filteredRecruiters = value;
                NotifyPropertyChanged();
            }
        }

        private void RefreshFilteredData()
        {
            FilterByPhoneRoom();
            FilterByLyncStatus();
        }

        private void FilterByLyncStatus()
        {
            if ((SelectedLyncStatusRange == null) || (SelectedLyncStatusRange.Name == "All")) return;

            List<Recruiter> fr = FilteredRecruiters.ToList();
            if (SelectedLyncStatusRange.Name == "Availible")
            {
                foreach (var r in fr)
                {
                    var p = new PresenceIndicator() { Source = r.sip };
                    switch (p.AvailabilityState)
                    {
                        case ContactAvailability.Invalid:
                            fr.Remove(r);
                            break;
                        case ContactAvailability.None:
                            fr.Remove(r);
                            break;
                        case ContactAvailability.Free:
                            break;
                        case ContactAvailability.FreeIdle:
                            break;
                        case ContactAvailability.Busy:
                            fr.Remove(r);
                            break;
                        case ContactAvailability.BusyIdle:
                            fr.Remove(r);
                            break;
                        case ContactAvailability.DoNotDisturb:
                            fr.Remove(r);
                            break;
                        case ContactAvailability.TemporarilyAway:
                            fr.Remove(r);
                            break;
                        case ContactAvailability.Away:
                            fr.Remove(r);
                            break;
                        case ContactAvailability.Offline:
                            fr.Remove(r);
                            break;
                        default:
                            fr.Remove(r);
                            break;
                    }
                }
            }
            else if (SelectedLyncStatusRange.Name == "Online")
            {
                foreach (var r in fr)
                {
                    var p = new PresenceIndicator() { Source = r.sip };
                    switch (p.AvailabilityState)
                    {
                        case ContactAvailability.Invalid:
                            fr.Remove(r);
                            break;
                        case ContactAvailability.None:
                            fr.Remove(r);
                            break;
                        case ContactAvailability.Free:
                            break;
                        case ContactAvailability.FreeIdle:
                            break;
                        case ContactAvailability.Busy:
                            break;
                        case ContactAvailability.BusyIdle:
                            break;
                        case ContactAvailability.DoNotDisturb:
                            break;
                        case ContactAvailability.TemporarilyAway:
                            fr.Remove(r);
                            break;
                        case ContactAvailability.Away:
                            fr.Remove(r);
                            break;
                        case ContactAvailability.Offline:
                            fr.Remove(r);
                            break;
                        default:
                            fr.Remove(r);
                            break;
                    }
                }
            }
            FilteredRecruiters = new ObservableCollection<Recruiter>(fr);
        }

        private void FilterByPhoneRoom()
        {
            var fr = new List<Recruiter>();
            if ((SelectedPhoneRoomName == null) || (SelectedPhoneRoomName == "All"))
                FilteredRecruiters = Recruiters;
            else
            {
                fr = (from fobjs in Recruiters
                      where fobjs.PhoneRoom == SelectedPhoneRoomName
                      select fobjs).ToList();

                if (FilteredRecruiters.Count == fr.Count())
                    return;
                FilteredRecruiters = new ObservableCollection<Recruiter>(fr);
            }
        }
        #endregion

        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetRecruiters();
            SelectedRecruiter = Recruiters.FirstOrDefault();
        }

        #region Recruiters



        private ObservableCollection<Recruiter> _recruiters = new ObservableCollection<Recruiter>();

        public ObservableCollection<Recruiter> Recruiters
        {
            get
            {
                return _recruiters;
            }
            set
            {
                _recruiters = value;
                NotifyPropertyChanged();
            }
        }


        public async void GetRecruiters()
        {
            try
            {
                var ro = await RecruiterSvc.GetRecruiters();
                var r = new ObservableCollection<Recruiter>();
                if (ro.Count > 0)
                {
                    foreach (var x in ro)
                    {
                        r.Add(x);
                    }
                    ShowGridData = true;
                    Recruiters = r;
                }
                else
                    ShowGridData = false;
                LoadedOk = true;
                RefreshFilteredData();
            }
            catch (Exception e)
            {
                LoadFailed(e);
            }
        }

        #endregion

        #region SelectedRecruiter

        private Recruiter _selectedRecruiter;
        public Recruiter  SelectedRecruiter
        {
            get
            {
                return _selectedRecruiter;
            }
            set
            {
                _selectedRecruiter = value;
                NotifyPropertyChanged();
                GetRecruiterLogs();
            }
        }
        #endregion

        #region RecruiterLogs

        private ObservableCollection<RecruiterLog> _recruiterLogs = new ObservableCollection<RecruiterLog>();

        public ObservableCollection<RecruiterLog> RecruiterLogs
        {
            get
            {
                return _recruiterLogs;
            }
            set
            {
                _recruiterLogs = value;
                NotifyPropertyChanged();
            }
        }
        
        private Boolean _showLogData = false;
        public Boolean ShowLogData
        {
            get
            {
                return _showLogData;
            }
            set
            {
                _showLogData = value;
                NotifyPropertyChanged();
            }
        }


        public async void GetRecruiterLogs()
        {
            if (SelectedRecruiter == null)
            {
                ShowLogData = false;
                LoadedOk = true;
            }
            else
                try
                {
                    var ro = await RecruiterSvc.GetRecruiterLog(SelectedRecruiter.sip, StartRptDate, EndRptDate);
                    var r = new ObservableCollection<RecruiterLog>();
                    if (ro.Count > 0)
                    {
                        foreach (var c in ro)
                        {
                            r.Add(c);
                        }
                        ShowLogData = true;
                        RecruiterLogs = r;
                    }
                    else
                        ShowLogData = false;
                    LoadedOk = true;
                }
                catch (Exception e)
                {
                    LoadFailed(e);
                }
        }

        #endregion
    }
}