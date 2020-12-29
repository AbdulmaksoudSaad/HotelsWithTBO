using HotelBedsIntegration.DBMapper;
using HotelBedsIntegration.Helper;
using HotelBedsIntegration.Models;
using HotelBedsIntegration.Models.Availability;
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
   public class CheckAvailability
    {
        public static async Task< Models.Availability.Hotel> checkAvailability(AvailabilityReq value, string SessionID)
        {
            try
            {
                //yyyy-mm-dd
                AvailabilityMapping.MapRequestToDB(value,SessionID);
                var hotels = await AvailabiltyService.CheckAvailabilityService(value,SessionID);

                if (hotels == null)
                {
                    return new Models.Availability.Hotel();
                }
                AvailabilityMapping.MapResponseToDB(hotels, SessionID);
                return hotels.hotel;
            }
            catch (Exception ex)
            {
                 var requestData = JsonConvert.SerializeObject(ex);

                LoggingHelper.WriteToFile("SMLogs/AvailabilityException", "AvailabilityException_" + SessionID, "AvailabilityException", requestData);
                return new Models.Availability.Hotel();
            }

        }
    }
}
