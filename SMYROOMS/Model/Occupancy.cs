using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
    public class Occupancy
    {
        public List<Pax> paxes { get; set; }
        public Occupancy()
        {
            paxes = new List<Pax>();
        }
    }
}
