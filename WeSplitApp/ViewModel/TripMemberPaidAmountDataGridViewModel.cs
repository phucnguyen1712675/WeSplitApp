using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitApp.ViewModel
{

    public class TripMemberPaidAmountDataGridViewModel : ViewModel
    {
        private TRIP _selectedTrip;
        public TRIP SelectedTrip
        {
            get => this._selectedTrip;
            set
            {
                this._selectedTrip = value;
                //OnPropertyChanged();
            }
        }
        public ObservableCollection<SelectableViewModel> Items { get; set; }
        public TripMemberPaidAmountDataGridViewModel()
        {
            foreach (var item in this.SelectedTrip.TRIP_MEMBER)
            {
                Items.Add(new SelectableViewModel
                {
                    Name = item.MEMBER.NAME,
                    PhoneNumber = item.MEMBER.PHONENUMBER.ToString(),
                    AmountPaid = item.AMOUNTPAID
                }); ;
            }
        }
    }
}
