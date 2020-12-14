using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WeSplitApp.Utils;
using WeSplitApp.View;

namespace WeSplitApp.ViewModel
{
    class LocationListViewModel : PagingListObjects
    {
        public ObservableCollection<LOCATION> LOCATIONS { get; set; }
        public PagingListObjects PagingListObjects { get; set; }
        public ObservableCollection<LOCATION> ToShowItems { get; set; }
        private LocationListViewModel()
        {
            this.LOCATIONS = new ObservableCollection<LOCATION>(HomeScreen.GetDatabaseEntities().LOCATIONS.ToList());
            //TODO read from data.config
            int RowsPerPage = 5;

            CalculatePagingInfo(RowsPerPage, LOCATIONS.Count);
            SelectedIndex = 0;
            DisplayObjects();
        }
        private static LocationListViewModel instance = null;
        public static LocationListViewModel Instance => instance ?? (instance = new LocationListViewModel());

        #region Paging

        public override void DisplayObjects()
        {
            var page = this.SelectedIndex + 1;
            var skip = (page - 1) * this.Paging.RowsPerPage;
            var take = this.Paging.RowsPerPage;

            this.ToShowItems = new ObservableCollection<LOCATION>(this.LOCATIONS.Skip(skip).Take(take));
        }
        public static bool getNewRowPerPage(int RowsPerPage) //được gọi trong setting
        {
            if (RowsPerPage > instance.LOCATIONS.Count)
            {
                return false;
            }
            instance.CalculatePagingInfo(RowsPerPage, instance.LOCATIONS.Count);

            return true;
        }
        public static int getRowsPerPage() => instance.Paging.RowsPerPage; //gọi lúc tắt app để lưu setting paging
        #endregion

        internal static void updateList(LOCATION newLocation)
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
