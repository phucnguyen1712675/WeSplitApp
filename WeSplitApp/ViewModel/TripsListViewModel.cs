﻿using System;
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
        public void search_byTripName()
        {
            string request = HomeScreen.GetHomeScreenInstance().SearchTextBox.Text;
            if (request.Length <= 0)
            {

            }
            else
            {
                //search by TITLE
                var requestText = convertToUnSign(request.Trim().ToLower());
                var b = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable().Where(t => convertToUnSign(t.TITTLE.Trim().ToLower()).Contains(requestText))).ToList();
                
                
                this.ToShowItems = new ObservableCollection<TRIP>(b);
            }
        }
        public string convertToUnSign(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }

        #endregion

    }
}
