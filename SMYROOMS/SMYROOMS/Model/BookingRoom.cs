using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
   public class BookingRoom
    {
        public int? occupancyRefId { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public Price price { get; set; }
        public BookingRoom()
        {
            price = new Price();
        }
    }
}
