using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WeSplitApp.Utils;
using WeSplitApp.View;

namespace WeSplitApp.ViewModel
{
    class LocationListViewModel : PagingListObjects
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
           this.LOCATIONS = new ObservableCollection<LOCATION>(HomeScreen.GetDatabaseEntities().LOCATIONS.ToList());
            //TODO read from data.config
            int RowsPerPage = 5;

            CalculatePagingInfo(RowsPerPage, LOCATIONS.Count);
            SelectedIndex = 0;
            DisplayMembers();
        }

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

        public override void DisplayMembers()
        {
            var page = this.SelectedIndex + 1;
            var skip = (page - 1) * this._paging.RowsPerPage;
            var take = this._paging.RowsPerPage;

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

        public static int getRowsPerPage() //gọi lúc tắt app để lưu setting paging
        {
            return instance.Paging.RowsPerPage;
        }
        #endregion

        internal static void updateList(LOCATION newLocation)
        {
            if (instance != null)
            {
                instance.LOCATIONS.Add(newLocation);
                instance.DisplayMembers();
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
            instance.DisplayMembers();
        }
    }
}
