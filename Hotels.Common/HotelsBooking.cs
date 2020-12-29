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
    
    public partial class HotelsBooking
    {
        public int ID { get; set; }
        public string Booking_No { get; set; }
        public string SessionId { get; set; }
        public string City { get; set; }
        public string HotelProviderID { get; set; }
        public string Hotel_ID { get; set; }
        public Nullable<int> Rooms_Qty { get; set; }
        public Nullable<int> Pax_Qty { get; set; }
        public Nullable<System.DateTime> Booking_Time { get; set; }
        public string Booking_Status { get; set; }
        public string Customer_ID { get; set; }
        public string Provider_ID { get; set; }
        public Nullable<double> Sell_Price { get; set; }
        public string Sell_Currency { get; set; }
        public string Booking_Phone_Code { get; set; }
        public string Booking_phone { get; set; }
        public string Booking_Email { get; set; }
        public string Booking_Conf { get; set; }
        public string HotelConfirmationNo { get; set; }
        public string Pax_Name { get; set; }
        public Nullable<double> Foreign_Amount { get; set; }
        public Nullable<double> Total_Cost_Main_Currency { get; set; }
        public string PromoCode_ID { get; set; }
        public Nullable<double> PromoCode_Amount { get; set; }
        public string Sales_Channel { get; set; }
        public Nullable<int> Form_Of_Payment { get; set; }
        public string NotificationKey { get; set; }
        public string voucherPdf { get; set; }
        public string InvoicePdf { get; set; }
    }
}
