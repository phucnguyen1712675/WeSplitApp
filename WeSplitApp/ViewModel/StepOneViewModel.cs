using MaterialDesignExtensions.Model;

namespace WeSplitApp.ViewModel
{
    public class StepOneViewModel : Step
    {
        public StepOneViewModel() : base() { }

        public override void Validate()
        {
            // place your validation logic here
            //     assuming we have a input field for the birthday of the person and want to check the input
            //bool ageOk = CheckAgeOfPerson();
            bool ageOk = true;

            // if the age is not OK, we set a validation error
            if (ageOk)
            {
                HasValidationErrors = false;
            }
            else
            {
                HasValidationErrors = true;
            }
        }
    }
}