using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.BLL.Service
{
  public  class GatewaySer
    {
        //http://46.166.160.65:7080/api/GetSelectedGateways?Currency=KWD
        public static async Task<List<string>> GetPaymentTransactionAsync(string  curr)
        {
            try
            {
                // string path = ConfigurationSettings.AppSettings["HBToken"];
                var client = new HttpClient();
                var url = "http://46.166.160.65:7080/api/GetSelectedGateways?Currency="+curr;
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                   
                    return new List<string>();
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
