using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class RequiredBookingData
    {
        public string HotelName { get; set; }
        public int Hotelstar { get; set; }
        public string HotelDesc { get; set; }
        public string Currency { get; set; }
        public string City { get; set; }
        public string address { get; set; }
        public string CheckIn { get; set; }
        public string Checkout { get; set; }
        public string hotelThumb { get; set; }
        public string providerID { get; set; }
        public string providerHotelCode { get; set; }
        public string providerHotelID { get; set; }
        public string location { get; set; }
        //MG
        public string Lat { get; set; }
        public string Lng { get; set; }

        public string TBONorms { get; set; }
        public List<SearchRoomResult> rooms { get; set; }
        public List<RoomResult> TBoRooms { get; set; }
        // for tbo price change
        public int Status { get; set; } // 1000 -Valid No Changes, 2000   -Valid with Changes , 5000 -Invalid 			or Error Happened
        public string Message { get; set; }

        public RequiredBookingData()
        {
            rooms = new List<SearchRoomResult>();
            TBoRooms = new List<RoomResult>();
        }

    }
}
