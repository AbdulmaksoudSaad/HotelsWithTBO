//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelBedsIntegration.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BookingRequest
    {
        public int id { get; set; }
        public string HolderName { get; set; }
        public string SurName { get; set; }
        public string Roomkey { get; set; }
        public string ClientReference { get; set; }
        public string session { get; set; }
    }
}