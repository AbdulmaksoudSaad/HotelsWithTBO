using HotelBedsIntegration.Helper;
using HotelBedsIntegration.Models;
using Hotels.Common.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
                string apiKey = ConfigurationSettings.AppSettings["HBToken"];
                string Secret = ConfigurationSettings.AppSettings["HBSecret"];

                if (apiKey == null)
                {
                    apiKey = "c2yc2s4vhhxqkhssr4p5jma6";
                }
                if (Secret == null)
                {
                    Secret = "UeF5JXgdqH";
                }
                string url = "";
        //        apiKey = "c2yc2s4vhhxqkhssr4p5jma6"; Secret = "UeF5JXgdqH";
                // Compute the signature to be used in the API call (combined key + secret + timestamp in seconds)
                string signature;
                using (var sha = SHA256.Create())
                {
                    long ts = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds / 1000;
                    Console.WriteLine("Timestamp: " + ts);
                    var computedHash = sha.ComputeHash(Encoding.UTF8.GetBytes(apiKey + Secret + ts));
                    signature = BitConverter.ToString(computedHash).Replace("-", "");
                }
                string path = ConfigurationSettings.AppSettings["HBBookingEndPonit"];

                LoggingHelper.WriteToFile("HBLogs/BookingRequests", "BookingRequest_" + SessionID, "BookingRequest", Newtonsoft.Json.JsonConvert.SerializeObject(value));
                if (value.paymentData == null)
                {
                    //   url = "https://api.test.hotelbeds.com/hotel-api/1.0/bookings";
                    url = path;
                }
                else
                {
                     path = ConfigurationSettings.AppSettings["HBBookingPayEndPonit"];
                    url = path;
                  //  url = "https://api-secure.test.hotelbeds.com/hotel-api/1.0/bookings";

                }

                client.DefaultRequestHeaders.Add("Api-Key", apiKey);
                client.DefaultRequestHeaders.Add("X-Signature", signature);
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                var response = client.PostAsJsonAsync(url, value).Result;

                stopWatch.Stop(); TimeSpan ts1 = stopWatch.Elapsed;

                if (response.IsSuccessStatusCode)
                {
                    DataBooking = await response.Content.ReadAsAsync<BookingRes>();

                    LoggingHelper.WriteToFile("HBLogs/BookingResponses", "BookingResponses_" + SessionID, "BookingResponses", "response time " + ts1.ToString() + Newtonsoft.Json.JsonConvert.SerializeObject(DataBooking));

                    return DataBooking;
                }
                else
                {
                    LoggingHelper.WriteToFile("HBLogs/BookingResponses", "BookingResponses_" + SessionID, "BookingResponses", "response time " + ts1.ToString() + Newtonsoft.Json.JsonConvert.SerializeObject(response));

                    return null;
                }
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggingHelper.WriteToFile("HBLogs/BookingException", "BookingException_" + SessionID, "BookingException", requestData);
                throw ex;
            }
        }
        }
    }
