using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
   public class DeltaPriceInput
    {
        public float amount { get; set; }
        public float percent { get; set; }
        public bool applyBoth { get; set; }
    }
}
