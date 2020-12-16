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
using WeSplitApp.ViewModel;

namespace WeSplitApp.View.Controls
{
    /// <summary>
    /// Interaction logic for HaveTakenTripsListControl.xaml
    /// </summary>
    public partial class HaveTakenTripsListControl : UserControl
    {
        private static HaveTakenTripsListControl haveTakenTripList = null;

        public static HaveTakenTripsListControl GetInstance() => haveTakenTripList;

        public HaveTakenTripsListControl()
        {
            InitializeComponent();
            DataContext = HaveTakenTripsListViewModel.Instance;
            haveTakenTripList = this;
        }

        private void orderChange(object sender, SelectionChangedEventArgs e)
        {
            if(sortListBox != null)
            {
                sortListBox.SelectedIndex = 0;
                SettingsViewModel.Instance.getTripLoadSortMethod(0);
            }
        }

        private void OnSelected1(object sender, RoutedEventArgs e)
        {
            SettingsViewModel.Instance.getTripLoadSortMethod(0);
        }

        private void OnSelected2(object sender, RoutedEventArgs e)
        {
            if (SortComboBox.SelectedIndex == 0)
            {
                SettingsViewModel.Instance.getTripLoadSortMethod(1);
            }
            else
            {
                SettingsViewModel.Instance.getTripLoadSortMethod(2);
            }
        }

        private void OnSelected3(object sender, RoutedEventArgs e)
        {
            if (SortComboBox.SelectedIndex == 0)
            {
                SettingsViewModel.Instance.getTripLoadSortMethod(3);
            }
            else
            {
                SettingsViewModel.Instance.getTripLoadSortMethod(4);
            }
        }

        private void OnSelected4(object sender, RoutedEventArgs e)
        {

            if (SortComboBox.SelectedIndex == 0)
            {
                SettingsViewModel.Instance.getTripLoadSortMethod(5);
            }
            else
            {
                SettingsViewModel.Instance.getTripLoadSortMethod(6);
            }
        }


    }
}
 