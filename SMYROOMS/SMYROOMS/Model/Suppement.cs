using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
    public class Supplement
    {
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? quantity { get; set; }
        public bool? mandatory { get; set; }
        public bool? effectiveDat { get; set; }
        public bool? expireDate { get; set; }
        public Price price { get; set; }
        
        public Supplement()
        {
            price = new Price();
        }
    }
}
