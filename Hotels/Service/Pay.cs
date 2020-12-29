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
    public class Pay
    {
        // call payment Transaction 
        public static async Task<PaymentTransaction> GetPaymentTransactionAsync(string tok,string BN,string sid)
        {
            try
            {
                string path = ConfigurationSettings.AppSettings["PayTransactionDomain"];
                var client = new HttpClient();
                
                var url = path+"/api/PaymentTransactionDetails?HG="+BN+"&tok="+tok+"&SID="+sid;
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                var response = client.GetAsync(url).Result;
                stopWatch.Stop(); TimeSpan ts = stopWatch.Elapsed;
                LoggingHelper.WriteToFile("ConfirmHotelStatus/PaymentTransactionResponse/", "PaymentTransaction" + "payCls" +sid, "ResponseData","response time "+ ts.ToString()+JsonConvert.SerializeObject(response));

                 
                if (response.IsSuccessStatusCode)
                {

                  var  Data = await response.Content.ReadAsAsync<PaymentTransaction>();
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
                var requestData = JsonConvert.SerializeObject(ex);
                LoggingHelper.WriteToFile("ConfirmHotelStatus/Error/", "PaymentTransaction" + "INPayapi" + sid, "InComingData", ex.InnerException?.Message + ex.Message + ex.StackTrace);

                return null;
            }
        }
        public static async Task<dynamic> GetPaymentLinkAsync(PayLinkRequest payLinkData )
        {
            try
            {
                LoggingHelper.WriteToFile("PaymentViewController/PaymentViewRequest/", "PaymentView" + "payCls" + payLinkData.BookingInfo.SearchID, "RequestData", JsonConvert.SerializeObject(payLinkData));

                string path = ConfigurationSettings.AppSettings["PayLinkDomain"];
                var client = new HttpClient();
                var url = path+"/api/paymentuilink";
              
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                var response = client.PostAsJsonAsync(url,payLinkData).Result;
                stopWatch.Stop(); TimeSpan ts = stopWatch.Elapsed;
                
                LoggingHelper.WriteToFile("PaymentViewController/PaymentViewResponse/", "PaymentView" + "payCls" + payLinkData.BookingInfo.SearchID, "ResponseData", "response time " + ts.ToString() + JsonConvert.SerializeObject(response));

                if (response.IsSuccessStatusCode)
                {

                    var Data = await response.Content.ReadAsAsync<dynamic>();
                    return Data;
                }
                else
                {
                    
                    return null;
                }
            }
            catch (Exception ex)
            {
              
                LoggingHelper.WriteToFile("PaymentViewController/ERRor/", "PaymentView" + "INPayapi" + payLinkData.BookingInfo.SearchID, "InComingData", ex.InnerException?.Message + ex.Message + ex.StackTrace);

                 
                return null;
            }
        }
    }
}