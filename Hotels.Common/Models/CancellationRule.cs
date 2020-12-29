using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    [DataContract]
    public class CancellationRule
    {
        [DataMember]
        public string FromDate { set; get; }
        [DataMember]
        public string ToDate { set; get; }
        public double Cost { set; get; }
        [DataMember]
        public double Price { set; get; }
        [DataMember]
        public string CanellationRuleText { set; get; }
        [DataMember]
        public string ChargeType { get; set; }
        [DataMember]
        public string Curency { get; set; }
    }
}
