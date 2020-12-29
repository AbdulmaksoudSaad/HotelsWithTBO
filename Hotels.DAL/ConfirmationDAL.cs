using HotelBedsIntegration.Models;
using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Hotels.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBO.DAL.Models.Context;

namespace Hotels.DAL
{
    public class ConfirmationDAL : IConfirmation
    {
        public ConfirmationModel GetConfirmationData(string sid, string BN)
        {
            try
            {
                ConfirmationModel confirmationModel = new ConfirmationModel();
                HotelBookingDBEntities BookingDB = new HotelBookingDBEntities();
                SearchDBEntities searchDB = new SearchDBEntities();
                hotelsDBEntities hotelsDB = new hotelsDBEntities();
                HotelBedEntity HBDB = new HotelBedEntity();
                 List<SearchRoomResult> searchRoomResults = new List<SearchRoomResult>();
                List<SearchRoomResult> SelectedRooms = new List<SearchRoomResult>();

                var BookingData = BookingDB.HotelsBookings.FirstOrDefault(a => a.SessionId == sid && a.Booking_No == BN);
                var Paxes = BookingDB.HotelBookingPaxs.Where(x => x.Booking_No == BN && x.SID == sid && x.PaxNo == 1);
                var SearchData = searchDB.SearchCriterias.FirstOrDefault(a => a.sID == sid);
                var RoomData = searchDB.SearchRoomDatas.Where(a => a.sID == sid).ToList();
                var Rooms = Paxes.Select(x => x.RoomRef.ToString()).ToList();
                foreach (var item in Rooms)
                {
                    var roomsearch = searchDB.SearchRoomResults.FirstOrDefault(r => r.sID == sid && r.HotelCode == BookingData.Hotel_ID && r.RoomCode == item);
                    SelectedRooms.Add(roomsearch);

                }
                if (BookingData.Provider_ID =="4")
                {
                    var hotelData = hotelsDB.hotels.FirstOrDefault(a => a.hotelID == BookingData.Hotel_ID);
                    var hotelDesc = hotelsDB.HotelsDescriptions.FirstOrDefault(a => a.hotelID == BookingData.Hotel_ID);
                    confirmationModel.hotel.HotelDescription = hotelDesc.Description1;
                    var hotelsImage = hotelsDB.HotelsImages.Where(a => a.HotelID == BookingData.Hotel_ID).ToList();
                    confirmationModel.hotel.City = hotelData.cityName;
                    confirmationModel.hotel.Country = hotelData.countryName;
                    confirmationModel.hotel.hotelCode = BookingData.Hotel_ID;
                    confirmationModel.hotel.hotelName = hotelData.hotelName;
                    confirmationModel.hotel.Address = hotelData.address;
                    confirmationModel.hotel.hotelStars = int.Parse(hotelData.rating);
                    if (hotelsImage.Count > 0)
                        confirmationModel.hotel.hotelThumb = hotelsImage[0].Thum;
                    confirmationModel.hotel.Location = hotelData.location;

                }
                else if (BookingData.Provider_ID == "5")
                {
                    using (TBOContext tBOContext = new TBOContext())
                    {
                        var hotelData = tBOContext.HotelDetails.FirstOrDefault(a => a.HotelCode == BookingData.Hotel_ID);
                        var hotelDesc = hotelData.Description;
                        var hotelsImage = tBOContext.HotelImages.Where(a => a.HotelCode == BookingData.Hotel_ID).ToList();
                        confirmationModel.hotel.City = hotelData.CityName;
                        confirmationModel.hotel.Country = hotelData.CountryName;
                        confirmationModel.hotel.hotelCode = BookingData.Hotel_ID;
                        confirmationModel.hotel.hotelName = hotelData.HotelName;
                        confirmationModel.hotel.Address = hotelData.Address;
                        //confirmationModel.hotel.hotelStars = int.Parse(hotelData.rating);
                        if (hotelsImage.Count > 0)
                            confirmationModel.hotel.hotelThumb = hotelsImage[0].URL;
                        confirmationModel.hotel.Location = hotelData.HotelLocation;

                    }

                }
                confirmationModel.hotel.CheckIn = SearchData.dateFrom.Value.ToString();
                confirmationModel.BookingTime = BookingData.Booking_Time.Value;
                confirmationModel.hotel.CheckOut = SearchData.dateTo.Value.ToString();
                        confirmationModel.hotel.Paxes = BookingData.Pax_Qty.Value;
                confirmationModel.hotel.Rooms = BookingData.Rooms_Qty.Value;
            var PConfirm  =BookingDB.BookingConfirmationDatas.FirstOrDefault(a => a.SessionID == sid && a.BookingNum == BN);
                if(PConfirm !=null)
                {
                    confirmationModel.ProviderConfirmation = PConfirm.Reference;
                }
                confirmationModel.hotel.sellCurrency = BookingData.Sell_Currency;
                confirmationModel.hotel.TotalSellPrice = BookingData.Sell_Price.Value;
                confirmationModel.bookingNum = BookingData.Booking_No;
                confirmationModel.mail = BookingData.Booking_Email;
                confirmationModel.status = BookingData.Booking_Status;
                CurrencyRepo repo = new CurrencyRepo();
                double ExcahngeRate = repo.GetEveryDayCurrenciesConversion(SearchData.currency, BookingData.Sell_Currency,sid, DateTime.Now).Result.Customer_Sell_Rate;
                foreach (var item in Paxes)
                {
                    ConfirmedTraveller traveller = new ConfirmedTraveller();
                    traveller.FirstName = item.First_name;
                    traveller.LastName = item.Last_Name;
                    traveller.Title = item.Salutations;
                     var roomResult = SelectedRooms.FirstOrDefault(x => x.RoomCode == item.RoomRef.ToString());
                    searchRoomResults.Add(roomResult);
                    var specRoom1 = SelectedRooms.FirstOrDefault(s => s.RoomCode == item.RoomRef.ToString());
                    if (specRoom1.Childern  != 0)
                    {
                     var specRoom=   SelectedRooms.FirstOrDefault(s => s.RoomCode == item.RoomRef.ToString());

                       var childern= RoomData.Where(a => a.adultNo == specRoom.Adults && a.childernNo == specRoom.Childern).ToList();
                      if (childern.Count == 1)
                        {
                         var childages=   childern[0].childAge.Split('-');
                            traveller.ChildAge.AddRange(childages.ToList());
                        }
                        else
                        {
                       var child= RoomData.FirstOrDefault(a => a.adultNo == specRoom.Adults && a.childernNo == specRoom.Childern && a.roomNo == item.RoomRef);
                            var childages=child.childAge.Split('-');
                            traveller.ChildAge.AddRange(childages.ToList());
                       }
                         
                    }
                    confirmationModel.travellers.Add(traveller);
                }
                foreach (var item in searchRoomResults)
                {
                    ConfirmedRoom confirmedRoom = new ConfirmedRoom();
                    if (BookingData.Provider_ID=="5")
                    {
                        var roomNo = int.Parse(item.RoomCode);
                        confirmedRoom.Adult = RoomData.FirstOrDefault(ad => ad.roomNo == roomNo).adultNo ?? default(int);
                        confirmedRoom.Child = RoomData.FirstOrDefault(ad => ad.roomNo == roomNo).childernNo ?? default(int);
             
                    }
                    else
                    {
                        confirmedRoom.Adult = item.Adults.Value;
                        confirmedRoom.Child = item.Childern.Value;

                    }
                    //var  RoomsImage = hotelsImage.Where(a => a.Category.ToLower() == "hotel rooms").ToList();
                    //***************
                    //if (RoomsImage.Count > 0)
                    //{  
                    //confirmedRoom.Image = RoomsImage[0].URL;
                    //}
                    confirmedRoom.IsRefundable = item.IsRefundable.Value;
                    confirmedRoom.Paxs = item.PaxSQty.Value;
                    confirmedRoom.RoomCode = item.RoomCode;
                    confirmedRoom.RoomMeal = item.meal;
                    confirmedRoom.RoomType = item.RoomName;
                    confirmedRoom.RateType = item.rateType;
                    List<CancelPolicy> cancelPolicies = new List<CancelPolicy>();
                    using (HotelBookingDBEntities db = new HotelBookingDBEntities())
                    {
                        cancelPolicies = db.CancelPolicies.Where(a => a.Sid == sid && a.HotelCode == BookingData.Hotel_ID && a.RoomCode.ToString() == item.RoomCode && a.ProviderID == BookingData.Provider_ID).ToList();
                    }

                    if (cancelPolicies.Count > 0)
                    {
                        
                        foreach (var p in cancelPolicies)
                        {
                            CancellationRule cancellation = new CancellationRule();
                            cancellation.FromDate = p.FromDate.ToString();
                            if(BookingData.Sell_Currency==p.Currency)
                            cancellation.Price = (double)p.SellPrice.Value;
                            else
                            {
                               
                                cancellation.Price = (double)p.SellPrice.Value * ExcahngeRate;
                            }

                            cancellation.CanellationRuleText = cancellation.Price + " " + BookingData.Sell_Currency + " From " + p.FromDate.Value.ToString("MMMM dd, yyyy")  ;
                            confirmedRoom.cancellations.Add(cancellation);
                        }
                      
                    }
                    confirmationModel.rooms.Add(confirmedRoom);
                }
               var ConfData= HBDB.BookingConfirmations.FirstOrDefault(a => a.BookingNum == BN);
                if(ConfData !=null)
                confirmationModel.PayableNote = "Payable through  "+ConfData.supplier+", acting as agent for the service operating company, details of which can be provided upon request. VAT: "+ConfData.Remark+" Reference:"+ ConfData.Reference;
                return confirmationModel;
            }catch(Exception ex)
            {
                LoggingHelper.WriteToFile("ConfirmationController/ERROR/", "ConfirmationDAL" + "INDAL" + sid, "ConfirmData", "Sid is " + sid + " and Booking is" + BN+ ex.InnerException?.Message+ ex.Message + ex.StackTrace);

                return null;
            }
        }

        public UpcomingHistory GetUpcomingAndHistoryData(string mail)
        {
            try
            {
                UpcomingHistory upcomingHistory = new UpcomingHistory();
                 
                HotelBookingDBEntities BookingDB = new HotelBookingDBEntities();
                SearchDBEntities searchDB = new SearchDBEntities();
                hotelsDBEntities hotelsDB = new hotelsDBEntities();
                var BookingData = BookingDB.HotelsBookings.Where(a => a.Booking_Email == mail ).ToList();
                foreach (var item in BookingData)
                {
                   ConfirmationModel BookingModel = new ConfirmationModel();
                    var SearchData = searchDB.SearchCriterias.FirstOrDefault(a => a.sID == item.SessionId);
                    if (SearchData.dateFrom >= DateTime.Now)
                    {
                        var hotelData = hotelsDB.hotels.FirstOrDefault(a => a.hotelID == item.Hotel_ID);
                        var hotelsImage = hotelsDB.HotelsImages.Where(a => a.HotelID == item.Hotel_ID).ToList();
                        BookingModel.hotel.CheckIn = SearchData.dateFrom.Value.ToString("dd-MM-yyyy");
                        BookingModel.hotel.CheckOut = SearchData.dateTo.Value.ToString("dd-MM-yyyy");
                        BookingModel.hotel.City = hotelData.cityName;
                        BookingModel.hotel.Country = hotelData.countryName;
                        BookingModel.hotel.hotelCode = item.Hotel_ID;
                        BookingModel.hotel.hotelName = hotelData.hotelName;
                        BookingModel.hotel.hotelStars = int.Parse(hotelData.rating) > 0 ? int.Parse(hotelData.rating) - 558 : 0;
                        BookingModel.hotel.hotelThumb = hotelsImage[0].Thum;
                        BookingModel.hotel.Location = hotelData.location;
                        BookingModel.hotel.Paxes = item.Pax_Qty.Value;
                        BookingModel.hotel.Rooms = item.Rooms_Qty.Value;
                        BookingModel.hotel.sellCurrency = item.Sell_Currency;
                        BookingModel.hotel.TotalSellPrice = item.Sell_Price.Value;
                        BookingModel.bookingNum = item.Booking_No;
                        BookingModel.mail = item.Booking_Email;
                        BookingModel.status = item.Booking_Status;
                        upcomingHistory.Upcoming.Add(BookingModel);

                    }
                    else
                    {
                        var hotelData = hotelsDB.hotels.FirstOrDefault(a => a.hotelID == item.Hotel_ID);
                        var hotelsImage = hotelsDB.HotelsImages.Where(a => a.HotelID == item.Hotel_ID).ToList();
                        BookingModel.hotel.CheckIn = SearchData.dateFrom.Value.ToString("dd-MM-yyyy");
                        BookingModel.hotel.CheckOut = SearchData.dateTo.Value.ToString("dd-MM-yyyy");
                        BookingModel.hotel.City = hotelData.cityName;
                        BookingModel.hotel.Country = hotelData.countryName;
                        BookingModel.hotel.hotelCode = item.Hotel_ID;
                        BookingModel.hotel.hotelName = hotelData.hotelName;
                        BookingModel.hotel.hotelStars = int.Parse(hotelData.rating) > 0 ? int.Parse(hotelData.rating) - 558 : 0;
                        BookingModel.hotel.hotelThumb = hotelsImage[0].Thum;
                        BookingModel.hotel.Location = hotelData.location;
                        BookingModel.hotel.Paxes = item.Pax_Qty.Value;
                        BookingModel.hotel.Rooms = item.Rooms_Qty.Value;
                        BookingModel.hotel.sellCurrency = item.Sell_Currency;
                        BookingModel.hotel.TotalSellPrice = item.Sell_Price.Value;
                        BookingModel.bookingNum = item.Booking_No;
                        BookingModel.mail = item.Booking_Email;
                        BookingModel.status = item.Booking_Status;
                        upcomingHistory.Histories.Add(BookingModel);
                    }
                   
                }
                

                return upcomingHistory;
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("UpcomingAndHistoryController/ERRor/", "UpcomingAndHistory" + "INController" + mail, "InComingData",   ex.InnerException?.Message + ex.Message + ex.StackTrace);

                return null;
            }
        }
    }
}
