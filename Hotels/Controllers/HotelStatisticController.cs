using Hotels.BLL;
using Hotels.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hotels.Controllers
{
    public class HotelStatisticController : ApiController
    { 
        public IHttpActionResult Get(DateTime fromDate, DateTime toDate)
        {
           LoggingHelper.WriteToFile("StatisticController/Incoming/", "INController", "StatisticData", "from date" + fromDate + "to" + toDate);

            SearchstatisticData searchstatistic = new SearchstatisticData();
          var data=  searchstatistic.SearchStatisticTransaction(fromDate, toDate);
            return Ok(data);
        }

    }
}
