using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSplitApp.Model;

namespace WeSplitApp.ViewModel
{
    public class HaveTakenTripsListViewViewModel : ViewModel
    {
        public ObservableCollection<TripsModel> HaveTakenTripsList { get; set; } 
    }
}
