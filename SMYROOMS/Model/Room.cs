using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
   
   
   public class Room
    {
        public int occupancyRefId { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public bool refundable { get; set; }
        public int units { get; set; }
        public RoomPrice roomPrice { get; set; }
        public Bed beds { get; set; }

        public Room()
        {
            roomPrice = new RoomPrice();
            beds = new Bed();
        }
    }
}
