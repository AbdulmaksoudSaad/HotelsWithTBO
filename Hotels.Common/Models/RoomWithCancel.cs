using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class RoomWithCancel
    {

        public int RoomIndex { set; get; }
        
        public double RatePerNight { set; get; }
        
        public double TotalSellPrice { set; get; }
        
        public List<CancellationRule> cancellationRules { set; get; }
        public RoomWithCancel()
        { 
            cancellationRules = new List<CancellationRule>();
        }
    }
}
