using Hotels.BLL;
using Hotels.Common.Helpers;
using Hotels.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hotels.Controllers
{
    public class ConfirmationController : ApiController
    {
       

        // GET: api/Confirmation/5  last api in flow 
        public IHttpActionResult Get(string sid,string bookingNum)
        {
            LoggingHelper.WriteToFile("ConfirmationController/DataIncoming/", "ConfirmationController" + "INController" +  sid, "ConfirmData", "Sid is "+sid +" and Booking is" +bookingNum );
            try
            {
                ConfirmationBLL confirmation = new ConfirmationBLL();
                var data = confirmation.GetConfirmationData(sid, bookingNum);
                if (data == null)
                {

                    return Ok("No Data Found");
                }
        /*    MailCLS mail = new MailCLS();
         var mailVal=    mail.SendMail(data).Result;
                if(mailVal != null && mailVal.status=="Success")
                {
                    data.salesInvoicePDF = mailVal.salesInvoicePDF;
                    data.confirmationPDF = mailVal.confirmationPDF;
                    data.mailStatus = "Success";
                }
                else
                {
                    var mailVal2 = mail.SendMail(data).Result;
                    if (mailVal2 != null && mailVal2.status == "Success")
                    {
                        data.salesInvoicePDF = mailVal2.salesInvoicePDF;
                        data.confirmationPDF = mailVal2.confirmationPDF;
                        data.mailStatus = "Success";
                    }
                }
                */
                
                return Ok(data);
            }
            catch(Exception ex)
            {
                LoggingHelper.WriteToFile("ConfirmationController/ERROR/", "ConfirmationController" + "INController" + sid, "ConfirmData", "Sid is " + sid + " and Booking is" + bookingNum+ ex.InnerException?.Message+ ex.Message + ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        
    }
}
