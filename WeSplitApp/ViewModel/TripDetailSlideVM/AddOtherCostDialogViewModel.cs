using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitApp.ViewModel.TripDetailSlideVM
{
    public class AddOtherCostDialogViewModel : AddCostToTripViewModel
    {
        public ObservableCollection<COSTINCURRED> CostIncurredList { get; set; }
        public COSTINCURRED SelectedItem { get; set; }
        public COSTINCURRED NewCostIncurred { get; set; }
        public bool IsEnabledSuggestion { get; set; }
        public AddOtherCostDialogViewModel()
        {
            this.Message = "Thêm chi phí khác";
            this.NewCostIncurred = new COSTINCURRED();
        }
    }
}
