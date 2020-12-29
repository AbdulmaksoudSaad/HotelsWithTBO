using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IDAL
{
  public interface ISearchRepo
    {
        void SaveSearchData(SearchData searchData);

        void SaveSearchResult(List<HotelSearchResult> hotelSearchResults, string sID);
    }
}
