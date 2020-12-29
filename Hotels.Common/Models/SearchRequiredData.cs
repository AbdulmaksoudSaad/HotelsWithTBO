using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class SearchRequiredData
    {
        public List<CityWithCountryName> cities { set; get; }

        public List<Country> countries { set; get; }

        public SearchRequiredData()
        {
             cities = new List<CityWithCountryName>();

            countries = new List<Country>();
        }
    }
}
