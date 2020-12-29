using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class SearchStatistic
    {
        
        public DateTime ChekIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string HotelID { set; get; }
        public string HotelName { get; set; }
        public string CityName { get; set; }
        public string ProvideID { set; get; }
        public string POS { set; get; }
        public DateTime? Date { get; set; }
        public string sID { set; get; }
        public string BookingNo { get; set; }
        public string Source { set; get; }
        public string BookingStatus { get; set; }
    }
}
