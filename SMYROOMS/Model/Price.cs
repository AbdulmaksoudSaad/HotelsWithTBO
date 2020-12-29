using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
     
   

    public class Price
    {
         
            public string currency { get; set; }
            public string binding { get; set; }
            public decimal? net { get; set; }
            public decimal? gross { get; set; }
            public change exchange { get; set; }
        public Price()
        {
            exchange = new change();
        }
    }
}
