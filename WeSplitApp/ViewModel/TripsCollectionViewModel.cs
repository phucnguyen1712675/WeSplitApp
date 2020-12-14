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
        public int SelectedIndex { get; set; }
        public HaveTakenTripsListViewModel HaveTakenTripsListViewModel { get; set; }
        public BeingTakenTripsListViewModel BeingTakenTripsListViewModel { get; set; }
        public TripsCollectionViewModel()
        {
            this.SelectedIndex = 0;
            // index = SelectedIndex;
            this.HaveTakenTripsListViewModel = new HaveTakenTripsListViewModel();
            this.BeingTakenTripsListViewModel = new BeingTakenTripsListViewModel();
        }
    }
}
