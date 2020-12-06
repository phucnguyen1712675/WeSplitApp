namespace WeSplitApp.ViewModel
{
    public class BeingTakenTripsListViewModel : ExpectedTripListViewModel
    {
        public override bool IsDone { get; set; } = false;
        public bool getIsDone()
        {
            return IsDone;
        }
    }
}
