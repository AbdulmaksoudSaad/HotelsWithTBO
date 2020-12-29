using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class ConfirmData
    {
        public HotelsBooking hotelsBooking { set; get; }
        public List< RoomDTP> Rooms { set; get; }
        public String  Pid { set; get; }
        public string fromDate { get; set; }
        public string ToDate { get; set; }
        public string Dur { get; set; }
        public string PreTokenTS { get; set; }
        public string PropertyTS { get; set; }



        public ConfirmData()
        {
            hotelsBooking = new HotelsBooking();
            Rooms = new List<RoomDTP>();
        }

    }
  public  class RoomDTP
    {
        public String  Rate  { get; set; }
        public int? RoomN { get; set; }
        public SearchRoomResult roomResult { get; set; }


        public List<HotelBookingPax> bookingPaxes { get; set; }
        public RoomDTP()
        {
            bookingPaxes = new List<HotelBookingPax>();
        }
    }
}
