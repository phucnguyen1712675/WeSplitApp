using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WeSplitApp.Utils;
using WeSplitApp.View;
using WeSplitApp.View.Controls;
namespace WeSplitApp.ViewModel
{
    public class HaveTakenTripsListViewModel : PagingListObjects
    {
        public bool IsDone { get; set; } = true;

        private static HaveTakenTripsListViewModel instance { get; set; }
        public static HaveTakenTripsListViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HaveTakenTripsListViewModel();
                }
                return instance;
            }
        }

        private readonly TripItemHandler _itemHandler;

        #region select view detail trip
        private ICommand _selectedCommand;
        public ICommand SelectedCommand => _selectedCommand ?? (_selectedCommand = new RelayCommand(x =>
        {
            ShowSelectedTrip(x as TRIP);
        }));

        private void ShowSelectedTrip(TRIP item)
        {
            HomeScreen.SetNavigationDrawerNavNull();
            HomeScreen.GetHomeScreenInstance().SetContentControl((new TripDetailsViewModel(item)));
        }
        #endregion

        public TripItemHandler GetData() => new TripItemHandler(HomeScreen.GetDatabaseEntities().TRIPS.Where(t => t.ISDONE == this.IsDone)
                                                                                                      .Select(t => t)
                                                                                                      .ToList());
        public HaveTakenTripsListViewModel()
        {
            this._itemHandler = GetData();

            //TODO read from data.config
            int RowsPerPage = 4;

            CalculatePagingInfo(RowsPerPage, Items.Count);
            DisplayObjects();
            instance = this;
        }

        public static void AddTrip(TRIP tRIP)
        {
            instance._itemHandler.Add(tRIP);
        }

        public List<TRIP> Items => _itemHandler.Items;

        private ObservableCollection<TRIP> _toShowItems;
        public ObservableCollection<TRIP> ToShowItems
        {
            get => this._toShowItems;
            set
            {
                this._toShowItems = value;
                OnPropertyChanged();
            }
        }

        #region paging

        public override void DisplayObjects()
        {
            var page = this.SelectedIndex + 1;
            var skip = (page - 1) * this._paging.RowsPerPage;
            var take = this._paging.RowsPerPage;

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

        public static int getRowsPerPage() //gọi lúc tắt app để lưu setting paging
        {
            return instance.Paging.RowsPerPage;
        }
        #endregion

        #region searching
        public ObservableCollection<TRIP> _searchResult;
        public ObservableCollection<TRIP> SearchResult
        {
            get => this._searchResult;
            set
            {
                this._searchResult = value;
                OnPropertyChanged();
            }
        }

        public void DisplayObjects_Search()
        {
            var page = this.SelectedIndex + 1;
            var skip = (page - 1) * this._paging.RowsPerPage;
            var take = this._paging.RowsPerPage;

            this.ToShowItems = new ObservableCollection<TRIP>(this.SearchResult.Skip(skip).Take(take));
        }
        public void search_byTripName()
        {
            string request = HomeScreen.GetHomeScreenInstance().SearchTextBox.Text;
            var requestText = convertUnicode.convertToUnSign(request.Trim().ToLower());
            string typeSearch = HaveTakenTripsListControl.GetInstance().SearchByComboBox.Text;
            //MessageBox.Show(typeSearch);
            List<TRIP> tripList = new List<TRIP>();
            //tripList = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable().Where(t => convertUnicode.convertToUnSign(t.TITTLE.Trim().ToLower()).Contains(request) && t.ISDONE == true).ToList());

            switch (typeSearch)
            {
                case "":
                case "Trip Title":
                    tripList = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable()
                        .Where(t => convertUnicode.convertToUnSign(t.TITTLE.Trim().ToLower()).Contains(requestText) && t.ISDONE == true)
                        .ToList());
                    break;
                case "Member Name":
                    // search member theo ten nhap vao 
                    var memlist = (HomeScreen.GetDatabaseEntities().MEMBERS.AsEnumerable()
                        .Where(mem => convertUnicode.convertToUnSign(mem.NAME.Trim().ToLower()).Contains(requestText))).ToList();

                    // tim chuyen di theo ten thanh vien
                    tripList.Clear();
                    foreach (var index in memlist)
                    {
                        //tim chuyen di co thanh vien index
                        var memberTripList = (from t in HomeScreen.GetDatabaseEntities().TRIPS
                                              join mem in HomeScreen.GetDatabaseEntities().TRIP_MEMBER on t.TRIP_ID equals mem.TRIP_ID
                                              where mem.MEMBER_ID == index.MEMBER_ID && t.ISDONE == true
                                              select t).ToList();
                        // kiem tra chuyen di da co trong List chua va them vao
                        foreach (TRIP trip in memberTripList)
                        {
                            if (tripList == null || !tripList.Contains(trip))
                            {
                                tripList.Add(trip);
                            }
                        }
                    }
                    break;
                case "Location Name":
                    MessageBox.Show("By dia diem");
                    break;
                default:
                    MessageBox.Show("liu liu do ngok");
                    break;
            }

            //Duoc xai
            instance.SearchResult = new ObservableCollection<TRIP>(tripList);
            instance.CalculatePagingInfo(instance.Paging.RowsPerPage, instance.SearchResult.Count);
            instance.DisplayObjects_Search();
      
        }


        #endregion
    }
}
