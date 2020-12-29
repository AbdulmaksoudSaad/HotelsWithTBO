using Newtonsoft.Json;
using SMYROOMS.DB;
using SMYROOMS.Helpers;
using SMYROOMS.Model;
using SMYROOMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Controller
{
  public  class SMCLSBooking
    {
        public static async Task<Bookingback> BookingHotel(HotelBookInput Value,string SessionID)
        {
            try
            {
                Bookingback BackData = new Bookingback();
                BookingService bookingService = new BookingService();
                var data = await bookingService.HotelBooking(Value,SessionID);
                var OutPutData = data.Data.hotelX.book.booking;
                 BookingMapper serMap = new BookingMapper();
                if (data.Data.hotelX.book.errors == null && data.Data.hotelX.book.warnings == null && data.Data.hotelX.book.booking != null)
                {
                    BackData = serMap.MapResponseofBooking(OutPutData,SessionID);
                    BookingDataEntry db = new BookingDataEntry();
                    db.SaveSMRBooking(BackData, SessionID);
                }
                else
                    return new Bookingback();
                return BackData;
            }
            catch(Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggerHelper.WriteToFile("c:/HotelsB2C/Logs/SMLogs/BookingException", "BookingException_" + SessionID, "BookingException", requestData);
                return new Bookingback();
            }
        }
    }
}
