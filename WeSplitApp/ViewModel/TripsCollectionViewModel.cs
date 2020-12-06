using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeSplitApp.View;
namespace WeSplitApp.ViewModel
{
    public class TripsCollectionViewModel : ViewModel
    {
        private int _selectedIndex;
        public int SelectedIndex
        {
            get => this._selectedIndex;
            set
            {
                this._selectedIndex = value;
                OnPropertyChanged();
                index = SelectedIndex;
                //Goi ham gi do khi Change
                //hamGiDo();
                HomeScreen.GetHomeScreenInstance().SearchTextBox.Text = String.Empty;
                
                if (index == 0)
                {
                    TripsListViewModel.instanse.search_byTripName();
                }
                else
                {
                    ExpectedTripListViewModel.instanse.search_byTripName();

                }

            }
        }
        public TripsCollectionViewModel()
        {
            this._selectedIndex = 0;
            index = SelectedIndex;
        }
        public static int index;
    }
}
