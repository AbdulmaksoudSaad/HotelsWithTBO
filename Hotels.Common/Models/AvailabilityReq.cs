using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class AvailabilityReq
    {
        public string PID { get; set; }
        public List<RoomAvailability> Rooms { get; set; }
        public string BookingNum { get; set; }
        public string HotelCode { get; set; }
        public string hotelID { get; set; }
        public string Sid { get; set; }
        public double TotalCost { get; set; }

        public AvailabilityReq()
        {
            Rooms = new List<RoomAvailability>();
        }
    }
}
