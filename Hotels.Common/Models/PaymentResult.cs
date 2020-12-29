using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class PaymentResult
    {
        public string HGNumber { get; set; }
        public string HGToken { get; set; }
        public string RedirectUrl { get; set; }
        public string PaymentOutput { get; set; }
        public string FraudOutput { get; set; }
        public string PostPayment { get; set; }
    }
}
