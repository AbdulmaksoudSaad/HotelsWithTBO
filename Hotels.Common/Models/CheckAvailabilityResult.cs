using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
     
    public  class CheckAvailabilityResult
    {
        // MG  
        public string ProviderCur { get; set; }
        public int Status { get; set; }
        public double TotalCost { get; set; }
         
        public List<RoomResult> Result { get; set; }
        public CheckAvailabilityResult()
        {
            Result = new List<RoomResult>();
        }
    }
}
