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
    
    public partial class BookingPriceChange
    {
        public int Id { get; set; }
        public Nullable<double> SellPrice { get; set; }
        public string Currency { get; set; }
        public Nullable<double> TotalCost { get; set; }
        public string MarkUpId { get; set; }
        public Nullable<double> MarkUpVal { get; set; }
        public string DiscountID { get; set; }
        public Nullable<double> DiscountVal { get; set; }
    }
}
