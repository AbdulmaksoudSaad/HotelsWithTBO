using Hotels.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hotels.Controllers
{
    public class AllCityController : ApiController
    {
        public IHttpActionResult Get( )//citysearchterm  
        {
            SearchResultData db = new SearchResultData();
            var result = db.AllCities();
            if (result.Count != null)
            {
                return Ok(result);
            }
            return Ok("NO Cities Found");

        }
    }
}
