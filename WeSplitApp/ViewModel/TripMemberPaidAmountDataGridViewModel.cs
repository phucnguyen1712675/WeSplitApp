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
        public ObservableCollection<SelectableViewModel> Items { get; set; }
        public TripMemberPaidAmountDataGridViewModel(TRIP trip) => Items = CreateData(trip);
        private ObservableCollection<SelectableViewModel> CreateData(TRIP trip)
        {
            var tempCollection = new ObservableCollection<SelectableViewModel>();

            foreach (var item in trip.TRIP_MEMBER)
            {
                tempCollection.Add(new SelectableViewModel
                {
                    Name = item.MEMBER.NAME,
                    PhoneNumber = item.MEMBER.PHONENUMBER.ToString(),
                    AmountPaid = item.AMOUNTPAID
                }); ;
            }

            return tempCollection;
        }
    }
}
