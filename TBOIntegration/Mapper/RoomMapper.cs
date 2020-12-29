using Hotels.BLL;
using Hotels.Common;
using Hotels.Common.Models;
using Hotels.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBO.WSDL.hotelServiceRef;
using static Hotels.Common.Models.HotelSearchRoom;
using static Hotels.Common.Models.RoomResult;
using static Hotels.Common.Models.SeparatedRoom;

namespace TBOIntegration.Mapper
{
    public static class RoomMapper
    {
        //map tbo rooms rsp to general res 

        public static HotelSearchRoom MapTboRoomRspTogenrl(HotelRoomAvailabilityResponse response, string SID)
        {

            if (response.HotelRooms == null)
            {
                return null;
            }
            //get ADT CHD NO
            SearchRepo manager = new SearchRepo();
            var rooms = manager.GetADTCHDNoBySid(SID);

            CurrencyManager currencyManager = new CurrencyManager();
            SalesRulesManager ServiceChargeManager = new SalesRulesManager();
            SalesRulesManager CancellationChargeManager = new SalesRulesManager();
            var BaseCur = ConfigurationSettings.AppSettings["BaseCur"];

            double ProviderExcahngeRate = currencyManager.GetCurrencyConversion(response.HotelRooms[0].RoomRate.Currency, BaseCur, SID);

            double ExcahngeRate = currencyManager.GetCurrencyConversion(BaseCur, response.HotelRooms[0].RoomRate.Currency, SID);

            AppliedSalesRule AppliedCancellationMarkup = CancellationChargeManager.ApplySalesRules("Markup");

            //
            //options for booking
            List<OptionsForBooking> options = new List<OptionsForBooking>();
            foreach (var item in response.OptionsForBooking.RoomCombination)
            {
                options.Add(new OptionsForBooking
                {
                    RoomIndex = item.RoomIndex
                });
            }
            List<RoomResult> roomResults = new List<RoomResult>();
            foreach (var item in response.HotelRooms.ToList())
            {
                //supplements
                List<Hotels.Common.Models.RoomResult.Supplement> supplements = new List<Hotels.Common.Models.RoomResult.Supplement>();
                if (item.Supplements != null)
                {
                    foreach (var sup in item.Supplements)
                    {
                        supplements.Add(new Hotels.Common.Models.RoomResult.Supplement
                        {
                            Price = sup.Price,
                            SuppChargeType = sup.SuppChargeType.ToString(),
                            SuppID = sup.SuppID,
                            SuppIsSelected = sup.SuppIsMandatory,
                            Cur = sup.CurrencyCode,
                        });
                    }
                }

                List<CancellationRule> cancellationRules = new List<CancellationRule>();
                foreach (var cancel in item.CancelPolicies.CancelPolicy)
                {
                    double costToSave = 0;
                    string Cur = "";
                    //Enum.TryParse(cancel.ChargeType.ToString(), out CancellationChargeTypeForHotel Type);

                    if (cancel.ChargeType == CancellationChargeTypeForHotel.Percentage)
                    {
                        costToSave = (double)cancel.CancellationCharge;
                        Cur = "%";
                    }
                    else
                    {
                        Cur = "KWD";
                        costToSave = (double)cancel.CancellationCharge * ProviderExcahngeRate; //Math.Round((double)cancel.CancellationCharge * ProviderExcahngeRate, 3);
                    }
                    cancellationRules.Add(new CancellationRule
                    {
                        Price = costToSave,
                        Curency = Cur,
                        FromDate = cancel.FromDate,
                        ToDate = cancel.ToDate,
                        //Cost = (double)cancel.CancellationCharge,
                        Cost = (double)cancel.CancellationCharge,
                        ChargeType = cancel.ChargeType.ToString(),
                    });

                }
                //foreach (var PaxNo in rooms)
                //{

                    roomResults.Add(new RoomResult
                    {
                        RoomIndex = item.RoomIndex,
                        RoomType = item.RoomTypeName +"   "+item?.RoomPromtion,
                        //RoomCode = item.RoomIndex,
                        RoomCode = item.RoomTypeCode,
                        RoomReference = item.RatePlanCode,
                        //Adult = rooms.FirstOrDefault(ad=>ad.roomNo == item.RoomIndex).adultNo ?? default(int),
                        //Child = rooms.FirstOrDefault(ad => ad.roomNo == item.RoomIndex).childernNo ?? default(int),
                        // RatePerNight = ((double)item.RoomRate.TotalFare * ProviderExcahngeRate ) * ExcahngeRate,

                        //TotalSellPrice = Math.Round(((double)item.RoomRate.TotalFare * ProviderExcahngeRate) * ExcahngeRate, 3),

                //tax
                        rateType = item.RoomRate.RoomTax.ToString(),
                        //base
                        rateClass = item.RoomRate.RoomFare.ToString(),
                        //RatePerNight = (double)item.RoomRate.DayRates[0].BaseFare,
                        CostPrice = (double)item.RoomRate.TotalFare,
                        Tax = item.RoomRate.RoomTax,
                        RoomMeal = item.MealType,
                        //sell curency   kwd
                        Curency = BaseCur,
                        TotalSellPrice = Math.Round((double)item.RoomRate.TotalFare * ProviderExcahngeRate, 3),
                        Supplements = supplements,
                        cancellationRules = cancellationRules,
                        Images = item?.RoomAdditionalInfo?.ImageURLs.ToList(),
                        Amenities = item.Amenities,
                        Inclusion = item.Inclusion
                    });
                //}
            }
            HotelSearchRoom hotelSearchRoom = new HotelSearchRoom();
            int PkageNO = 0;
            foreach (var item in options)
            {
                PkageNO++;
                SearchRoomData roomresult = new SearchRoomData();
                roomresult = null;
                SeparatedRoom srts = new SeparatedRoom();
                for (int i = 0; i < item.RoomIndex.Count(); i++)
                {
                    var resultsR = roomResults.Where(res => res.RoomIndex == item.RoomIndex[i]).FirstOrDefault();
                    resultsR.PackageNO = PkageNO;
                    srts.RoomResults.Add(resultsR);
                }
                hotelSearchRoom.rooms.Add(srts);

            }

            SeparatedRoom separatedRoom = new SeparatedRoom
            {
                OptionsForBookings = options,
                RoomResults = roomResults
            };

            var singles = roomResults.GroupBy(n => n.RoomCode).ToList()
                     //.Where(g => g.Count() == 1)
                     .Select(g => g.First()).ToList();
            hotelSearchRoom.Packages = singles;
            return hotelSearchRoom;
        }

        //public static void ApplyBusinesRules()
        //{
        //    TBOMapper tBOMapper = new TBOMapper();
        //    SalesRulesManager CancellationChargeManager = new SalesRulesManager();

        //    hotel.rooms = new List<RoomResult>();

        //    AppliedSalesRule AppliedCancellationMarkup = CancellationChargeManager.ApplySalesRules("Markup");

        //    for (int j = 0; j < searchOutputs[i].rooms.Count; j++)
        //    {
        //        for (int x = 0; x < searchOutputs[i].rooms[j].rates.Count; x++)
        //        {
        //            RoomResult room = new RoomResult();
        //            if (searchOutputs[i].rooms[j].rates[x].net == null)
        //            {
        //                continue;
        //            }
        //            room.CostPrice = Math.Round(double.Parse(searchOutputs[i].rooms[j].rates[x].net), 3);
        //            hotel.costPrice = room.CostPrice;
        //            CancellationChargeManager.SetResultCriteria(hotel.hotelName, hotel.hotelStars, hotel.costPrice, "4");
        //            ServiceChargeManager.SetResultCriteria(hotel.hotelName, hotel.hotelStars, room.CostPrice
        //                * ProviderExcahngeRate, "4");
        //            AppliedSalesRule AppliedMarkup = ServiceChargeManager.ApplySalesRules("Markup");
        //            AppliedSalesRule AppliedDiscount = ServiceChargeManager.ApplySalesRules("Discount");
        //            //  room.IsRefundable = searchOutputs[i].rooms[j].refundable;
        //            ////******
        //            room.RatePerNight = ((room.CostPrice * ProviderExcahngeRate / duration) + AppliedMarkup.Value - AppliedDiscount.Value) * ExcahngeRate;
        //            room.RatePerNight = Math.Round(room.RatePerNight, 3);
        //            room.TotalSellPrice = Math.Round(room.RatePerNight * duration, 3);
        //            room.RoomIndex = j + 1;  // index front use
        //            room.RoomReference = searchOutputs[i].rooms[j].rates[x].rateKey; // reference of provider
        //            /////
        //            room.RoomCode = searchOutputs[i].rooms[j].code;
        //            /////////
        //            room.RoomType = searchOutputs[i].rooms[j].name; // standard or double
        //            ///////
        //            room.RoomMeal = searchOutputs[i].rooms[j].rates[x].boardName;
        //            room.Adult = searchOutputs[i].rooms[j].rates[x].adults;
        //            room.Child = searchOutputs[i].rooms[j].rates[x].children;
        //            room.IsRefundable = searchOutputs[i].rooms[j].rates[x].rateClass == "NRF" ? false : true;
        //            ///////
        //            room.Paxs = searchOutputs[i].rooms[j].rates[x].adults + searchOutputs[i].rooms[j].rates[x].children;
        //            //***   room.Images = hotelData.Images.Where(a => a.Category.ToLower() == "hotel rooms").Select(a => a.Url).ToList();
        //            room.DiscountId = AppliedDiscount.ID;
        //            room.MarkupId = AppliedMarkup.ID;
        //            room.DiscountValue = AppliedDiscount.Value * ExcahngeRate;
        //            room.MarkupValue = AppliedMarkup.Value * ExcahngeRate;
        //            room.rateClass = searchOutputs[i].rooms[j].rates[x].rateClass;
        //            room.rateType = searchOutputs[i].rooms[j].rates[x].rateType;
        //            room.childrenAges = searchOutputs[i].rooms[j].rates[x].childrenAges;
        //            room.paymentType = searchOutputs[i].rooms[j].rates[x].paymentType;
        //            room.boardCode = searchOutputs[i].rooms[j].rates[x].boardCode;
        //            room.cancellationRules = searchOutputs[i].rooms[j].rates[x].cancellationPolicies == null ?
        //                new List<CancellationRule>() { new CancellationRule
        //                { Cost = 0, Price = 0, CanellationRuleText = null, FromDate = null, ToDate = null } } :
        //            searchOutputs[i].rooms[j].rates[x].cancellationPolicies.Select(a => new CancellationRule
        //            {
        //                Price = Math.Round(((double.Parse(a.amount) * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3),
        //                CanellationRuleText = searchData.Lang.ToLower() == "en" ? Math.Round(((double.Parse(a.amount) * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3) + "" + searchData.Currency + " To " + a.from : a.from + " إلى " + "" + searchData.Currency + Math.Round(((double.Parse(a.amount) * ProviderExcahngeRate) /*+ AppliedCancellationMarkup.Value*/) * ExcahngeRate, 3),
        //                Cost = Math.Round(double.Parse(a.amount)),
        //                FromDate = a.from.ToString()
        //            }).ToList();
        //            hotel.rooms.Add(room);
        //        }
        //    }


        //}
    }
}
