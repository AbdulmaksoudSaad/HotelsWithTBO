using Hotels.Common;
using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IDAL
{
   public interface IProvidersChannel
    {
        string GetSearchData(List<string> Hotelids);
        void SaveSearchResult(List<HotelChannelResult> results, string sID);
        List<hotelsProvider> GetHotelProvides(List<string> Hotelids);
        HotelPackagesDetails GetHotelPackages(string pid, string sid, string hid);
    }
}
