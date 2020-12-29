using Hotels.Common.Models;
using Hotels.DAL;
using Hotels.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.BLL
{
    public class HotelManager : IHotelManager
    {
        public List<HotelDetails> GetChannelHotelData(List<string> HotelProviderIds, string providerId)
        {
            HotelRepo repo = new HotelRepo();
            return repo.GetChannelHotelData(HotelProviderIds, providerId);
        }

        public List<HotelDetails> GetHotelData(List<string> HotelProviderIds, string providerId)
        {
            HotelRepo repo = new HotelRepo();
            return repo.GetHotelData(HotelProviderIds, providerId);
        }

        public CheckAvailabilityForTs GetHotelDataForTsAvailability(AvailabilityReq roomsReq)
        {
            HotelRepo repo = new HotelRepo();
         return  repo.GetHotelDataForTsAvailability(roomsReq);
            
        }
         //for tbo only
        public List<HotelDetails> GetHotelDetailsForTBO(List<string> HotelProviderIds)
        {
            TBORepo tBORepo = new TBORepo();
            return tBORepo.GetHotelDetails(HotelProviderIds);
        }

    }
}
