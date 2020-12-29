using HotelBedsIntegration.Models;
using Hotels.BLL;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.ProviderManagment
{
    public class HBMapper : IDisposable
    {
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        // map rooms ??????!!!!!!!!!!!!!!
        public HotelSearchResponse MapSearchResult(List<Hotel> searchOutputs,   SearchData searchData)
        {
            try
            {
                //save result
                //apply salesRules
                HotelSearchResponse searchResponse = new HotelSearchResponse();
                CurrencyManager currencyManager = new CurrencyManager();
                SalesRulesManager ServiceChargeManager = new SalesRulesManager();
                SalesRulesManager CancellationChargeManager = new SalesRulesManager();
                var BaseCur = ConfigurationSettings.AppSettings["BaseCur"];

                //start sales rules service charge
                ServiceChargeManager.FillSalesRules(searchData.POS, "Hotel", "Service Charge");//1
                ServiceChargeManager.PrepareSearchCriteriaDic(searchData);//2
                ////
                ///cancellation charge 
                CancellationChargeManager.FillSalesRules(searchData.POS, "Hotel", "Cancellation Charge");
                CancellationChargeManager.PrepareSearchCriteriaDic(searchData);
                //

                List<string> hotelIds = searchOutputs.Select(a => a.code.ToString()).ToList();
                HotelManager manager = new HotelManager();
                List<HotelDetails> HotelDataList = manager.GetHotelData(hotelIds, "4");
                List<HotelSearchResult> results = new List<HotelSearchResult>();
                double ProviderExcahngeRate = currencyManager.GetCurrencyConversion(searchOutputs[0].currency, BaseCur, searchData.sID);
                ////////////double ProviderExcahngeRate = currencyManager.GetCurrencyConversion("EUR", BaseCur);/////MG
                double ExcahngeRate = currencyManager.GetCurrencyConversion(BaseCur, searchData.Currency, searchData.sID);
              //  searchResponse.Locations = HotelDataList.GroupBy(x => x.Location).Select(x => x.FirstOrDefault()).Select(a=>a.Location).ToList();
               
                int duration = Convert.ToInt32((searchData.DateTo - searchData.DateFrom).TotalDays);
                {
                    DateTime CheckInDate = searchData.DateFrom;
                    for (int i = 0; i < searchOutputs.Count; i++)
                    {
                        
                        HotelDetails hotelData = HotelDataList.Where(a => a.ProviderHotelId == searchOutputs[i].code.ToString()).FirstOrDefault();
                        HotelSearchResult hotel = new HotelSearchResult();
                        if (hotelData != null)
                        {
                            hotel.providerHotelCode = searchOutputs[i].code.ToString();
                            hotel.City = hotelData.City;
                            hotel.hotelName = hotelData.HotelName;
                            
                            hotel.Country = hotelData.Country;
                            hotel.hotelStars =int.Parse( hotelData.Rating);
                            var images = hotelData.Images.FirstOrDefault();
                            if (images != null)
                            {
                                hotel.hotelThumb = images.Thum;
                            }
                        //***    hotel.hotelImages = hotelData.Images.Select(a => a.Url).ToList();
                            hotel.Amenities = hotelData.hotelAmenities;
                            hotel.Lat = hotelData.Lat;
                            hotel.Lng = hotelData.Lng;
                            hotel.providerID = "4";
                            hotel.hotelDescription = hotelData.LongDescriptin;
                            hotel.shortcutHotelDescription = hotelData.ShortDescription;
                            hotel.ZipCode = hotelData.Zipcode;
                            hotel.Location = hotelData.Location;
                            hotel.Address = hotelData.Address;
                            hotel.providerHotelID = hotelData.ProviderHotelId;
                            hotel.hotelCode = hotelData.HotelId;
                            hotel.sellCurrency = searchData.Currency;
                          
                            hotel.costCurrency = searchOutputs[i].currency;
                            //set sales rules cirtiera
                            //  hotel.hotelRate = ((hotel.costPrice)+ AppliedMarkup.Value-AppliedDiscount.Value)* ExcahngeRate;

                            hotel.rooms = new List<RoomResult>();
                           
                            AppliedSalesRule AppliedCancellationMarkup = CancellationChargeManager.ApplySalesRules("Markup");

                            for (int j = 0; j < searchOutputs[i].rooms.Count; j++)
                            {
                                for (int x = 0; x < searchOutputs[i].rooms[j].rates.Count; x++)
                                {
                                    RoomResult room = new RoomResult();

                                    if(searchOutputs[i].rooms[j].rates[x].net== null)
                                    {
                                        continue;
                                    }
                                    room.CostPrice = Math.Round(double.Parse(searchOutputs[i].rooms[j].rates[x].net), 3);
                                    hotel.costPrice = room.CostPrice;
                                    CancellationChargeManager.SetResultCriteria(hotel.hotelName, hotel.hotelStars, hotel.costPrice, "4");
                                    ServiceChargeManager.SetResultCriteria(hotel.hotelName, hotel.hotelStars, room.CostPrice
                                        * ProviderExcahngeRate, "4");
                                    AppliedSalesRule AppliedMarkup = ServiceChargeManager.ApplySalesRules("Markup");
                                    AppliedSalesRule AppliedDiscount = ServiceChargeManager.ApplySalesRules("Discount");

                                    //  room.IsRefundable = searchOutputs[i].rooms[j].refundable;
                                    ////******
                                    room.RatePerNight = ((room.CostPrice * ProviderExcahngeRate / duration) + AppliedMarkup.Value - AppliedDiscount.Value) * ExcahngeRate;
                                    room.RatePerNight = Math.Round(room.RatePerNight , 3);

                                    room.TotalSellPrice = Math.Round(room.RatePerNight * duration, 3);
                                    
                                    room.RoomIndex = j + 1;  // index front use
                                    room.RoomReference = searchOutputs[i].rooms[j].rates[x].rateKey; // reference of provider
                                    /////
                                    room.RoomCode = searchOutputs[i].rooms[j].code;
                                    /////////
                                    room.RoomType = searchOutputs[i].rooms[j].name; // standard or double
                                    ///////
                                    room.RoomMeal = searchOutputs[i].rooms[j].rates[x].boardName; 
                                    room.Adult = searchOutputs[i].rooms[j].rates[x].adults;
                                    room.Child = searchOutputs[i].rooms[j].rates[x].children;
                                   room.IsRefundable = searchOutputs[i].rooms[j].rates[x].rateClass== "NRF"?false:true;

                                    ///////
                                    room.Paxs = searchOutputs[i].rooms[j].rates[x].adults + searchOutputs[i].rooms[j].rates[x].children;
                                 //***   room.Images = hotelData.Images.Where(a => a.Category.ToLower() == "hotel rooms").Select(a => a.Url).ToList();
                                    room.DiscountId = AppliedDiscount.ID;
                                    room.MarkupId = AppliedMarkup.ID;
                                    room.DiscountValue = AppliedDiscount.Value * ExcahngeRate;
                                    room.MarkupValue = AppliedMarkup.Value * ExcahngeRate;
                                    room.rateClass = searchOutputs[i].rooms[j].rates[x].rateClass;
                                    room.rateType = searchOutputs[i].rooms[j].rates[x].rateType;
                                    room.childrenAges = searchOutputs[i].rooms[j].rates[x].childrenAges;
                                    room.paymentType = searchOutputs[i].rooms[j].rates[x].paymentType;
                                    room.boardCode= searchOutputs[i].rooms[j].rates[x].boardCode;
                                    room.cancellationRules = searchOutputs[i].rooms[j].rates[x].cancellationPolicies == null ?
                                        new List<CancellationRule>() { new CancellationRule
                                        { Cost = 0, Price = 0, CanellationRuleText = null, FromDate = null, ToDate = null } } :
                                    searchOutputs[i].rooms[j].rates[x].cancellationPolicies.Select(a => new CancellationRule
                                    {

                                        Price = Math.Round(((double.Parse(a.amount) * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3),
                                        CanellationRuleText = searchData.Lang.ToLower() == "en" ? Math.Round(((double.Parse(a.amount) * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3) + "" + searchData.Currency + " To " + a.from : a.from + " إلى " + "" + searchData.Currency + Math.Round(((double.Parse(a.amount) * ProviderExcahngeRate) /*+ AppliedCancellationMarkup.Value*/) * ExcahngeRate, 3),
                                        Cost = Math.Round(double.Parse(a.amount)),
                                        FromDate = a.from.ToString()
                                       
                                    }).ToList();
                                    hotel.rooms.Add(room);
                                }
                            }

                            var minRoom = hotel.rooms.Where(a => a.RatePerNight == hotel.rooms.Min(x => x.RatePerNight)).FirstOrDefault();
                            if (minRoom != null)
                            {
                                hotel.hotelRate = minRoom.RatePerNight;
                                hotel.costPrice = minRoom.CostPrice;
                                hotel.TotalSellPrice = minRoom.TotalSellPrice; 
                                hotel.MarkupId = minRoom.MarkupId;
                                hotel.MarkupValue = minRoom.MarkupValue;
                                hotel.DiscountId = minRoom.DiscountId;
                                hotel.DiscountValue = minRoom.DiscountValue;
                                
                            }
                            results.Add(hotel);
                        }
                    }

                    searchResponse.HotelResult = results;
                    return searchResponse;
                }
            }
            catch (Exception ex)
            {

                LoggingHelper.WriteToFile("MapSearchResult/Errors/", "MapSearchResult_" + searchData.sID,ex.Message , ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);
                return new HotelSearchResponse();
            }
        }

        public void Dispose()
        {
            handle.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
