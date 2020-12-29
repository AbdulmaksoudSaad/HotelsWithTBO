using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
   public class BookingHotel
    {
        public string creationDate { get; set; }
        public string checkIn { get; set; }
        public string checkOut { get; set; }
        public string hotelCode { get; set; }
        public string hotelName { get; set; }
        public List<BookingRoom> rooms { get; set; }
        public List<PaxBack> occupancies { get; set; }
        public string boardCode { get; set; }
        public BookingHotel()
        {
            rooms = new List<BookingRoom>();
            occupancies = new List<PaxBack>();
        }
    }
}
