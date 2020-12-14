using MaterialDesignExtensions.Controls;
using MaterialDesignThemes.Wpf;
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

namespace WeSplitApp.ViewModel.TripDetailSlideVM
{
    public class ImagePresenterViewModel : ViewModel
    {
        private TRIP _selectedTrip;
        public TRIP SelectedTrip
        {
            get => this._selectedTrip;
            set
            {
                this._selectedTrip = value;
                CalculatePagingInfo();
                DisplayTrips();
            }
        }
        private int _selectedIndex;
        public int SelectedIndex
        {
            get => this._selectedIndex;
            set
            {
                if(this._selectedIndex < value)
                {
                    this._selectedIndex = value;
                    if (CanGoToTheNextPage())
                    {
                        DisplayTrips();
                    }
                }
                else
                {
                    this._selectedIndex = value;
                    if (CanGoToThePreviousPage())
                    {
                        DisplayTrips();
                    }
                }
            }
        }


        private ObservableCollection<TRIP_IMAGES> _imageToShowCollection;
        public ObservableCollection<TRIP_IMAGES> ImageToShowCollection 
        {
            get => this._imageToShowCollection;
            set {
                this._selectedIndex = 0;
                this._imageToShowCollection = value;
                this._selectedIndex = this._selectedIndex % value.Count;
            }
        }
        private ICommand _previousCommand { get; set; }
        public ICommand PreviousCommand => this._previousCommand ?? (this._previousCommand = new CommandHandler(() => MyPreviousAction(), () => CanExecute));
        private ICommand _nextCommand { get; set; }
        public ICommand NextCommand => this._nextCommand ?? (this._nextCommand = new CommandHandler(() => MyNextAction(), () => CanExecute));
        //public ICommand RunAddNewImagesCommand => new AnotherCommandImplementation(ExecuteAddNewImageDialog);

        public ImagePresenterViewModel()
        {
            this._paging = new Paging();
            this.SelectedIndex = 0;
        }

        private Paging _paging;
        private void CalculatePagingInfo()
        {
            var count = this.SelectedTrip.TRIP_IMAGES.Count;
            var rowsPerPage = this._paging.RowsPerPage;

            // Tinh toan phan trang
            this._paging = new Paging()
            {
                CurrentPage = 1,
                TotalPages = count / rowsPerPage +
                    (((count % rowsPerPage) == 0) ? 0 : 1)
            };
        }
        //private void SetSelectedImage() => this.SelectedImage = this.ImageCollection[this.SelectedIndex];
        //private void SetSelectedImage() => this.SelectedImage = this.ImageToShowCollection[this.SelectedIndex % this._paging.RowsPerPage];
        private bool CanGoToTheNextPage() => this.SelectedIndex % this._paging.RowsPerPage == 0;
        private bool CanGoToThePreviousPage() => this.SelectedIndex % this._paging.RowsPerPage == this._paging.RowsPerPage - 1;

        private void DisplayTrips()
        {
            var page = this.SelectedIndex / this._paging.RowsPerPage + 1;
            var skip = (page - 1) * this._paging.RowsPerPage;
            var left = this.SelectedTrip.TRIP_IMAGES.Count - skip;
            var take = this._paging.RowsPerPage <= left ? this._paging.RowsPerPage : left;
            this.ImageToShowCollection = new ObservableCollection<TRIP_IMAGES>(this.SelectedTrip.TRIP_IMAGES.Skip(skip).Take(take).ToList());
        }
        private void MyNextAction() => this.SelectedIndex += 1;
        private void MyPreviousAction() => this.SelectedIndex -= 1;
        public bool CanExecute => true;
    }
}   
