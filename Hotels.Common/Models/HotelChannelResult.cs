using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    [DataContract]
    public class HotelChannelResult
    {

        [DataMember]
        public string hotelId{ get; set; }
        
        public string providerID { get; set; }
        
        public string providerHotelCode { get; set; }
        
        public string providerHotelID { get; set; }

        public int MarkupId { get; set; }

        public int DiscountId { get; set; }

        public double MarkupValue { get; set; }

        public double DiscountValue { get; set; }
        
         
        public string costCurrency { get; set; }
       
        public double costPrice { get; set; }
        [DataMember]
        public string sellCurrency { get; set; }
        [DataMember]
        public string DeepLink { get; set; }
        [DataMember]
        public double PricePerNight { get; set; }
        [DataMember]
        public double PricePerAllNight { get; set; }

        public List<RoomPackages> packages { get; set; }
        public HotelChannelResult()
        {
            packages = new List<RoomPackages>();
        }

    }
}
