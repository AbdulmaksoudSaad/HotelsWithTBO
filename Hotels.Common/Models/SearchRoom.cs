using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    public class SearchRoom
    {
        public int Adult { set; get; }//Adult
        public List<int> Child { set; get; }  
        public SearchRoom() {
            Child = new List<int>();
        }
    }
}
