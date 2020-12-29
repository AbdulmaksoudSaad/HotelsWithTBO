using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
   public class RoomPackages
    {
        public string RoomCategory { get; set; }
        public int PackagesNo { get; set; }
        public List<RoomPackage> roomPackages { get; set; }
        public RoomPackages()
        {
            roomPackages = new List<RoomPackage>();
        }
    }
}
