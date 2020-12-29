using HotelBedsIntegration.Controller;
using HotelBedsIntegration.Models.Availability;
using Hotels.BLL;
using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Hotels.DAL;
using IntegrationTotalStay.Management;
using SMYROOMS.Controller;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBOIntegration.Services;

namespace Hotels.ProviderManagment
{
    public class AvailabilityManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="availability"></param>
        /// <param name="Curr">user selected curency</param>
        /// <param name="src"></param>
        /// <returns></returns>
        public static CheckAvailabilityResult checkAvailabilityManager(Hotels.Common.Models.AvailabilityReq availability, string Curr, string src)
        {
            CheckAvailabilityResult availabilityResult = new CheckAvailabilityResult();

            //try
            //{
            CurrencyManager currencyManager = new CurrencyManager();

            SalesRulesManager CancellationChargeManager = new SalesRulesManager();
            CheckAvailabilityRepo.SaveRequestToDB(availability);
            double TotalCost = 0;
            double ProviderExcahngeRate = 0;
            double newcost = 0;
            var SearchData = SearchRepo.GetSearchDataBySession(availability.Sid);
            int duration = SearchData.duration ?? default(int);//Convert.ToInt32((SearchData.dateTo - SearchData.dateFrom).Value.TotalDays);
            var BaseCur = ConfigurationSettings.AppSettings["BaseCur"];

            if (SearchData != null)
            {
                availabilityResult = GetRoomsFromProviders(availability, Convert.ToDateTime(SearchData.dateFrom).ToString("yyyy-MM-dd"), duration.ToString());
                if (availabilityResult == null)
                {
                    return null;
                }
                if (availability.PID == "5")
                {
                    ProviderExcahngeRate = currencyManager.GetCurrencyConversion(Curr, BaseCur, availability.Sid);
                }
                if (availability.PID == "4")
                {
                    // get Hotel Beds provider Curency                              
                    var HotelBedsCur = ConfigurationSettings.AppSettings["HotelBedsCurency"];
                    ProviderExcahngeRate = currencyManager.GetCurrencyConversion(HotelBedsCur, BaseCur, availability.Sid);
                    ////ProviderExcahngeRate = currencyManager.GetCurrencyConversion("EUR", BaseCur);availabilityResult.ProviderCur

                }
                else if (availability.PID == "2")
                {
                    ProviderExcahngeRate = currencyManager.GetCurrencyConversion(availabilityResult.ProviderCur, BaseCur, availability.Sid);
                    ////providerexcahngerate = currencymanager.getcurrencyconversion("usd", basecur, availability.sid);

                }
                if (Curr == null)
                {
                    Curr = SearchData.currency;
                }
                double ExcahngeRate = currencyManager.GetCurrencyConversion(BaseCur, Curr, availability.Sid);
                double MarkUpRate = currencyManager.GetCurrencyConversion(BaseCur, SearchData.currency, availability.Sid);
                var roomsRules = AvailabiltyRooms.getRoomsRules(availability.HotelCode, availability.Sid, availability.PID);
                if (roomsRules == null)
                {
                    return null;
                }

                AppliedSalesRule AppliedCancellationMarkup = CancellationChargeManager.ApplySalesRules("Markup");

                for (int i = 0; i < availabilityResult.Result.Count; i++)
                {
                    var roomData = roomsRules.FirstOrDefault(a => a.RoomCode == availabilityResult.Result[i].RoomIndex.ToString());
                    availabilityResult.Result[i].RatePerNight = ((availabilityResult.Result[i].CostPrice * ProviderExcahngeRate / duration) + (roomData.MarkupVal.Value / MarkUpRate) - (roomData.DiscountVal.Value / MarkUpRate)) * ExcahngeRate;
                    availabilityResult.Result[i].TotalSellPrice = Math.Round(availabilityResult.Result[i].RatePerNight * duration, 3);
                    TotalCost += availabilityResult.Result[i].CostPrice;
                    foreach (var item in availabilityResult.Result[i].cancellationRules) ////  
                    {

                        item.Price = Math.Round(((item.Cost * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3);
                        item.CanellationRuleText = SearchData.language.ToLower() == "en" ? Math.Round(((item.Cost * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3) + " " + SearchData.currency + " of the booking amount " + item.FromDate : item.FromDate + " TO " + "" + SearchData.currency + Math.Round(((item.Cost * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3);
                    }
                }
                newcost = availability.TotalCost;

                //availabilityResult.TotalCost = TotalCost;
            }
            CheckAvailabilityRepo.SaveResponseToDB(availabilityResult, newcost, availability, SearchData.currency, src);
            return availabilityResult;
            //}
            //catch (Exception ex)
            //{
            //    LoggingHelper.WriteToFile("AvailabilityController/Errors/", "AvailabilityController" + "providerManagement" + availability.Sid, ex.InnerException?.Message, ex.Message + ex.StackTrace);

            //    return null;
            //}
        }
        public static CheckAvailabilityResult GetRoomsFromProviders(Common.Models.AvailabilityReq roomsReq, String ArrivalDate, string dur)
        {
            //try
            //{
            CheckAvailabilityResult results = new CheckAvailabilityResult();
            results.TotalCost = 0;

            #region redo
            //if (roomsReq.PID == "4")
            //{
            //    HotelBedsIntegration.Models.Availability.AvailabilityReq req = new HotelBedsIntegration.Models.Availability.AvailabilityReq();
            //    req.rooms = new List<RoomReq>();
            //    req.upselling = "True";
            //    foreach (var item in roomsReq.Rooms.Select(a => a.RoomRef).ToList())
            //    {
            //        RoomReq roomReq = new RoomReq();
            //        roomReq.rateKey = item;

            //        req.rooms.Add(roomReq);
            //    }
            //    var availabilityRes = HotelBedsIntegration.Controller.CheckAvailability.checkAvailability(req, roomsReq.Sid);
            //    //MG
            //    results.ProviderCur = availabilityRes.Result.currency;

            //    if (availabilityRes.Result.rooms != null)
            //    {
            //        if (roomsReq.TotalCost == double.Parse(availabilityRes.Result.totalNet) || roomsReq.TotalCost > double.Parse(availabilityRes.Result.totalNet))
            //        {
            //            results.Status = 0;
            //            results.TotalCost = roomsReq.TotalCost;
            //        }
            //        else
            //        {
            //            results.Status = 1;
            //            results.TotalCost = double.Parse(availabilityRes.Result.totalNet);
            //        }
            //        foreach (var item in availabilityRes.Result.rooms)
            //        {
            //            for (int i = 0; i < item.rates.Count; i++)
            //            {
            //                RoomResult roomResult = new RoomResult();

            //                roomResult.CostPrice = double.Parse(item.rates[i].net);
            //                var room = roomsReq.Rooms.Where(a => a.RoomRef == item.rates[i].rateKey).OrderBy(a => a.RoomId).ToList();/////////get specific roomid
            //                if (room.Count > 1)
            //                {
            //                    if (results.Result.FirstOrDefault(x => x.RoomCode == item.rates[i].rateKey) == null)
            //                    {
            //                        roomResult.RoomIndex = room[0].RoomId;
            //                    }
            //                    else
            //                    {
            //                        roomResult.RoomIndex = room[results.Result.Where(x => x.RoomCode == item.rates[i].rateKey).ToList().Count].RoomId;
            //                    }
            //                }
            //                else
            //                {
            //                    roomResult.RoomIndex = room[0].RoomId;
            //                }
            //                roomResult.RoomCode = item.rates[i].rateKey;
            //                if (results.Status == 0)
            //                {
            //                    roomResult.CostPrice = roomsReq.Rooms.FirstOrDefault(v => v.RoomRef == item.rates[i].rateKey).Cost;
            //                }
            //                else
            //                {
            //                    roomResult.CostPrice = double.Parse(item.rates[i].net);
            //                }
            //                // roomResult.TotalSellPrice = double.Parse(r.net);

            //                foreach (var Cancel in item.rates[i].cancellationPolicies)
            //                {
            //                    roomResult.cancellationRules.Add(new CancellationRule
            //                    {
            //                        Cost = double.Parse(Cancel.amount),
            //                        FromDate = Cancel.from.ToLongDateString(),
            //                        ToDate = null,
            //                        CanellationRuleText = null
            //                    });

            //                }
            //                results.Result.Add(roomResult);
            //            }
            //        }

            //    }
            //    else
            //    {
            //        results.Status = 2;
            //    }
            //}
            //else if (roomsReq.PID == "2")
            //{
            //    // get hotelproperty and arrival and duration   // get search rooms result from db
            //    HotelManager hotelManager = new HotelManager();
            //    var hotelsData = hotelManager.GetHotelDataForTsAvailability(roomsReq);
            //    if (hotelsData != null)
            //    {
            //        foreach (var item in hotelsData.roomResults)
            //        {
            //            // call ts  and map it with db and ..or new obj 
            //            var roomsResults = new List<SearchRoomResult>();
            //            roomsResults.Add(item);
            //            var availReq = AvailabiltyManager.prepareAvailabilityObj(roomsResults, hotelsData.HodelData.ProviderHotelCode, ArrivalDate, dur);

            //            var availRes = IntegrationTotalStay.Controller.CheckAvailability.GetTSAvailability(availReq, roomsReq.Sid);
            //            if (availRes != null)
            //            {
            //                RoomResult roomResult = new RoomResult();
            //                roomResult.Adult = item.Adults.Value;
            //                roomResult.Child = item.Childern.Value;

            //                roomResult.RoomIndex = int.Parse(item.RoomCode);
            //                roomResult.RoomReference = availRes.PreBookingToken;
            //                if (availRes.TotalPrice == item.costPrice.Value.ToString() || double.Parse(availRes.TotalPrice) < item.costPrice.Value)
            //                {
            //                    results.Status = 0;
            //                    roomResult.CostPrice = item.costPrice.Value;
            //                }
            //                else
            //                {
            //                    results.Status = 1;
            //                    roomResult.CostPrice = double.Parse(availRes.TotalPrice);
            //                }
            //                // handel cancel policy 

            //                foreach (var cancel in availRes.Cancellations.Cancellation)
            //                {
            //                    CancellationRule cancellation = new CancellationRule();
            //                    DateTime dateFrom = Convert.ToDateTime(cancel.StartDate);
            //                    cancellation.FromDate = dateFrom.ToString("MMMM dd, yyyy");
            //                    cancellation.ToDate = cancel.EndDate;
            //                    cancellation.Cost = double.Parse(cancel.Penalty);
            //                    roomResult.cancellationRules.Add(cancellation);
            //                }

            //                // roomResult.RoomCode=
            //                results.Result.Add(roomResult);
            //                results.TotalCost += double.Parse(availRes.TotalPrice);
            //            }
            //            else
            //            {
            //                results.Status = 2;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        return null;
            //    }

            //}
            ////else if (PID == "3")
            //{
            //    var SearchData = SearchRepo.GetSearchDataBySession(Sid);
            //    DateTime CheckInDate = SearchData.dateFrom.Value; //searchData.DateFrom;

            //    var SMRRuslt = SMCLSValidation.ValidationQuote(Rooms[0], Sid);
            //    results.TotalCost = double.Parse(SMRRuslt.Result.price.net.ToString());
            //    if (SMRRuslt.Result != null)
            //    {
            //        if (Cost == double.Parse(SMRRuslt.Result.price.net.ToString()))
            //        {
            //            results.Status = 0;
            //        }
            //        else
            //        {
            //            results.Status = 1;
            //        }
            //        foreach (var item in Rooms)
            //        {
            //            RoomResult SMroomResult = new RoomResult();
            //            SMroomResult.RoomCode = SMRRuslt.Result.optionRefId;
            //            foreach (var c in SMRRuslt.Result.cancelPolicy.cancelPenalties)
            //            {
            //                SMroomResult.cancellationRules.Add(new CancellationRule
            //                {

            //                    Cost = double.Parse(c.value.ToString()),
            //                    FromDate = CheckInDate.AddHours(-c.hoursBefore.Value).ToString("dd MMM yyyy")
            //                    // Cost = Math.Round(((a.value.Value * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3),
            //                    //CanellationRuleText = searchData.Lang.ToLower() == "en" ? Math.Round(((a.value.Value * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3) + "" + searchData.Currency + " To " + CheckInDate.AddHours(-a.hoursBefore.Value).ToString("dd MMM yyyy") : CheckInDate.AddHours(-a.hoursBefore.Value).ToString("dd MMM yyyy") + " إلى " + "" + searchData.Currency + Math.Round(((a.value.Value * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3)


            //                });
            //            }
            //            results.Result.Add(SMroomResult);
            //        }
            //    }
            //    else
            //    {
            //        results.Status = 2;
            //    }
            //} 
            #endregion
            if (roomsReq.PID == "5")
            {
                // get hotelproperty and arrival and duration   // get search rooms result from db
                HotelManager hotelManager = new HotelManager();
                //FOr  TBO 
                var hotelsData = hotelManager.GetHotelDataForTsAvailability(roomsReq);
                if (hotelsData != null)
                {
                    //TBO 
                    SearchDBEntities searchDB = new SearchDBEntities();
                    string TBOSession = searchDB.ProviderSessions.FirstOrDefault(ps => ps.SearchId == roomsReq.Sid).PSession;//Select(s => s.PSession).ToString();
                    int ResIndex = int.Parse(searchDB.SearchHotelResults.FirstOrDefault(hotl => hotl.sID == roomsReq.Sid && hotl.HotelCode == roomsReq.HotelCode).ResIndex.ToString());//Select(indx => indx.ResIndex).ToString();

                    //get room indexes to send in pricing req
                    List<int> indexs = new List<int>();

                    foreach (var indx in roomsReq.Rooms)
                    {
                        indexs.Add(indx.RoomId);
                    }
                    //TBO.WSDL.hotelServiceRef.AvailabilityAndPricingRequest req = new TBO.WSDL.hotelServiceRef.AvailabilityAndPricingRequest
                    //{
                    //    SessionId = TBOSession,
                    //    ResultIndex = ResIndex,
                    //    OptionsForBooking = new TBO.WSDL.hotelServiceRef.BookingOptions
                    //    {
                    //        RoomCombination = new TBO.WSDL.hotelServiceRef.RoomCombination[]
                    //        {
                    //                    new TBO.WSDL.hotelServiceRef.RoomCombination{RoomIndex = indexs.ToArray()}
                    //        }
                    //    }
                    //};
                    //var availRes = AvailablityPricingService.PricingService(req, roomsReq.Sid);
                    //if (availRes != null)
                    //{
                        var BaseCur = ConfigurationSettings.AppSettings["BaseCur"];
                        CurrencyManager currencyManager = new CurrencyManager();
                        //double ProviderExcahngeRate = currencyManager.GetCurrencyConversion("USD", BaseCur, req.SessionId);

                        RoomResult roomResult = new RoomResult();
                        //roomResult.HotelNorms = availRes.HotelCancellationPolicies?.HotelNorms;
                        //if (availRes?.PriceVerification?.PriceChanged == false)
                        //{
                            results.Status = 0;
                            foreach (var item in hotelsData.roomResults)
                            {
                                roomResult.TotalSellPrice = (double)item.SellPrice;//Math.Round((double)item.SellPrice * ProviderExcahngeRate, 3);
                                roomResult.CostPrice = item.SellPrice ?? default(double);
                                results.TotalCost += (double)item.SellPrice;//Math.Round((double)item.SellPrice * ProviderExcahngeRate, 3);
                                roomResult.Adult = item.Adults.Value;
                                roomResult.Child = item.Childern.Value;

                                roomResult.RoomIndex = int.Parse(item.RoomCode);
                                results.Result.Add(roomResult);

                            }

                        //}
                        //else
                        //{
                            
                        //    results.Status = 1;
                        //    foreach (var item in availRes?.PriceVerification.HotelRooms)
                        //    {
                        //        roomResult.TotalSellPrice = Math.Round((double)item.RoomRate.TotalFare * ProviderExcahngeRate, 3); 
                        //        roomResult.CostPrice = (double)item.RoomRate.TotalFare;
                        //        results.TotalCost += roomResult.TotalSellPrice; //Math.Round((double)item.RoomRate.TotalFare * ProviderExcahngeRate, 3);
                        //        roomResult.RoomIndex = item.RoomIndex;
                        //        results.Result.Add(roomResult);
                        //        // update price in search db 
                        //        //CheckAvailabilityRepo repo = new CheckAvailabilityRepo();
                        //        //repo.UpdateTBORoomsPrice(availRes?.PriceVerification.HotelRooms);
                        //        //update tbo new rooms prices
                        //        var Newroom = searchDB.SearchRoomResults.FirstOrDefault(room => room.sID == req.SessionId && room.RoomCode == item.RoomIndex.ToString());
                        //        Newroom.costPrice = (double)item.RoomRate.TotalFare;
                        //        Newroom.rateClass = item.RoomRate.RoomFare.ToString();
                        //        Newroom.rateType = item.RoomRate.RoomTax.ToString();
                        //        Newroom.SellPrice = roomResult.TotalSellPrice; 

                        //        searchDB.SaveChanges();

                        //    }

                        }
                        //handel cancel policy 
                        //foreach (var cancel in availRes.HotelCancellationPolicies?.CancelPolicies?.CancelPolicy)
                        //{
                        //    CancellationRule cancellation = new CancellationRule();
                        //    //DateTime dateFrom = Convert.ToDateTime(cancel.FromDate);
                        //    cancellation.FromDate = cancel.FromDate;// dateFrom.ToString("MMMM dd, yyyy");
                        //    cancellation.ToDate = cancel.ToDate;
                        //    cancellation.Cost = (double)cancel.CancellationCharge;
                        //    cancellation.ChargeType = cancel.ChargeType.ToString();
                        //    roomResult.cancellationRules.Add(cancellation);
                        //}
                        //var countroom = 0;
                        //foreach (var item in hotelsData.roomResults)
                        //{
                        //    countroom++;
                        //    // call ts  and map it with db and ..or new obj 
                        //    // var roomsResults = new List<SearchRoomResult>();
                        //    // roomsResults.Add(item);
                        //    roomResult.Adult = item.Adults.Value;
                        //    roomResult.Child = item.Childern.Value;

                        //    roomResult.RoomIndex = int.Parse(item.RoomCode);
                            //roomResult.RoomReference = availRes.PreBookingToken;
                            //results = new CheckAvailabilityResult();

                            //if (availRes?.PriceVerification?.PriceChanged == false)
                            //{
                            //    //results.Status = 0;
                            //    roomResult.CostPrice = item.costPrice.Value;
                            //    results.TotalCost += item.costPrice.Value;

                            //}
                            //else
                            //{
                            //    // results.Status = 1;

                            //    roomResult.CostPrice = (double)availRes.PriceVerification.HotelRooms[countroom].RoomRate.TotalFare;
                            //    results.TotalCost += (double)availRes.PriceVerification.HotelRooms[countroom].RoomRate.TotalFare;

                            //}

                        //}    //end foreach

                        // roomResult.RoomCode=
                        //results.Result.Add(roomResult);
                    //}
                    //else
                    //{
                    //    results.Status = 2;
                    //}
                }
                else
                {
                    return null;
                }
            return results;

        }
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }
    
}
