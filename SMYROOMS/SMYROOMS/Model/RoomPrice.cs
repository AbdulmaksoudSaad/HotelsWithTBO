using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
    public class RoomPrice
    {
        public Price price { get; set; }
        public PriceBreakdown breakdown { get; set; }

        public RoomPrice()
        {
            price = new Price();
            breakdown = new PriceBreakdown();
        }
    }
}
