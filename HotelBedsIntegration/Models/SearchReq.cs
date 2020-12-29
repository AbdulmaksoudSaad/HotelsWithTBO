using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBedsIntegration.Models
{
    public class Stay
    {
        public string checkIn { get; set; }
        public string checkOut { get; set; }
       // public string shiftDays { get; set; }
        public bool allowOnlyShift { get; set; }
    }

    public class PaxReq
    {
        public string type { get; set; } 
        public int age { get; set; } //chd only
    }

    public class Occupancy
    {
        public int rooms { get; set; }
        public int adults { get; set; }
        public int children { get; set; }
        public List<PaxReq> paxes { get; set; }
        public Occupancy() {
            paxes = new List<PaxReq>();
        }
    }

    public class HotelsReq
    {
        public List<int> hotel { get; set; }
        public HotelsReq() {
            hotel = new List<int>();
        }
    }

    public class HBSearchData
    {
        public Stay stay { get; set; }
        public List<Occupancy> occupancies { get; set; }
        public HotelsReq hotels { get; set; }

       public  HBSearchData() {
            stay = new Stay();
            occupancies = new List<Occupancy>();
            hotels = new HotelsReq();
        }
    }
}
