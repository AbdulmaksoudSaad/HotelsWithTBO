using Hotels.BLL;
using Hotels.Common.Helpers;
using Hotels.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hotels.Controllers
{
    public class PaymentViewController : ApiController
    {
      
        // GET: api/PaymentView/5
        public IHttpActionResult Get(string bookingnum ,string sid ,string ip,string Pos,string lang ,string NotificationTok)
        {
            LoggingHelper.WriteToFile("PaymentViewController/PaymentViewData/", "PaymentViewController" + "INController" + sid, "InComingData", "sid" + sid + " , BookingNum" + bookingnum +",pos is"+Pos+", ip "+ip+"lang is"+lang+ "NotificationTok is"+ NotificationTok);

            try
            {
                if (lang.ToLower() != "ar")
                {
                    lang="en";
                }
                // fill paylink data 
                GetPayData payData = new GetPayData();
                var data = payData.GetPayLinkRequest(bookingnum, sid, ip, Pos,NotificationTok);
                //call service 
                if (data != null)
                {
                    data.PaymentAuthData.SuccessUrl = data.PaymentAuthData.SuccessUrl + "&LangCode=" + lang.ToLower();
                    data.PaymentAuthData.FailUrl = data.PaymentAuthData.FailUrl + "&LangCode=" + lang.ToLower();

                    var link = Pay.GetPaymentLinkAsync(data).Result;
                    if (link != null)
                    {
                        link = link + "&LangCode=" + lang.ToLower();//+ "&Currency="+data.PaymentFareDetails.CustomerPaymentCurrency;
                        return Ok(new { Link = link });
                    }
                    else
                        return Ok("No Payment Link Found");

                }
                return Ok("No Payment Data Found");
            }catch(Exception ex)
            {
                LoggingHelper.WriteToFile("PaymentViewController/ERRor/", "PaymentView" + "INController" + sid, "InComingData", ex.InnerException?.Message + ex.Message + ex.StackTrace);

                return BadRequest(ex.Message);
            }
        }

       
    }
}
