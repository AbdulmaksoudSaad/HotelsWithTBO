using Hotels.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hotels.Controllers
{
    public class PaymentGatewayController : ApiController
    {
         

        // GET: api/PaymentGateway/5
        public IHttpActionResult Get(string bookingnum)
        {
            try
            {
                GetGateways payGateway = new GetGateways();
                var data = payGateway.GetPayGateWay(bookingnum );
                if (data == null)
                {
                    return Ok(new List<Common.Models.PaymentGateWay>());
                }
                return Ok(data);
            }catch(Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

         
    }
}
