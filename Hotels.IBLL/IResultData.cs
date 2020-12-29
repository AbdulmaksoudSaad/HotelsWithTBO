using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IBLL
{
    public interface IResultData
    {
        SearchRequiredData GetAllCitiesAndCountries();

        void SaveSearchData(SearchData searchData);

        void SaveSearchResult(List<HotelSearchResult> hotelSearchResults, string sID);
    }
}
