using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBOIntegration.Models.Search.CommonAttr;

namespace TBOIntegration.Models.Search.Rsp
{
    public class SearchResp
    {
        public Status Status { get; set; }
        public string ResponseTime { get; set; }
        public string SessionId { get; set; }
        public string NoOfRoomsRequested { get; set; }
        public string CityId { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public List<RoomGuest> RoomGuests { get; set; }
        public List<HotelResult> HotelResult { get; set; }

    }
}
