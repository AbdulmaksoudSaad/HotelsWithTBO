using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class SeparatedRoom
    {
     public List<RoomResult> RoomResults { get; set; }
        public SeparatedRoom()
        {
            RoomResults = new List<RoomResult>();
            OptionsForBookings = new List<OptionsForBooking>();

        }

        public List<OptionsForBooking> OptionsForBookings { get; set; }
        public class OptionsForBooking
        {
            public int[] RoomIndex { get; set; }
        }
    }
}
