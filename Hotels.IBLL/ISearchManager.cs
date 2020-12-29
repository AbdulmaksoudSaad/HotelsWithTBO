using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IBLL
{
    public interface ISearchManager
    {

        void SaveSearchCriteria();

        void SaveSearchResult(List<HotelSearchResult> hotelSearchResults , string sID);
    }
}
