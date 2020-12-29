using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IntegrationTotalStay.Model.Availability
{
    [XmlRoot(ElementName = "ReturnStatus")]
    public class ReturnStatus
    {
        [XmlElement(ElementName = "Success")]
        public string Success { get; set; }
        [XmlElement(ElementName = "Exception")]
        public string Exception { get; set; }
    }

    [XmlRoot(ElementName = "Cancellation")]
    public class Cancellation
    {
        [XmlElement(ElementName = "StartDate")]
        public string StartDate { get; set; }
        [XmlElement(ElementName = "EndDate")]
        public string EndDate { get; set; }
        [XmlElement(ElementName = "Penalty")]
        public string Penalty { get; set; }
    }

    [XmlRoot(ElementName = "Cancellations")]
    public class Cancellations
    {
        [XmlElement(ElementName = "Cancellation")]
        public List<Cancellation> Cancellation { get; set; }
    }

    [XmlRoot(ElementName = "PreBookResponse")]
    public class PreBookResponse
    {
        [XmlElement(ElementName = "ReturnStatus")]
        public ReturnStatus ReturnStatus { get; set; }
        [XmlElement(ElementName = "PreBookingToken")]
        public string PreBookingToken { get; set; }
        [XmlElement(ElementName = "TotalPrice")]
        public string TotalPrice { get; set; }
        [XmlElement(ElementName = "TotalCommission")]
        public string TotalCommission { get; set; }
        [XmlElement(ElementName = "VATOnCommission")]
        public string VATOnCommission { get; set; }
        [XmlElement(ElementName = "Cancellations")]
        public Cancellations Cancellations { get; set; }
        
    }
}
