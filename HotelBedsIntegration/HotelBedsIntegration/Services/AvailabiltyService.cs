using HotelBedsIntegration.Helper;
using HotelBedsIntegration.Models.Availability;
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
    class AvailabiltyService
    {
        public static async Task<AvailabilityRes> CheckAvailabilityService(AvailabilityReq value, string SessionID)
        {
            try { 
            var client = new HttpClient();
            var Data = new AvailabilityRes();
            const string apiKey = "c2yc2s4vhhxqkhssr4p5jma6";
            const string Secret = "UeF5JXgdqH";

            // Compute the signature to be used in the API call (combined key + secret + timestamp in seconds)
            string signature;
            using (var sha = SHA256.Create())
            {
                long ts = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds / 1000;
                Console.WriteLine("Timestamp: " + ts);
                var computedHash = sha.ComputeHash(Encoding.UTF8.GetBytes(apiKey + Secret + ts));
                signature = BitConverter.ToString(computedHash).Replace("-", "");
            }
            LogData.WriteToFile("c:/HotelsB2C/Logs/HBLogs/AvailabilityRequests", "AvailabilityRequest_" + SessionID, "AvailabilityRequest", Newtonsoft.Json.JsonConvert.SerializeObject(value));


            var url = "https://api.test.hotelbeds.com/hotel-api/1.0/checkrates";
            client.DefaultRequestHeaders.Add("Api-Key", "c2yc2s4vhhxqkhssr4p5jma6");
            client.DefaultRequestHeaders.Add("X-Signature", signature);

            var response = client.PostAsJsonAsync(url, value).Result;



            if (response.IsSuccessStatusCode)
            {
                Data = await response.Content.ReadAsAsync<AvailabilityRes>();
                LogData.WriteToFile("c:/HotelsB2C/Logs/HBLogs/AvailabilityResponses", "AvailabilityResponses_" + SessionID, "AvailabilityResponses", Newtonsoft.Json.JsonConvert.SerializeObject(Data));

                return Data;
            }
            else
            {
                LogData.WriteToFile("c:/HotelsB2C/Logs/HBLogs/AvailabilityResponses", "AvailabilityResponses_" + SessionID, "AvailabilityResponses", Newtonsoft.Json.JsonConvert.SerializeObject(response));

                return null;
            }
        }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LogData.WriteToFile("c:/HotelsB2C/Logs/HBLogs/AvailabilityException", "AvailabilityException_" + SessionID, "AvailabilityException", requestData);
                return null;
            }

        }
        }
    }
