using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IBLL
{
    public interface IHotelManager
    {
      List<HotelDetails> GetHotelData(List<string>HotelProviderIds,string providerId);
        CheckAvailabilityForTs GetHotelDataForTsAvailability(Common.Models.AvailabilityReq roomsReq);
        List<HotelDetails> GetChannelHotelData(List<string> HotelProviderIds, string providerId);


    }
}
