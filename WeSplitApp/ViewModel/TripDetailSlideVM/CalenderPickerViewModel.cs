using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WeSplitApp.ViewModel.TripDetailSlideVM
{
    public class CalenderPickerViewModel : ViewModel
    {
        public DateTime DisplayDate { get; set; }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => this._selectedDate;
            set
            {
                this._selectedDate = value;

                this.DisplayDate = this._selectedDate;
            }
        }
    }
}
