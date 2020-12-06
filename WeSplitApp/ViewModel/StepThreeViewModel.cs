using MaterialDesignExtensions.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WeSplitApp.View;

namespace WeSplitApp.ViewModel
{
    public class StepThreeViewModel : Step
    {
        //public static Dictionary<MEMBER, string> Trip_Members = new Dictionary<MEMBER, string>();
        public StepThreeViewModel() {}
        public override void Validate()
        {
            bool ageOk = true;
            if (AddNewTripViewModel.Instance.AddTrip.TRIP_MEMBER.Count == 0) ageOk = false;

            if (ageOk) {
                HasValidationErrors = false;
            }
            else
            {
                HasValidationErrors = true;
                MessageBox.Show("Nhập thiếu trường dữ liệu");
            }
        }

        public static bool isAddOk()
        {
            bool ageOk = true;
            if (AddNewTripViewModel.Instance.AddTrip.TRIP_MEMBER.Count == 0) ageOk = false;
            if (ageOk)
            {

                TRIP temp = AddNewTripViewModel.Instance.AddTrip;
                if (temp.RETURNDATE < DateTime.Now) { 
                    temp.ISDONE = true;
                }
                else temp.ISDONE = false;
                HomeScreen.GetDatabaseEntities().TRIPS.Add(temp);
                HomeScreen.GetDatabaseEntities().SaveChanges();
                if (temp.ISDONE == true)
                {
                    HaveTakenTripsListViewModel.AddTrip(temp);

                }
                else
                {
                    BeingTakenTripsListViewModel.AddTrip(temp);
                }
                MessageBox.Show("Thêm thành công");
            }
            else
            {
                MessageBox.Show("Nhập thiếu trường dữ liệu");
            }
            return ageOk;
        }
    }
}
