using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IntegrationTotalStay.Model.Availability
{
    [XmlRoot(ElementName = "LoginDetails")]
    public class LoginDetails
    {
        [XmlElement(ElementName = "Login")]
        public string Login { get; set; }
        [XmlElement(ElementName = "Password")]
        public string Password { get; set; }
        [XmlElement(ElementName = "CurrencyID")]
        public string CurrencyID { get; set; }
    }

    [XmlRoot(ElementName = "ChildAge")]
    public class ChildAge
    {
        [XmlElement(ElementName = "Age")]
        public string Age { get; set; }
    }

    [XmlRoot(ElementName = "ChildAges")]
    public class ChildAges
    {
        [XmlElement(ElementName = "ChildAge")]
        public List<ChildAge> ChildAge { get; set; }
        public ChildAges()
        {
            ChildAge = new List<ChildAge>();
        }
    }

    [XmlRoot(ElementName = "RoomBooking")]
    public class RoomBooking
    {
        [XmlElement(ElementName = "PropertyRoomTypeID")]
        public string PropertyRoomTypeID { get; set; }
        [XmlElement(ElementName = "BookingToken")]

        public string  BookingToken { get; set; }

        [XmlElement(ElementName = "MealBasisID")]
        public string MealBasisID { get; set; }
        [XmlElement(ElementName = "Adults")]
        public string Adults { get; set; }
        [XmlElement(ElementName = "Children")]
        public string Children { get; set; }
        [XmlElement(ElementName = "ChildAges")]
        public ChildAges ChildAges { get; set; }
        [XmlElement(ElementName = "Infants")]
        public string Infants { get; set; }
        public RoomBooking()
        {
            ChildAges = new ChildAges();
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
        [XmlElement(ElementName = "ArrivalDate")]
        public string ArrivalDate { get; set; }
        [XmlElement(ElementName = "Duration")]
        public string Duration { get; set; }
        [XmlElement(ElementName = "RoomBookings")]
        public RoomBookings RoomBookings { get; set; }
        public BookingDetails()
        {
            RoomBookings = new RoomBookings();
        }
    }

    [XmlRoot(ElementName = "PreBookRequest")]
    public class PreBookRequest
    {
        [XmlElement(ElementName = "LoginDetails")]
        public LoginDetails LoginDetails { get; set; }
        [XmlElement(ElementName = "BookingDetails")]
        public BookingDetails BookingDetails { get; set; }
        public PreBookRequest()
        {
            LoginDetails = new LoginDetails();
            BookingDetails = new BookingDetails();
        }
    }
}
