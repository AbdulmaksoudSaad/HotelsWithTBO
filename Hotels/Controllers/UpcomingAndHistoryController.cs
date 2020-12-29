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
    public class UpcomingAndHistoryController : ApiController
    {
        // GET: api/UpcomingAndHistory
        public IHttpActionResult Get(string mail)
        {
            LoggingHelper.WriteToFile("UpcomingAndHistoryController/InComingUpcomingAndHistoryData/", "UpcomingAndHistory" + "INController" + mail, "InComingData", "mail :" + mail + " , mail is" + mail);

            try
            {
                ConfirmationBLL confirmation = new ConfirmationBLL();
                var data = confirmation.GetUpcomingAndHistoryData(mail);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }catch (Exception ex)
            {
                LoggingHelper.WriteToFile("UpcomingAndHistoryController/ERRor/", "UpcomingAndHistory" + "INController" + mail, "InComingData",  ex.InnerException?.Message + ex.Message + ex.StackTrace);

                return BadRequest(ex.Message);
            }
        }

       
    }
}
