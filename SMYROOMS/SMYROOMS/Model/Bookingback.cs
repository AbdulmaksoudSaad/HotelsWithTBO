using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
   public class Bookingback
    {
        public Reference reference { get; set; }
        public HolderInput holder { get; set; }
        public BookingHotel hotel { get; set; }
        public Price price { get; set; }
        public CancelPolicy cancelPolicy { get; set; }
        public string remarks { get; set; }
        public string status { get; set; }
        public string payable { get; set; }
        public Bookingback()
        {
            reference = new Reference();
            holder = new HolderInput();
            hotel = new BookingHotel();
            price = new Price();
            cancelPolicy = new CancelPolicy();
        }
    }
}
