using HotelBedsIntegration.Helper;
using HotelBedsIntegration.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HotelBedsIntegration.Services
{
   public class BookingService
    {
        public static async Task<BookingRes> BookingRoomsService(BookingReq value ,string SessionID)
        {
            try
            {

                var client = new HttpClient();
                BookingRes DataBooking = new BookingRes();
                const string apiKey = "c2yc2s4vhhxqkhssr4p5jma6";
                const string Secret = "UeF5JXgdqH";
                string url = "";

                // Compute the signature to be used in the API call (combined key + secret + timestamp in seconds)
                string signature;
                using (var sha = SHA256.Create())
                {
                    long ts = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds / 1000;
                    Console.WriteLine("Timestamp: " + ts);
                    var computedHash = sha.ComputeHash(Encoding.UTF8.GetBytes(apiKey + Secret + ts));
                    signature = BitConverter.ToString(computedHash).Replace("-", "");
                }

                LogData.WriteToFile("c:/HotelsB2C/Logs/HBLogs/BookingRequests", "BookingRequest_" + SessionID, "BookingRequest", Newtonsoft.Json.JsonConvert.SerializeObject(value));
                if (value.paymentData == null)
                {
                    url = "https://api.test.hotelbeds.com/hotel-api/1.0/bookings";
                }
                else
                {
                    url = "https://api-secure.test.hotelbeds.com/hotel-api/1.0/bookings";

                }
                client.DefaultRequestHeaders.Add("Api-Key", "c2yc2s4vhhxqkhssr4p5jma6");
                client.DefaultRequestHeaders.Add("X-Signature", signature);

                var response = client.PostAsJsonAsync(url, value).Result;



                if (response.IsSuccessStatusCode)
                {
                    DataBooking = await response.Content.ReadAsAsync<BookingRes>();

                    LogData.WriteToFile("c:/HotelsB2C/Logs/HBLogs/BookingResponses", "BookingResponses_" + SessionID, "BookingResponses", Newtonsoft.Json.JsonConvert.SerializeObject(DataBooking));

                    return DataBooking;
                }
                else
                {
                    LogData.WriteToFile("c:/HotelsB2C/Logs/HBLogs/BookingResponses", "BookingResponses_" + SessionID, "BookingResponses", Newtonsoft.Json.JsonConvert.SerializeObject(response));

                    return null;
                }
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LogData.WriteExceptionToFile("c:/HotelsB2C/Logs/HBLogs/BookingException", "BookingException_" + SessionID, "BookingException", requestData);
                return null;
            }
        }
        }
    }
