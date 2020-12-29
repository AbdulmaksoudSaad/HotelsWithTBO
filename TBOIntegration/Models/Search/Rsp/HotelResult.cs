using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBOIntegration.Models.Search.Rsp
{
    public class HotelResult
    {
        public string ResultIndex { get; set; }
        public HotelInfo HotelInfo { get; set; }
        public MinHotelPrice MinHotelPrice { get; set; }
        public string IsPkgProperty { get; set; }
        public string IsPackageRate { get; set; }
        public string MappedHotel { get; set; }
        public string IsHalal { get; set; }
    }
}
