using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IntegrationTotalStay.Model
{
    /*   [XmlRoot(ElementName = "ReturnStatus")]
       public class ReturnStatus
       {
           [XmlElement(ElementName = "Success")]
           public string Success { get; set; }
           [XmlElement(ElementName = "Exception")]
           public string Exception { get; set; }
       }

       [XmlRoot(ElementName = "RoomType")]
       public class RoomType
       {
           [XmlElement(ElementName = "Seq")]
           public string Seq { get; set; }
           [XmlElement(ElementName = "PropertyRoomTypeID")]
           public string PropertyRoomTypeID { get; set; }

           [XmlElement(ElementName = "MealBasisID")]
           public string MealBasisID { get; set; }
           [XmlElement(ElementName = "RoomType")]
           public string RoomTyper { get; set; }
           [XmlElement(ElementName = "RoomView")]
           public string RoomView { get; set; }
           [XmlElement(ElementName = "MealBasis")]
           public string MealBasis { get; set; }
           [XmlElement(ElementName = "SubTotal")]
           public string SubTotal { get; set; }
           [XmlElement(ElementName = "Discount")]
           public string Discount { get; set; }
           [XmlElement(ElementName = "OnRequest")]
           public string OnRequest { get; set; }
           [XmlElement(ElementName = "Total")]
           public string Total { get; set; }
           [XmlElement(ElementName = "Adults")]
           public string Adults { get; set; }
           [XmlElement(ElementName = "Children")]
           public string Children { get; set; }
           [XmlElement(ElementName = "Infants")]
           public string Infants { get; set; }
           [XmlElement(ElementName = "Adjustments")]
           public string Adjustments { get; set; }
           [XmlElement(ElementName = "Errata")]
           public string Errata { get; set; }
           [XmlElement(ElementName = "OptionalSupplements")]
           public string OptionalSupplements { get; set; }
       }

       [XmlRoot(ElementName = "RoomTypes")]
       public class RoomTypes
       {
           [XmlElement(ElementName = "RoomType")]
           public List<RoomType> RoomType { get; set; }
       }

       [XmlRoot(ElementName = "PropertyResult")]
       public class PropertyResult
       {
           [XmlElement(ElementName = "TotalProperties")]
           public string TotalProperties { get; set; }
           [XmlElement(ElementName = "PropertyID")]
           public string PropertyID { get; set; }
           [XmlElement(ElementName = "PropertyReferenceID")]
           public string PropertyReferenceID { get; set; }
           [XmlElement(ElementName = "PropertyName")]
           public string PropertyName { get; set; }
           [XmlElement(ElementName = "Rating")]
           public string Rating { get; set; }
           [XmlElement(ElementName = "OurRating")]
           public string OurRating { get; set; }
           [XmlElement(ElementName = "Country")]
           public string Country { get; set; }
           [XmlElement(ElementName = "Region")]
           public string Region { get; set; }
           [XmlElement(ElementName = "Resort")]
           public string Resort { get; set; }
           [XmlElement(ElementName = "SearchURL")]
           public string SearchURL { get; set; }
           [XmlElement(ElementName = "RoomTypes")]
           public RoomTypes RoomTypes { get; set; }
       }

       [XmlRoot(ElementName = "PropertyResults")]
       public class PropertyResults
       {
           [XmlElement(ElementName = "PropertyResult")]
           public List<PropertyResult> PropertyResult { get; set; }
       }

       [XmlRoot(ElementName = "SearchResponse")]
       public class SearchResponse
       {
           [XmlElement(ElementName = "ReturnStatus")]
           public ReturnStatus ReturnStatus { get; set; }
           [XmlElement(ElementName = "SearchURL")]
           public string SearchURL { get; set; }
           [XmlElement(ElementName = "PropertyResults")]
           public PropertyResults PropertyResults { get; set; }
       }*/
    [XmlRoot(ElementName = "ReturnStatus")]
    public class ReturnStatus
    {
        [XmlElement(ElementName = "Success")]
        public string Success { get; set; }
        [XmlElement(ElementName = "Exception")]
        public string Exception { get; set; }
    }

    [XmlRoot(ElementName = "RoomType")]
    public class RoomType
    {
        [XmlElement(ElementName = "Seq")]
        public string Seq { get; set; }
        [XmlElement(ElementName = "PropertyRoomTypeID")]
        public string PropertyRoomTypeID { get; set; }
        [XmlElement(ElementName = "MealBasisID")]
        public string MealBasisID { get; set; }
        [XmlElement(ElementName = "RoomType")]
        public string RoomTyper { get; set; }
        [XmlElement(ElementName = "RoomView")]
        public string RoomView { get; set; }
        [XmlElement(ElementName = "MealBasis")]
        public string MealBasis { get; set; }
        [XmlElement(ElementName = "SubTotal")]
        public string SubTotal { get; set; }
        [XmlElement(ElementName = "Discount")]
        public string Discount { get; set; }
        [XmlElement(ElementName = "OnRequest")]
        public string OnRequest { get; set; }
        [XmlElement(ElementName = "Total")]
        public string Total { get; set; }
        [XmlElement(ElementName = "Adults")]
        public string Adults { get; set; }
        [XmlElement(ElementName = "Children")]
        public string Children { get; set; }
        [XmlElement(ElementName = "Infants")]
        public string Infants { get; set; }
        [XmlElement(ElementName = "Adjustments")]
        public string Adjustments { get; set; }
        [XmlElement(ElementName = "OptionalSupplements")]
        public string OptionalSupplements { get; set; }
        [XmlElement(ElementName = "BookingToken")]
        public string BookingToken { get; set; }
        [XmlElement(ElementName = "RSP")]
        public string RSP { get; set; }
        [XmlElement(ElementName = "Errata")]
        public Errata Errata { get; set; }
    }

    [XmlRoot(ElementName = "RoomTypes")]
    public class RoomTypes
    {
        [XmlElement(ElementName = "RoomType")]
        public List<RoomType> RoomType { get; set; }
    }

    [XmlRoot(ElementName = "PropertyResult")]
    public class PropertyResult
    {
        [XmlElement(ElementName = "TotalProperties")]
        public string TotalProperties { get; set; }
        [XmlElement(ElementName = "PropertyID")]
        public string PropertyID { get; set; }
        [XmlElement(ElementName = "PropertyReferenceID")]
        public string PropertyReferenceID { get; set; }
        [XmlElement(ElementName = "PropertyName")]
        public string PropertyName { get; set; }
        [XmlElement(ElementName = "Rating")]
        public string Rating { get; set; }
        [XmlElement(ElementName = "OurRating")]
        public string OurRating { get; set; }
        [XmlElement(ElementName = "Country")]
        public string Country { get; set; }
        [XmlElement(ElementName = "Region")]
        public string Region { get; set; }
        [XmlElement(ElementName = "Resort")]
        public string Resort { get; set; }
        [XmlElement(ElementName = "SearchURL")]
        public string SearchURL { get; set; }
        [XmlElement(ElementName = "RoomTypes")]
        public RoomTypes RoomTypes { get; set; }
    }

    [XmlRoot(ElementName = "Erratum")]
    public class Erratum
    {
        [XmlElement(ElementName = "Subject")]
        public string Subject { get; set; }
        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }
    }

    [XmlRoot(ElementName = "Errata")]
    public class Errata
    {
        [XmlElement(ElementName = "Erratum")]
        public List<Erratum> Erratum { get; set; }
    }

    [XmlRoot(ElementName = "PropertyResults")]
    public class PropertyResults
    {
        [XmlElement(ElementName = "PropertyResult")]
        public List<PropertyResult> PropertyResult { get; set; }
    }

    [XmlRoot(ElementName = "SearchResponse")]
    public class SearchResponse
    {
        [XmlElement(ElementName = "ReturnStatus")]
        public ReturnStatus ReturnStatus { get; set; }
        [XmlElement(ElementName = "SearchURL")]
        public string SearchURL { get; set; }
        [XmlElement(ElementName = "PropertyResults")]
        public PropertyResults PropertyResults { get; set; }
    }
}
