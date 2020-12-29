using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBO.DAL.Models
{
    public class RoomImage
    {
        public int RoomImageId { get; set; }
        public string RoomName { get; set; }
        public string RoomTypeCode { get; set; }
        public string URL { get; set; }
        public string HotelCode { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
