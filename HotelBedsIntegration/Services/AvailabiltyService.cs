using HotelBedsIntegration.Helper;
using HotelBedsIntegration.Models.Availability;
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
    class AvailabiltyService
    {
        public static async Task<AvailabilityRes> CheckAvailabilityService(AvailabilityReq value, string SessionID)
        {
            try { 
            var client = new HttpClient();
            var Data = new AvailabilityRes();
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
          //      apiKey = "c2yc2s4vhhxqkhssr4p5jma6";
         //   Secret = "UeF5JXgdqH";
                // Compute the signature to be used in the API call (combined key + secret + timestamp in seconds)
                string signature;
            using (var sha = SHA256.Create())
            {
                long ts = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds / 1000;
                Console.WriteLine("Timestamp: " + ts);
                var computedHash = sha.ComputeHash(Encoding.UTF8.GetBytes(apiKey + Secret + ts));
                signature = BitConverter.ToString(computedHash).Replace("-", "");
            }
                LoggingHelper.WriteToFile("HBLogs/AvailabilityRequests", "AvailabilityRequest_" + SessionID, "AvailabilityRequest", Newtonsoft.Json.JsonConvert.SerializeObject(value));
                string path = ConfigurationSettings.AppSettings["HBAvailabilityEndPonit"];
                var url = path;

            //    var url = "https://api.test.hotelbeds.com/hotel-api/1.0/checkrates";
                client.DefaultRequestHeaders.Add("Api-Key", apiKey);
            client.DefaultRequestHeaders.Add("X-Signature", signature);
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                var response = client.PostAsJsonAsync(url, value).Result;
                    stopWatch.Stop(); TimeSpan ts1 = stopWatch.Elapsed;
                if (response.IsSuccessStatusCode)
            {
                Data = await response.Content.ReadAsAsync<AvailabilityRes>();
                    LoggingHelper.WriteToFile("HBLogs/AvailabilityResponses", "AvailabilityResponses_" + SessionID, "AvailabilityResponses", "response time " + ts1.ToString() + Newtonsoft.Json.JsonConvert.SerializeObject(Data));

                return Data;
            }
            else
            {
                    LoggingHelper.WriteToFile("HBLogs/AvailabilityResponses", "AvailabilityResponses_" + SessionID, "AvailabilityResponses", "response time " + ts1.ToString() + Newtonsoft.Json.JsonConvert.SerializeObject(response));

                return null;
            }
        }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggingHelper.WriteToFile("HBLogs/AvailabilityException", "AvailabilityException_" + SessionID, "AvailabilityException", requestData);
                return null;
            }

        }
        }
    }
