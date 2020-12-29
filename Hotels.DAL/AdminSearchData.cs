using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Hotels.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.DAL
{
    public class AdminSearchData : IAdminSearchDA
    {
        public bool CheckBooking(string BN)
        {
            HotelBookingDBEntities BookingDB = new HotelBookingDBEntities();
            var booking = BookingDB.HotelsBookings.FirstOrDefault(a => a.Booking_No == BN);
            if (booking != null)
            {
                return true;
            }
            return false;

        }

        public void EditBookingPaxes(BookingPassenger passenger)
        {
            HotelBookingDBEntities BookingDB = new HotelBookingDBEntities();
            var pax = BookingDB.HotelBookingPaxs.FirstOrDefault(p => p.Booking_No == passenger.bookingNo && p.Room_No == passenger.roomNo);
            if (pax != null)
            {
                pax.Salutations = passenger.salutation;
                pax.Last_Name = passenger.lastName;
                pax.First_name = passenger.firstName;
                BookingDB.SaveChanges();
            }

        }

        public List<CustomerData> GetAllCustomer()
        {
            HotelBookingDBEntities BookingDB = new HotelBookingDBEntities();
            List<CustomerData> customers = new List<CustomerData>();
            var AllCustomer = BookingDB.Customers.ToList();
            foreach (var item in AllCustomer)
            {
                CustomerData customerData = new CustomerData();
                customerData.Email = item.Email;
                customerData.First_Name = item.First_Name;
                customerData.Last_Name = item.Last_Name;
                customerData.Phone = item.Phone;
                customerData.Phone_Code = item.Phone_Code;
                customerData.Salutations = item.Salutations;

                customers.Add(customerData);

            }
            return customers;
        }

        public AdminBookingDetails GetBookingDetails(string BN)
        {
            try
            {
                HotelBookingDBEntities BookingDB = new HotelBookingDBEntities();
                SearchDBEntities searchDB = new SearchDBEntities();
                AdminBookingDetails requiredData = new AdminBookingDetails();
                hotelsDBEntities db = new hotelsDBEntities();
                CurrencyRepo repo = new CurrencyRepo();
                BusinessRulesDBEntities dbp = new BusinessRulesDBEntities();

                double CancelRate = 0;
                var BaseCur = ConfigurationSettings.AppSettings["BaseCur"];

                var BookinData = BookingDB.HotelsBookings.FirstOrDefault(a => a.Booking_No.ToLower() == BN.ToLower());
                var searchData = searchDB.SearchCriterias.FirstOrDefault(a => a.sID == BookinData.SessionId);
                requiredData.bookingNo = BN;
                requiredData.bookingPhoneCode = BookinData.Booking_Phone_Code;
                requiredData.bookingTime = BookinData.Booking_Time.Value;
                requiredData.CBNumberData.LanguagesList = BookingDB.Languages.ToList();
                var lang = requiredData.CBNumberData.LanguagesList.FirstOrDefault(a => a.LanguageCode.ToLower() == searchData.language.ToLower());
                if (lang != null)
                    requiredData.CBNumberData.Language = lang.LanguageID;
                requiredData.CBNumberData.PointsOfSaleList = BookingDB.PointsOfSales.ToList();
                var pointofsale = requiredData.CBNumberData.PointsOfSaleList.FirstOrDefault(a => a.NameCode.ToLower() == searchData.pos.ToLower());
                if (pointofsale != null)
                    requiredData.CBNumberData.PointOfSale = int.Parse(pointofsale.Code);

                requiredData.CBNumberData.SourceList = BookingDB.SourceTraffics.ToList();
                var src = requiredData.CBNumberData.SourceList.FirstOrDefault(a => a.Source.ToLower() == searchData.source.ToLower());
                if (src != null)
                    requiredData.CBNumberData.Source = src.Code;
                requiredData.checkIN = searchData.dateFrom.Value;
                requiredData.checkOut = searchData.dateTo.Value;
                requiredData.city = searchData.cityName;
                var cityData = db.Cities.FirstOrDefault(c => c.ID.ToString() == searchData.cityName);
                requiredData.cityName = cityData.City1;
                var conversionRate = repo.GetEveryDayCurrenciesConversion(BookinData.Sell_Currency, BaseCur, searchData.sID, DateTime.Now).Result.Customer_Sell_Rate;
                requiredData.costAmount = BookinData.Sell_Price.Value * conversionRate;
                requiredData.costCurrency = BaseCur;
                requiredData.country = cityData.countryName;
                requiredData.customerEmail = BookinData.Booking_Email;
                var name = BookinData.Pax_Name.Split(' ');
                requiredData.customerFirstName = name[0];
                requiredData.customerID = BookinData.Customer_ID;
                requiredData.customerLastName = name[1];
                requiredData.customerPhone = BookinData.Booking_phone;
                requiredData.customerPhoneCode = BookinData.Booking_Phone_Code;
                requiredData.ForeignAmount = BookinData.Sell_Price.Value; //**//
                requiredData.hotel = BookinData.Hotel_ID;
                requiredData.hotelConfirmationNo = "";
                var hoteldata = db.hotels.FirstOrDefault(a => a.hotelID == BookinData.Hotel_ID);
                requiredData.hotelName = hoteldata.hotelName;
                requiredData.lastBookingStatus = BookinData.Booking_Status;
                requiredData.paxName = BookinData.Pax_Name;
                requiredData.paxQty = BookinData.Pax_Qty.Value;
                // get provider 
                var prov = dbp.HotelProviders.FirstOrDefault(a => a.Provider_ID.ToString() == BookinData.Provider_ID);
                if (prov != null)
                    requiredData.provider = prov.Provider_Name;
                requiredData.roomsQty = BookinData.Rooms_Qty.Value;
                requiredData.salesChannel = searchData.source;
                requiredData.sellCurrency = BookinData.Sell_Currency;//*///
                var ExchangeRate = repo.GetEveryDayCurrenciesConversion(BaseCur, BookinData.Sell_Currency, searchData.sID, DateTime.Now).Result.Customer_Sell_Rate;

                requiredData.sellCurrencyExchRate = ExchangeRate;//**///from base to user//
                requiredData.sellPrice = BookinData.Sell_Price.Value * conversionRate;//**// base curr//
                var CostData = BookingDB.AvailabilityRes.FirstOrDefault(a => a.BookingNum == BookinData.Booking_No && a.Sid == BookinData.SessionId);
                requiredData.SupplierCost = CostData.NewTotalcost.Value;/////////////////****************** //
                requiredData.TotalCostDinars = BookinData.Sell_Price.Value * conversionRate;//**//***with base curr//
                var dataHotelStautsList = BookingDB.HotelBookingStatus.Where(a => a.Booking_No == BN).ToList();
                foreach (var item in dataHotelStautsList)
                {
                    BookingStatusList bookingStatus = new BookingStatusList();
                    bookingStatus.Booking_No = BookinData.Booking_No;
                    bookingStatus.Booking_Status = item.Booking_Status;
                    bookingStatus.Status_Time = item.Status_Time.Value;
                    requiredData.BookingStatusList.Add(bookingStatus);
                }

                var BookingRooms = BookingDB.HotelBookingRooms.Where(x => x.Booking_No == BookinData.Booking_No && x.SID == BookinData.SessionId).ToList();
                for (int i = 0; i < BookingRooms.Count; i++)
                {
                    BookingRoom bookingRoom = new BookingRoom();
                    bookingRoom.bookingNo = BookingRooms[i].Booking_No;
                    bookingRoom.meal = BookingRooms[i].Meal;
                    bookingRoom.paxQty = BookingRooms[i].Paxs_Qty.Value;
                    bookingRoom.roomCategory = BookingRooms[i].Room_Category;
                    bookingRoom.roomNo = i + 1;//**// incremented//
                    var roomnum = BookingRooms[i].Room_No.ToString();
                    var RoomsStats = BookingDB.HotelBookingRoomsStatus.Where(a => a.Booking_No == BN && a.Room_No.ToString() == roomnum).OrderByDescending(a => a.Id).FirstOrDefault();
                    if (RoomsStats != null)
                        bookingRoom.RoomStatus = RoomsStats.Room_Status.ToString();//status add culomn//
                    bookingRoom.roomType = BookingRooms[i].Room_Category;
                    var availabilityHBroom = BookingDB.availabilityRoomRes.Where(x => x.BookingNum == BN && x.roomId.ToString() == roomnum).OrderByDescending(a => a.id).FirstOrDefault();
                    if (availabilityHBroom != null)
                        bookingRoom.TotalCostPerRoom = availabilityHBroom.Cost.Value;///********************************* provider cost//
                    else
                    {
                        var availabilityroom = searchDB.SearchRoomResults.FirstOrDefault(x => x.sID == BookinData.SessionId && x.HotelCode == BookinData.Hotel_ID && x.RoomCode == BookingRooms[i].Room_No.ToString());
                        if (availabilityroom != null)
                            bookingRoom.TotalCostPerRoom = availabilityroom.costPrice.Value;
                    }
                    var roomNum = BookingRooms[i].Room_No.Value;
                    var RoomCancels = BookingDB.CancelPolicies.Where(x => x.RoomCode == roomNum && x.Sid == BookinData.SessionId && x.HotelCode == BookinData.Hotel_ID).ToList();

                    if (RoomCancels.Count > 0)
                    {
                        CancelRate = repo.GetEveryDayCurrenciesConversion(RoomCancels[0].Currency, BaseCur, searchData.sID, DateTime.Now).Result.Customer_Sell_Rate;

                    }
                    foreach (var item in RoomCancels)
                    {
                        SupplierCancellation supplierCancellation = new SupplierCancellation();
                        CustomerCancellation cancellation = new CustomerCancellation();
                        cancellation.Booking_No = BookinData.Booking_No;
                        cancellation.Date_From = item.FromDate.Value;
                        cancellation.Date_To = item.ToDate;
                        cancellation.ID = item.Id;
                        cancellation.No_Show_Amount = (double)Math.Round(item.SellPrice.Value, 3) * CancelRate;
                        cancellation.Room_No = BookingRooms[i].Room_No.Value;
                        cancellation.Rule_Text = cancellation.No_Show_Amount.ToString() + " From " + item.FromDate.Value;
                        supplierCancellation.Booking_No = BookinData.Booking_No;
                        supplierCancellation.Date_From = item.FromDate.Value;
                        supplierCancellation.Date_To = item.ToDate;
                        supplierCancellation.ID = item.Id;
                        supplierCancellation.No_Show_Amount = (double)Math.Round(item.SellPrice.Value, 3) * CancelRate;
                        supplierCancellation.Room_No = BookingRooms[i].Room_No.Value;
                        bookingRoom.SupplierCancellations.Add(supplierCancellation);
                        bookingRoom.CustomerCancellations.Add(cancellation);
                    }
                    var BookingNights = BookingDB.HotelBookingNights.Where(a => a.Booking_No == BookinData.Booking_No && a.Room_No == roomNum).ToList();
                    foreach (var item in BookingNights)
                    {
                        BookingNight night = new BookingNight();
                        night.bookingNo = item.Booking_No;
                        night.currency = BaseCur;
                        night.exchangeRate = 1;
                        night.nightDate = item.NightDate.Value;
                        night.rate = item.Rate.Value * conversionRate;
                        night.roomNo = item.Room_No.Value;
                        bookingRoom.bookingNights.Add(night);
                    }
                    var BookingPaxes = BookingDB.HotelBookingPaxs.Where(a => a.SID == BookinData.SessionId && a.Booking_No == BookinData.Booking_No && a.Room_No == roomNum && a.PaxNo == 1).ToList();
                    foreach (var item in BookingPaxes)
                    {
                        BookingPassenger passenger = new BookingPassenger();
                        passenger.bookingNo = item.Booking_No;
                        passenger.DateOfBirth = Convert.ToDateTime("0001- 01-01T00:00:00");//********* BD and Just lead(okay)
                        passenger.firstName = item.First_name;
                        passenger.lastName = item.Last_Name;
                        passenger.nationality = searchData.passengerNationality;
                        passenger.paxType = item.Pax_Type;
                        passenger.phone = item.Phone;
                        passenger.phoneCode = item.Phone_Code;
                        passenger.roomNo = item.Room_No.Value;
                        passenger.salutation = item.Salutations;
                        bookingRoom.bookingPassengers.Add(passenger);
                    }
                    var dataRoomStautsList = BookingDB.HotelBookingRoomStatusValues.ToList();
                    foreach (var item in dataRoomStautsList)
                    {
                        RoomStatusList roomStatus = new RoomStatusList();
                        roomStatus.ID = item.ID;
                        roomStatus.Status = item.Status;
                        bookingRoom.RoomStatusList.Add(roomStatus);
                    }


                    requiredData.bookingRooms.Add(bookingRoom);
                }



                return requiredData;
        }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("AdminPannelController/ERROR/", "GetBookingDetails" + "INDAL" + BN, "Data", "bookin is " + BN + " and booking is" + BN + ex.Message + ex.StackTrace);

                return new AdminBookingDetails();
    }

}

        public List<HotelBookingStatusValue> GetBookingStautsDetails()
        {
            HotelBookingDBEntities BookingDB = new HotelBookingDBEntities();
            return BookingDB.HotelBookingStatusValues.ToList();
        }

        public List<AdminRequiredData> GetHotelAdimSearchData(string Key)
        {
            try
            {
                HotelBookingDBEntities BookingDB = new HotelBookingDBEntities();
                SearchDBEntities searchDB = new SearchDBEntities();
                List<AdminRequiredData> requiredData = new List<AdminRequiredData>();
                var ListSearchData = BookingDB.HotelsBookings.Where(a => a.Booking_No.ToLower().Contains(Key.ToLower()) || a.Booking_Email.ToLower().Contains(Key.ToLower()) || a.Booking_Status.ToLower().Contains(Key.ToLower())).ToList();
                foreach (var item in ListSearchData)
                {
                    AdminRequiredData adminData = new AdminRequiredData();
                    adminData.bookingNumber = item.Booking_No;
                    adminData.creationDate = item.Booking_Time.Value;
                    adminData.customerEmail = item.Booking_Email;
                    adminData.proveider = item.Provider_ID;
                    adminData.status = item.Booking_Status;
                    var Criteria = searchDB.SearchCriterias.FirstOrDefault(x => x.sID == item.SessionId);
                    adminData.checkin = Criteria.dateFrom.Value;
                    adminData.checkout = Criteria.dateTo.Value;
                    var Names = item.Pax_Name.Split(' ');
                    adminData.LeadFirstName = Names[0];
                    adminData.LeadLastName = Names[1];
                    adminData.Price = item.Sell_Price.Value;
                    adminData.Currency = item.Sell_Currency;
                    if (item.Provider_ID == "4")
                    {
                        adminData.proveiderName = "HotelBeds";
                    }
                    else if (item.Provider_ID == "2")
                    {
                        adminData.proveiderName = "Total Stay";
                    }
                    requiredData.Add(adminData);
                }
                return requiredData;
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("AdminPannelController/ERROR/", "HotelAdminSearchController" + "INDAL" + Key, "Data", "key is " + Key + " and key is" + Key + ex.InnerException?.Message + ex.Message + ex.StackTrace);

                return new List<AdminRequiredData>();
            }
        }

        public List<ClsProvider> GetProviderDetails()
        {
            List<ClsProvider> providers = new List<ClsProvider>();
            BusinessRulesDBEntities db = new BusinessRulesDBEntities();
            var provs = db.HotelProviders.ToList();
            foreach (var item in provs)
            {
                ClsProvider provider = new ClsProvider();
                provider.Currency = item.Provider_Currency_Code;
                provider.ProviderId = item.Provider_ID.Value;
                provider.ProviderName = item.Provider_Name;
                provider.status = item.Active_Status.Value;
				if (provider.status == true)
				{
					providers.Add(provider);

				}
			}
            return providers;
        }

        public List<AdminRequiredData> QueueTransaction(DateTime fromDate, DateTime toDate)
        {
            try
            {
                HotelBookingDBEntities BookingDB = new HotelBookingDBEntities();
                //  SearchDBEntities searchDB = new SearchDBEntities();
                List<AdminRequiredData> requiredData = new List<AdminRequiredData>();
                toDate = toDate.AddDays(1);
                var ListSearchData = BookingDB.HotelsBookings.Where(a => a.Booking_Time.Value <= toDate && a.Booking_Time.Value >= fromDate).ToList();
                foreach (var item in ListSearchData)
                {
                    AdminRequiredData adminData = new AdminRequiredData();
                    adminData.bookingNumber = item.Booking_No;
                    adminData.creationDate = item.Booking_Time.Value;
                    adminData.customerEmail = item.Booking_Email;
                    adminData.proveider = item.Provider_ID;
                    adminData.status = item.Booking_Status;

                    requiredData.Add(adminData);
                }
                return requiredData;
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("AdminPannelController/ERROR/", "QueueTransactionController" + "INDAL", "Data", "from is " + fromDate + " and to is" + toDate + ex.InnerException?.Message + ex.Message + ex.StackTrace);

                return new List<AdminRequiredData>();
            }
        }

        public bool UpdateBookingStatus(string BN, string Status)
        {
            try
            {
                HotelBookingDBEntities db = new HotelBookingDBEntities();
                var BookingData = db.HotelsBookings.FirstOrDefault(x => x.Booking_No == BN);
                BookingData.Booking_Status = Status;
                HotelBookingStatu bookingStatus = new HotelBookingStatu();
                bookingStatus.Booking_No = BN;
                bookingStatus.Booking_Status = Status;
                bookingStatus.Status_Time = DateTime.Now;
                db.HotelBookingStatus.Add(bookingStatus);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("AdminPannelController/ERROR/", "ChangeBookingStatus" + "INDAL" + BN, "Data", "bookin is " + BN + " and booking is" + BN + ex.InnerException?.Message + ex.Message + ex.StackTrace);

                return false;
            }

        }
    }
}
