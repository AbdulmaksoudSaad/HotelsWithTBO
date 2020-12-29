using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class CheckAvailabilityForTs
    {
        public SearchHotelResult HodelData { get; set; }
        public List<SearchRoomResult> roomResults { get; set; }

        public CheckAvailabilityForTs()
        {
            HodelData = new SearchHotelResult();
            roomResults = new List<SearchRoomResult>();
        }

    }
}
