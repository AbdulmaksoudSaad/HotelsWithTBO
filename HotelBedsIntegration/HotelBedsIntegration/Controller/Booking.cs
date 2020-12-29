using HotelBedsIntegration.DBMapper;
using HotelBedsIntegration.Helper;
using HotelBedsIntegration.Models;
using HotelBedsIntegration.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBedsIntegration.Controller
{
  public  class BookingFlow
    {
        public static async Task<Models.Booking>  BookingRooms(BookingReq value, string SessionID)
        {
            try
            {
                //yyyy-mm-dd
                BookingMapping.MapRequestToDB(value,SessionID);
                var hotels = await BookingService.BookingRoomsService(value,SessionID);

                if ( hotels == null)
                {
                    return new Models.Booking();
                }
                BookingMapping.MapResponseToDB(hotels, SessionID);
                return hotels.booking;
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LogData.WriteToFile("c:/HotelsB2C/Logs/HBLogs/BookingException", "BookingException_" + SessionID, "BookingException", requestData);
                return new Models. Booking();
            }

        }

    }
}
