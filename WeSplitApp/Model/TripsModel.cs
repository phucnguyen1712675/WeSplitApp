using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitApp.Model
{
    public class TripsModel : INotifyPropertyChanged
    {
#pragma warning disable 67
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 67

        public int TripId { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public string ThumbnailImage { get; set; }
        public ObservableCollection<Location> Route { get; set; }
        public ObservableCollection<string> PhotoCollection { get; set; }
        public ObservableCollection<Member> MemberCollection { get; set; }
        public double? CurrentProceeeds { get; set; }
        public double TotalCosts { get; set; }
        public DateTime ToGoDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }

    public enum LocationType
    {
        Province,
        City
    }

    public abstract class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        //public LocationType Type { get; set; }
    }

    public enum Gender
    {
        Nam, 
        Nữ
    }

    public class Member
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string Avatar { get; set; } 
    }
}
