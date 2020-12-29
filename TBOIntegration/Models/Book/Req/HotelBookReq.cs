using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBOIntegration.Models.Book.Req
{

    public class HotelBookReq
    {
        public string ClientReferenceNumber { get; set; }
        public string GuestNationality { get; set; }
        public List<Guest> Guests { get; set; }
        public AddressInfo AddressInfo { get; set; }
        public PaymentInfo PaymentInfo { get; set; }
        public string SessionId { get; set; }
        public int NoOfRooms { get; set; }
        public int ResultIndex { get; set; }
        public string HotelCode { get; set; }
        public string HotelName { get; set; }
        public List<HotelRoom> HotelRooms { get; set; }

    }
    public class Supplement
    {
        public int SuppID { get; set; }
        public string SuppChargeType { get; set; }
        public decimal Price { get; set; }
        public bool SuppIsSelected { get; set; }
    }
    public class Guest
    {
        public bool LeadGuest { get; set; }
        /// <summary>
        ///   Adult,Child
        /// </summary>
        public string GuestType { get; set; }
        public int GuestInRoom { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class AddressInfo
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CountryCode { get; set; }
        public string AreaCode { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }

    public class PaymentInfo
    {
        /// <summary>
        /// Set true, to voucher a booking
        /// Set false, to create a confirm booking
        /// </summary>
        public bool VoucherBooking { get; set; }
        public string PaymentModeType { get; set; }
    }

    public class RoomRate
    {
        public decimal RoomFare { get; set; }
        public string Currency { get; set; }
        public string AgentMarkUp { get; set; }
        public decimal RoomTax { get; set; }
        public decimal TotalFare { get; set; }
    }

    public class HotelRoom
    {
        public int RoomIndex { get; set; }
        public string RoomTypeName { get; set; }
        public string RoomTypeCode { get; set; }
        public string RatePlanCode { get; set; }
        public RoomRate RoomRate { get; set; }
        public List<Supplement> Supplements { get; set; }

    }

}
