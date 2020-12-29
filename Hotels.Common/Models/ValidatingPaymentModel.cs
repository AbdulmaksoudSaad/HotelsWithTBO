using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
 public class ValidatingPaymentModel
    {
    
            public int Status { get; set; } // 1000 -Valid No Changes, 2000   -Valid with Changes , 5000 -Invalid 			or Error Happened
            public string Message { get; set; }
            public PaymentFare PaymentFareDetails { get; set; }
            public string HGToken { get; set; }
            public PaymentResult paymentResult { get; set; }
        public ValidatingPaymentModel()
        {
            PaymentFareDetails = new PaymentFare();
            paymentResult = new PaymentResult();
        }
        }
    }
 
