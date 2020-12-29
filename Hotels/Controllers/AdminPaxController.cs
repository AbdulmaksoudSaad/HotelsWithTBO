using Hotels.BLL;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hotels.Controllers
{
    public class AdminPaxController : ApiController
    {
        [Route("api/GetCustomer")]
        public IHttpActionResult Get()
        {
            try
            {
                AdminSearchBLL adminSearchBLL = new AdminSearchBLL();
              var Customers=  adminSearchBLL.GetAllCustomer( );
                return Ok(Customers);
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("AdminPaxController/ERROR/", "GetCustomer" + "INController" ,"Data", "bookin is "  + " and booking is"   + ex.Message + ex.StackTrace);

                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("api/EditPax")]
        public IHttpActionResult EditPaxes([FromBody]BookingPassenger val)
        {
            try
            {
                AdminSearchBLL adminSearchBLL = new AdminSearchBLL();
                adminSearchBLL.EditBookingPaxes(val);
                return Ok();
            }catch(Exception ex)
            {
                LoggingHelper.WriteToFile("AdminPaxController/ERROR/", "EditPax" + "INController" + val.bookingNo, "Data", "bookin is " + val.bookingNo + " and booking is" + val.bookingNo + ex.Message + ex.StackTrace);

                return BadRequest(ex.Message);
            }
        }

        // POST: api/AdminPax
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AdminPax/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AdminPax/5
        public void Delete(int id)
        {
        }
    }
}
