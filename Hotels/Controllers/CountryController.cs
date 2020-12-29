using Hotels.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hotels.Controllers
{
    public class CountryController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            SearchResultData db = new SearchResultData();
            var result = db.GetAllCountries();
            return Ok(result);
        }


    }
}
