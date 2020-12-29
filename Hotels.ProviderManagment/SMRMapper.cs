using Hotels.BLL;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Microsoft.Win32.SafeHandles;
using SMYROOMS.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.ProviderManagment
{
    public class SMRMapper :IDisposable
    {
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
      
        public List<HotelSearchResult> MapSearchResult(List<SearchOutputData> searchOutputs, List<BoardCode> boardCodes, SearchData searchData)
        {
            try
            {
                //save result
                //apply salesRules
                CurrencyManager currencyManager = new CurrencyManager();
                SalesRulesManager ServiceChargeManager = new SalesRulesManager();
                SalesRulesManager CancellationChargeManager = new SalesRulesManager();

                //start sales rules service charge
                ServiceChargeManager.FillSalesRules(searchData.POS, "Hotel", "Service Charge");
                ServiceChargeManager.PrepareSearchCriteriaDic(searchData);
                ////
                /////cancellation charge 
                CancellationChargeManager.FillSalesRules(searchData.POS, "Hotel", "Cancellation Charge");
                CancellationChargeManager.PrepareSearchCriteriaDic(searchData);
                //

                List<string> hotelIds = searchOutputs.Select(a => a.hotelCode).ToList();
                HotelManager manager = new HotelManager();
                List<HotelDetails> HotelDataList = manager.GetHotelData(hotelIds, "3");
                List<HotelSearchResult> results = new List<HotelSearchResult>();
                var BaseCur = ConfigurationSettings.AppSettings["BaseCur"];
                // Provider Exchange Rate From User Curency To Provider Curency
                double ProviderExcahngeRate = currencyManager.GetCurrencyConversion(searchData.Currency, BaseCur, searchData.sID);
                /////double ProviderExcahngeRate = currencyManager.GetCurrencyConversion("EUR", BaseCur);
                // Exchange Rate From Base Curency to user cur
                double ExcahngeRate = currencyManager.GetCurrencyConversion(BaseCur, searchData.Currency, searchData.sID);
                int duration = Convert.ToInt32((searchData.DateTo - searchData.DateFrom).TotalDays);
                {
                    DateTime CheckInDate = searchData.DateFrom;
                    for (int i = 0; i < searchOutputs.Count; i++)
                    {
                        BoardCode boardCode = boardCodes.Where(a => a.Code == searchOutputs[i].boardCode).FirstOrDefault();
                        HotelDetails hotelData = HotelDataList.Where(a => a.ProviderHotelId == searchOutputs[i].hotelCode).FirstOrDefault();
                        HotelSearchResult hotel = new HotelSearchResult();
                        hotel.hotelCode = searchOutputs[i].hotelCode;
                        hotel.City = hotelData.City;
                        hotel.hotelName = hotelData.HotelName;
                        hotel.Country = hotelData.Country;
                        hotel.hotelStars = int.Parse(hotelData.Rating) - 558;
                        var images = hotelData.Images.FirstOrDefault();
                        if (images != null)
                        {
                            hotel.hotelThumb = images.Thum;
                        }
                        hotel.hotelImages = hotelData.Images.Select(a => a.Thum).ToList();
                        hotel.Lat = hotelData.Lat;
                        hotel.Lng = hotelData.Lng;
                        hotel.providerID = "3";
                        hotel.hotelDescription = hotelData.LongDescriptin;
                        hotel.shortcutHotelDescription = hotelData.ShortDescription;
                        hotel.ZipCode = hotelData.Zipcode;
                        hotel.Location = hotelData.Location;
                        hotel.Address = hotelData.Address;
                        hotel.providerHotelID = hotelData.ProviderHotelId;
                        hotel.providerHotelCode = hotelData.HotelId;
                        hotel.sellCurrency = searchData.Currency;
                        //  hotel.costPrice =//Math.Round( (double.Parse(searchOutputs[i].price.net.Value.ToString()) * ProviderExcahngeRate) /duration,3);
                        hotel.costCurrency = searchOutputs[i].price.currency;
                        //set sales rules cirtiera



                        //  hotel.hotelRate = ((hotel.costPrice)+ AppliedMarkup.Value-AppliedDiscount.Value)* ExcahngeRate;

                        hotel.rooms = new List<RoomResult>();
                        CancellationChargeManager.SetResultCriteria(hotel.hotelName, hotel.hotelStars, hotel.costPrice, "3");
                        AppliedSalesRule AppliedCancellationMarkup = CancellationChargeManager.ApplySalesRules("Markup");

                        for (int j = 0; j < searchOutputs[i].rooms.Count; j++)
                        {
                            RoomResult room = new RoomResult();
                            room.CostPrice = Math.Round(double.Parse(searchOutputs[i].rooms[j].roomPrice.price.net.Value.ToString()), 3);

                            ServiceChargeManager.SetResultCriteria(hotel.hotelName, hotel.hotelStars, room.CostPrice
                                *ProviderExcahngeRate, "3");
                            AppliedSalesRule AppliedMarkup = ServiceChargeManager.ApplySalesRules("Markup");
                            AppliedSalesRule AppliedDiscount = ServiceChargeManager.ApplySalesRules("Discount");
                           
                            room.IsRefundable = searchOutputs[i].rooms[j].refundable;
                            room.RatePerNight = ((double.Parse(searchOutputs[i].rooms[j].roomPrice.price.net.Value.ToString()) * ProviderExcahngeRate / duration) + AppliedMarkup.Value - AppliedDiscount.Value) * ExcahngeRate;
                            room.TotalSellPrice = Math.Round(room.RatePerNight * duration, 3);
                            room.RoomIndex = j + 1;
                            room.RoomReference = searchOutputs[i].id;

                            room.RoomType = searchOutputs[i].rooms[j].description;
                            room.RoomMeal = boardCode.Name;
                            room.Images = hotelData.Images.Where(a => a.Category.ToLower() == "hotel rooms").Select(a => a.Thum).ToList();
                            room.DiscountId = AppliedDiscount.ID;
                            room.MarkupId = AppliedMarkup.ID;
                            room.DiscountValue = AppliedDiscount.Value * ExcahngeRate;
                            room.MarkupValue = AppliedMarkup.Value * ExcahngeRate;
                            room.cancellationRules = searchOutputs[i].cancelPolicy.cancelPenalties==null? new List<CancellationRule>() {
                                new CancellationRule { Cost = Math.Round((AppliedCancellationMarkup.Value) * ExcahngeRate, 3) } } : 
                            searchOutputs[i].cancelPolicy.cancelPenalties.Select(a => new CancellationRule
                            {
                                ToDate = CheckInDate.AddHours(-a.hoursBefore.Value).ToString("dd MMM yyyy"),
                                Cost = Math.Round(((a.value.Value * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3),
                                CanellationRuleText=searchData.Lang.ToLower()=="en"? Math.Round(((a.value.Value * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3) +"" +searchData.Currency+" To " + CheckInDate.AddHours(-a.hoursBefore.Value).ToString("dd MMM yyyy") : CheckInDate.AddHours(-a.hoursBefore.Value).ToString("dd MMM yyyy") +" إلى " + "" + searchData.Currency +Math.Round(((a.value.Value * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3) 



                            }).ToList();
                            hotel.rooms.Add(room);
                        }
                        var minRoom = hotel.rooms.Where(a => a.RatePerNight == hotel.rooms.Min(x => x.RatePerNight)).FirstOrDefault();
                        hotel.hotelRate = minRoom.RatePerNight;
                        hotel.costPrice = minRoom.CostPrice;
                        hotel.TotalSellPrice = minRoom.TotalSellPrice;
                        hotel.MarkupId = minRoom.MarkupId;
                        hotel.MarkupValue = minRoom.MarkupValue;
                        hotel.DiscountId = minRoom.DiscountId;
                        hotel.DiscountValue = minRoom.DiscountValue;
                        results.Add(hotel);
                    }
                    return results;
                }
            }
            catch (Exception ex) {

                LoggingHelper.WriteToFile("MapSearchResult/Errors/", "MapSearchResult_" + searchData.sID, ex.InnerException?.Message, ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);
                return new List<HotelSearchResult>();
            }
        }

        public void Dispose()
        {
            handle.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
