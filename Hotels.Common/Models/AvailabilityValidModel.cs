using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
 public   class AvailabilityValidModel
    {
        public String Curr { get; set; }
        public  AvailabilityReq  availabilityReq { get; set; }
        public AvailabilityValidModel()
        {
            availabilityReq = new AvailabilityReq();
        }

    }
}
