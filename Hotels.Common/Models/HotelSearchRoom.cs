using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    [DataContract]
    public class HotelSearchRoom
    {
        [DataMember]
        public string hotelCode { get; set; }
        [DataMember]
        public string hotelName { get; set; }
        [DataMember]
        public double hotelRate { get; set; }
        [DataMember]
        public string providerID { get; set; }
        [DataMember]
        public string providerHotelCode { get; set; }
        [DataMember]
        public string providerHotelID { get; set; }
        [DataMember]
        public string hotelThumb { get; set; }
        [DataMember]
        public string hotelDescription { get; set; }
        [DataMember]
        public string shortcutHotelDescription { get; set; }

        public int MarkupId { get; set; }

        public int DiscountId { get; set; }

        public double MarkupValue { get; set; }

        public double DiscountValue { get; set; }
        [DataMember]
        public List<string> hotelImages { get; set; }

        public string LatLong { get; set; }
        [DataMember]
        public string Location { get; set; }
        [DataMember]
        public int hotelStars { get; set; }

        public string costCurrency { get; set; }

        public double costPrice { get; set; }
        [DataMember]
        public double TotalSellPrice { get; set; }
        [DataMember]
        public string sellCurrency { get; set; }
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
        [DataMember]
        public List<SeparatedRoom> rooms { set; get; }
        [DataMember]
        public List<RoomResult> Packages { get; set; }

        public HotelSearchRoom()
        {
            Amenities = new List<HotelAmenity>();
            rooms = new List<SeparatedRoom>();

        }
    }
}
