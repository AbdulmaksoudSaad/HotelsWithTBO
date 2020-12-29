using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBedsIntegration.Models.Availability
{
      
   

    public class AuditData
    {
        public string processTime { get; set; }
        public string timestamp { get; set; }
        public string requestHost { get; set; }
        public string serverId { get; set; }
        public string environment { get; set; }
        public string release { get; set; }
        public string token { get; set; }
        public string @internal { get; set; }
    }

    public class CancellationPolicy
    {
        public string amount { get; set; }
        public DateTime from { get; set; }
    }

    public class Rate
    {
        public string rateKey { get; set; }
        public string rateClass { get; set; }
        public string rateType { get; set; }
        public string net { get; set; }
        public string sellingRate { get; set; }
        public bool hotelMandatory { get; set; }
        public string rateComments { get; set; }
        public string paymentType { get; set; }
        public bool packaging { get; set; }
        public string boardCode { get; set; }
        public string boardName { get; set; }
        public List<CancellationPolicy> cancellationPolicies { get; set; }
        public int rooms { get; set; }
        public int adults { get; set; }
        public int children { get; set; }
        public string childrenAges { get; set; }
    }

    public class Room
    {
        public string code { get; set; }
        public string name { get; set; }
        public List<Rate> rates { get; set; }
    }

    public class Tax
    {
        public bool included { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
    }

    public class Taxes
    {
        public List<Tax> taxes { get; set; }
        public bool allIncluded { get; set; }
    }

    public class Rate2
    {
        public string rateKey { get; set; }
        public string rateClass { get; set; }
        public string rateType { get; set; }
        public string net { get; set; }
        public string sellingRate { get; set; }
        public bool hotelMandatory { get; set; }
        public int allotment { get; set; }
        public string paymentType { get; set; }
        public bool packaging { get; set; }
        public string boardCode { get; set; }
        public string boardName { get; set; }
        public Taxes taxes { get; set; }
        public int rooms { get; set; }
        public int adults { get; set; }
        public int children { get; set; }
        public string childrenAges { get; set; }
        public string rateup { get; set; }
    }

    public class Room2
    {
        public string code { get; set; }
        public string name { get; set; }
        public List<Rate2> rates { get; set; }
    }

    public class Upselling
    {
        public List<Room2> rooms { get; set; }
    }

    public class Hotel
    {
        public string checkOut { get; set; }
        public string checkIn { get; set; }
        public int code { get; set; }
        public string name { get; set; }
        public string categoryCode { get; set; }
        public string categoryName { get; set; }
        public string destinationCode { get; set; }
        public string destinationName { get; set; }
        public int zoneCode { get; set; }
        public string zoneName { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public List<Room> rooms { get; set; }
        public string totalNet { get; set; }
        public string currency { get; set; }
        public Upselling upselling { get; set; }
        public bool paymentDataRequired { get; set; }
    }

    public class AvailabilityRes
    {
        public AuditData auditData { get; set; }
        public Hotel hotel { get; set; }
    }
}
