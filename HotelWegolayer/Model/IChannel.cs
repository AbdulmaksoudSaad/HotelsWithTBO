using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelWegolayer.Model
{
    interface IChannelSearch<T>
    {
        void GetHotelsProviderID(List<string> Hotelid);
        void SaveMetaSearchData(T Data);
        void GetHotelSearchResultForAllProviders();

        void SaveSearchResultForAllProviders();




    }
}
