using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeSplitApp.Utils;
using WeSplitApp.ViewModel;

namespace WeSplitApp.View.Controls
{
    /// <summary>
    /// Interaction logic for TripsListViewControl.xaml
    /// </summary>
    public partial class HaveTakenTripsListViewControl : UserControl
    {
        public HaveTakenTripsListViewControl() => InitializeComponent();

        private Paging _paging;
        private List<TRIP> _tripList;
        private List<TRIP> _tripToShowList;
        private bool _isDone = true;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this._tripList = GetTripList();
            calculatePagingInfo();
        }

        private List<TRIP> GetTripList() => HomeScreen.GetDatabaseEntities().TRIPS
                                                        .Where(t=> t.ISDONE == this._isDone)
                                                        .Select(t => t)
                                                        .ToList();

        private void pagingInfoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => DisplayTrips();

        private void calculatePagingInfo()
        {
            var count = this._tripList.Count();
            var rowsPerPage = 8;

            // Tinh toan phan trang
            _paging = new Paging()
            {
                CurrentPage = 1,
                TotalPages = count / rowsPerPage +
                    (((count % rowsPerPage) == 0) ? 0 : 1)
            };

            pagingInfoComboBox.ItemsSource = _paging.Pages;
            pagingInfoComboBox.SelectedIndex = 0;
        }

        private void DisplayTrips()
        {
            var page = pagingInfoComboBox.SelectedIndex + 1;
            var skip = (page - 1) * this._paging.RowsPerPage;
            var take = this._paging.RowsPerPage;

            this._tripToShowList = this._tripList.Skip(skip)
                                                .Take(take)
                                                .ToList();

            this.tripListView.ItemsSource = this._tripToShowList;
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            if (pagingInfoComboBox.SelectedIndex > 0)
            {
                pagingInfoComboBox.SelectedIndex -= 1;
            }
            else
            {
                MessageBox.Show("Minimum page!", "Reach Minimum page", MessageBoxButton.OKCancel);
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (pagingInfoComboBox.SelectedIndex < pagingInfoComboBox.Items.Count - 1)
            {
                pagingInfoComboBox.SelectedIndex += 1;
            }
            else
            {
                MessageBox.Show("Maximum page!", "Reach Maximum page", MessageBoxButton.OKCancel);
            }
        }

        private void tripListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = this.tripListView.SelectedItem as TRIP;
            HomeScreen.GetHomeScreenInstance().SetContentControl((new TripDetailsViewModel(selectedItem)));
            //HomeScreen.GetHomeScreenInstance().NotButtonClickAction(new TripDetailsViewModel(selectedItem));
        }
    }
}
