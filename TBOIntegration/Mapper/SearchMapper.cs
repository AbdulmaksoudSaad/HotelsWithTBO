using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBO.WSDL.hotelServiceRef;
using TBOIntegration.Models.Search.Req;

namespace TBOIntegration.Mapper
{
    public static class SearchMapper
    {
        //map general req to TBO req
        public static HotelSearchRequest MapSearchReq(SearchReq searchReq)
        {
            List<TBO.WSDL.hotelServiceRef.RoomGuest> guests = new List<TBO.WSDL.hotelServiceRef.RoomGuest>();
            foreach (var item in searchReq.RoomGuests)
            {
                guests.Add(new TBO.WSDL.hotelServiceRef.RoomGuest()
                {
                    AdultCount = item.AdultCount,
                    ChildAge = item.ChildAge,
                    ChildCount = item.ChildCount
                });
            }
            HotelSearchRequest TBOReq = new HotelSearchRequest
            {
                CheckInDate = searchReq.CheckInDate.Date,
                CheckOutDate = searchReq.CheckOutDate.Date,
                CityId =searchReq.CityId,
                CityName = searchReq.CityName,
                CountryName = searchReq.CountryName,
                GuestNationality = searchReq.GuestNationality,
                NoOfRooms = searchReq.NoOfRooms,
                RoomGuests = guests.ToArray(),
                Filters = new Filters { HotelCodeList = string.Join(",", searchReq.HotelCodeList.ToArray())}
            };
            return TBOReq;
        }


        ////map TBO Rsp to General Rsp
        //public static Hotels.Common.Models.HotelSearchResponse MapSearchRsp(TBO.WSDL.hotelServiceRef.HotelSearchResponse TBORsp)
        //{
        //    Hotels.Common.Models.HotelSearchResponse response = new Hotels.Common.Models.HotelSearchResponse
        //    {
        //        HotelResult =
        //    };
        //    TBORsp.
        //}
    }
}
