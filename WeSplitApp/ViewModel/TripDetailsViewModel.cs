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
        private ICommand _backCommand { get; set; }
        public ICommand BackCommand => this._backCommand ?? (this._backCommand = new CommandHandler(() => MyBackAction(), () => CanExecute));
        public bool CanExecute
        {
            get
            {
                // check if executing is allowed, i.e., validate, check if a process is running, etc. 
                return true;
            }
        }

        public void MyBackAction()
        {
            var isDone = bool.Parse(SelectedTrip.ISDONE.ToString());
            if (isDone)
            {
                HomeScreen.GetHomeScreenInstance().SetContentControl(new HaveTakenTripsListViewViewModel());
            }
            else
            {
                HomeScreen.GetHomeScreenInstance().SetContentControl(new BeingTakenTripsListViewViewModel());
            }
        }

        public TripDetailsViewModel(TRIP trip)
        {
            SelectedTrip = trip;
            this.MISSINGAMOUNT = trip.CURRENTPROCEEDS - trip.TOTALCOSTS;
        }
    }
}
    