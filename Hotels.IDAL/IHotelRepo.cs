using Hotels.Common;
using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IDAL
{
    public interface IHotelRepo 
    {

        List<HotelProviderData> GetHotelIdsForActiveProviders(int CityId);

        List<HotelDetails> GetHotelData(List<string> HotelProvidersIds, string providerId);
        CheckAvailabilityForTs GetHotelDataForTsAvailability(Common.Models.AvailabilityReq roomsReq);

        List<HotelDetails> GetChannelHotelData(List<string> HotelProvidersIds, string providerId); 

    }
}
