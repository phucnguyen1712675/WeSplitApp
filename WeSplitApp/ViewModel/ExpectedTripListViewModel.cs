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

namespace WeSplitApp.ViewModel
{
    public abstract class ExpectedTripListViewModel : ViewModel
    {
        private Paging _paging = new Paging();

        public abstract bool IsDone { get; set; }

        private int _selectedIndex;
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
        public bool CanExecute
        {
            get
            {
                // check if executing is allowed, i.e., validate, check if a process is running, etc. 
                return true;
            }
        }


        private readonly TripItemHandler _itemHandler;

        private ICommand _selectedCommand;
        public ICommand SelectedCommand => _selectedCommand ?? (_selectedCommand = new RelayCommand(x =>
        {
            ShowSelectedTrip(x as TRIP);
        }));
        private void ShowSelectedTrip(TRIP item) => HomeScreen.GetHomeScreenInstance().SetContentControl((new TripDetailsViewModel(item)));

        public TripItemHandler GetData() => new TripItemHandler(HomeScreen.GetDatabaseEntities().TRIPS.Where(t => t.ISDONE == this.IsDone)
                                                                                                      .Select(t => t)
                                                                                                      .ToList());
        public ExpectedTripListViewModel()
        {
            this._itemHandler = GetData();
            CalculatePagingInfo();
            instanse = this;
        }
        public List<TRIP> Items => _itemHandler.Items;

        private ObservableCollection<TRIP> _ToShowItems1;
        public ObservableCollection<TRIP> ToShowItems1
        {
            get => this._ToShowItems1;
            set
            {
                this._ToShowItems1 = value;
                OnPropertyChanged();
            }
        }

        private void CalculatePagingInfo()
        {
            var count = this.Items.Count();
            var rowsPerPage = this._paging.RowsPerPage;

            // Tinh toan phan trang
            _paging = new Paging()
            {
                CurrentPage = 1,
                TotalPages = count / rowsPerPage +
                    (((count % rowsPerPage) == 0) ? 0 : 1)
            };
            this.SelectedIndex = 0;
        }

        private void DisplayTrips()
        {
            var page = this.SelectedIndex + 1;
            var skip = (page - 1) * this._paging.RowsPerPage;
            var take = this._paging.RowsPerPage;

            this.ToShowItems1 = new ObservableCollection<TRIP>(this.Items.Skip(skip)
                                                                        .Take(take)
                                                                        .ToList());
        }
        public static ExpectedTripListViewModel instanse { get; set; }
        public void search_byTripName()
        {
            string request = HomeScreen.GetHomeScreenInstance().SearchTextBox.Text;
            if (request.Length <= 0)
            {
                var all = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable().Where(t => t.ISDONE == false)).ToList();
                this.ToShowItems1 = new ObservableCollection<TRIP>(all);
            }
            else
            {
                //search by TITLE
                var requestText = convertUnicode.convertToUnSign(request.Trim().ToLower());
                var expectedTripList = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable().Where(t => convertUnicode.convertToUnSign(t.TITTLE.Trim().ToLower()).Contains(requestText)&& t.ISDONE==false)).ToList();
                //MessageBox.Show(b[0].TITTLE);
                this.ToShowItems1 = new ObservableCollection<TRIP>(expectedTripList);
            }
        }
    }
}
