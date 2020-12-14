using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeSplitApp.Utils;
using WeSplitApp.View;

namespace WeSplitApp.ViewModel
{
   public class TripViewModel : SortMethodList
    {
        public bool IsDone { get; set; } = false;

        protected TripItemHandler _itemHandler;

        public TripItemHandler ItemHandler { get => this._itemHandler;
            set
            {
                this._itemHandler = value;
                OnPropertyChanged();
            }
        }

        public List<TRIP> Items {
            get => this.ItemHandler.Items;
            set
            {
                this.ItemHandler = new TripItemHandler(value);
                OnPropertyChanged();
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
                _itemHandler = new TripItemHandler(resultSort);
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
            var result2 =   result.OrderByDescending(c => c.TITTLE).ToList();
            return result;
        }

        private List<TRIP> SetAscendingPositionAccordingToName()
        {
            var result =  Items.OrderBy(c => c.TITTLE).ToList();
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

        private void ShowSelectedTrip(TRIP item)
        {
            HomeScreen.SetNavigationDrawerNavNull();
            HomeScreen.GetHomeScreenInstance().SetContentControl((new TripDetailsViewModel(item)));
        }

        /*        private void ShowSelectedTrip(TRIP trip)
                {
                    HomeScreen.SetNavigationDrawerNavNull();
                    var newTripViewModel = new TripDetailsViewModel
                    {
                        SelectedTrip = trip
                    };
                    HomeScreen.GetHomeScreenInstance().SetContentControl(newTripViewModel);
                }*/
        #endregion

        #region paging

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

        public override void DisplayObjects()
        {
            var page = this.SelectedIndex + 1;
            var skip = (page - 1) * this._paging.RowsPerPage;
            var take = this._paging.RowsPerPage;

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

        public virtual void search_byTripName() { }
        #endregion


        public void AddTrip(TRIP tRIP)
        {
            _itemHandler.Add(tRIP);
            DisplayObjects();
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
