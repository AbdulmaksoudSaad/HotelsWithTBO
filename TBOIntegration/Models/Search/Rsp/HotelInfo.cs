using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBOIntegration.Models.Search.Rsp
{
    public class HotelInfo
    {
        public string HotelCode { get; set; }
        public string HotelName { get; set; }
        public string HotelPicture { get; set; }
        public string HotelDescription { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string HotelAddress { get; set; }
        public string Rating { get; set; }
        public string TripAdvisorRating { get; set; }
        public string TripAdvisorReviewURL { get; set; }
        public string TagIds { get; set; }
        public string HotelPromotion { get; set; }
    }
}
