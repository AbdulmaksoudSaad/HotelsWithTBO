using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class RoomPackage
    {
        
        public double PricePerNight { get; set; } 
        public double PricePerAllNight { get; set; }
        public int No { get; set; }
        public List<RoomResult> roomResults { get; set; }
        public RoomPackage()
        {
            roomResults = new List<RoomResult>();
        }


    }
}
