using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
   public class PaymentGateWay
    {
        public double amount { get; set; }
        public string paymentMethod { get; set; }
        public string currency { get; set; }
        

    }
}