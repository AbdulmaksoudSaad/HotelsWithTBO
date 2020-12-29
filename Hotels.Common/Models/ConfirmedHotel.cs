using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
   public class ConfirmedHotel
    {
        
        public string hotelCode { get; set; }
         
        public string hotelName { get; set; }
        public string Address { get; set; }
        public string hotelThumb { get; set; }
        public string Location { get; set; }
        public int hotelStars { get; set; }
        public double TotalSellPrice { get; set; }
        public string sellCurrency { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int Paxes { get; set; }
        public int Rooms { get; set; }
        public string CheckIn { set; get; }
        public string CheckOut { set; get; }
        public string HotelDescription { get; set; }

    }
}
