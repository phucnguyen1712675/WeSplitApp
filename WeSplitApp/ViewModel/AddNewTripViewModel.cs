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
                    new Step() { Header = new StepTitleHeader() { FirstLevelTitle = "What is a Stepper?" }, Content = new StepOneViewModel() },
                    new Step() { Header = new StepTitleHeader() { FirstLevelTitle = "Layout and navigation" }, Content = new StepTwoViewModel() },
                    new Step() { Header = new StepTitleHeader() { FirstLevelTitle = "Steps", SecondLevelTitle = "Header and content" }, Content = new StepThreeViewModel() },
                    new Step() { Header = new StepTitleHeader() { FirstLevelTitle = "Validation" }, Content = new StepFourViewModel() }
                };
            }
        }

        public AddNewTripViewModel() : base() { }
    }
}
