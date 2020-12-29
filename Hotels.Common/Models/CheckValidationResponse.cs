using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    class CheckValidationResponse
    {
        public int Status { get; set; }
        public double TotalPrice { get; set; }

        public List<RoomWithCancel> Result { get; set; }
        public CheckValidationResponse()
        {
            Result = new List<RoomWithCancel>();
        }
    }
}
 
