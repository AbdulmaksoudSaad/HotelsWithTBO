using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    [DataContract]
    public class RoomResult
    {
        [DataMember]
        public int PackageNO { set; get; }
        [DataMember]
        public int RoomIndex { set; get; }
        [DataMember]
        public string RoomCode { set; get; } 
        [DataMember]
        public string RoomReference { set; get; }
        [DataMember]
        public int Paxs { get; set; }
        [DataMember]
        public int Adult { get; set; }
        [DataMember]
        public int Child { get; set; }
        [DataMember]
        public string RoomType { set; get; }
        [DataMember]
        public string RoomMeal { set; get; }
        [DataMember]
        public double RatePerNight { set; get; } //
       
        public double CostPrice { set; get; } //
        [DataMember]
        public double TotalSellPrice { set; get; } //all  night 
         
        public int MarkupId { set; get; }
       
          public int DiscountId { set; get; }
      
        public double MarkupValue { set; get; }
        public string rateClass { get; set; }
        public string rateType { get; set; }
        public string childrenAges { get; set; }
        public string boardCode { get; set; }
        public string paymentType { get; set; }
        public double DiscountValue { set; get; }
        [DataMember]
        public bool IsRefundable { set; get; }
      
        public string MealID { set; get; }

        public string BookingKeyTS { set; get; }

        [DataMember]
        public List<string> Images { set; get; }
        [DataMember]
        public List<CancellationRule> cancellationRules { set; get; }
        public RoomResult() {
            Images = new List<string>();
            cancellationRules = new List<CancellationRule>();
            Supplements = new List<Supplement>();
        }
        public decimal Tax { get; set; }
        [DataMember]
        public List<Supplement> Supplements { get; set; }
        public class Supplement
        {
            public int SuppID { get; set; }
            public string SuppChargeType { get; set; }
            public decimal Price { get; set; }
            public bool SuppIsSelected { get; set; }
            public string Cur { get; set; }
        }
        [DataMember]
        public string Curency { get; set; }
        [DataMember]
        public string ChargeType { get; set; }
        [DataMember]
        public string[] HotelNorms { get; set; }
        [DataMember]
        public string Amenities { get; set; }
        [DataMember]
        public string Inclusion { get; set; }
    }
}
