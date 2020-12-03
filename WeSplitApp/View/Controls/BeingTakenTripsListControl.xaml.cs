using System;
using System.Collections.Generic;
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

namespace WeSplitApp.View.Controls
{
    /// <summary>
    /// Interaction logic for BeingTakenTripsListControl.xaml
    /// </summary>
    public partial class BeingTakenTripsListControl : UserControl
    {
        public BeingTakenTripsListControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            calculatePagingInfo();
        }

        private Paging _paging;
        private List<TRIP> _tripsList;
        private bool _isDone = false;

        private void pagingInfoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => DisplayTrips();

        void calculatePagingInfo()
        {
            var query = from t in HomeScreen.GetDatabaseEntities().TRIPS.ToList()
                        where t.ISDONE == this._isDone
                        select t;
            var count = query.Count();
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

            var query = from t in HomeScreen.GetDatabaseEntities().TRIPS.ToList()
                        where t.ISDONE == this._isDone
                        select t;

            this._tripsList = query
                .Skip(skip).Take(take)
                .ToList();
            this.tripListView.ItemsSource = this._tripsList;
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
    }
}
