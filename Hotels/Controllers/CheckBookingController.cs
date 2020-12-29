using Hotels.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hotels.Controllers
{
    public class CheckBookingController : ApiController
    {
        // GET: api/CheckBooking
        

        // GET: api/CheckBooking/5
        public IHttpActionResult Get(string bookingNum)
        {
            AdminSearchBLL checking = new AdminSearchBLL();
            var status= checking.CheckBooking(bookingNum);

            return Ok(new { StatusCode = status } );
        }

        // POST: api/CheckBooking
        public void Post([FromBody]string value)
        {
        }

        
    }
}
