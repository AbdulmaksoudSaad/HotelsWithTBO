using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
    public class CancelPolicy
    {
        public bool refundable { get; set; }
        public List<CancelPenalty> cancelPenalties { get; set; }
        public CancelPolicy()
        {
            cancelPenalties = new List<CancelPenalty>();
        }
    }
   
}
