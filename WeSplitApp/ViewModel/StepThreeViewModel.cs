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
        public override void Validate()
        {
            bool ageOk = true;
            if (AddNewTripViewModel.Instance.AddTrip.TRIP_MEMBER.Count == 0) ageOk = false;

            if (ageOk)
            {
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
            bool ageOk = AddNewTripViewModel.Instance.AddTrip.TRIP_MEMBER.Count != 0;
            if (ageOk)
            {
                TRIP temp = AddNewTripViewModel.Instance.AddTrip;
                temp.ISDONE = temp.RETURNDATE < DateTime.Now;
                HomeScreen.GetDatabaseEntities().TRIPS.Add(temp);
                HomeScreen.GetDatabaseEntities().SaveChanges();
                if ((bool)temp.ISDONE)
                {
                    HaveTakenTripsListViewModel.Instance.AddTrip(temp);
                }
                else
                {
                    BeingTakenTripsListViewModel.Instance.AddTrip(temp);
                }
                SettingsViewModel.Instance.UpdateTripSortMethod();
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
