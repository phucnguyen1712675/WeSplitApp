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
    public class ImagePresenterViewModel : ViewModel
    {
        private Paging _paging = new Paging();

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
        public List<string> ImageCollection { get; set; }
        public ObservableCollection<string> ImageToShowCollection { get; set; }

        private string _selectedImage;
        public string SelectedImage
        {
            get => this._selectedImage;
            set
            {
                this._selectedImage = value;
                OnPropertyChanged();
            }
        }
        private ICommand _previousCommand { get; set; }
        public ICommand PreviousCommand => this._previousCommand ?? (this._previousCommand = new CommandHandler(() => MyPreviousAction(), () => CanExecute));
        private ICommand _nextCommand { get; set; }
        public ICommand NextCommand => this._nextCommand ?? (this._nextCommand = new CommandHandler(() => MyNextAction(), () => CanExecute));

        private void MyNextAction()
        {
            if (this.SelectedIndex < this.ImageCollection.Count - 1)
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

        public ImagePresenterViewModel(TRIP trip)
        {
            this.ImageCollection = HomeScreen.GetDatabaseEntities().TRIP_IMAGES
                                                                .Where(c => c.TRIP_ID == trip.TRIP_ID)
                                                                .Select(c => c.IMAGE).ToList();

            CalculatePagingInfo();
            DisplayTrips();
        }

        private void CalculatePagingInfo()
        {
            var count = this.ImageCollection.Count;
            var rowsPerPage = this._paging.RowsPerPage;

            // Tinh toan phan trang
            this._paging = new Paging()
            {
                CurrentPage = 1,
                TotalPages = count / rowsPerPage +
                    (((count % rowsPerPage) == 0) ? 0 : 1)
            };
            this.SelectedIndex = 0;
        }

        private void SetSelectedImage() => this.SelectedImage = this.ImageCollection[this.SelectedIndex];

        private void DisplayTrips()
        {
            if (this.SelectedIndex / this._paging.RowsPerPage == 1 || this.SelectedIndex == 0)
            {
                var page = this.SelectedIndex / this._paging.RowsPerPage + 1;
                var skip = (page - 1) * this._paging.RowsPerPage;
                var take = this._paging.RowsPerPage;

                this.ImageToShowCollection = new ObservableCollection<string>(this.ImageCollection.Skip(skip).Take(take).ToList());
            }
            SetSelectedImage();
        }
    }
}
