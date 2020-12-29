using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    public class HotelSearchResponse
    {
        public int Status { get; set; } //status 0 done ,1 not valid,2 provider  
        public List<string> Locations { set; get; }
        public List<HotelSearchResult> HotelResult { set; get; }
        public ResultException ResultException { get; set; }

        //MG
        public DateTime CheckIn { set; get; }
        public DateTime CheckOut { set; get; }

        public HotelSearchResponse()
        {
            Locations = new List<string>();
            HotelResult = new List<HotelSearchResult>();
        }
    }
}
