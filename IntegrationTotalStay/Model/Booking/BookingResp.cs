using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IntegrationTotalStay.Model.Booking
{
    [XmlRoot(ElementName = "ReturnStatus")]
    public class ReturnStatus
    {
        [XmlElement(ElementName = "Success")]
        public string Success { get; set; }
        [XmlElement(ElementName = "Exception")]
        public string Exception { get; set; }
    }

    [XmlRoot(ElementName = "PaymentDue")]
    public class PaymentDue
    {
        [XmlElement(ElementName = "Amount")]
        public string Amount { get; set; }
        [XmlElement(ElementName = "DateDue")]
        public string DateDue { get; set; }
    }

    [XmlRoot(ElementName = "PaymentsDue")]
    public class PaymentsDue
    {
        [XmlElement(ElementName = "PaymentDue")]
        public PaymentDue PaymentDue { get; set; }
    }

    [XmlRoot(ElementName = "PropertyBooking")]
    public class PropertyBooking
    {
        [XmlElement(ElementName = "PropertyBookingReference")]
        public string PropertyBookingReference { get; set; }
        [XmlElement(ElementName = "Supplier")]
        public string Supplier { get; set; }
        [XmlElement(ElementName = "SupplierReference")]
        public string SupplierReference { get; set; }
    }

    [XmlRoot(ElementName = "PropertyBookings")]
    public class PropertyBookings
    {
        [XmlElement(ElementName = "PropertyBooking")]
        public PropertyBooking PropertyBooking { get; set; }
    }

    [XmlRoot(ElementName = "BookResponse")]
    public class BookResponse
    {
        [XmlElement(ElementName = "ReturnStatus")]
        public ReturnStatus ReturnStatus { get; set; }
        [XmlElement(ElementName = "BookingReference")]
        public string BookingReference { get; set; }
        [XmlElement(ElementName = "TradeReference")]
        public string TradeReference { get; set; }
        [XmlElement(ElementName = "TotalPrice")]
        public string TotalPrice { get; set; }
        [XmlElement(ElementName = "TotalCommission")]
        public string TotalCommission { get; set; }
        [XmlElement(ElementName = "CustomerTotalPrice")]
        public string CustomerTotalPrice { get; set; }
        [XmlElement(ElementName = "PaymentsDue")]
        public PaymentsDue PaymentsDue { get; set; }
        [XmlElement(ElementName = "PropertyBookings")]
        public PropertyBookings PropertyBookings { get; set; }
    }

}
