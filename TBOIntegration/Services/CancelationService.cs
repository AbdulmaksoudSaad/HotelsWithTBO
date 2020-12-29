using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBO.WSDL.hotelServiceRef;
using TBOIntegration.Helper;

namespace TBOIntegration.Services
{
    public static class CancelationService
    {
        public static HotelCancelResponse Cancel(string ConfirmNo)
        {
            var UName = ConfigurationSettings.AppSettings["TBOUserName"];
            var UPass = ConfigurationSettings.AppSettings["TBOPassword"];

            HotelCancelRequest req = new HotelCancelRequest
            {
                Credentials = new AuthenticationData()
                {
                    UserName = UName,
                    Password = UPass
                },
                //RequestType = "HotelCancel",
                ConfirmationNo = ConfirmNo,
                Remarks = "test cancel"
            };

            IHotelService proxy = TBOCredentials.CreateProxy();
            var sid = new Guid();
            ProviderLogger.LogHotelCancelReq(req, sid.ToString());
            HotelCancelResponse resp = new HotelCancelResponse();
            resp = proxy.HotelCancel(req);
            ProviderLogger.LogHotelCancelResp(resp, sid.ToString());
            return resp;
        }
    }
}
