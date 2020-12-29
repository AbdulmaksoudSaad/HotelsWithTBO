using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
    
    public class SearchInputData
    {
        public string checkin { get; set; }
        public string checkout { get; set; }
        public List<string> hotels { get; set; }
        public List<Occupancy> occupancies { get; set; }
        public string Nationality { get; set; }
        public string currency { get; set; }


        public SearchInputData()
        {
            hotels = new List<string>();
            occupancies = new List<Occupancy>();
        }

        
    }
}
