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
    
    public partial class HotelRoom
    {
        public int ID { get; set; }
        public string roomTypeCode { get; set; }
        public string Name { get; set; }
        public string Twin { get; set; }
        public Nullable<int> maxAdult { get; set; }
        public Nullable<int> maxExtraBed { get; set; }
        public Nullable<int> maxChildren { get; set; }
        public Nullable<int> roomPaxCapacity { get; set; }
        public Nullable<int> allowedAdultsWithoutChildren { get; set; }
        public Nullable<int> allowedAdultsWithChildren { get; set; }
    }
}
