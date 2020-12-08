using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WeSplitApp.Utils;
using WeSplitApp.View;

namespace WeSplitApp.ViewModel
{
    class LocationListViewModel : SortMethodList
    {
        public ObservableCollection<LOCATION> _lOCATIONS;
        public ObservableCollection<LOCATION> LOCATIONS { 
            get => this._lOCATIONS; 
            set
            {
                this._lOCATIONS = value;
                OnPropertyChanged();
            }
        }
        public PagingListObjects PagingListObjects { get; set; }

        private static LocationListViewModel instance = null;
        public static LocationListViewModel Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new LocationListViewModel();
                }
                return instance;
            }
        }
        private LocationListViewModel()
        {
            MySort = new Dictionary<string, Delegate> {
                  { "Mặc định", new Func<List<LOCATION>>(SetDefaultPosition)},
            { "Tên tăng dần", new Func<List<LOCATION>>(SetAscendingPositionAccordingToName)},
            { "Tên giảm đần", new Func<List<LOCATION>>(SetDescendingPositionAccordingToName)}
            };

            this.LOCATIONS = new ObservableCollection<LOCATION>(HomeScreen.GetDatabaseEntities().LOCATIONS.ToList());
        }

        public int GetMaximum()
        {
            return LOCATIONS.Count();
        }

        #region sort
        internal List<string> getSortMethod()
        {
            return MySort.Keys.ToList();
        }

        protected override void SetSort(string method)
        {
            if (MySort.ContainsKey(method))
            {
                List<LOCATION> resultSort = (List<LOCATION>)MySort[method].DynamicInvoke();
                LOCATIONS = new ObservableCollection<LOCATION>(resultSort);
                DisplayObjects();
            }
        }
        private List<LOCATION> SetDescendingPositionAccordingToName()
        {
            return LOCATIONS.OrderBy(c => c.NAME).ToList();
        }

        private List<LOCATION> SetAscendingPositionAccordingToName()
        {
            return LOCATIONS.OrderByDescending(c => c.NAME).ToList();
        }

        private List<LOCATION> SetDefaultPosition()
        {
            return LOCATIONS.OrderBy(c => c.LOCATION_ID).ToList();
        }

        public void MakeSort(string method)
        {
            SetSort(method);
        }

        public void MakeSort(int method)
        {
            MakeSort(MySort.Keys.ToList()[method]);
        }
        #endregion

        #region Paging
        private ObservableCollection<LOCATION> _toShowItems;
        public ObservableCollection<LOCATION> ToShowItems
        {
            get => this._toShowItems;
            set
            {
                this._toShowItems = value;
                OnPropertyChanged();
            }
        }

        public override void DisplayObjects()
        {
            var page = this.SelectedIndex + 1;
            var skip = (page - 1) * this._paging.RowsPerPage;
            var take = this._paging.RowsPerPage;

            this.ToShowItems = new ObservableCollection<LOCATION>(this.LOCATIONS.Skip(skip).Take(take));
        }

        public bool getNewRowPerPage(int RowsPerPage) //được gọi trong setting
        {
            if (RowsPerPage > LOCATIONS.Count)
            {
                return false;
            }
            CalculatePagingInfo(RowsPerPage, LOCATIONS.Count);

            return true;
        }

        public int getRowsPerPage() //gọi lúc tắt app để lưu setting paging
        {
            return Paging.RowsPerPage;
        }
        #endregion

        internal void updateList(LOCATION newLocation)
        {
            if (instance != null)
            {
                instance.LOCATIONS.Add(newLocation);
                instance.DisplayObjects();
            }
        }
        public void searchLocation_ByName()
        {

            string request = HomeScreen.GetHomeScreenInstance().SearchTextBox.Text;
            //MessageBox.Show("Chay duoc roi");
            List<LOCATION> locationList;
            if (request.Length <= 0)
            {
                locationList = (HomeScreen.GetDatabaseEntities().LOCATIONS).ToList();
                //this.ToShowItems = new ObservableCollection<TRIP>(all);
            }
            else
            {
                //search by TITLE
                var requestText = convertUnicode.convertToUnSign(request.Trim().ToLower());
                locationList = (HomeScreen.GetDatabaseEntities().LOCATIONS.AsEnumerable().Where(loca => convertUnicode.convertToUnSign(loca.NAME.Trim().ToLower()).Contains(requestText))).ToList();

                //MessageBox.Show(b[0].TITTLE);
                //this.ToShowItems = new ObservableCollection<TRIP>(tripList);
            }
            instance.LOCATIONS = new ObservableCollection<LOCATION>(locationList);
            instance.CalculatePagingInfo(instance.Paging.RowsPerPage, instance.LOCATIONS.Count);
            instance.DisplayObjects();
        }
    }
}
