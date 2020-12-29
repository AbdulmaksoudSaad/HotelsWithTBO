using Hotels.Common.Models;
using Hotels.ProviderManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.ServOrch
{
   public class CheckAvailabillity
    {
        public static CheckAvailabilityResult GetCheckAvailabilityResult(Hotels.Common.Models.AvailabilityReq availability ,string curr,string src)
        {
            CheckAvailabilityResult availabilityResult = new CheckAvailabilityResult();
          availabilityResult  =AvailabilityManager.checkAvailabilityManager(availability ,curr,src);

            return availabilityResult;
        }
    }
}
