using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBO.DAL.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string RoomTypeCode { get; set; }
        public string HotelCode { get; set; }
        public virtual ICollection<RoomImage> RoomImages { get; set; }
    }
}
