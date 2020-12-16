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
using WeSplitApp.ViewModel.TripDetailSlideVM;

namespace WeSplitApp.ViewModel
{
    public class TripDetailsViewModel : ViewModel
    {
        private TRIP _selectedTrip;
        public TRIP SelectedTrip
        {
            get => this._selectedTrip;
            set
            {
                this._selectedTrip = value;

                this.Slide1_IntroViewModel = new Slide1_IntroViewModel
                {
                    SelectedTrip = value
                };
                this.Slide2_ProceedsViewModel = new Slide2_ProceedsViewModel 
                {
                    SelectedTrip = value
                };
                this.Slide3_TotalCostsViewModel = new Slide3_TotalCostsViewModel 
                {
                    SelectedTrip = value
                };
            }
        }
        public Slide1_IntroViewModel Slide1_IntroViewModel { get; set; }
        public Slide2_ProceedsViewModel Slide2_ProceedsViewModel { get; set; }
        public Slide3_TotalCostsViewModel Slide3_TotalCostsViewModel { get; set; }
    }
}
    