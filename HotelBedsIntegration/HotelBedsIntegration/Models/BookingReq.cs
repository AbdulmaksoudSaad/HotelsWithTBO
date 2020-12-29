using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBedsIntegration.Models
{
     
    public class Holder
    {
        public string name { get; set; }
        public string surname { get; set; }
    }

    public class PaxB
    {
        public string roomId { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
    }

    public class RoomB
    {
        public string rateKey { get; set; }
        public List<PaxB> paxes { get; set; }
    }
    public class PaymentCard
    {
        public string cardType { get; set; }
        public string cardHolderName { get; set; }
        public int cardNumber { get; set; }
        public int expiryDate { get; set; }
        public int cardCVC { get; set; }
    }

    public class ContactData
    {
        public string email { get; set; }
        public string phoneNumber { get; set; }
    }

    public class PaymentData
    {
        public PaymentCard paymentCard { get; set; }
        public ContactData contactData { get; set; }
    }
    public class BookingReq
    {
        public Holder holder { get; set; }
        public List<RoomB> rooms { get; set; }
        public string clientReference { get; set; }
        public PaymentData paymentData { get; set; }
    }
}
