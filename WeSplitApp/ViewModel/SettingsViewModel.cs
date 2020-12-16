using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitApp.ViewModel
{
    public class SettingsViewModel : ViewModel
    {
        private static SettingsViewModel instance;
        public static SettingsViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    new SettingsViewModel();
                }
                return instance;
            }
            set => instance = value;
        }

        private SettingsViewModel()
        {
            LoadAll();
            instance = this;
        }

        public void LoadAll()
        {
            //load handle here
            LoadMemberViewModel();
            LoadLocationViewModel();
            LoadTripViewModel();
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            SplashScreen = bool.Parse(config.AppSettings.Settings["isSplashScreenAllowed"].Value);
        }

        public void SaveAll()
        {
            //Save all before closing app
            SaveMemberViewModel();
            SaveLocationViewModel();
            SaveTripViewModel();
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["isSplashScreenAllowed"].Value = SplashScreen.ToString();
            config.Save(ConfigurationSaveMode.Minimal);
        }

        #region Splash Screen
        private bool _splashScreen;
        public bool SplashScreen
        {
            get => this._splashScreen;
            set
            {
                this._splashScreen = value;
            }
        }

        #endregion

        #region Member
        public void SaveMemberViewModel()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["MemberCurrentPaging"].Value = MemberCurrentPaging.ToString();
            config.AppSettings.Settings["MemberLoadSortMethod"].Value = MemberLoadSortMethod.ToString();
            config.Save(ConfigurationSaveMode.Minimal);
        }
        public void LoadMemberViewModel()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            MemberMaxPaging = MemberListViewModel.Instance.GetMaximum();
            MemberCurrentPaging = int.Parse(config.AppSettings.Settings["MemberCurrentPaging"].Value);
            MemberSortMethods = MemberListViewModel.Instance.getSortMethod();
            MemberLoadSortMethod = int.Parse(config.AppSettings.Settings["MemberLoadSortMethod"].Value);
            MemberListViewModel.Instance.MakeSort(MemberLoadSortMethod);
        }

        private int _memberCurrentPaging;
        public int MemberCurrentPaging
        {
            get => this._memberCurrentPaging;
            set
            {
                this._memberCurrentPaging = value;
                MemberListViewModel.Instance.getNewRowPerPage(_memberCurrentPaging);
            }
        }

        private int _memberMaxPaging;
        public int MemberMaxPaging
        {
            get => _memberMaxPaging;
            set
            {
                _memberMaxPaging = value;
            }
        }

        private int _memberLoadSortMethod;
        public int MemberLoadSortMethod
        {
            get => this._memberLoadSortMethod;
            set
            {
                this._memberLoadSortMethod = value;
                MemberListViewModel.Instance.MakeSort(MemberLoadSortMethod);
            }
        }

        public void UpdateMemberSortMethod() // chạy mỗi khi thêm mới, edit thông tin
        {
            MemberListViewModel.Instance.MakeSort(MemberLoadSortMethod);
        }

        public List<string> MemberSortMethods { get; set; }

        internal void UpdateMemberMaxPaging()
        {
            MemberMaxPaging = MemberListViewModel.Instance.GetMaximum();
        }

        #endregion

        #region Location
        public void SaveLocationViewModel()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["LocationCurrentPaging"].Value = LocationCurrentPaging.ToString();
            config.AppSettings.Settings["LocationLoadSortMethod"].Value = LocationLoadSortMethod.ToString();
            config.Save(ConfigurationSaveMode.Minimal);
        }
        public void LoadLocationViewModel()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            LocationMaxPaging = LocationListViewModel.Instance.GetMaximum();
            LocationCurrentPaging = int.Parse(config.AppSettings.Settings["LocationCurrentPaging"].Value);
            LocationSortMethods = LocationListViewModel.Instance.getSortMethod();
            LocationLoadSortMethod = int.Parse(config.AppSettings.Settings["LocationLoadSortMethod"].Value);
            LocationListViewModel.Instance.MakeSort(LocationLoadSortMethod);
        }

        private int _locationCurrentPaging;
        public int LocationCurrentPaging
        {
            get => this._locationCurrentPaging;
            set
            {
                this._locationCurrentPaging = value;
                LocationListViewModel.Instance.getNewRowPerPage(_locationCurrentPaging);
            }
        }

        private int _locationMaxPaging;
        public int LocationMaxPaging
        {
            get => _locationMaxPaging;
            set
            {
                _locationMaxPaging = value;
            }
        }

        private int _locationLoadSortMethod;
        public int LocationLoadSortMethod
        {
            get => this._locationLoadSortMethod;
            set
            {
                this._locationLoadSortMethod = value;
                LocationListViewModel.Instance.MakeSort(LocationLoadSortMethod);
            }
        }

        public void UpdateLocationSortMethod() // chạy mỗi lần thêm mới, edit thông tin
        {
            LocationListViewModel.Instance.MakeSort(LocationLoadSortMethod);
        }

        public List<string> LocationSortMethods { get; set; }

        internal void UpdateLocationMaxPaging()
        {
            LocationMaxPaging = LocationListViewModel.Instance.GetMaximum();
        }
        #endregion

        #region Trip 
        public void SaveTripViewModel()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["TripCurrentPaging"].Value = TripCurrentPaging.ToString();
            config.AppSettings.Settings["TripLoadSortMethod"].Value = TripLoadSortMethod.ToString();
            config.Save(ConfigurationSaveMode.Minimal);
        }
        public void LoadTripViewModel()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            TripCurrentPaging = int.Parse(config.AppSettings.Settings["TripCurrentPaging"].Value);
            TripSortMethods = BeingTakenTripsListViewModel.Instance.getSortMethod();
            TripLoadSortMethod = int.Parse(config.AppSettings.Settings["TripLoadSortMethod"].Value);

            HaveTakenTripsListViewModel.Instance.MakeSort(LocationLoadSortMethod);
            BeingTakenTripsListViewModel.Instance.MakeSort(TripLoadSortMethod);

            int HaveTrips = HaveTakenTripsListViewModel.Instance.GetMaxiMum();
            int BeingTrips = BeingTakenTripsListViewModel.Instance.GetMaxiMum();
            TripMaxPaging = (BeingTrips > HaveTrips) ? BeingTrips : HaveTrips;
        }

        private int _tripCurrentPaging;
        public int TripCurrentPaging
        {
            get => this._tripCurrentPaging;
            set
            {
                this._tripCurrentPaging = value;
               BeingTakenTripsListViewModel.Instance.getNewRowPerPage(_tripCurrentPaging);
               HaveTakenTripsListViewModel.Instance.getNewRowPerPage(_tripCurrentPaging);
            }
        }

        private int _tripMaxPaging;
        public int TripMaxPaging
        {
            get => _tripMaxPaging;
            set
            {
                _tripMaxPaging = value;
            }
        }

        private int _tripLoadSortMethod;
        public int TripLoadSortMethod
        {
            get => this._tripLoadSortMethod;
            set
            {
                this._tripLoadSortMethod = value;
                BeingTakenTripsListViewModel.Instance.MakeSort(_tripLoadSortMethod);
                HaveTakenTripsListViewModel.Instance.MakeSort(_tripLoadSortMethod);
            }
        }

        public void UpdateTripSortMethod() // chạy mỗi lần thêm mới, edit thông tin
        {
            BeingTakenTripsListViewModel.Instance.MakeSort(_tripLoadSortMethod);
            HaveTakenTripsListViewModel.Instance.MakeSort(_tripLoadSortMethod);
        }

        public List<string> TripSortMethods { get; set; }

        internal void UpdateTripMaxPaging()
        {
            int BeingTrips = BeingTakenTripsListViewModel.Instance.GetMaxiMum();
            int HaveTrips = BeingTakenTripsListViewModel.Instance.GetMaxiMum();
            TripMaxPaging = (BeingTrips > HaveTrips) ? BeingTrips : HaveTrips;
        }


        #endregion
    }
}
