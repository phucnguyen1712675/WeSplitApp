//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeSplitApp
{
    using System;
    using System.Collections.ObjectModel;
    using PropertyChanged;

    [AddINotifyPropertyChangedInterface]
    public partial class TRIP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRIP()
        {
            this.TRIP_COSTINCURRED = new ObservableCollection<TRIP_COSTINCURRED>();
            this.TRIP_LOCATION = new ObservableCollection<TRIP_LOCATION>();
            this.TRIP_MEMBER = new ObservableCollection<TRIP_MEMBER>();
            this.TRIP_IMAGES = new ObservableCollection<TRIP_IMAGES>();
        }
    
        public int TRIP_ID { get; set; }
        public string TITTLE { get; set; }
        public string DESCRIPTION { get; set; }
        public string THUMNAILIMAGE { get; set; }
        public double CURRENTPROCEEDS { get; set; }
        public double TOTALCOSTS { get; set; }
        public Nullable<System.DateTime> TOGODATE { get; set; }
        public Nullable<System.DateTime> RETURNDATE { get; set; }
        public Nullable<bool> ISDONE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<TRIP_COSTINCURRED> TRIP_COSTINCURRED { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<TRIP_LOCATION> TRIP_LOCATION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<TRIP_MEMBER> TRIP_MEMBER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<TRIP_IMAGES> TRIP_IMAGES { get; set; }
    }
}
