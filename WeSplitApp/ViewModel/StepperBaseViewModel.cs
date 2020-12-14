using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitApp.ViewModel
{
    public class StepperBaseViewModel : ViewModel
    {
        public bool ContentAnimationsEnabled { get; set; }
        public bool IsLinear { get; set; }
        public StepperLayout Layout { get; set; }
        public IEnumerable<StepperLayout> Layouts => new List<StepperLayout>()
        {
            StepperLayout.Horizontal,
            StepperLayout.Vertical
        };

        public StepperBaseViewModel()
        {
            this.Layout = StepperLayout.Vertical;
            this.IsLinear = true;
            this.ContentAnimationsEnabled = true;
        }
    }
}
