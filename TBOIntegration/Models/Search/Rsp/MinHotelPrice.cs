using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBOIntegration.Models.Search.Rsp
{
    public class MinHotelPrice
    {
        public string TotalPrice { get; set; }
        public string Currency { get; set; }
        public string B2CRates { get; set; }
        public string OriginalPrice { get; set; }
    }
}
