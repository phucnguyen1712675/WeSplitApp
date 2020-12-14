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
                this.SelectedIndex = 0;
                CalculatePagingInfo();
                DisplayTrips();
            }
        }
        public int SelectedIndex { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int RowsPerPage { get; set; } = 4;
        public int ImagesCount => this.SelectedTrip.TRIP_IMAGES.Count;
        public List<TRIP_IMAGES> ImageToShowCollection { get; set; }
        private ICommand _previousCommand { get; set; }
        public ICommand PreviousCommand => this._previousCommand ?? (this._previousCommand = new CommandHandler(() => MyPreviousAction(), () => CanExecute));
        private ICommand _nextCommand { get; set; }
        public ICommand NextCommand => this._nextCommand ?? (this._nextCommand = new CommandHandler(() => MyNextAction(), () => CanExecute));
        private void CalculatePagingInfo()
        {
            var count = this.SelectedTrip.TRIP_IMAGES.Count;
            this.CurrentPage = 1;
            this.TotalPage = count / this.RowsPerPage + (((count % this.RowsPerPage) == 0) ? 0 : 1);
        }
        private bool CanGoToTheNextPage() => this.CurrentPage < this.TotalPage && this.SelectedIndex == this.RowsPerPage - 1;
        private bool CanGoToThePreviousPage() => this.CurrentPage > 1 && this.SelectedIndex == 0;
        private void DisplayTrips()
        {
            var skip = (this.CurrentPage - 1) * this.RowsPerPage;
            var left = ImagesCount - skip;
            var take = this.RowsPerPage <= left ? this.RowsPerPage : left;
            var list = this.SelectedTrip.TRIP_IMAGES.Skip(skip).Take(take).ToList();
            this.ImageToShowCollection = list;
        }
        private void MyNextAction()
        {
            if (CanGoToTheNextPage())
            {
                this.CurrentPage += 1;
                DisplayTrips();
                this.SelectedIndex = 0;
            }
            else
            {
                this.SelectedIndex += 1;
            }
        }
        private void MyPreviousAction()
        {
            if (CanGoToThePreviousPage())
            {
                this.CurrentPage -= 1;
                DisplayTrips();
                this.SelectedIndex = this.RowsPerPage - 1;
            }
            else
            {
                this.SelectedIndex -= 1;
            }
        }
        public bool CanExecute => true;
    }
}
