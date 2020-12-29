using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBedsIntegration.Models
{
    
    public class AuditDataB
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

    public class ModificationPolicies
    {
        public bool cancellation { get; set; }
        public bool modification { get; set; }
    }

    public class HolderB
    {
        public string name { get; set; }
        public string surname { get; set; }
    }

    public class PaxRes
    {
        public int roomId { get; set; }
        public string type { get; set; }
        public int age { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
    }

    public class CancellationPolicy
    {
        public string amount { get; set; }
        public DateTime from { get; set; }
    }

    public class RateB
    {
        public string rateClass { get; set; }
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
    }

    public class RoomRes
    {
        public string status { get; set; }
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public List<PaxRes> paxes { get; set; }
        public List<RateB> rates { get; set; }
    }

    public class Supplier
    {
        public string name { get; set; }
        public string vatNumber { get; set; }
    }

    public class HotelB
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
        public List<RoomRes> rooms { get; set; }
        public string totalNet { get; set; }
        public string currency { get; set; }
        public Supplier supplier { get; set; }
    }

    public class InvoiceCompany
    {
        public string code { get; set; }
        public string company { get; set; }
        public string registrationNumber { get; set; }
    }

    public class Booking
    {
        public string reference { get; set; }
        public string clientReference { get; set; }
        public string creationDate { get; set; }
        public string status { get; set; }
        public ModificationPolicies modificationPolicies { get; set; }
        public string creationUser { get; set; }
        public HolderB holder { get; set; }
        public HotelB hotel { get; set; }
        public InvoiceCompany invoiceCompany { get; set; }
        public double totalNet { get; set; }
        public double pendingAmount { get; set; }
        public string currency { get; set; }
    }

    public class BookingRes
    {
        public AuditDataB auditData { get; set; }
        public Booking booking { get; set; }
    }
}
