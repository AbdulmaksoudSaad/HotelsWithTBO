using Hotels.ProviderManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hotels.Controllers
{
    public class CancellationBookingController : ApiController
    {
      

        // GET: api/CancellationBooking/5
        public IHttpActionResult Get(string bookingNumber)
        {
            CancellationManager cancellation = new CancellationManager();
            cancellation.CancelForAllProviders(bookingNumber);
            return Ok(cancellation.cancelData);
        }

        // POST: api/CancellationBooking
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CancellationBooking/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CancellationBooking/5
        public void Delete(int id)
        {
        }
    }
}
