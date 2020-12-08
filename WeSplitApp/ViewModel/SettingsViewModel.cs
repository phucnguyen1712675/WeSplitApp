using System;
using System.Collections.Generic;
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

        private SettingsViewModel() {
            LoadAll();
            instance = this;
        }

        public void LoadAll()
        {
            //load handle here
            LoadMemberViewModel();
            LoadLocationViewModel();

            /*
             * LoadTripViewModel()
             */
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            SplashScreen = bool.Parse(config.AppSettings.Settings["isSplashScreenAllowed"].Value);
        }

        public void SaveAll()
        {
            //Save all before closing app
            SaveMemberViewModel();
            SaveLocationViewModel();

            /*
             * SaveTripViewModel()
             */
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
                OnPropertyChanged();
            }
        }

        #endregion

        #region Member
        public void SaveMemberViewModel() {
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        private int _memberLoadSortMethod;
        public int MemberLoadSortMethod
        {
            get => this._memberLoadSortMethod;
            set
            {
                this._memberLoadSortMethod = value;
                OnPropertyChanged();
                MemberListViewModel.Instance.MakeSort(MemberLoadSortMethod);
            }
        }

        public List<string> MemberSortMethods { get; set; }

        internal void UpdateMemberMaxPaging()
        {
            MemberMaxPaging = MemberListViewModel.Instance.GetMaximum();
        }
        #endregion

        #region Location
        public void SaveLocationViewModel() {
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        private int _locationLoadSortMethod;
        public int LocationLoadSortMethod
        {
            get => this._locationLoadSortMethod;
            set
            {
                this._locationLoadSortMethod = value;
                OnPropertyChanged();
                LocationListViewModel.Instance.MakeSort(LocationLoadSortMethod);
            }
        }

        public List<string> LocationSortMethods { get; set; }

        internal void UpdateLocationMaxPaging()
        {
            LocationMaxPaging = LocationListViewModel.Instance.GetMaximum();
        }
        #endregion

        #region Trip //TODO tạo delegate dictionary trong Trip
        public void SaveTripViewModel() { }
        public void LoadtripViewModel()
        {
            //TODO load things here
            /*LocationMaxPaging = LocationListViewModel.Instance.GetMaximum();
            LocationCurrentPaging = 4;
            LocationSortMethods = LocationListViewModel.Instance.getSortMethod();
            LocationLoadSortMethod = 0;
            LocationListViewModel.Instance.MakeSort(LocationLoadSortMethod);*/
        }

        private int _tripCurrentPaging;
        public int TripCurrentPaging
        {
            get => this._tripCurrentPaging;
            set
            {
                this._tripCurrentPaging = value;
                OnPropertyChanged();
               //LocationListViewModel.Instance.getNewRowPerPage(_locationCurrentPaging);
               //tạo hàm tựa trên để tính toán phân trang lại của Trip List
            }
        }

        private int _tripMaxPaging;
        public int tripMaxPaging
        {
            get => _tripMaxPaging;
            set
            {
                _tripMaxPaging = value;
                OnPropertyChanged();
            }
        }

        private int _tripLoadSortMethod;
        public int TripLoadSortMethod
        {
            get => this._tripLoadSortMethod;
            set
            {
                this._tripLoadSortMethod = value;
                OnPropertyChanged();
               // MemberListViewModel.Instance.MakeSort(LocationLoadSortMethod);
               //tạo hàm tựa như trên để sort theo index truyền vào
            }
        }

        public List<string> tripSortMethods { get; set; }

        internal void UpdateTripMaxPaging()
        {
            //LocationMaxPaging = LocationListViewModel.Instance.GetMaximum();
            //tạo hàm tựa như trên để update lại max paging đc binding trong setting 
        }
        #endregion
    }
}
