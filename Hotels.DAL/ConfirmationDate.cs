using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.DAL
{
    public class ConfirmationDate
    {

        public static ConfirmData GetAllConfirmationData(string Sid, string BN)
        {
            try
            {
                // get holder from (hotelBooking) and roomsandpaxes from (hotelBookingpax)
                HotelBookingDBEntities db = new HotelBookingDBEntities();
                SearchDBEntities SDB = new SearchDBEntities();
                ConfirmData confirmData = new ConfirmData();
                List<SearchRoomResult> Roomrates = new List<SearchRoomResult>();
                var BookingData = db.HotelsBookings.FirstOrDefault(s => s.SessionId == Sid && s.Booking_No == BN);
                var Paxes = db.HotelBookingPaxs.Where(a => a.SID == Sid && a.Booking_No == BN).ToList();
                var rooms = Paxes.Where(x=>x.PaxNo==1).Select(a => a.Room_No.Value.ToString()).ToList() ;
                // rate from searchroom result
                foreach (var item in rooms)
                {
                    Roomrates.Add( SDB.SearchRoomResults.Where(a =>/* rooms.Contains(a.RoomCode)*/a.RoomCode==item && a.sID == Sid && a.HotelCode == BookingData.Hotel_ID).FirstOrDefault());
                }
               
                confirmData.hotelsBooking = BookingData;
               var searchReq= SDB.SearchCriterias.FirstOrDefault(a => a.sID == Sid);
                var toDated = searchReq.dateTo.Value;
                var fromDated =  searchReq.dateFrom.Value;

                confirmData.fromDate =  Convert.ToDateTime(searchReq.dateFrom.Value).ToString("yyyy-MM-dd"); 
                confirmData.ToDate = Convert.ToDateTime(searchReq.dateTo.Value).ToString("yyyy-MM-dd");
                confirmData.Dur = Convert.ToInt32((toDated - fromDated).TotalDays).ToString() ;
                confirmData.PropertyTS = SDB.SearchHotelResults.FirstOrDefault(x => x.sID == Sid && x.HotelCode == BookingData.Hotel_ID).ProviderHotelCode;
                confirmData.Pid = BookingData.Provider_ID;
                int indx = 1;
                foreach (var item in Roomrates)
                {
                    RoomDTP roomDTP = new RoomDTP();
                    roomDTP.Rate = item.RoomReference;
                    roomDTP.RoomN = int.Parse(item.RoomCode);
                    roomDTP.roomResult = item;
                    roomDTP.bookingPaxes = Paxes.Where(a => a.Room_No.ToString() == item.RoomCode &&a.RoomRef==indx).ToList();
                    confirmData.Rooms.Add(roomDTP);
                    indx = indx + 1;
                }
                return confirmData;
            }catch(Exception ex)
            {
                LoggingHelper.WriteToFile("SaveBookingConfirmationController/Errors/", "ConfirmationDate GetConfirmationDate" + "DAL" +  Sid, ex.InnerException?.Message, ex.Message + ex.StackTrace);

                return null;
            }
        }
        public static string ChangeBookingStatus(string Sid, string BN,string status)
        {
            try
            {
                HotelBookingDBEntities db = new HotelBookingDBEntities();

                var BookingData = db.HotelsBookings.FirstOrDefault(s => s.SessionId == Sid && s.Booking_No == BN);

                BookingData.Booking_Status = status;
                var j = db.SaveChanges();
                if (j > 0)
                {
                    return "Done";
                }
                return null;
            }
            catch(Exception ex)
            {
                LoggingHelper.WriteToFile("SaveBookingConfirmationController/Errors/", "ConfirmationDate ChangeBookingStatus" + "DAL" + Sid, ex.InnerException?.Message, ex.Message + ex.StackTrace);

                return null;
            }
        }
        public static string saveBookingConfirmation(BookingConfirmationData data)
        {
            try
            {
                HotelBookingDBEntities db = new HotelBookingDBEntities();

                var BookingData = db.BookingConfirmationDatas.Add(data);

                var j = db.SaveChanges();
                if (j > 0)
                {
                    return "Done";
                }
                return null;
            }catch(Exception ex)
            {

                LoggingHelper.WriteToFile("SaveBookingConfirmationController/Errors/", "ConfirmationDate SaveConfirmedData" + "DAL" + data.SessionID, ex.InnerException?.Message, ex.Message + ex.StackTrace);

                return null;
            }
        }
        public static bool CheckBookingConfirmation(string BN, string sid)
        {
            HotelBookingDBEntities db = new HotelBookingDBEntities();
         var data=   db.BookingConfirmationDatas.FirstOrDefault(a => a.SessionID == sid && a.BookingNum == BN);
            if(data != null)
            {
                return true;
            }
            return false;
        }
    }
}
