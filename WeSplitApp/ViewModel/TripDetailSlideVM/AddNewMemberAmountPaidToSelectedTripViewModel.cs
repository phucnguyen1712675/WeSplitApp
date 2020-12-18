using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitApp.ViewModel.TripDetailSlideVM
{
    public class AddNewMemberAmountPaidToSelectedTripViewModel : ViewModel
    {
        public ObservableCollection<MemberChipStyle> Members { get; set; }
    }
}
