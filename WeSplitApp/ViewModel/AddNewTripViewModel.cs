using MaterialDesignExtensions.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitApp.ViewModel
{
    public class AddNewTripViewModel : StepperBaseViewModel
    {
        public List<IStep> Steps
        {
            get
            {
                return new List<IStep>()
                {
                    new Step() { Header = new StepTitleHeader() { FirstLevelTitle = "Thông tin chuyến đi" }, Content = new StepOneViewModel() },
                    new Step() { Header = new StepTitleHeader() { FirstLevelTitle = "Thành viên" }, Content = new StepTwoViewModel() },
                    new Step() { Header = new StepTitleHeader() { FirstLevelTitle = "Điểm dừng", SecondLevelTitle = "Header and content" }, Content = new StepThreeViewModel() }
                };
            }
        }

        public AddNewTripViewModel() : base() { }
    }
}
