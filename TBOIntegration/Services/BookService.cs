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
    public static class BookService
    {
        public static HotelBookResponse Booking(HotelBookRequest req , string SID)
        {
            var UName = ConfigurationSettings.AppSettings["TBOUserName"];
            var UPass = ConfigurationSettings.AppSettings["TBOPassword"];

            IHotelService proxy = TBOCredentials.CreateProxy();

            req.Credentials = new AuthenticationData()
            {
                UserName = UName,
                Password = UPass
            };
            req.RestrictDuplicateBooking = true;
            ProviderLogger.LogHotelBookReq(req, SID);
            HotelBookResponse resp = new HotelBookResponse();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            resp = proxy.HotelBook(req);
            stopwatch.Stop();
            TimeSpan ts1 = stopwatch.Elapsed;
            TimeSpan baseInterval = new TimeSpan(0, 0, 60);
            if (ts1 > baseInterval)
            {
                BookDetail.DetailService(req.ClientReferenceNumber);
            }
            ProviderLogger.LogHotelBookResp(resp, SID);

            return resp;
        }

        
    }
}
