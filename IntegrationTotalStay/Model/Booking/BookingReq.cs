using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IntegrationTotalStay.Model.Booking
{
    [XmlRoot(ElementName = "LoginDetails")]
    public class LoginDetails
    {
        [XmlElement(ElementName = "Login")]
        public string Login { get; set; }
        [XmlElement(ElementName = "Password")]
        public string Password { get; set; }
    }

    [XmlRoot(ElementName = "Guest")]
    public class Guest
    {
        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "Title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "FirstName")]
        public string FirstName { get; set; }
        [XmlElement(ElementName = "LastName")]
        public string LastName { get; set; }
        [XmlElement(ElementName = "Age")]
        public string Age { get; set; }
        [XmlElement(ElementName = "DateOfBirth")]
        public string DateOfBirth { get; set; }
        [XmlElement(ElementName = "Nationality")]
        public string Nationality { get; set; }
    }

    [XmlRoot(ElementName = "Guests")]
    public class Guests
    {
        [XmlElement(ElementName = "Guest")]
        public List<Guest> Guest { get; set; }
        public Guests()
        {
            Guest = new List<Guest>();
        }
    }

    [XmlRoot(ElementName = "RoomBooking")]
    public class RoomBooking
    {
        [XmlElement(ElementName = "PropertyRoomTypeID")]
        public string PropertyRoomTypeID { get; set; }
        [XmlElement(ElementName = "BookingToken")]
        public string BookingToken { get; set; }
        [XmlElement(ElementName = "MealBasisID")]
        public string MealBasisID { get; set; }
        [XmlElement(ElementName = "Adults")]
        public string Adults { get; set; }
        [XmlElement(ElementName = "Children")]
        public string Children { get; set; }
        [XmlElement(ElementName = "Infants")]
        public string Infants { get; set; }
        [XmlElement(ElementName = "Guests")]
        public Guests Guests { get; set; }
        [XmlElement(ElementName = "OptionalSupplements")]
        public string OptionalSupplements { get; set; }
        public RoomBooking()
        {
            Guests = new Guests();
        }
    }

    [XmlRoot(ElementName = "RoomBookings")]
    public class RoomBookings
    {
        [XmlElement(ElementName = "RoomBooking")]
        public List<RoomBooking> RoomBooking { get; set; }
        public RoomBookings()
        {
            RoomBooking = new List<RoomBooking>();
        }
    }

    [XmlRoot(ElementName = "BookingDetails")]
    public class BookingDetails
    {
        [XmlElement(ElementName = "PropertyID")]
        public string PropertyID { get; set; }
        [XmlElement(ElementName = "PreBookingToken")]
        public string PreBookingToken { get; set; }
        [XmlElement(ElementName = "ArrivalDate")]
        public string ArrivalDate { get; set; }
        [XmlElement(ElementName = "Duration")]
        public string Duration { get; set; }
        [XmlElement(ElementName = "LeadGuestTitle")]
        public string LeadGuestTitle { get; set; }
        [XmlElement(ElementName = "LeadGuestFirstName")]
        public string LeadGuestFirstName { get; set; }
        [XmlElement(ElementName = "LeadGuestLastName")]
        public string LeadGuestLastName { get; set; }
        [XmlElement(ElementName = "LeadGuestAddress1")]
        public string LeadGuestAddress1 { get; set; }
        [XmlElement(ElementName = "LeadGuestPostcode")]
        public string LeadGuestPostcode { get; set; }
        [XmlElement(ElementName = "LeadGuestBookingCountryID")]
        public string LeadGuestBookingCountryID { get; set; }
        [XmlElement(ElementName = "LeadGuestPhone")]
        public string LeadGuestPhone { get; set; }
        [XmlElement(ElementName = "LeadGuestEmail")]
        public string LeadGuestEmail { get; set; }
        [XmlElement(ElementName = "ContractSpecialOfferID")]
        public string ContractSpecialOfferID { get; set; }
        [XmlElement(ElementName = "ContractArrangementID")]
        public string ContractArrangementID { get; set; }
        [XmlElement(ElementName = "TradeReference")]
        public string TradeReference { get; set; }
        [XmlElement(ElementName = "Request")]
        public string Request { get; set; }
        [XmlElement(ElementName = "CCCardTypeID")]
        public string CCCardTypeID { get; set; }
        [XmlElement(ElementName = "CCIssueNumber")]
        public string CCIssueNumber { get; set; }
        [XmlElement(ElementName = "CCAmount")]
        public string CCAmount { get; set; }
        [XmlElement(ElementName = "RoomBookings")]
        public RoomBookings RoomBookings { get; set; }
        public BookingDetails()
        {
            RoomBookings = new RoomBookings();
        }
    }

    [XmlRoot(ElementName = "BookRequest")]
    public class BookRequest
    {
        [XmlElement(ElementName = "LoginDetails")]
        public LoginDetails LoginDetails { get; set; }
        [XmlElement(ElementName = "BookingDetails")]
        public BookingDetails BookingDetails { get; set; }
        public BookRequest()
        {
            LoginDetails = new LoginDetails();
            BookingDetails = new BookingDetails();
        }
    }
}
