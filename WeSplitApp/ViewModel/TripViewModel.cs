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
    public class TripViewModel : SortMethodList
    {
        public bool IsDone { get; set; } = false;

        protected TripItemHandler _itemHandler;

        public TripItemHandler ItemHandler
        {
            get => this._itemHandler;
            set
            {
                this._itemHandler = value;
            }
        }

        public List<TRIP> Items
        {
            get => this.ItemHandler.Items;
            set
            {
                this.ItemHandler = new TripItemHandler(value);
            }
        }

        public TripItemHandler GetData() => new TripItemHandler(HomeScreen.GetDatabaseEntities().TRIPS.Where(t => t.ISDONE == this.IsDone)
                                                                                              .Select(t => t)
                                                                                              .ToList());

        public void TripSortMethods()
        {
            MySort = new Dictionary<string, Delegate> {
                  { "Mặc định", new Func<List<TRIP>>(SetDefaultPosition)},
                    { "Tên tăng dần", new Func<List<TRIP>>(SetAscendingPositionAccordingToName)},
                    { "Tên giảm đần", new Func<List<TRIP>>(SetDescendingPositionAccordingToName)},
                    { "Ngày đi tăng dần", new Func<List<TRIP>>(SetAscendingPositionAccordingToGoDate)},
                    { "Ngày đi giảm đần", new Func<List<TRIP>>(SetDescendingPositionAccordingToGoDate)},
                    { "Ngày về tăng dần", new Func<List<TRIP>>(SetAscendingPositionAccordingToReturnDate)},
                    { "Ngày về giảm đần", new Func<List<TRIP>>(SetDescendingPositionAccordingToReturnDate)}
                    };
        }

        #region Sort
        protected override void SetSort(string method)
        {
            if (MySort.ContainsKey(method))
            {
                List<TRIP> resultSort = (List<TRIP>)MySort[method].DynamicInvoke();
                ItemHandler = new TripItemHandler(resultSort);
                DisplayObjects();
            }
        }

        private List<TRIP> SetDescendingPositionAccordingToReturnDate()
        {
            return Items.OrderByDescending(c => c.RETURNDATE).ToList();
        }

        private List<TRIP> SetAscendingPositionAccordingToReturnDate()
        {
            return Items.OrderBy(c => c.RETURNDATE).ToList();
        }

        private List<TRIP> SetDescendingPositionAccordingToGoDate()
        {
            return Items.OrderByDescending(c => c.TOGODATE).ToList();
        }

        private List<TRIP> SetAscendingPositionAccordingToGoDate()
        {
            return Items.OrderBy(c => c.TOGODATE).ToList();
        }

        private List<TRIP> SetDescendingPositionAccordingToName()
        {
            var result = Items;
            var result2 = result.OrderByDescending(c => c.TITTLE).ToList();
            return result;
        }

        private List<TRIP> SetAscendingPositionAccordingToName()
        {
            var result = Items.OrderBy(c => c.TITTLE).ToList();
            return result;
        }

        private List<TRIP> SetDefaultPosition()
        {
            return Items.OrderBy(c => c.TRIP_ID).ToList();
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

        #region select view detail trip
        private ICommand _selectedCommand;
        public ICommand SelectedCommand => _selectedCommand ?? (_selectedCommand = new RelayCommand(x =>
        {
            ShowSelectedTrip(x as TRIP);
        }));

        private void ShowSelectedTrip(TRIP trip)
        {
            HomeScreen.SetNavigationDrawerNavNull();
            var newTripViewModel = new TripDetailsViewModel
            {
                SelectedTrip = trip
            };
            HomeScreen.GetHomeScreenInstance().SetContentControl(newTripViewModel);
        }
        #endregion

        #region paging

        private ObservableCollection<TRIP> _toShowItems;
        public ObservableCollection<TRIP> ToShowItems
        {
            get => this._toShowItems;
            set
            {
                this._toShowItems = value;

            }
        }

        public override void DisplayObjects()
        {
            var page = this.SelectedIndex + 1;
            var skip = (page - 1) * this.Paging.RowsPerPage;
            var take = this.Paging.RowsPerPage;

            this.ToShowItems = new ObservableCollection<TRIP>(this.Items.Skip(skip).Take(take));
        }

        public bool getNewRowPerPage(int RowsPerPage) // được gọi trong setting
        {
            if (RowsPerPage > Items.Count)
            {
                return false;
            }
            CalculatePagingInfo(RowsPerPage, Items.Count);
            SelectedIndex = 0;
            DisplayObjects();
            return true;
        }
        #endregion

        #region search
        public ObservableCollection<TRIP> _searchResult;
        public ObservableCollection<TRIP> SearchResult
        {
            get => this._searchResult;
            set
            {
                this._searchResult = value;
            }
        }

        public void DisplayObjects_Search()
        {
            var page = this.SelectedIndex + 1;
            var skip = (page - 1) * this.Paging.RowsPerPage;
            var take = this.Paging.RowsPerPage;

            this.ToShowItems = new ObservableCollection<TRIP>(this.SearchResult.Skip(skip).Take(take));
        }

        public void search_byTripName()
        {
            string request = HomeScreen.GetHomeScreenInstance().SearchTextBox.Text;
            var requestText = convertUnicode.convertToUnSign(request.Trim().ToLower());
            string typeSearch = HomeScreen.GetHomeScreenInstance().SearchByComboBox.Text;
            //if (IsDone)
            //{
            //    typeSearch = HaveTakenTripsListControl.GetInstance().SearchByComboBox.Text;
            //}
            //else
            //{
            //    typeSearch = BeingTakenTripsListControl.GetInstance().SearchByComboBox.Text;
            //}
            //MessageBox.Show(typeSearch);
            List<TRIP> tripList = new List<TRIP>();

            switch (typeSearch)
            {
                case "Tên chuyến đi":
                    //tripList = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable()
                    //    .Where(t => convertUnicode.convertToUnSign(t.TITTLE.Trim().ToLower()).Contains(requestText) && t.ISDONE == IsDone)
                    //    .ToList());
                    tripList = (ItemHandler.Items.AsEnumerable()
                        .Where(t => convertUnicode.convertToUnSign(t.TITTLE.Trim().ToLower()).Contains(requestText) && t.ISDONE == IsDone)
                        .ToList());
                    
                    //MessageBox.Show(b[0].TITTLE);
                    break;
                case "Tên thành viên":
                    // search member theo ten nhap vao 
                    var memlist = (HomeScreen.GetDatabaseEntities().MEMBERS.AsEnumerable()
                        .Where(mem => convertUnicode.convertToUnSign(mem.NAME.Trim().ToLower()).Contains(requestText))).ToList();
                    tripList.Clear();
                    // tim chuyen di theo ten thanh vien
                    foreach (var index in memlist)
                    {
                        //tim chuyen di co thanh vien index
                        var memberTripList = (from t in ItemHandler.Items
                                              join mem in HomeScreen.GetDatabaseEntities().TRIP_MEMBER on t.TRIP_ID equals mem.TRIP_ID
                                              where mem.MEMBER_ID == index.MEMBER_ID && t.ISDONE == IsDone
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
                case "Tên địa điểm":
                    // search member theo ten nhap vao 
                    var locationList = (HomeScreen.GetDatabaseEntities().LOCATIONS.AsEnumerable()
                        .Where(loca => convertUnicode.convertToUnSign(loca.NAME.Trim().ToLower()).Contains(requestText))).ToList();
                    tripList.Clear();
                    // tim chuyen di theo ten thanh vien
                    foreach (var index in locationList)
                    {
                        //tim chuyen di co thanh vien index
                        var locationTripList = (from t in ItemHandler.Items
                                                join loca in HomeScreen.GetDatabaseEntities().TRIP_LOCATION on t.TRIP_ID equals loca.TRIP_ID
                                              where loca.LOCATION_ID == index.LOCATION_ID && t.ISDONE == IsDone
                                              select t).ToList();
                        // kiem tra chuyen di da co trong List chua va them vao
                        foreach (TRIP trip in locationTripList)
                        {
                            if (tripList == null || !tripList.Contains(trip))
                            {
                                tripList.Add(trip);
                            }
                        }

                    }
                    break;
                default:
                    MessageBox.Show("Chưa chọn loại tìm kiếm");
                    break;
            }
            SearchResult = new ObservableCollection<TRIP>(tripList);
            CalculatePagingInfo(Paging.RowsPerPage, SearchResult.Count);
            DisplayObjects_Search();

        }
        #endregion


        public void AddTrip(TRIP tRIP)
        {
            ItemHandler.Add(tRIP);
            DisplayObjects();
            if (Items.Count % Paging.RowsPerPage == 1)
                CalculatePagingInfo(Paging.RowsPerPage, Items.Count, SelectedIndex);
        }

        internal int GetMaxiMum()
        {
            return Items.Count();
        }

        public List<string> getSortMethod()
        {
            return MySort.Keys.ToList();
        }
    }
}
