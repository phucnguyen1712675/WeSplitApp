using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitApp.ViewModel.TripDetailSlideVM
{
    public abstract class AddCostToTripViewModel : ViewModel
    {
        public string Message { get; set; }
        public double Cost { get; set; }
        public int SelectedIndexListBox { get; set; }
        public int SelectedIndexComboBox { get; set; }
        public AddCostToTripViewModel()
        {
            this.Cost = 0.0;
            this.SelectedIndexListBox = 0;
            this.SelectedIndexComboBox = 0;
        }
    }
}
