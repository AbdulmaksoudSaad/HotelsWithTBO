//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hotels.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class Supplement
    {
        public int ID { get; set; }
        public string SID { get; set; }
        public string HotelCode { get; set; }
        public Nullable<int> RoomIndex { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<bool> IsSelected { get; set; }
        public string ChargeType { get; set; }
    }
}