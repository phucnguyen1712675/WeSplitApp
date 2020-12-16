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
        public ObservableCollection<LOCATION> LOCATIONS { get; set; }
        public PagingListObjects PagingListObjects { get; set; }
        public ObservableCollection<LOCATION> ToShowItems { get; set; }
        private LocationListViewModel()
        {
            MySort = new Dictionary<string, Delegate> {
                  { "Mặc định", new Func<List<LOCATION>>(SetDefaultPosition)},
            { "Tên tăng dần", new Func<List<LOCATION>>(SetAscendingPositionAccordingToName)},
            { "Tên giảm đần", new Func<List<LOCATION>>(SetDescendingPositionAccordingToName)}
            };

            this.LOCATIONS = new ObservableCollection<LOCATION>(HomeScreen.GetDatabaseEntities().LOCATIONS.ToList());
            this.searchLOCATIONS = this.LOCATIONS;
        }

        public int GetMaximum()
        {
            return searchLOCATIONS.Count();
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
                searchLOCATIONS = new ObservableCollection<LOCATION>(resultSort);
                DisplayObjects();
            }
        }
        private List<LOCATION> SetDescendingPositionAccordingToName()
        {
            return searchLOCATIONS.OrderByDescending(c => c.NAME).ToList();
        }
        private static LocationListViewModel instance = null;
        public static LocationListViewModel Instance => instance ?? (instance = new LocationListViewModel());

        private List<LOCATION> SetAscendingPositionAccordingToName()
        {
            return searchLOCATIONS.OrderBy(c => c.NAME).ToList();
        }

        private List<LOCATION> SetDefaultPosition()
        {
            return searchLOCATIONS.OrderBy(c => c.LOCATION_ID).ToList();
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

        public override void DisplayObjects()
        {
            var page = this.SelectedIndex + 1;
            var skip = (page - 1) * this.Paging.RowsPerPage;
            var take = this.Paging.RowsPerPage;

            this.ToShowItems = new ObservableCollection<LOCATION>(this.searchLOCATIONS.Skip(skip).Take(take));
        }
        public bool getNewRowPerPage(int RowsPerPage) //được gọi trong setting
        {
            if (RowsPerPage > searchLOCATIONS.Count)
            {
                return false;
            }
            CalculatePagingInfo(RowsPerPage, searchLOCATIONS.Count);

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
                LOCATIONS.Add(newLocation);
                DisplayObjects();
                if (LOCATIONS.Count % Paging.RowsPerPage == 1)
                    CalculatePagingInfo(Paging.RowsPerPage, LOCATIONS.Count, SelectedIndex);
            }
        }

        public ObservableCollection<LOCATION> searchLOCATIONS { get; set; }
        public void searchLocation_ByName()
        {
            string request = HomeScreen.GetHomeScreenInstance().SearchTextBox.Text;
            //MessageBox.Show("Chay duoc roi");
            List<LOCATION> locationList;
            if (request.Length <= 0)
            {
                locationList = (instance.LOCATIONS).ToList();
                //this.ToShowItems = new ObservableCollection<TRIP>(all);
            }
            else
            {
                //search by TITLE
                var requestText = convertUnicode.convertToUnSign(request.Trim().ToLower());
                locationList = (instance.LOCATIONS.AsEnumerable().Where(loca => convertUnicode.convertToUnSign(loca.NAME.Trim().ToLower()).Contains(requestText))).ToList();

                //MessageBox.Show(b[0].TITTLE);
                //this.ToShowItems = new ObservableCollection<TRIP>(tripList);
            }
            instance.searchLOCATIONS = new ObservableCollection<LOCATION>(locationList);
            instance.CalculatePagingInfo(instance.Paging.RowsPerPage, instance.searchLOCATIONS.Count);
            instance.DisplayObjects();
        }
    }
}
