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
    
    public partial class CheckAvailabiltyReq
    {
        public int ID { get; set; }
        public string SID { get; set; }
        public string Pid { get; set; }
        public string BookinNum { get; set; }
        public string HotelCode { get; set; }
        public Nullable<double> TotalCost { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
    }
}
