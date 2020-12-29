using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class RestCriteraiData
    {
        public SearchData searchData { get; set; }
        public string HotelName { get; set; }
        public int HotelStars { get; set; }
        public double cost { get; set; }
        public string Pid { get; set; }
        public string Curr { get; set; }
        public string pos { get; set; }
    }
}
