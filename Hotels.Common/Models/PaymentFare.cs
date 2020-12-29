using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class PaymentFare
    {
        public decimal FareAmount { get; set; } //Must be in AED
        public decimal TaxAmount { get; set; } //Must be in AED
        public decimal TotalChargeAmount { get; set; } // Must be in AED
        public string CustomerPaymentCurrency { get; set; } // Customer Currency
        public decimal TotalAmount { get; set; }  //Must be in AED
        public decimal ExchangeRate { get; set; }
    }
}
