using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    public class Charge
    {
        public string ChargeName { get; set; }
        public string ChargeCategory { get; set; }
        public double CahargeValue { get; set; }
    }

    public class PaymentFareDetails
    {
        public double FareAmount { get; set; }
        public double TotalChargeAmount { get; set; }
        public double TaxAmount { get; set; }
        public string CustomerPaymentCurrency { get; set; }
        public double TotalAmount { get; set; }
        public double ExchangeRate { get; set; }
    }

    public class Customers
    {
        public string CustomerEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public string PhoneCountryCode { get; set; }
        public string PhoneCodeCountry { get; set; }
        public string CustomerPhone { get; set; }
        public string IP { get; set; }
        public string PaymentLocation { get; set; }
    }

    public class BookingInfo
    {
        public DateTime BookingDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public string Product { get; set; }
        public string HGNumber { get; set; }
        public string SearchID { get; set; }
        public string Description { get; set; }
    }

    public class PaymentAuthData
    {
        public string HGToken { get; set; }
        public int HGTokenStatus { get; set; }
        public string SuccessUrl { get; set; }
        public string FailUrl { get; set; }
        public string HGTrackId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentToken { get; set; }
    }

    public class ActionsUrl
    {
        public string ValidationUrl { get; set; }
        public string PrePaymentUrl { get; set; }
        public string PostPaymentUrl { get; set; }
    }

    public class PayLinkRequest
    {
        public List<Charge> Charges { get; set; }
        public PaymentFareDetails PaymentFareDetails { get; set; }
        public Customers Customer { get; set; }
        public BookingInfo BookingInfo { get; set; }
        public PaymentAuthData PaymentAuthData { get; set; }
        public ActionsUrl ActionsUrl { get; set; }
        public string FormData { get; set; }
        public PayLinkRequest()
        {
            Charges = new List<Charge>();
            PaymentFareDetails = new PaymentFareDetails();
            Customer = new Customers();
            BookingInfo = new BookingInfo();
            PaymentAuthData = new PaymentAuthData();
            ActionsUrl = new ActionsUrl();
        }
    }
}
