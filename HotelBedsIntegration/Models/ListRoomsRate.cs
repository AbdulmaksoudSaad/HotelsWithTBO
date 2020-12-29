using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBedsIntegration.Models
{
   public class ChannelRoomsRate
    {
        public List<Rate> RoomRates { get; set; }
        public ChannelRoomsRate()
        {
            RoomRates = new List<Rate>();
        }
    }
}
