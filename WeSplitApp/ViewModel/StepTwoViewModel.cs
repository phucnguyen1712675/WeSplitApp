using MaterialDesignExtensions.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace WeSplitApp.ViewModel
{
    public class StepTwoViewModel : Step
    {
        public StepTwoViewModel():base(){ }

        public override void Validate()
        {
            bool ageOk = true;
            if (AddNewTripViewModel.Instance.AddTrip.TRIP_LOCATION.Count ==0) ageOk = false;
           // if (AddNewTripViewModel.Instance.AddTrip.TRIP_COSTINCURRED.Count == 0) ageOk = false;

            if (ageOk)
            {
                string totalcost = AddNewTripViewModel.Instance.AddTrip.TOTALCOSTS.ToString();
                HasValidationErrors = false;
            }
            else
            {
                HasValidationErrors = true;
                MessageBox.Show("Nhập thiếu trường dữ liệu");
            }
        }
    }
}