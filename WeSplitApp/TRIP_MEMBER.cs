//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using PropertyChanged;

namespace WeSplitApp
{
    using System;
    using System.Collections.ObjectModel;

    [AddINotifyPropertyChangedInterface]
    public partial class TRIP_MEMBER
    {
        public int TRIP_ID { get; set; }
        public int MEMBER_ID { get; set; }
        public double AMOUNTPAID { get; set; }
        public Nullable<int> BYMEMBER_ID { get; set; }
    
        public virtual MEMBER MEMBER { get; set; }
        public virtual TRIP TRIP { get; set; }
        public virtual MEMBER MEMBER1 { get; set; }
    }
}
