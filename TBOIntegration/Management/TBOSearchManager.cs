using Hotels.BLL;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Hotels.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBOIntegration.Mapper;
using TBOIntegration.Models.Search.CommonAttr;
using TBOIntegration.Models.Search.Req;
using TBOIntegration.Services;

namespace TBOIntegration.Management
{
    public class TBOSearchManager
    {
        public TBOSearchManager()
        {
            searchData = new SearchData();
            HotelIds = new List<string>();
            searchOutputs = new List<TBO.WSDL.hotelServiceRef.Hotel_Result>();
        }

        public SearchData searchData { set; get; }
        public List<string> HotelIds { set; get; }
        // TBO Response
        public List<TBO.WSDL.hotelServiceRef.Hotel_Result> searchOutputs { set; get; }

        public SearchReq PreparesSearchObj()
        {
            try
            {
                SearchReq searchInputData = new SearchReq();
                searchInputData.CheckInDate = Convert.ToDateTime(searchData.DateFrom);
                searchInputData.CheckOutDate = Convert.ToDateTime(searchData.DateTo);
                searchInputData.RoomGuests = new List<RoomGuest>();
                searchInputData.GuestNationality = searchData.Nat;
                searchInputData.HotelCodeList = HotelIds;
                searchInputData.NoOfRooms = searchData.SearchRooms.Count;
                searchInputData.CityId = int.Parse(searchData.CityName);
                foreach (var item in searchData.SearchRooms)
                {
                    RoomGuest guest = new RoomGuest
                    {
                        AdultCount = item.Adult,
                        ChildAge = item.Child.ToArray(),
                        ChildCount = item.Child.Count
                    };

                    searchInputData.RoomGuests.Add(guest);

                }


                return searchInputData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GetSearchResult()
        {
            try
            {
                SearchReq searchInputData = PreparesSearchObj();
                //map from general req to tbo req
                var TBOReq = SearchMapper.MapSearchReq(searchInputData);

                var searchResponse = SearchService.Search(TBOReq, searchData.sID);

                //save Provider session id in database
                SessionRepo manager = new SessionRepo();
                manager.SaveSessions(5, searchResponse.SessionId, searchData.sID ,searchData.SearchRooms);

                searchOutputs = searchResponse.HotelResultList.ToList();
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("TBOLogs/SearchController/Errors/", "TBOIntegrationManagement" + "INController" + searchData.sID, ex.InnerException?.Message, ex.Message + ex.StackTrace);

                throw ex;
            }
        }

        // Hotel Rooms HotelSearchRoom
        public HotelSearchRoom GetAvailableRoom(string SessionId, int ResIndex, string HotelCode, string SID)
        {
            //var roomexists= 
            //if (true)
            //{

            //}
            var Result = RoomAvailabiltyService.Availabilty(SessionId, ResIndex, HotelCode, SID);
            //map tbo rooms rsp to general res 
            HotelSearchRoom hotelSearchRoom = RoomMapper.MapTboRoomRspTogenrl(Result, SID);
            // apply business rules on rooms
            //ApplyRoomsBusiness(rooms);
            //save rooms in DB 
            SearchRepo manager = new SearchRepo();
            manager.SaveSearchRooms(hotelSearchRoom.Packages, SID, HotelCode);

            var Res = manager.GetTBOHotelDetails(hotelSearchRoom, HotelCode,SID);


            // return Res;
            return hotelSearchRoom;
        }

       



        public void ApplyRoomsBusiness(List<RoomResult> rooms)
        {
            CurrencyManager currencyManager = new CurrencyManager();
            SalesRulesManager ServiceChargeManager = new SalesRulesManager();
            SalesRulesManager CancellationChargeManager = new SalesRulesManager();
            var BaseCur = ConfigurationSettings.AppSettings["BaseCur"];

            double ProviderExcahngeRate = currencyManager.GetCurrencyConversion(searchOutputs[0].MinHotelPrice.Currency, BaseCur, searchData.sID);
            double ExcahngeRate = currencyManager.GetCurrencyConversion(BaseCur, searchData.Currency, searchData.sID);

            AppliedSalesRule AppliedCancellationMarkup = CancellationChargeManager.ApplySalesRules("Markup");
            List<RoomResult> result = new List<RoomResult>();
           
            //    for (int j = 0; j < rooms.Count; j++)
            //    {
            //        for (int x = 0; x < rooms[j].rates.Count; x++)
            //        {
            //            RoomResult room = new RoomResult();
            //            if (rooms[j].rates[x].net == null)
            //            {
            //                continue;
            //            }
            //            room.CostPrice = Math.Round(double.Parse(rooms[j].rates[x].net), 3);
            //            hotel.costPrice = room.CostPrice;
            //            CancellationChargeManager.SetResultCriteria(hotel.hotelName, hotel.hotelStars, hotel.costPrice, "5");
            //            ServiceChargeManager.SetResultCriteria(hotel.hotelName, hotel.hotelStars, room.CostPrice
            //                * ProviderExcahngeRate, "4");
            //            AppliedSalesRule AppliedMarkup = ServiceChargeManager.ApplySalesRules("Markup");
            //            AppliedSalesRule AppliedDiscount = ServiceChargeManager.ApplySalesRules("Discount");
            //            room.RatePerNight = ((room.CostPrice * ProviderExcahngeRate / duration) + AppliedMarkup.Value - AppliedDiscount.Value) * ExcahngeRate;
            //            room.RatePerNight = Math.Round(room.RatePerNight, 3);
            //            room.TotalSellPrice = Math.Round(room.RatePerNight * duration, 3);
            //            room.RoomIndex = j + 1;  // index front use
            //            room.RoomReference = rooms[j].rates[x].rateKey; // reference of provider
            //            /////
            //            room.RoomCode = rooms[j].code;
            //            /////////
            //            room.RoomType = rooms[j].name; // standard or double
            //            ///////
            //            room.RoomMeal = rooms[j].rates[x].boardName;
            //            room.Adult = rooms[j].rates[x].adults;
            //            room.Child = rooms[j].rates[x].children;
            //            room.IsRefundable = rooms[j].rates[x].rateClass == "NRF" ? false : true;
            //            ///////
            //            room.Paxs = rooms[j].rates[x].adults + rooms[j].rates[x].children;
            //            //***   room.Images = hotelData.Images.Where(a => a.Category.ToLower() == "hotel rooms").Select(a => a.Url).ToList();
            //            room.DiscountId = AppliedDiscount.ID;
            //            room.MarkupId = AppliedMarkup.ID;
            //            room.DiscountValue = AppliedDiscount.Value * ExcahngeRate;
            //            room.MarkupValue = AppliedMarkup.Value * ExcahngeRate;
            //            room.rateClass = rooms[j].rates[x].rateClass;
            //            room.rateType = rooms[j].rates[x].rateType;
            //            room.childrenAges = rooms[j].rates[x].childrenAges;
            //            room.paymentType = rooms[j].rates[x].paymentType;
            //            room.boardCode = rooms[j].rates[x].boardCode;
            //            room.cancellationRules = rooms[j].rates[x].cancellationPolicies == null ?
            //            new List<CancellationRule>() { new CancellationRule
            //            { Cost = 0, Price = 0, CanellationRuleText = null, FromDate = null, ToDate = null } } :
            //            rooms[j].rates[x].cancellationPolicies.Select(a => new CancellationRule
            //            {
            //                Price = Math.Round(((double.Parse(a.amount) * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3),
            //                CanellationRuleText = searchData.Lang.ToLower() == "en" ? Math.Round(((double.Parse(a.amount) * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3) + "" + searchData.Currency + " To " + a.from : a.from + " إلى " + "" + searchData.Currency + Math.Round(((double.Parse(a.amount) * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3),
            //                Cost = Math.Round(double.Parse(a.amount)),
            //                FromDate = a.from.ToString()
            //            }).ToList();
            //        }
            //    }

        }

    }
}
