using IntegrationTotalStay.Model.Availability;
using IntegrationTotalStay.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTotalStay.Controller
{
   public  class CheckAvailability
    {
        public static PreBookResponse GetTSAvailability(PreBookRequest search,string sid)
        {
            search.LoginDetails.Login = "Citybookerstest";
            search.LoginDetails.Password = "xmltest";
            var res = CheckAvailabilty.getTsHotelAvailability(search);
            if (res != null)
            {
                return res;
            }
            return null;
        }
    }
}
 
