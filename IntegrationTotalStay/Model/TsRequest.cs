using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IntegrationTotalStay.Model
{
    [XmlRoot(ElementName = "LoginDetails")]
    public class LoginDetails
    {
        [XmlElement(ElementName = "Login")]
        public string Login { get; set; }
        [XmlElement(ElementName = "Password")]
        public string Password { get; set; }
    }

    [XmlRoot(ElementName = "RoomRequest")]
    public class RoomRequest
    {
        [XmlElement(ElementName = "Adults")]
        public string Adults { get; set; }
        [XmlElement(ElementName = "Children")]
        public string Children { get; set; }
        [XmlElement(ElementName = "Infants")]
        public string Infants { get; set; }
        [XmlElement(ElementName = "ChildAges")]
        public ChildAges ChildAges { get; set; }
        public RoomRequest()
        {
            ChildAges = new ChildAges();
        }
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

    [XmlRoot(ElementName = "RoomRequests")]
    public class RoomRequests
    {
        [XmlElement(ElementName = "RoomRequest")]
        public List<RoomRequest> RoomRequest { get; set; }
        public RoomRequests()
        {
            RoomRequest = new List<RoomRequest>();
        }
    }

    [XmlRoot(ElementName = "SearchDetails")]
    public class SearchDetails
    {
        [XmlElement(ElementName = "ArrivalDate")]
        public string ArrivalDate { get; set; }
        [XmlElement(ElementName = "Duration")]
        public string Duration { get; set; }
        [XmlElement(ElementName = "RegionID")]
        public string RegionID { get; set; }
        [XmlElement(ElementName = "MealBasisID")]
        public string MealBasisID { get; set; }
        [XmlElement(ElementName = "MinStarRating")]
        public string MinStarRating { get; set; }
        [XmlElement(ElementName = "RoomRequests")]
        public RoomRequests RoomRequests { get; set; }
        public SearchDetails()
        {
            RoomRequests = new RoomRequests();
        }
    }

    [XmlRoot(ElementName = "SearchRequest")]
    public class SearchRequest
    {
        [XmlElement(ElementName = "LoginDetails")]
        public LoginDetails LoginDetails { get; set; }
        [XmlElement(ElementName = "SearchDetails")]
        public SearchDetails SearchDetails { get; set; }
        public SearchRequest()
        {
            LoginDetails = new LoginDetails();
            SearchDetails = new SearchDetails();
        }
    }
}
