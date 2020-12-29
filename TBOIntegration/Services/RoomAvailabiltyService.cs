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
    public static class RoomAvailabiltyService
    {
        public static HotelRoomAvailabilityResponse Availabilty(string SessionId, int ResIndex, string HotelCode, string SID)
        {

            var UName = ConfigurationSettings.AppSettings["TBOUserName"];
            var UPass = ConfigurationSettings.AppSettings["TBOPassword"];
            HotelRoomAvailabilityRequest req = new HotelRoomAvailabilityRequest
            {
                SessionId = SessionId,
                ResultIndex = ResIndex,
                HotelCode = HotelCode,
                IsCancellationPolicyRequired = true, // already come in Pricing
                Credentials = new AuthenticationData()
                {
                    UserName = UName,
                    Password = UPass
                }
            };

            ProviderLogger.LogRoomAvailabiltyReq(req, SID);

            IHotelService proxy = TBOCredentials.CreateProxy();
            HotelRoomAvailabilityResponse resp = new HotelRoomAvailabilityResponse();
            resp = proxy.AvailableHotelRooms(req);
            ProviderLogger.LogRoomAvailabilityProviderRsp(resp, SID);

            return resp;



        }
    }
}
