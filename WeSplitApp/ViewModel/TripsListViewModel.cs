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
    public abstract class TripsListViewModel : PagingListObjects
    {
        public abstract bool IsDone { get; set; }

       /* private int _selectedIndex;
        public int SelectedIndex
        {
            get => this._selectedIndex;
            set
            {
                this._selectedIndex = value;
                OnPropertyChanged();
                DisplayTrips();
            }
        }

        private ICommand _previousCommand { get; set; }
        public ICommand PreviousCommand => this._previousCommand ?? (this._previousCommand = new CommandHandler(() => MyPreviousAction(), () => CanExecute));
        private ICommand _nextCommand { get; set; }
        public ICommand NextCommand => this._nextCommand ?? (this._nextCommand = new CommandHandler(() => MyNextAction(), () => CanExecute));

        private void MyNextAction()
        {
            if (this.SelectedIndex < this._paging.TotalPages - 1)
            {
                this.SelectedIndex += 1;
            }
            else
            {
                MessageBox.Show("Maximum page!", "Reach Maximum page", MessageBoxButton.OKCancel);
            }
        }

        private void MyPreviousAction()
        {
            if (this.SelectedIndex > 0)
            {
                this.SelectedIndex -= 1;
            }
            else
            {
                MessageBox.Show("Minimum page!", "Reach Minimum page", MessageBoxButton.OKCancel);
            }
        }

        #endregion
        public bool CanExecute
        {
            get
            {
                return true;
            }
        }*/

        private static TripsListViewModel instance { get; set; }

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
        public TripsListViewModel()
        {
            this._itemHandler = GetData();

            //TODO read from data.config
            int RowsPerPage = 3;

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
        public static TripsListViewModel instanse { get; set; }
        public void search_byTripName(string typeSearch)
        {
            string request = HomeScreen.GetHomeScreenInstance().SearchTextBox.Text;
            MessageBox.Show(typeSearch);
            //switch (typeSearch)
            //{
            //    case "":
            //    case "Trip Title":
            //        var requestText = convertUnicode.convertToUnSign(request.Trim().ToLower());
            //        var tripList = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable().Where(t => convertUnicode.convertToUnSign(t.TITTLE.Trim().ToLower()).Contains(requestText) && t.ISDONE == true));

            //        //MessageBox.Show(b[0].TITTLE);
            //        this.ToShowItems = new ObservableCollection<TRIP>(tripList);
            //        break;
            //    case "Member Name":
            //        MessageBox.Show("By ten thanh vien");
            //        break;
            //    case "Location Name":
            //        MessageBox.Show("By dia diem");
            //        break;
            //    default:
            //        MessageBox.Show("liu liu do ngok");
            //        break;
            //}
            //if (request.Length <= 0)
            //{
            //    var all = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable().Where(t => t.ISDONE == true)).ToList();
            //    this.ToShowItems = new ObservableCollection<TRIP>(all);
            //}
            //else
            //{
            //    //search by TITLE
            //    var requestText = convertUnicode.convertToUnSign(request.Trim().ToLower());
            //    var tripList = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable().Where(t => convertUnicode.convertToUnSign(t.TITTLE.Trim().ToLower()).Contains(requestText) && t.ISDONE == true));

            //    //MessageBox.Show(b[0].TITTLE);
            //    this.ToShowItems = new ObservableCollection<TRIP>(tripList);
            //}
        }
    

        #endregion

    }
}
