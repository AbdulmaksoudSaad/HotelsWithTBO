using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBOIntegration.Models.Search.CommonAttr;

namespace TBOIntegration.Models.Search.Req
{
    public class SearchReq
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public int CityId { get; set; }
        public int NoOfRooms { get; set; }
        public string GuestNationality { get; set; }
        public List<RoomGuest> RoomGuests { get; set; }
        public List<string> HotelCodeList { get; set; }

    }
}
