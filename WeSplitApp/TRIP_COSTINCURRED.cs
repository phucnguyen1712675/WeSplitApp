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
    public partial class TRIP_COSTINCURRED
    {
        public int COST_ID { get; set; }
        public int TRIP_ID { get; set; }
        public Nullable<double> COST { get; set; }
    
        public virtual COSTINCURRED COSTINCURRED { get; set; }
        public virtual TRIP TRIP { get; set; }
    }
}
