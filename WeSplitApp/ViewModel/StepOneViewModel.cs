using MaterialDesignExtensions.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace WeSplitApp.ViewModel
{
    public class StepOneViewModel : Step
    {
        public StepOneViewModel() : base() { }

        public override void Validate()
        {
            bool ageOk = true;

            string Name = AddNewTripViewModel.Instance.AddTrip.TITTLE;
            string Description = AddNewTripViewModel.Instance.AddTrip.DESCRIPTION;
            string Image = AddNewTripViewModel.Instance.AddTrip.THUMNAILIMAGE;
            string ToGoDate = AddNewTripViewModel.Instance.AddTrip.TOGODATE != null ? ((DateTime)AddNewTripViewModel.Instance.AddTrip.TOGODATE).ToString() : null;
            string ReturnDate = AddNewTripViewModel.Instance.AddTrip.RETURNDATE != null ? ((DateTime)AddNewTripViewModel.Instance.AddTrip.RETURNDATE).ToString() : null;
            ObservableCollection<TRIP_IMAGES> Trip_Images = AddNewTripViewModel.Instance.AddTrip.TRIP_IMAGES;

            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(Image)
                 || string.IsNullOrEmpty(ToGoDate) || string.IsNullOrEmpty(ReturnDate) || Trip_Images == null)
                ageOk = false;

            
            if (ageOk)
            {
                if (Convert.ToDateTime(ReturnDate) < Convert.ToDateTime(ToGoDate))
                {

                    HasValidationErrors = true;
                    MessageBox.Show("Ngày về không thể sau ngày đi");
                }
                else
                {
                    foreach (var item in AddNewTripViewModel.Instance.AddTrip.TRIP_IMAGES)
                    {
                        if (item.IMAGE.Contains(AppDomain.CurrentDomain.BaseDirectory))
                        {
                            item.IMAGE = "\\" + item.IMAGE.Remove(0, AppDomain.CurrentDomain.BaseDirectory.Length);
                        }
                    }
                    HasValidationErrors = false;
                }
            }
            else
            {
                HasValidationErrors = true;
                MessageBox.Show("Nhập thiếu trường dữ liệu");
            }
        }
    }
}