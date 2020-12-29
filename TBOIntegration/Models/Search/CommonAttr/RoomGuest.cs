using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBOIntegration.Models.Search.CommonAttr
{
    public class RoomGuest
    {
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int[] ChildAge { get; set; }

    }
}
