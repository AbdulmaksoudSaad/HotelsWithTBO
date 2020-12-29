using Hotels.BLL;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using IntegrationTotalStay.Model;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
 

namespace Hotels.ProviderManagment
{
    public class TSMapper : IDisposable
    {
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public HotelSearchResponse MapSearchResult(List<PropertyResult> searchOutputs, SearchData searchData)
        {
            try
            {
            //    var json = new JavaScriptSerializer().Serialize(obj);
                //save result
                //apply salesRules
                HotelSearchResponse searchResponse = new HotelSearchResponse();
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
                var BaseCur = ConfigurationSettings.AppSettings["BaseCur"];

                List<string> hotelIds = searchOutputs.Select(a => a.PropertyReferenceID).ToList();
                HotelManager manager = new HotelManager();
                List<HotelDetails> HotelDataList = manager.GetHotelData(hotelIds, "2");
                List<HotelSearchResult> results = new List<HotelSearchResult>();
                /////double ProviderExcahngeRate = currencyManager.GetCurrencyConversion("USD", BaseCur);
                double ProviderExcahngeRate = currencyManager.GetCurrencyConversion(searchData.Currency, BaseCur, searchData.sID);
                double ExcahngeRate = currencyManager.GetCurrencyConversion(BaseCur, searchData.Currency, searchData.sID);
              //  searchResponse.Locations = HotelDataList.GroupBy(x => x.Location).Select(x => x.FirstOrDefault()).Select(a => a.Location).ToList();

                int duration = Convert.ToInt32((searchData.DateTo - searchData.DateFrom).TotalDays);
                {
                    DateTime CheckInDate = searchData.DateFrom;
                    for (int i = 0; i < searchOutputs.Count; i++)
                    {

                        HotelDetails hotelData = HotelDataList.Where(a => a.ProviderHotelId == searchOutputs[i].PropertyReferenceID.ToString()).FirstOrDefault();
                        HotelSearchResult hotel = new HotelSearchResult();
                        if (hotelData != null)
                        {


                            hotel.providerHotelCode = searchOutputs[i].PropertyID.ToString();
                            hotel.City = hotelData.City;
                            hotel.hotelName = hotelData.HotelName;
                            hotel.Country = hotelData.Country;
                            hotel.hotelStars = int.Parse(hotelData.Rating) > 0 ? int.Parse(hotelData.Rating) - 558 : 0;
                            var images = hotelData.Images.FirstOrDefault();
                            if (images != null)
                            {
                                hotel.hotelThumb = images.Thum;
                            }
                            hotel.hotelImages = hotelData.Images.Select(a => a.Thum).ToList();
                            hotel.Amenities = hotelData.hotelAmenities;
                            hotel.Lat = hotelData.Lat;
                            hotel.Lng = hotelData.Lng;
                            hotel.providerID = "2";
                            hotel.hotelDescription = hotelData.LongDescriptin;
                            hotel.shortcutHotelDescription = hotelData.ShortDescription;
                            hotel.ZipCode = hotelData.Zipcode;
                            hotel.Location = hotelData.Location;
                            hotel.Address = hotelData.Address;
                            hotel.providerHotelID = hotelData.ProviderHotelId;
                            hotel.hotelCode = hotelData.HotelId;
                            hotel.sellCurrency = searchData.Currency;
                            //  hotel.costPrice =//Math.Round( (double.Parse(searchOutputs[i].price.net.Value.ToString()) * ProviderExcahngeRate) /duration,3);
                            hotel.costCurrency = "USD";
                            //set sales rules cirtiera
                            //  hotel.hotelRate = ((hotel.costPrice)+ AppliedMarkup.Value-AppliedDiscount.Value)* ExcahngeRate;

                            hotel.rooms = new List<RoomResult>();
                            CancellationChargeManager.SetResultCriteria(hotel.hotelName, hotel.hotelStars, hotel.costPrice, "4");
                            AppliedSalesRule AppliedCancellationMarkup = CancellationChargeManager.ApplySalesRules("Markup");

                            for (int j = 0; j < searchOutputs[i].RoomTypes.RoomType.Count; j++)
                            {
                                 
                                    RoomResult room = new RoomResult();
 
                                    room.CostPrice = Math.Round(double.Parse(searchOutputs[i].RoomTypes.RoomType[j].Total), 3);

                                    ServiceChargeManager.SetResultCriteria(hotel.hotelName, hotel.hotelStars, room.CostPrice
                                        * ProviderExcahngeRate, "2");
                                    AppliedSalesRule AppliedMarkup = ServiceChargeManager.ApplySalesRules("Markup");
                                    AppliedSalesRule AppliedDiscount = ServiceChargeManager.ApplySalesRules("Discount");

                                    //  room.IsRefundable = searchOutputs[i].rooms[j].refundable;
                                    ////******
                                    room.RatePerNight = ((room.CostPrice * ProviderExcahngeRate / duration) + AppliedMarkup.Value - AppliedDiscount.Value) * ExcahngeRate;
                                   room.RatePerNight = Math.Round(room.RatePerNight , 3);

                                room.TotalSellPrice = Math.Round(room.RatePerNight * duration, 3);
                                    room.RoomIndex = j + 1;
                                    room.RoomReference = searchOutputs[i].RoomTypes.RoomType[j].PropertyRoomTypeID;
                                    /////
                                    room.RoomCode = searchOutputs[i].RoomTypes.RoomType[j].Seq;//
                                    /////////
                                    room.RoomType = searchOutputs[i].RoomTypes.RoomType[j].RoomTyper;
                                room.IsRefundable = searchOutputs[i].RoomTypes.RoomType[j].RoomTyper.Contains("Non-Refundable")|| searchOutputs[i].RoomTypes.RoomType[j].RoomTyper.Contains("Non Refundable")?false:true;
                                room.MealID= searchOutputs[i].RoomTypes.RoomType[j].MealBasisID;
                                ///////
                                room.RoomMeal = searchOutputs[i].RoomTypes.RoomType[j].MealBasis;
                                    room.Adult =int.Parse( searchOutputs[i].RoomTypes.RoomType[j].Adults);
                                    room.Child = int.Parse( searchOutputs[i].RoomTypes.RoomType[j].Children);
                               room.BookingKeyTS = searchOutputs[i].RoomTypes.RoomType[j].BookingToken;
                                    ///////
                                    room.Paxs = int.Parse(searchOutputs[i].RoomTypes.RoomType[j].Adults) + int.Parse(searchOutputs[i].RoomTypes.RoomType[j].Children);
                                    room.Images = hotelData.Images.Where(a => a.Category.ToLower() == "hotel rooms").Select(a => a.Thum).ToList();
                                    room.DiscountId = AppliedDiscount.ID;
                                    room.MarkupId = AppliedMarkup.ID;
                                    room.DiscountValue = AppliedDiscount.Value * ExcahngeRate;
                                    room.MarkupValue = AppliedMarkup.Value * ExcahngeRate;
                                    room.cancellationRules = null;
                                   /* room.cancellationRules = searchOutputs[i].rooms[j].rates[x].cancellationPolicies == null ? new List<CancellationRule>() { new CancellationRule { Cost = Math.Round((AppliedCancellationMarkup.Value) * ExcahngeRate, 3) } } :
                                    searchOutputs[i].rooms[j].rates[x].cancellationPolicies.Select(a => new CancellationRule
                                    {

                                        Cost = Math.Round(((double.Parse(a.amount) * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3),
                                        CanellationRuleText = searchData.Lang.ToLower() == "en" ? Math.Round(((double.Parse(a.amount) * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3) + "" + searchData.Currency + " To " + a.from : a.from + " إلى " + "" + searchData.Currency + Math.Round(((double.Parse(a.amount) * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3)
                                    }).ToList();*/
                                    hotel.rooms.Add(room);
                                
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

                LoggingHelper.WriteToFile("SearchController/MapSearchResultTS/Errors/", "MapSearchResult_" + searchData.sID, ex.Message, ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);
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
