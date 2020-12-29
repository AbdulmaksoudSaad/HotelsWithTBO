using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IDAL
{
   public interface IResultData
    {
        List<CityWithCountryName> GetAllCities( string code);
        List<Country> GetAllCounty();
        List<CityWithCountryName> AllCities();
    }
}
