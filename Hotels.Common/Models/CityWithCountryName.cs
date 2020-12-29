using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
   public class CityWithCountryName
    {
        public int CityId { get; set; }
        public string City  { get; set; }
        public string Country { get; set; }

        public string CityWithCountry { get; set; }
        public string CountryCode { get; set; }

        //public int? ProviderCity { get; set; }
        //public int? ProviderId { get; set; }
    }
}
