using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
     public class PaxBack
    {
        public int? id { get; set; }
        public List<Pax> paxes { get; set; }
        public PaxBack()
        {
            paxes = new List<Pax>();
        }
    }
}
