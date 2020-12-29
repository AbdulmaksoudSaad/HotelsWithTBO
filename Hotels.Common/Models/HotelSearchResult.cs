using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
 

namespace Hotels.Common.Models
{
    [DataContract]
    public class HotelSearchResult
    {
        [DataMember]
        public string hotelCode { get; set; } // hotelId in DB
        [DataMember]
        public string hotelName { get; set; }
        [DataMember]
        public double hotelRate { get; set; } //price per night
        [DataMember]
        public string providerID { get; set; } //provider  
        [DataMember]
        public string providerHotelCode { get; set; } 
        [DataMember]
        public string providerHotelID { get; set; }
        [DataMember]
        public string hotelThumb { get; set; } // main img
        [DataMember]
        public string hotelDescription { get; set; }
        [DataMember]
        public string shortcutHotelDescription { get; set; }
        
        public int MarkupId { get; set; }
        
        public int DiscountId { get; set; }
      
        public double MarkupValue{ get; set; }
         
        public double DiscountValue { get; set; }
        [DataMember]
        public List<string> hotelImages { get; set; }
        [DataMember]
        public string LatLong { get; set; }
        [DataMember]
        public string Location { get; set; } // 
        [DataMember]
        public int hotelStars { get; set; } // 
        [DataMember]
        public string costCurrency { get; set; }
        [DataMember]
        public double costPrice { get; set; } // 
        [DataMember]
        public double TotalSellPrice { get; set; } // all night  
        [DataMember]
        public string sellCurrency { get; set; } // 
        [DataMember]
        public string Lat { get; set; }
        [DataMember]
        public string Lng { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public double CityTaxValue { get; set; }
        [DataMember]
        public string CityTaxCurrency { get; set; }
        [DataMember]
        public List<HotelAmenity> Amenities { get; set; }
        public List<RoomResult> rooms { set; get; }
        //tbo
        [DataMember]
        public int ResIndex { get; set; }

    }
}
