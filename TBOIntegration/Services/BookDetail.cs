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
    public static class BookDetail
    {
        public static HotelBookingDetailResponse DetailService(string CRefNo)
        {
            var UName = ConfigurationSettings.AppSettings["TBOUserName"];
            var UPass = ConfigurationSettings.AppSettings["TBOPassword"];

            IHotelService proxy = TBOCredentials.CreateProxy();
            HotelBookingDetailRequest req = new HotelBookingDetailRequest
            {
                Credentials = new AuthenticationData()
                {
                    UserName = UName,
                    Password = UPass
                },
                //BookingId = BookId,
                //ConfirmationNo = ConfirmNo,
                ClientReferenceNumber= CRefNo
            };

            var sid = new Guid();
            ProviderLogger.LogBookingDetailReq(req, sid.ToString());
            HotelBookingDetailResponse resp = new HotelBookingDetailResponse();
            resp = proxy.HotelBookingDetail(req);
            ProviderLogger.LogBookingDetailRsp(resp, sid.ToString());

            return resp;
        }
    }
}
