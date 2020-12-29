using Hotels.BLL;
using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TBO.WSDL.hotelServiceRef;
using TBOIntegration.Management;
using TBOIntegration.Services;

namespace Hotels.Controllers
{
    public class HotelRoomsController : ApiController
    {
        public IHttpActionResult Get(string sid, string hotel, string Pid)
        {
            // call bll
            try
            {
                LoggingHelper.WriteToFile("HotelRoomsController/GetRoomsOFHotel/", "INController" + sid, "Rooms:", "Sid: " + sid + " , pid: " + Pid + " , hotelId: " + hotel);
                //TBO
                if (Pid == "5")
                {
                    SearchDBEntities searchDB = new SearchDBEntities();
                    // call Room availablity api 
                    string TBOSession = searchDB.ProviderSessions.FirstOrDefault(ps => ps.SearchId == sid).PSession;//Select(s => s.PSession).ToString();
                    int ResIndex = int.Parse(searchDB.SearchHotelResults.FirstOrDefault(hotl => hotl.sID == sid && hotl.HotelCode == hotel).ResIndex.ToString());//Select(indx => indx.ResIndex).ToString();
                    TBOSearchManager manager = new TBOSearchManager();
                    var TBORooms = manager.GetAvailableRoom(TBOSession, ResIndex, hotel, sid);
                    if (TBORooms != null)
                    {
                        LoggingHelper.WriteToFile("HotelRoomsController/GetRoomsOFHotel/", "OutController" + sid, "RoomsResult", JsonConvert.SerializeObject(TBORooms));

                        return Ok(TBORooms);

                    }
                    else
                    {
                        return Ok("No Result Found");
                    }
                }
                else
                {

                    var Rooms = GetRoom.GetRoomsByHotelIDAndProvide(sid, Pid, hotel);
                    if (Rooms != null)
                    {

                        LoggingHelper.WriteToFile("HotelRoomsController/GetRoomsOFHotel/", "OutController" + sid, "RoomsResult", JsonConvert.SerializeObject(Rooms));

                        return Ok(Rooms);

                    }
                    else
                    {
                        return Ok("No Result Found");

                    }
                }


            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("HotelRoomsController/Errors/", "INController" + sid, "Exception", ex.InnerException?.Message + ex.Message + ex.StackTrace);

                return BadRequest(ex.Message);
            }
        }
        public IHttpActionResult Get(string sid, string hotel, string Pid, string rooms)
        {
            // call bll
            try
            {
                LoggingHelper.WriteToFile("HotelRoomsController/GetRoomsNumber/", "SearchController" + "INController" + sid, "Rooms", "Sid" + sid);


                var Rooms = GetRoom.GetRoomsData(sid, hotel, Pid, rooms);
                if (Rooms != null && Rooms.rooms.Count > 0)
                {
                    #region tbo  disable to not calling pricing api
                    if (Pid == "5")
                    {
                        // var ActionUrlData = ConfigurationSettings.AppSettings["ActionUrl"];
                        // var BaseCur = ConfigurationSettings.AppSettings["BaseCur"];
                        //payLinkData.ActionsUrl.ValidationUrl = ActionUrlData + "/api/HotelCheckAvailability?sid=" + Sid + "&bookingnum=" + BN;

                        SearchDBEntities searchDB = new SearchDBEntities();

                        RequiredBookingData requiredBooking = new RequiredBookingData();
                        //var data = searchDB.SearchCriterias.FirstOrDefault(a => a.sID == sid);
                        var HRooms = searchDB.SearchRoomResults.Where(a => a.sID == sid && a.HotelCode == hotel && a.ProviderId.ToString() == Pid).ToList();
                        //requiredBooking.Currency = data?.currency;
                        //requiredBooking.City = data?.cityName;
                        var RoomsCode = rooms.Split('-');
                        foreach (var item in RoomsCode)
                        {
                            var roomdata = HRooms.FirstOrDefault(a => a.RoomCode == item);

                            requiredBooking.rooms.Add(roomdata);
                        }
                        //TBO 
                        //call check availability tbo to get hotel norms and cancel policy
                        //get room indexes to send in pricing req
                        List<int> indexs = new List<int>();

                        foreach (var indx in requiredBooking.rooms)
                        {
                            indexs.Add(int.Parse(indx.RoomCode));
                        }
                        string TBOSession = searchDB.ProviderSessions.FirstOrDefault(ps => ps.SearchId == sid).PSession;//Select(s => s.PSession).ToString();
                        int ResIndex = int.Parse(searchDB.SearchHotelResults.FirstOrDefault(hotl => hotl.sID == sid && hotl.HotelCode == hotel).ResIndex.ToString());//Select(indx => indx.ResIndex).ToString();
                        AvailabilityAndPricingRequest req = new AvailabilityAndPricingRequest
                        {
                            SessionId = TBOSession,
                            ResultIndex = ResIndex,
                            OptionsForBooking = new TBO.WSDL.hotelServiceRef.BookingOptions
                            {
                                RoomCombination = new TBO.WSDL.hotelServiceRef.RoomCombination[]
                                {
                                  new TBO.WSDL.hotelServiceRef.RoomCombination{RoomIndex = indexs.ToArray()}
                                }
                            }
                        };
                        var availRes = AvailablityPricingService.PricingService(req, sid);
                        RoomResult roomResult = new RoomResult();
                        if (availRes != null)
                        {
                            if (availRes?.PriceVerification?.Status == PriceVerificationStatus.Failed
                                    || availRes?.PriceVerification?.Status == PriceVerificationStatus.NotAvailable)
                            {
                                return Ok("No Result Found");

                            }
                            if (availRes.HotelCancellationPolicies != null)
                            {

                                roomResult.HotelNorms = availRes.HotelCancellationPolicies?.HotelNorms;
                                //handel cancel policy 
                                var BaseCur = ConfigurationSettings.AppSettings["BaseCur"];
                                CurrencyManager currencyManager = new CurrencyManager();
                                double ProviderExcahngeRate = currencyManager.GetCurrencyConversion("USD", BaseCur, sid);

                                foreach (var cancel in availRes.HotelCancellationPolicies?.CancelPolicies?.CancelPolicy)
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
                                    CancellationRule cancellation = new CancellationRule();
                                    cancellation.FromDate = cancel.FromDate;
                                    cancellation.ToDate = cancel.ToDate;
                                    cancellation.Cost = (double)cancel.CancellationCharge;
                                    cancellation.ChargeType = cancel.ChargeType.ToString();
                                    cancellation.Curency = Cur;
                                    cancellation.Price = costToSave; //
                                    roomResult.cancellationRules.Add(cancellation);
                                }

                                
                                if (availRes?.PriceVerification?.PriceChanged == true)
                                {
                                    //var roomsTbo = searchDB.SearchRoomResults.Where(a => a.sID == sid && a.HotelCode == hotel && a.ProviderId.ToString() == Pid).ToList();
                                    Rooms.Status = 1;
                                    Rooms.Message = "Price Change";
                                    //results.Status = 1;
                                    foreach (var item in availRes?.PriceVerification.HotelRooms)
                                    {
                                        roomResult.TotalSellPrice = Math.Round((double)item.RoomRate.TotalFare * ProviderExcahngeRate, 3);
                                        roomResult.CostPrice = (double)item.RoomRate.TotalFare;
                                        //results.TotalCost += roomResult.TotalSellPrice; //Math.Round((double)item.RoomRate.TotalFare * ProviderExcahngeRate, 3);
                                        roomResult.RoomIndex = item.RoomIndex;
                                        //results.Result.Add(roomResult);

                                        //update Rooms obj with new price
                                        Rooms.rooms.FirstOrDefault(r => r.RoomCode == item.RoomIndex.ToString()).costPrice = (double)item.RoomRate.TotalFare;
                                        Rooms.rooms.FirstOrDefault(r => r.RoomCode == item.RoomIndex.ToString()).SellPrice = Math.Round((double)item.RoomRate.TotalFare * ProviderExcahngeRate, 3);

                                        // update price in search db 
                                       // update tbo new rooms prices
                                        SearchRoomResult Newroom = new SearchRoomResult();
                                        Newroom = searchDB.SearchRoomResults.FirstOrDefault(room => room.sID == sid && room.RoomCode == item.RoomIndex.ToString());
                                        Newroom.costPrice = (double)item.RoomRate.TotalFare;
                                        Newroom.rateClass = item.RoomRate.RoomFare.ToString();
                                        Newroom.rateType = item.RoomRate.RoomTax.ToString();
                                        Newroom.SellPrice = roomResult.TotalSellPrice;

                                        searchDB.SaveChanges();

                                    }
                                }
                                else
                                {
                                    Rooms.Status = 0;
                                    Rooms.Message = "No Price Change";

                                }

                                Rooms.TBoRooms.Add(roomResult);
                            }

                        }
                        #endregion
                        LoggingHelper.WriteToFile("HotelRoomsController/GetRoomsOFHotel/", "SearchController" + "OutController" + sid, "RoomsResult", JsonConvert.SerializeObject(Rooms));

                        return Ok(Rooms);

                    }
                }
                return Ok("No Result Found");
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("HotelRoomsController/Errors/", "SearchController" + "INController" + sid, "Exception", ex.InnerException?.Message + ex.Message + ex.StackTrace);

                return BadRequest(ex.Message);
            }
        }
    }
}
