using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
   public class ConfirmedRoom
    {
        public string RoomCode { set; get; }
        public int Paxs { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public string RoomType { set; get; }
        public string RoomMeal { set; get; }
        public bool IsRefundable { set; get; }
        public string Image { set; get; }
        public string RateType { set; get; }
        public string BoardCode { set; get; }
        public List<CancellationRule> cancellations { set; get; }
        public ConfirmedRoom()
        {
            cancellations = new List<CancellationRule>();
        }
    }

    public class ConfirmedCancellationRule
    {
        
        public string FromDate { set; get; }
        
        public string ToDate { set; get; }
      //  public double Cost { set; get; }
         
        public double Price { set; get; }
    }
}
