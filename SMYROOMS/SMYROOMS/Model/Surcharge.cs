using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
    public class Surcharge
    {
        public Price price { get; set; }
        public bool mandator { get; set; }
        public string description { get; set; }
        public string chargeType { get; set; }
        public Surcharge()
        {
            price = new Price();
        }
    }
}
