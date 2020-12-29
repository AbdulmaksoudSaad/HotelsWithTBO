using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
    public class CancelPenalty
    {
        public string penaltyType { get; set; }
        public int?  hoursBefore { get; set; }
        public float? value { get; set; }
        public string currency { get; set; }

    }
}
