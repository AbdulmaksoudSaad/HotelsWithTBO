using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBOIntegration.Models.Book.Rsp
{
    public class HotelBookResp
    {
        public Status Status { get; set; }
        public string BookingStatus { get; set; }
        public int BookingId { get; set; }
        public int TripId { get; set; }
    }
}
