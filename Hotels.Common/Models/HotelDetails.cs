using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    public class HotelDetails
    {
        public int ID { set; get; }
        public string HotelId { set; get; }
        public string Address { set; get; }
        public string Rating { set; get; }
        public string HotelName { set; get; }
        public string Zipcode { set; get; }
        public string ProviderHotelId { set; get; }
        public string ProviderID { set; get; }
        public string Location { set; get; }
        public string ShortDescription { set; get; }
        public string LongDescriptin { set; get; }
        public string City { set; get; }
        public string Country { set; get; }
        public string Lat { set; get; }
        public string Lng { set; get; }
        public string Location1 { set; get; }
        public string Location2 { set; get; }
        public string Location3 { set; get; }
        public List<Image> Images { set; get; }
        public List<HotelAmenity> hotelAmenities { set; get; }
        public HotelDetails()
        {

            Images = new List<Image>();
        }
    }
}
