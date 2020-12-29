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
    public static class AvailablityPricingService
    {
        public static AvailabilityAndPricingResponse PricingService(AvailabilityAndPricingRequest req, string SID)
        {
            var UName = ConfigurationSettings.AppSettings["TBOUserName"];
            var UPass = ConfigurationSettings.AppSettings["TBOPassword"];

            req.Credentials = new AuthenticationData()
            {
                UserName = UName,
                Password = UPass
            };

            IHotelService proxy = TBOCredentials.CreateProxy();
            ProviderLogger.LogAvailablityPricingReq(req, SID);
            AvailabilityAndPricingResponse resp = new AvailabilityAndPricingResponse();
            resp = proxy.AvailabilityAndPricing(req);
            ProviderLogger.LogAvailablityPricingProviderRsp(resp, SID);
            return resp;
        }
    }
}
