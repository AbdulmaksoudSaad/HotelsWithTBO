using Hotels.Common.Helpers;
using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.DAL
{
  public  class MailCLS
    {
        public async Task<MailObj>  SendMail(ConfirmationModel val)
        {
            try
            {
                var client = new HttpClient();
                string path = ConfigurationSettings.AppSettings["sendmail"];
                var url = path+"/api/HotelBookingEmail";
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                var response = client.PostAsJsonAsync(url, val).Result;
                stopWatch.Stop(); TimeSpan ts1 = stopWatch.Elapsed;
                if (response.IsSuccessStatusCode)
                {
                    var Data = await response.Content.ReadAsAsync<MailObj>();
                  LoggingHelper.WriteToFile("SendMailApi", "result", "inMailCLS", "response time " + ts1.ToString() +  Data.status);
                    return Data;
                }
                else
                {
                 LoggingHelper.WriteToFile("SendMailApi", "result", "inMailCLS", "response time " + ts1.ToString() +  response.StatusCode.ToString());

                    return null;
                }
            }catch(Exception ex)
            {

                return null;
            }
           
        }
    }
}
