using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBO.DAL.Models
{
    public class HotelDetail
    {
        public HotelDetail()
        {
            Facilities = new HashSet<Facility>(); ;
        }

        public int HotelDetailId { get; set; }
        public string Address { get; set; }
        public string HotelLocation { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string Description { get; set; }
        public string FaxNumber { get; set; }
        public string Map { get; set; }
        public string PhoneNumber { get; set; }
        public string PinCode { get; set; }
        public string HotelWebsiteUrl { get; set; }
        public string TripAdvisorRating { get; set; }
        public string TripAdvisorReviewURL { get; set; }
        public string CityName { get; set; }
        public string HotelCode { get; set; }
        public string HotelName { get; set; }
        public string HotelRating { get; set; }

        public string Attraction { get; set; }
        public string CityCode { get; set; }

        public virtual ICollection<Facility> Facilities { get; set; }
    }
}
