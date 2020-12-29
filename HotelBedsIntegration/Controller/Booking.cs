using HotelBedsIntegration.DBMapper;
using HotelBedsIntegration.Helper;
using HotelBedsIntegration.Models;
using HotelBedsIntegration.Services;
using Hotels.Common.Helpers;
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
        public static async Task<Models.BookingStatus> BookingRooms(BookingReq value, string SessionID,string BN)
        {
            BookingRes hotels = new BookingRes();
            BookingStatus booking = new BookingStatus();
            try
            {
                //yyyy-mm-dd
               
                BookingMapping.MapRequestToDB(value,SessionID,BN);
                hotels = await BookingService.BookingRoomsService(value,SessionID);

                if ( hotels == null)
                {
                    booking.status = 2;
                    return booking;
                }
                BookingMapping.MapResponseToDB(hotels, SessionID,BN);
                booking.status = 0;
                booking.booking = hotels.booking;
                return booking;
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggingHelper.WriteToFile("HBLogs/BookingException", "BookingException_" + SessionID, "BookingException", requestData);
                 if(hotels != null)
                {
                    booking.status = 1;
                    booking.booking = hotels.booking;
                    return booking;
                }
                 
                    booking.status = 2;
                    booking.booking = null;
                    return booking;

                 

            }

        }

    }
}
