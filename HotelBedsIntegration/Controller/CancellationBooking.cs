using HotelBedsIntegration.Helper;
using HotelBedsIntegration.Models.Cancellation;
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
  public class Cancellation
    {
        public static async Task<CancellationBookingData> CancelBooking(string Ref, string BN)
        {
            try
            {
                //yyyy-mm-dd
                
                var hotelCancel = await BOOKINGCANCELLATION.CancelService(Ref, BN);

                if (hotelCancel == null)
                {
                    return null;
                }
              
                return hotelCancel;
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggingHelper.WriteToFile("SMLogs/CancelException", "CancelException" + BN, "CancelException", requestData);
                return null;
            }

        }
    }
}
