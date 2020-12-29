using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public class PaymentTransaction
    {
        public string PaymentMethod { get; set; }
        public decimal GatewayCharges { get; set; }
        public string HGNumber { get; set; }
        public string PaymentID { get; set; }
        public string TransID { get; set; }
        public string TransRef { get; set; }
        public string MerchantRef { get; set; }
        public int? PaymentGatewayHGUniqueId { get; set; }
        public string PaymentOrderID { get; set; }
        public decimal? PaymentAmount { get; set; }
        public string PaymentCurrency { get; set; }
        public string PaymentCreditCard { get; set; }
        public string CreditCardBank { get; set; }
        public string CreditCardNationality { get; set; }
        public string HGResponseCode { get; set; }
        public string HGResponseMessage { get; set; }
        public string PaymentGatewayCardType { get; set; }
        public string PaymentGatewayMarkupAmount { get; set; }
        public string PaymentGatewayMarkupID { get; set; }
        public string PaymentLocation { get; set; }
        public DateTime? PaymentDateTime { get; set; }
        public string PaymentCustomerIP { get; set; }
        public string FraudStatus { get; set; }
        public string HGPaymentToken { get; set; }
        public string CustomerEmail { get; set; }
        public string GatewayResponse { get; set; }
        public string Product { get; set; }
    }
}
