using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Hotels.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBO.DAL.Models.Context;

namespace Hotels.DAL
{
    public class PayLinkDB : IPaymentData
    {
        public PayLinkRequest GetPayLinkRequestDA(string BN, string Sid,string ip ,string pos, string NotTok)
        {
            try
            {
                PayLinkRequest payLinkData = new PayLinkRequest();
                SearchDBEntities searchDB = new SearchDBEntities();
                CurrencyRepo currencyManager = new CurrencyRepo();

                double totalPrice = 0;
                HotelBookingDBEntities hotelBookingDB = new HotelBookingDBEntities();
                var paxes = hotelBookingDB.HotelBookingPaxs.Where(x => x.Booking_No == BN && x.SID == Sid && x.PaxNo == 1);
                var Customer = paxes.FirstOrDefault(a => a.Booking_No == BN && a.SID == Sid && a.RoomRef == 1 && a.PaxNo == 1);
                var BookingData = hotelBookingDB.HotelsBookings.FirstOrDefault(x => x.Booking_No == BN && x.SessionId == Sid);
                var SearchData = searchDB.SearchCriterias.FirstOrDefault(x => x.sID == Sid);
                var PaxsRooms = paxes.Select(x => x.Room_No.ToString()).ToList();

                var Bookingrooms = searchDB.SearchRoomResults.Where(a => a.sID == Sid && a.HotelCode == BookingData.Hotel_ID && PaxsRooms.Contains(a.RoomCode)).ToList();
                if(Customer==null || Bookingrooms.Count==0 || SearchData == null|| BookingData ==null)
                {
                    return null;
                }
                payLinkData.Customer.CustomerEmail = BookingData.Booking_Email;

                payLinkData.Customer.CustomerPhone = Customer.Phone;
                payLinkData.Customer.FirstName = Customer.First_name;
                payLinkData.Customer.LastName = Customer.Last_Name;
                payLinkData.Customer.IP = ip;
                payLinkData.Customer.Nationality = SearchData.passengerNationality;
                payLinkData.Customer.PaymentLocation = pos;
                payLinkData.Customer.PhoneCodeCountry = Customer.Phone_Code;
                payLinkData.Customer.PhoneCountryCode = null;
                payLinkData.BookingInfo.BookingDate = Convert.ToDateTime(string.Format("{0:s}", DateTime.Now));
                payLinkData.BookingInfo.CheckInDate = SearchData.dateFrom.Value;
                payLinkData.BookingInfo.Description = "";
                payLinkData.BookingInfo.HGNumber = BN;
                payLinkData.BookingInfo.Product = "Hotel";
                payLinkData.BookingInfo.SearchID = Sid;
                var url = ConfigurationSettings.AppSettings["SuccessUrl"];
                payLinkData.PaymentAuthData.FailUrl = url + "?HG=" + BN + "&sid=" + Sid;
                payLinkData.PaymentAuthData.SuccessUrl = url + "?HG=" + BN + "&sid=" + Sid;
                payLinkData.PaymentAuthData.HGToken = null;
                payLinkData.PaymentAuthData.HGTokenStatus = 0;
                payLinkData.PaymentAuthData.HGTrackId = null;
                payLinkData.PaymentAuthData.PaymentMethod = null;
                payLinkData.PaymentAuthData.PaymentToken = null;
                var ActionUrlData = ConfigurationSettings.AppSettings["ActionUrl"];
                var BaseCur = ConfigurationSettings.AppSettings["BaseCur"];
                // stop call cHotelCheckAvailability for tbo 
                payLinkData.ActionsUrl.ValidationUrl = ActionUrlData + "/api/HotelCheckAvailability?sid=" + Sid + "&bookingnum=" + BN;
                payLinkData.ActionsUrl.PostPaymentUrl = ActionUrlData + "/Api/ConfirmHotelStatus?sid=" + Sid + "&bookingNum=" + BN;
                payLinkData.ActionsUrl.PrePaymentUrl = "";

                foreach (var item in Bookingrooms)
                {
                    totalPrice += item.SellPrice.Value;

                }
                double BaseExcahngeRate = currencyManager.GetEveryDayCurrenciesConversion(Bookingrooms[0].SellCurrency, BaseCur,Sid, DateTime.Now).Result.Customer_Sell_Rate;
                double ExcahngeRate = currencyManager.GetEveryDayCurrenciesConversion(BaseCur, BookingData.Sell_Currency, Sid,DateTime.Now).Result.Customer_Sell_Rate;

                totalPrice = totalPrice * BaseExcahngeRate;
                payLinkData.PaymentFareDetails.CustomerPaymentCurrency = BookingData.Sell_Currency;
                payLinkData.PaymentFareDetails.FareAmount = totalPrice;
                payLinkData.PaymentFareDetails.TaxAmount = 0;
                payLinkData.PaymentFareDetails.TotalAmount = totalPrice;
                payLinkData.PaymentFareDetails.TotalChargeAmount = 0;
                payLinkData.PaymentFareDetails.ExchangeRate = ExcahngeRate;
                payLinkData.FormData = null;
                BookingData.NotificationKey = NotTok;
                hotelBookingDB.SaveChanges();

                return payLinkData;
            }catch(Exception ex)
            {
                LoggingHelper.WriteToFile("PaymentViewController/ERRor/", "PaymentView" + "INDAL" + Sid, "InComingData", ex.InnerException?.Message + ex.Message + ex.StackTrace);

                return null;
            }
        }

        public RestCriteraiData GetDataForGatewayDA(string BN  )
        {
            try
            {
                SearchDBEntities searchDB = new SearchDBEntities();
                HotelBookingDBEntities hotelBookingDB = new HotelBookingDBEntities();
                hotelsDBEntities hotelsDB = new hotelsDBEntities();
                SearchData searchData = new SearchData();
                RestCriteraiData restCriterai = new RestCriteraiData();
                var booking = hotelBookingDB.HotelsBookings.FirstOrDefault(x => x.Booking_No == BN);

                var search = searchDB.SearchCriterias.FirstOrDefault(a => a.sID == booking.SessionId);
                restCriterai.searchData = new SearchData();
                restCriterai.searchData.CityName = search.cityName;
                restCriterai.searchData.Currency = search.currency;
                restCriterai.searchData.DateFrom = search.dateFrom.Value;
                restCriterai.searchData.DateTo = search.dateTo.Value;
                restCriterai.searchData.Lang = search.language;
                restCriterai.searchData.Nat = search.passengerNationality;
                restCriterai.searchData.POS = search.pos;
                restCriterai.pos = search.pos;
                restCriterai.searchData.sID = search.sID;
                restCriterai.searchData.Source = search.source;
             var Rooms=   searchDB.SearchRoomDatas.Where(a => a.sID == booking.SessionId).ToList();
                foreach (var item in Rooms)
                {
                    SearchRoom searchRoom = new SearchRoom();
                    searchRoom.Adult = item.adultNo.Value;
                    for (int i = 0; i < item.childernNo.Value; i++)
                    {
                        searchRoom.Child.Add(2);
                    }
                    restCriterai.searchData.SearchRooms.Add(searchRoom);
                }
           // var booking = hotelBookingDB.HotelsBookings.FirstOrDefault(x => x.SessionId == Sid && x.Booking_No == BN);
                restCriterai.cost = booking.Sell_Price.Value;
                restCriterai.Curr = booking.Sell_Currency;
                restCriterai.Pid = booking.Provider_ID;
                if (restCriterai.Pid == "5")
                {
                    TBOContext bOContext = new TBOContext();
                    var Hotel = bOContext.HotelDetails.FirstOrDefault(a => a.HotelCode == booking.Hotel_ID);
                    restCriterai.HotelName = Hotel.HotelName;
                    //restCriterai.HotelStars = int.Parse(Hotel.rating);
                }
                else if (restCriterai.Pid=="4")
                {
                    var Hotel = hotelsDB.hotels.FirstOrDefault(a => a.hotelID == booking.Hotel_ID);
                    restCriterai.HotelName = Hotel.hotelName;
                    restCriterai.HotelStars = int.Parse(Hotel.rating);
                }
                
                return restCriterai;
            }
            catch (Exception ex)
            {
                 
                return null;
            }
        }

        string IPaymentData.GetDataForGatewayDA(string BN, string Sid)
        {
            throw new NotImplementedException();
        }
    }
}
