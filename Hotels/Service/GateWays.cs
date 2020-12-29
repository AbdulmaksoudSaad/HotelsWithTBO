using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Hotels.Service
{
    public class GateWays
    {
        public static async Task<List<string>> GetPaymentGatewaysAsync(string curr)
        {
            try
            {
                 string path = ConfigurationSettings.AppSettings["selectedGateway"];
                Stopwatch stopWatch = new Stopwatch();
                var client = new HttpClient();
                var url = path+"/api/GetSelectedGateways?Currency=" + curr;
                stopWatch.Start();
                
               
                var response = client.GetAsync(url).Result;
                stopWatch.Stop(); TimeSpan ts = stopWatch.Elapsed;
                LoggingHelper.WriteToFile("GetSelectedGateways/", "InGateway", "ResponseData", "response time " + ts.ToString() + JsonConvert.SerializeObject(response));

                if (response.IsSuccessStatusCode)
                {
                     
                    var Data = await response.Content.ReadAsAsync<List<string>>();
                    return Data;
                }
                else
                {
                    
                    return new List<string>();
                }
            }
            catch (Exception ex)
            {
                 // LogData.WriteToFile("c:/HotelsB2C/Logs/HBLogs/SearchException", "SearchException_" + sid, "SearchException", requestData);
                return new List<string>();
            }
        }
        // second call to sales rules ////////////////////////////////////////////////////////////////
        public static async Task<SalesRules> GetSaleRuleForGateAsync(string pos)
        {
            try
            {
                  string path = ConfigurationSettings.AppSettings["SaleRuleUrl"];
                var client = new HttpClient();
                var url = path+"/api/salesRules?category=hotel&pos=" + pos + "&service=Payment Gateway";
                 
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                var response = client.GetAsync(url).Result;
                stopWatch.Stop(); TimeSpan ts = stopWatch.Elapsed;
                LoggingHelper.WriteToFile("GetSaleRuleForGateWay/", "InGateway" , "Response Data For Payment Gateway", "response time " + ts.ToString()+ " Status " + JsonConvert.SerializeObject(response));

                if (response.IsSuccessStatusCode)
                {
                    var Data = await response.Content.ReadAsAsync<SalesRules>();
                    return Data;
                }
                else
                {
                  
                    //    LogData.WriteToFile("c:/HotelsB2C/Logs/HBLogs/SearchResponses", "SearchResponses_" + SessionID, "SearchResponses", Newtonsoft.Json.JsonConvert.SerializeObject(response));
                    return null;
                }
            }
            catch (Exception ex)
            {
                //var requestData = JsonConvert.SerializeObject(ex);
                // LogData.WriteToFile("c:/HotelsB2C/Logs/HBLogs/SearchException", "SearchException_" + sid, "SearchException", requestData);
                return null;
            }
        }
    }
}