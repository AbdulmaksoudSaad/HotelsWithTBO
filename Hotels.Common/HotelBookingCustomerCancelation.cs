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
    
    public partial class HotelBookingCustomerCancelation
    {
        public int ID { get; set; }
        public string Booking_No { get; set; }
        public Nullable<int> Room_No { get; set; }
        public Nullable<System.DateTime> Date_From { get; set; }
        public Nullable<System.DateTime> Date_To { get; set; }
        public string Amend_Restriction { get; set; }
        public string Cancel_Restriction { get; set; }
        public Nullable<double> Cancel_Amount { get; set; }
        public Nullable<double> Customer_Cancel_Amount { get; set; }
        public Nullable<double> No_Show_Amount { get; set; }
        public string Rule_Text { get; set; }
        public string Markup_Id { get; set; }
        public Nullable<double> Markup_Amount { get; set; }
    }
}
