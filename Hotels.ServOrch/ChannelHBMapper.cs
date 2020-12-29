using HotelBedsIntegration.Models;
using Hotels.BLL;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
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
   public class ChannelHBMapper:IDisposable
    {
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public void Dispose()
        {
            handle.Dispose();
            GC.SuppressFinalize(this);
        }

        public List<HotelChannelResult> MapSearchResult(List<Hotel> searchOutputs, SearchData searchData)
        {
            try
            {
                //save result
                //apply salesRules
                List<HotelChannelResult> searchResponse = new List<HotelChannelResult>();
                CurrencyManager currencyManager = new CurrencyManager();
                SalesRulesManager ServiceChargeManager = new SalesRulesManager();
                SalesRulesManager CancellationChargeManager = new SalesRulesManager();
                var BaseCur = ConfigurationSettings.AppSettings["BaseCur"];

                //start sales rules service charge
                ServiceChargeManager.FillSalesRules(searchData.POS, "Hotel", "Service Charge");//1
                ServiceChargeManager.PrepareSearchCriteriaDic(searchData);//2
                ////
                /////cancellation charge 
                CancellationChargeManager.FillSalesRules(searchData.POS, "Hotel", "Cancellation Charge");
                CancellationChargeManager.PrepareSearchCriteriaDic(searchData);
                //

                List<string> hotelIds = searchOutputs.Select(a => a.code.ToString()).ToList();
                HotelManager manager = new HotelManager();
                List<HotelDetails> HotelDataList = manager.GetChannelHotelData(hotelIds, "4");
                //  List<HotelSearchResult> results = new List<HotelSearchResult>();
                double ProviderExcahngeRate = currencyManager.GetCurrencyConversion(searchOutputs[0].currency, BaseCur, searchData.sID);
                //double ProviderExcahngeRate = currencyManager.GetCurrencyConversion("EUR", BaseCur);
                double ExcahngeRate = currencyManager.GetCurrencyConversion(BaseCur, searchData.Currency, searchData.sID);
                //  searchResponse.Locations = HotelDataList.GroupBy(x => x.Location).Select(x => x.FirstOrDefault()).Select(a=>a.Location).ToList();

                int duration = Convert.ToInt32((searchData.DateTo - searchData.DateFrom).TotalDays);
                {
                    DateTime CheckInDate = searchData.DateFrom;
                    for (int i = 0; i < searchOutputs.Count; i++)
                    {

                        HotelDetails hotelData = HotelDataList.Where(a => a.ProviderHotelId == searchOutputs[i].code.ToString()).FirstOrDefault();
                        HotelChannelResult hotel = new HotelChannelResult();
                        if (hotelData != null)
                        { 
                            hotel.providerHotelCode = searchOutputs[i].code.ToString();
                            hotel.providerID = "4";
                            
                            hotel.providerHotelID = hotelData.ProviderHotelId;
                            hotel.hotelId = hotelData.HotelId;
                            hotel.sellCurrency = searchData.Currency;
                            //  hotel.costPrice =//Math.Round( (double.Parse(searchOutputs[i].price.net.Value.ToString()) * ProviderExcahngeRate) /duration,3);
                            hotel.costCurrency = searchOutputs[i].currency;
                           
                            CancellationChargeManager.SetResultCriteria(hotelData.HotelName, int.Parse(hotelData.Rating), hotel.costPrice, "4");
                            AppliedSalesRule AppliedCancellationMarkup = CancellationChargeManager.ApplySalesRules("Markup");
                            int roomcount = 0;
                            for (int j = 0; j < searchOutputs[i].rooms.Count; j++)
                            {
                                RoomPackages roomPackage = new RoomPackages();
                               List< ChannelRoomsRate >roomsRate = new List<ChannelRoomsRate>();
                                roomPackage.RoomCategory = searchOutputs[i].rooms[j].name;
                                List<RoomPackage> Containers = new List<RoomPackage>();
                                for (int r = 0; r < searchData.SearchRooms.Count; r++)
                                {
                                    ChannelRoomsRate roomRate = new ChannelRoomsRate();
                                    /*  var rooms = searchOutputs[i].rooms[j].rates.GroupBy(c => new
                                      {
                                          c.adults,
                                          c.children,
                                      }).Select(c => c.Where(a=>a.adults== searchData.SearchRooms[r].Adult &&a.children== searchData.SearchRooms[r].Child.Count).ToList());
                                            */
                                  var rooms=searchOutputs[i].rooms[j].rates.Where(a => a.adults == searchData.SearchRooms[r].Adult && a.children == searchData.SearchRooms[r].Child.Count).GroupBy(p => p.rateKey).Select(grp => grp.FirstOrDefault()).ToList();
                                    if (rooms.Count > 0)
                                    {
                                        roomRate.RoomRates.AddRange(rooms);
                                        roomsRate.Add(roomRate);
                                        
                                    }
                                    else
                                    {
                                        var roomnames = searchOutputs[i].rooms[j].name.Split(' ');
                                        var key = roomnames[roomnames.Length-1];
                                      var roomstype=  searchOutputs[i].rooms.Where(a => a.name.Contains(key));
                                        foreach (var item in roomstype)
                                        {
                                         var RoomsSameType=   item.rates.Where(a => a.adults == searchData.SearchRooms[r].Adult && a.children == searchData.SearchRooms[r].Child.Count).GroupBy(p => p.rateKey).Select(grp => grp.FirstOrDefault()).ToList();
                                            if (RoomsSameType.Count > 0)
                                            {
                                                roomRate.RoomRates.AddRange(RoomsSameType);
                                                roomsRate.Add(roomRate);

                                            }
                                             
                                        }
                                       
                                    }
                                    if (roomRate.RoomRates.Count == 0)
                                    {
                                        roomsRate = new List<ChannelRoomsRate>();
                                        break;
                                    }
                                }
                                int max = 0;
                                if (roomsRate.Count > 0)
                                {
                                      max = roomsRate.Max(r => r.RoomRates.Count);
                                }
                                 
                                for (int ra = 0; ra < max; ra++)
                                {
                                    RoomPackage Container = new RoomPackage();
                                    double sellPerNight = 0;
                                    double sellPerAll = 0;

                                    List<RoomResult> resultsPackage = new List<RoomResult>();
                                    foreach (var item in roomsRate)
                                    {
                                        int indx = ra;
                                        if (ra >= item.RoomRates.Count)
                                        {
                                            indx = item.RoomRates.Count-1;
                                        }
                                        RoomResult room = new RoomResult();
                                        room.PackageNO = ra + 1;
                                        if (item.RoomRates[indx].net == null)
                                        {
                                            continue;
                                        }
                                        room.CostPrice = Math.Round(double.Parse(item.RoomRates[indx].net), 3);

                                        ServiceChargeManager.SetResultCriteria(hotelData.HotelName, int.Parse(hotelData.Rating), room.CostPrice
                                            * ProviderExcahngeRate, "4");
                                        AppliedSalesRule AppliedMarkup = ServiceChargeManager.ApplySalesRules("Markup");
                                        AppliedSalesRule AppliedDiscount = ServiceChargeManager.ApplySalesRules("Discount");

                                        //  room.IsRefundable = searchOutputs[i].rooms[j].refundable;
                                        ////******
                                        room.RatePerNight = ((room.CostPrice * ProviderExcahngeRate / duration) + AppliedMarkup.Value - AppliedDiscount.Value) * ExcahngeRate;
                                        room.RatePerNight = Math.Round(room.RatePerNight, 3);
                                        sellPerNight += room.RatePerNight;
                                        room.TotalSellPrice = Math.Round(room.RatePerNight * duration, 3);
                                        sellPerAll += room.TotalSellPrice;
                                        roomcount += 1;
                                        room.RoomIndex = roomcount ;
                                        room.RoomReference = item.RoomRates[indx].rateKey;
                                        /////
                                        room.RoomCode = searchOutputs[i].rooms[j].code;
                                        /////////
                                        room.RoomType = searchOutputs[i].rooms[j].name;
                                        ///////
                                        room.RoomMeal = item.RoomRates[indx].boardName;
                                        room.Adult = item.RoomRates[indx].adults;
                                        room.Child = item.RoomRates[indx].children;
                                        room.IsRefundable = item.RoomRates[indx].rateClass == "NRF" ? false : true;

                                        ///////
                                        room.Paxs = item.RoomRates[indx].adults + item.RoomRates[indx].children;
                                        //***   room.Images = hotelData.Images.Where(a => a.Category.ToLower() == "hotel rooms").Select(a => a.Url).ToList();
                                        room.DiscountId = AppliedDiscount.ID;
                                        room.MarkupId = AppliedMarkup.ID;
                                        room.DiscountValue = AppliedDiscount.Value * ExcahngeRate;
                                        room.MarkupValue = AppliedMarkup.Value * ExcahngeRate;

                                        room.cancellationRules = item.RoomRates[indx].cancellationPolicies == null ? new List<CancellationRule>() { new CancellationRule { Cost = 0, Price = 0, CanellationRuleText = null, FromDate = null, ToDate = null } } :
                                        item.RoomRates[indx].cancellationPolicies.Select(a => new CancellationRule
                                        {

                                            Price = Math.Round(((double.Parse(a.amount) * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3),
                                            CanellationRuleText = searchData.Lang.ToLower() == "en" ? Math.Round(((double.Parse(a.amount) * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3) + "" + searchData.Currency + " To " + a.from : a.from + " إلى " + "" + searchData.Currency + Math.Round(((double.Parse(a.amount) * ProviderExcahngeRate) + AppliedCancellationMarkup.Value) * ExcahngeRate, 3),
                                            Cost = Math.Round(double.Parse(a.amount)),
                                            FromDate = a.from.ToString()

                                        }).ToList();
                                        resultsPackage.Add(room);
                                    }
                                    Container.No = ra + 1;
                                    Container.PricePerNight = sellPerNight;
                                    Container.PricePerAllNight = sellPerAll;
                                    Container.roomResults.AddRange(resultsPackage);
                                    Containers.Add(Container);
                                }
                                roomPackage.roomPackages.AddRange(Containers);
                                hotel.packages.Add(roomPackage);
                            }
                            double MinPerNight = 0;
                            double MinperAll = 0;
                        var mindata= hotel.packages[0].roomPackages.Where(a => a.PricePerAllNight == hotel.packages[0].roomPackages.Min(x => x.PricePerAllNight)).FirstOrDefault();
                            if (mindata != null)
                            {
                                MinperAll = mindata.PricePerAllNight;
                                MinPerNight = mindata.PricePerNight;
                            }
                            foreach (var item in hotel.packages)
                            {
                                var minPackage=    item.roomPackages.Where(a => a.PricePerAllNight == item.roomPackages.Min(x => x.PricePerAllNight)).FirstOrDefault();
                                if(minPackage != null)
                                {
                                    if (minPackage.PricePerAllNight < MinperAll)
                                    {
                                        MinperAll = minPackage.PricePerAllNight;
                                        MinPerNight = minPackage.PricePerNight;
                                    }
                                }
                            }
                          // var minRoom = hotel.rooms.Where(a => a.RatePerNight == hotel.rooms.Min(x => x.RatePerNight)).FirstOrDefault();
                           if (MinperAll >0)
                            {
                                hotel.PricePerNight = MinPerNight;
                                //   hotel.costPrice = minRoom.CostPrice;
                                hotel.PricePerAllNight = MinperAll;
                               
                           }
                            searchResponse.Add(hotel);
                        }
                    }
                  //  searchResponse.HotelResult = results;
                    return searchResponse;
                }
            }
            catch (Exception ex)
            {

                LoggingHelper.WriteToFile("MapSearchResult/Errors/", "MapSearchResult_" + searchData.sID, ex.Message, ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);
                return new List<HotelChannelResult>();
            }
        }

    }
}
