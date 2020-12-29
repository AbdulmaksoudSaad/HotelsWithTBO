using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBO.WSDL.hotelServiceRef;
using TBOIntegration.Helper;

namespace TBOIntegration.Services
{
    public static class SearchService
    {
        public static HotelSearchResponse Search(HotelSearchRequest req ,string SID)
        {

            var UName = ConfigurationSettings.AppSettings["TBOUserName"];
            var UPass = ConfigurationSettings.AppSettings["TBOPassword"];

            IHotelService proxy = TBOCredentials.CreateProxy();
            req.Credentials = new AuthenticationData()
            {
                UserName = UName,
                Password = UPass
            };

            ProviderLogger.LogSearchProviderReq(req, SID);

            HotelSearchResponse resp = new HotelSearchResponse();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            resp =  proxy.HotelSearch(req);


            stopWatch.Stop();
            TimeSpan ts1 = stopWatch.Elapsed;

            ProviderLogger.LogSearchProviderRsp(resp, SID, ts1.ToString());

            return resp;
        }
    }
}
