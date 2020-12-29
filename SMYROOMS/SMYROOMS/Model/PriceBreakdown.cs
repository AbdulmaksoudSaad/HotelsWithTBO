using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
    public class PriceBreakdown
    {
        public string effectiveDate { get; set; }
        public string expireDate { get; set; }
        public Price price { get; set; }
        public PriceBreakdown()
        {
            price = new Price();
        }
    }
}
