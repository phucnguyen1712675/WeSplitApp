using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeSplitApp.View;

namespace WeSplitApp.ViewModel.DialogHelperClass
{
    class LocationAddDialogViewModel : AddDialogBaseViewModel
    {
        public LOCATION NewLocation { get; set; }

        public string Tittle { get; set; }

        public LocationAddDialogViewModel(LOCATION location, string tittle)
        {
            NewLocation = location;
            Tittle = tittle;
            if (location.LOCATION_ID == 0) type = "add";
            else type = "edit";
        }

        public override void YesCommandAction()
        {
            if (!string.IsNullOrEmpty(NewLocation.NAME)
                && !string.IsNullOrEmpty(NewLocation.ADDRESS)
                && !string.IsNullOrEmpty(NewLocation.DESCRIPTION))
            {
                if (NewLocation.TYPE == null) NewLocation.TYPE = false;

                if (type == "add")
                {
                    HomeScreen.GetDatabaseEntities().LOCATIONS.Add(NewLocation);

                    // trường hợp đang làm trong màn hình AddTrip <=> refresh list Locations
                    if (AddNewTripViewModel.Instance != null) { AddNewTripViewModel.Instance.LOCATIONs.Add(NewLocation); }
                    //trường hợp đang làm trong màn hình LocationList <=> refresh list Location
                    LocationListViewModel.Instance.updateList(NewLocation);
                }
                else
                {
                    LOCATION location = HomeScreen.GetDatabaseEntities().LOCATIONS.First(item => item.LOCATION_ID == NewLocation.LOCATION_ID);
                    location = NewLocation;
                }
                HomeScreen.GetDatabaseEntities().SaveChanges();
                SettingsViewModel.Instance.UpdateLocationSortMethod();
                MessageBox.Show("Thành công");
            }
            else
            {
                MessageBox.Show("Điền thiếu dữ liệu !");
            }
        }
    }
}
