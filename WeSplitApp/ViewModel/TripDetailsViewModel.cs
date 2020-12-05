using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WeSplitApp.Utils;
using WeSplitApp.View;

namespace WeSplitApp.ViewModel
{
    public class TripDetailsViewModel : ViewModel
    {
        public double MISSINGAMOUNT { get; set; }
        public static TRIP SelectedTrip { get; set; }
       

        public TripDetailsViewModel(TRIP trip)
        {
            SelectedTrip = trip;
            this.MISSINGAMOUNT = trip.CURRENTPROCEEDS - trip.TOTALCOSTS;
        }
    }
}
    