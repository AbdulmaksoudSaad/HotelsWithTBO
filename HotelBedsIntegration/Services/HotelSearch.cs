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
   public class HotelSearchSer
    {
        public static async Task<RootObject> SearchHotelService(HBSearchData value ,string SessionID)
        {
            try
            {
           string path = ConfigurationSettings.AppSettings["HBSearchEndPonit"];

                var client = new HttpClient();
                var Data = new RootObject();
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
               //   apiKey = "c2yc2s4vhhxqkhssr4p5jma6";
                 //  Secret = "UeF5JXgdqH";

                // apiKey = "t4g8uy2qkvkb4wj5bc9q37k2";
                //  Secret = "SjxGKSA37w";
                // 3rd key: hqm7exuenr4bptxhkkpa453s and secret:dbE7FQP7mE 
                //  apiKey = "hqm7exuenr4bptxhkkpa453s";
                 //  Secret = "dbE7FQP7mE";
                // Compute the signature to be used in the API call (combined key + secret + timestamp in seconds)

            //    apiKey = "m758a2qayme2rk9upnu9qpp3";
            ///      Secret = "XPvvZeFeFJ";
                string signature;
                using (var sha = SHA256.Create())
                {
                    long ts = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds / 1000;
                    Console.WriteLine("Timestamp: " + ts);
                    var computedHash = sha.ComputeHash(Encoding.UTF8.GetBytes(apiKey + Secret + ts));
                    signature = BitConverter.ToString(computedHash).Replace("-", "");
                }

                //   var url = "https://api.test.hotelbeds.com/hotel-api/1.0/hotels";
                var url = path;
               
                client.DefaultRequestHeaders.Add("Api-Key", apiKey);
                client.DefaultRequestHeaders.Add("X-Signature", signature);
                LoggingHelper.WriteToFile("HBLogs/SearchRequests", "SearchRequest_" + SessionID, "SearchRequest", Newtonsoft.Json.JsonConvert.SerializeObject(value));
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                var response = client.PostAsJsonAsync(url, value).Result;


                stopWatch.Stop(); TimeSpan ts1 = stopWatch.Elapsed;
                if (response.IsSuccessStatusCode)
                {
                    Data = await response.Content.ReadAsAsync<RootObject>();
                    LoggingHelper.WriteToFile("HBLogs/SearchResponses", "SearchResponses_" + SessionID, "SearchResponses", "response time " + ts1.ToString() + Newtonsoft.Json.JsonConvert.SerializeObject(Data));

                    return Data;
                }
                else
                {
                    LoggingHelper.WriteToFile("HBLogs/SearchResponses", "SearchResponses_" + SessionID, "SearchResponses", "response time " + ts1.ToString() + Newtonsoft.Json.JsonConvert.SerializeObject(response));
                    return null;
                }
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggingHelper.WriteToFile("HBLogs/SearchException", "SearchException_" + SessionID, "SearchException", requestData);

                return null;
            }
        }
    }
}
