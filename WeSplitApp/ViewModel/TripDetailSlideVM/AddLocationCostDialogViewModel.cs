using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitApp.ViewModel.TripDetailSlideVM
{
    public class AddLocationCostDialogViewModel : AddCostToTripViewModel
    {
        public ObservableCollection<LOCATION> LocationList { get; set; }
        public LOCATION SelectedItem { get; set; }
        public LOCATION NewLocation { get; set; }
        public AddLocationCostDialogViewModel()
        {
            this.Message = "Thêm chi phí tại các địa điểm";
            this.NewLocation = new LOCATION();
        }
    }
}
