using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.DAL
{
    public class CheckAvailabilityRepo
    {

        public static void SaveRequestToDB(AvailabilityReq req)
        {
            try
            {
                using (HotelBookingDBEntities db = new HotelBookingDBEntities())
                {
                    CheckAvailabiltyReq availabiltyReq = new CheckAvailabiltyReq();
                    availabiltyReq.BookinNum = req.BookingNum;
                    availabiltyReq.HotelCode = req.HotelCode;
                    availabiltyReq.Pid = req.PID;
                    availabiltyReq.SID = req.Sid;
                    availabiltyReq.TotalCost = req.TotalCost;
                    availabiltyReq.CreateAt = DateTime.Now;
                    var data = db.CheckAvailabiltyReqs;
                    db.CheckAvailabiltyReqs.Add(availabiltyReq);
                    foreach (var item in req.Rooms)
                    {
                        AvailabilityRoom room = new AvailabilityRoom();
                        room.BookingNum = req.BookingNum;
                        room.RoomCode = item.RoomId.ToString();
                        room.RoomRef = item.RoomRef;
                        room.Sid = req.Sid;
                        db.AvailabilityRooms.Add(room);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("AvailabilityController/Errors/", "CheckAvailabilityRepo" + "SaveRequestToDB" + req.Sid, ex.InnerException?.Message, ex.Message + ex.StackTrace);

            }
        }
        public static void SaveResponseToDB(CheckAvailabilityResult res, double newCost, AvailabilityReq req, string currency, string src)
        {
            try
            {
                using (HotelBookingDBEntities db = new HotelBookingDBEntities())
                {
                    AvailabilityRe availabilityRes = new AvailabilityRe();
                    availabilityRes.TotalsellPrice = res.TotalCost;
                    availabilityRes.NewTotalcost = newCost;
                    availabilityRes.OldTotalcost = req.TotalCost;
                    availabilityRes.BookingNum = req.BookingNum;
                    availabilityRes.PID = req.PID;
                    availabilityRes.SellCurrency = currency;
                    availabilityRes.Sid = req.Sid;
                    availabilityRes.Status = res.Status;
                    availabilityRes.hotelCode = req.HotelCode;
                    db.AvailabilityRes.Add(availabilityRes);
                    foreach (var item in res.Result)
                    {
                        availabilityRoomRe roomRes = new availabilityRoomRe();
                        roomRes.BookingNum = req.BookingNum;
                        roomRes.Pid = req.PID;
                        roomRes.Sid = req.Sid;
                        roomRes.RoomRef = item.RoomCode;
                        roomRes.Cost = item.CostPrice;
                        roomRes.Courrency = currency;
                        roomRes.roomId = item.RoomIndex;
                        roomRes.SellPrice = item.TotalSellPrice;
                        if (src == "policy")
                        {
                            foreach (var cancel in item.cancellationRules)
                            {
                                CancelPolicy policy = new CancelPolicy();
                                policy.Cost = (decimal)cancel.Cost;
                                //if (req.PID == "4")
                                //{
                                    //policy.Currency = "EUR";
                                    policy.Currency = currency;
                               // }
                                //else if (req.PID == "2")
                                //{
                                    //policy.Currency = "USD";
                               // }
                                policy.FromDate = DateTime.Parse(cancel.FromDate);
                                policy.HotelCode = req.HotelCode;
                                policy.ProviderCurrency = currency;
                                policy.ProviderID = req.PID;
                                policy.RoomCode = item.RoomIndex;
                                policy.SellPrice = (decimal)cancel.Price;
                                policy.Sid = req.Sid;
                                if (cancel.ToDate != null)
                                    policy.ToDate = DateTime.Parse(cancel.ToDate);
                                db.CancelPolicies.Add(policy);
                            }
                        }
                        db.availabilityRoomRes.Add(roomRes);
                    }
                    if (res.Status != 0)
                    {
                        var bookingData = db.HotelsBookings.FirstOrDefault(a => a.SessionId == req.Sid && a.Booking_No == req.BookingNum);
                        if (bookingData != null)
                        {
                            bookingData.Sell_Price = res.TotalCost;
                        }
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("AvailabilityController/Errors/", "CheckAvailabilityRepo" + "SaveResponseToDB" + req.Sid, ex.InnerException?.Message, ex.Message + ex.StackTrace);

            }
        }
        public static AvailabilityValidModel GetAvailabilityFromDB(string sid, string BN)
        {
            AvailabilityValidModel availabilityValid = new AvailabilityValidModel();
            AvailabilityReq availabilityReq = new AvailabilityReq();
            try
            {
                SearchDBEntities searchDB = new SearchDBEntities();
                using (HotelBookingDBEntities db = new HotelBookingDBEntities())
                {
                    var HotelBookin = db.HotelsBookings.FirstOrDefault(x => x.SessionId == sid && x.Booking_No == BN);
                    availabilityReq.HotelCode = HotelBookin.Hotel_ID;
                    availabilityReq.BookingNum = BN;
                    availabilityReq.PID = HotelBookin.Provider_ID;
                    availabilityReq.Sid = sid;
                    availabilityValid.Curr = HotelBookin.Sell_Currency;
                    var BookedRooms = db.HotelBookingRooms.Where(x => x.SID == sid && x.Booking_No == BN).ToList();
                    var lstRooms = BookedRooms.Select(s => s.Room_No.ToString()).ToList();
                    var roomsData = searchDB.SearchRoomResults.Where(x => x.sID == sid && x.HotelCode == HotelBookin.Hotel_ID && lstRooms.Contains(x.RoomCode) && x.ProviderId.ToString() == HotelBookin.Provider_ID).ToList();
                    if (roomsData != null && roomsData.Count >0)
                    {
                        for (int i = 0; i < BookedRooms.Count; i++)
                        {
                            RoomAvailability room = new RoomAvailability();
                            var roomData = roomsData.FirstOrDefault(a => a.RoomCode == BookedRooms[i].Room_No.ToString());
                            room.RoomId = int.Parse(roomData.RoomCode);
                            room.RoomRef = roomData.RoomReference;
                            room.Cost = roomData.costPrice.Value;
                            availabilityReq.Rooms.Add(room);
                            availabilityReq.TotalCost += roomData.costPrice.Value;
                        }
                    }

                }
                availabilityValid.availabilityReq = availabilityReq;
                return availabilityValid;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static AvailabilityReq GetAvailabilityForCancelFromDB(string sid, string HotelCode, string roomcode, string pid)
        {

            AvailabilityReq availabilityReq = new AvailabilityReq();
            try
            {
                SearchDBEntities searchDB = new SearchDBEntities();
                using (HotelBookingDBEntities db = new HotelBookingDBEntities())
                {

                    availabilityReq.HotelCode = HotelCode;

                    availabilityReq.PID = pid;
                    availabilityReq.Sid = sid;



                    var roomsData = searchDB.SearchRoomResults.Where(x => x.sID == sid && x.HotelCode == HotelCode && x.RoomCode == roomcode && x.ProviderId.ToString() == pid).ToList();
                    for (int i = 0; i < roomsData.Count; i++)
                    {
                        RoomAvailability room = new RoomAvailability();

                        room.RoomId = int.Parse(roomsData[i].RoomCode);
                        room.RoomRef = roomsData[i].RoomReference;
                        availabilityReq.Rooms.Add(room);
                        availabilityReq.TotalCost += roomsData[i].costPrice.Value;
                    }
                }

                return availabilityReq;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static List<CancellationRule> GetCancelFromDB(string sid, string HotelCode, string roomcode, string pid)
        {
            try
            {
                List<CancelPolicy> cancelPolicies = new List<CancelPolicy>();
                List<CancellationRule> cancellationRules = new List<CancellationRule>();
                SearchDBEntities searchDB = new SearchDBEntities();

                using (HotelBookingDBEntities db = new HotelBookingDBEntities())
                {
                    cancelPolicies = db.CancelPolicies.Where(a => a.Sid == sid && a.HotelCode == HotelCode && a.RoomCode.ToString() == roomcode && a.ProviderID == pid).ToList();
                }
                if (cancelPolicies.Count > 0)
                {
                    var searchData = searchDB.SearchCriterias.FirstOrDefault(x => x.sID == sid);
                    foreach (var item in cancelPolicies)
                    {
                        CancellationRule cancellation = new CancellationRule();
                        cancellation.FromDate = item.FromDate.ToString();
                        cancellation.Price = (double)item.SellPrice.Value;

                        cancellation.CanellationRuleText = searchData.language.ToLower() == "en" ? item.SellPrice.Value + " " + searchData.currency + " of the booking amount " + item.FromDate.Value.ToString("MMMM dd, yyyy") : item.FromDate.Value.ToString("MMMM dd, yyyy") + " إلى " + "" + searchData.currency + item.SellPrice;
                        cancellationRules.Add(cancellation);
                    }
                    return cancellationRules;
                }
                return new List<CancellationRule>();

            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("AvailabilityController/Errors/", "getcancelpolicy" + "INDAL" + sid, ex.InnerException?.Message, ex.Message + ex.StackTrace);

                return new List<CancellationRule>();
            }




        }
    }
}
