using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.DAL
{
    public class CityRepo
    {
      public async void myfun()
        {
            var client = new HttpClient();
            var url = "http://192.168.1.91:7791/api/PaymentTransactionDetails?HG=";
            var response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var Data = await response.Content.ReadAsAsync<string>();
            }
        }
    }
}
