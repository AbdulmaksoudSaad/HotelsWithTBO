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
    
    public partial class BookingConfirmationData
    {
        public int ID { get; set; }
        public string Reference { get; set; }
        public string ClientReference { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string Status { get; set; }
        public string HolderFirstName { get; set; }
        public string HolderLastName { get; set; }
        public string HotelCode { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public string Currency { get; set; }
        public string ProviderCurrency { get; set; }
        public Nullable<int> ProviderId { get; set; }
        public string supplerReference { get; set; }
        public string SessionID { get; set; }
        public string BookingNum { get; set; }
    }
}
