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
    
    public partial class RoomTax
    {
        public int Id { get; set; }
        public Nullable<int> RoomId { get; set; }
        public string included { get; set; }
        public string amount { get; set; }
        public string Currency { get; set; }
    }
}