using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TBO.WSDL.hotelServiceRef;

namespace TBOIntegration.Services
{
    class TBOCredentials
    {
        public static IHotelService CreateProxy()
        {
            /*"https://api.tbotechnology.in/hotelapi_v7/hotelservice.svc"*/

            var Url = ConfigurationSettings.AppSettings["TBOEndPoint"];
            ChannelFactory<IHotelService> factory =
             new ChannelFactory<IHotelService>("WSHttpBinding_IHotelService", new EndpointAddress(Url));
            IHotelService proxy = factory.CreateChannel();
            return proxy;
        }
    }
}
