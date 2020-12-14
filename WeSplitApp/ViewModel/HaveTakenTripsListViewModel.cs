using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WeSplitApp.Utils;
using WeSplitApp.View;

namespace WeSplitApp.ViewModel
{
    public class HaveTakenTripsListViewModel : PagingListObjects
    {
        public bool IsDone { get; set; }
        private static HaveTakenTripsListViewModel instance { get; set; }

        private ICommand _selectedCommand;
        public ICommand SelectedCommand => _selectedCommand ?? (_selectedCommand = new RelayCommand(x =>
        {
            ShowSelectedTrip(x as TRIP);
        }));
        public ObservableCollection<TRIP> ToShowItems { get; set; }
        public HaveTakenTripsListViewModel()
        {
            this.IsDone = true;
            this._itemHandler = GetData();
            //TODO read from data.config
            int RowsPerPage = 4;

            CalculatePagingInfo(RowsPerPage, Items.Count);
            DisplayObjects();
            instance = this;
        }
        private readonly TripItemHandler _itemHandler;
        public List<TRIP> Items => _itemHandler.Items;

        private void ShowSelectedTrip(TRIP trip)
        {
            HomeScreen.SetNavigationDrawerNavNull();
            var newTripViewModel = new TripDetailsViewModel
            {
                SelectedTrip = trip
            };
            HomeScreen.GetHomeScreenInstance().SetContentControl(newTripViewModel);
        }
        public TripItemHandler GetData() => new TripItemHandler(HomeScreen.GetDatabaseEntities().TRIPS.Where(t => t.ISDONE == this.IsDone)
                                                                                                      .Select(t => t)
                                                                                                      .ToList());
        public static void AddTrip(TRIP tRIP) => instance._itemHandler.Add(tRIP);
        
        #region paging

        public override void DisplayObjects()
        {
            var page = this.SelectedIndex + 1;
            var skip = (page - 1) * this.Paging.RowsPerPage;
            var take = this.Paging.RowsPerPage;

            this.ToShowItems = new ObservableCollection<TRIP>(this.Items.Skip(skip).Take(take));
        }

        public static bool getNewRowPerPage(int RowsPerPage) // được gọi trong setting
        {
            if (RowsPerPage > instance.Items.Count)
            {
                return false;
            }
            instance.CalculatePagingInfo(RowsPerPage, instance.Items.Count);

            return true;
        }

        public static int getRowsPerPage() => instance.Paging.RowsPerPage; //gọi lúc tắt app để lưu setting paging
        #endregion

        #region searching
        /*public void search_byTripName(string typeSearch)
        {
            string request = HomeScreen.GetHomeScreenInstance().SearchTextBox.Text;
            //MessageBox.Show(typeSearch);
            switch (typeSearch)
            {
                case "":
                case "Trip Title":
                    var requestText = convertUnicode.convertToUnSign(request.Trim().ToLower());
                    var tripList = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable().Where(t => convertUnicode.convertToUnSign(t.TITTLE.Trim().ToLower()).Contains(requestText) && t.ISDONE == true));

                    //MessageBox.Show(b[0].TITTLE);
                    this.ToShowItems = new ObservableCollection<TRIP>(tripList);
                    break;
                case "Member Name":
                    MessageBox.Show("By ten thanh vien");
                    break;
                case "Location Name":
                    MessageBox.Show("By dia diem");
                    break;
                default:
                    MessageBox.Show("liu liu do ngok");
                    break;
            }
            if (request.Length <= 0)
            {
                var all = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable().Where(t => t.ISDONE == true)).ToList();
                this.ToShowItems = new ObservableCollection<TRIP>(all);
            }
            else
            {
                //search by TITLE
                var requestText = convertUnicode.convertToUnSign(request.Trim().ToLower());
                var tripList = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable().Where(t => convertUnicode.convertToUnSign(t.TITTLE.Trim().ToLower()).Contains(requestText) && t.ISDONE == true));

                //MessageBox.Show(b[0].TITTLE);
                this.ToShowItems = new ObservableCollection<TRIP>(tripList);
            }
        }*/
        #endregion
    }
}
