using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    public class RoomForSearch
    {
        public int num { get; set; }
        public List<SearchRoomData> rooms { get; set; }
    /*    public RoomForSearch()
        {
            rooms = new List<SearchRoomData>();
        }*/

    }
}
