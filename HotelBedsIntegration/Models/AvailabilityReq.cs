using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBedsIntegration.Models.Availability
{
      
    
    public class RoomReq
    {
        public string rateKey { get; set; }
    }

    public class AvailabilityReq
    {
        public string upselling { get; set; }
        public List<RoomReq> rooms { get; set; }
    }
}
