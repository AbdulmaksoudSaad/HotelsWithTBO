using Hotels.BLL;
using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Hotels.DAL;
using Hotels.Service;
using Hotels.ServOrch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hotels.Controllers
{
    public class HotelBookingController : ApiController
    {
        [HttpOptions]
        public IHttpActionResult Post()
        {
            return Ok();
        }
        [HttpPost]
        // POST: api/HotelBooking
        public IHttpActionResult Post([FromBody]CheckOutData value)
        {
            LoggingHelper.WriteToFile("SaveBookingController/Request/", "SaveController" + "INController" + value.Sid,"RequestObject" , JsonConvert.SerializeObject(value));

            try
            {
                if (ModelState.IsValid)
                {
                    var BookingNum = BookingManager.GetBookingNumberAndManageBooking(value);
                    if (BookingNum != null)
                    {
                       // var myAnonymousType = new { bookingNum= BookingNum};
                        return Ok(new { bookingNum = BookingNum  });
                    }
                    return Ok("Booking Number not Created");
                }
                return BadRequest(ModelState);
            }catch(Exception ex)
            {
                LoggingHelper.WriteToFile("SaveBookingController/Errors/", "SaveController" + "INController"+value.Sid, ex.InnerException?.Message, ex.Message + ex.StackTrace);

                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("Api/ConfirmHotel")]
        public IHttpActionResult HotelsConformation(string SID ,string BookingNum)
        {
            try
            {
                LoggingHelper.WriteToFile("SaveBookingConfirmationController/InComingConfirmationData/", "ConfirmedproviderController" + "INController" + SID, "InComingData",  "sid"+SID +" , BookingNum"+BookingNum);
                ValidatingPaymentModel validating = new ValidatingPaymentModel();
                 
                var Confirm = ConfirmationBooking.GetConfirmationBooking(SID, BookingNum);
                // if(Confirm)
                if (Confirm.status == 0)
                    validating.Status = 0;
                else if (Confirm.status == 1)
                    validating.Status = 1;
                else if(Confirm.status==2)
                    validating.Status = 2;
                
                return Ok(validating);
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("SaveBookingConfirmationController/Errors/", "ConfirmedController" + "INController" + SID, ex.InnerException?.Message, ex.Message + ex.StackTrace);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Api/ConfirmHotelStatus")]
        public IHttpActionResult HotelsConformation(string sid ,string bookingNum ,string tok)
        {
            ValidatingPaymentModel validating;
            validating = null;
            NotificationCls notification = new NotificationCls();
            BookingStatus value = new BookingStatus();
            MailObj mailVal = new MailObj();
            string Notstatus = "";string Mess = "";
            //[FromBody] BookingStatus value
            try
            {
                HotelBookingCls hotelBooking = new HotelBookingCls();
                value.BookingNum = bookingNum;
                value.Sid = sid;
                value.Status = "";
                LoggingHelper.WriteToFile("SaveBookingConfirmationController/ConfirmHotelStatusapi/", "ConfirmedstatusController" + "INController" + value.Sid, "InComingData", "sid" + value.Sid + " , BookingNum" + value.BookingNum);
                // call payment Transaction
                var payResult = Pay.GetPaymentTransactionAsync(tok, bookingNum, sid).Result;
                if (payResult != null)
                {
                    if (payResult.HGResponseCode == "100" || payResult.HGResponseCode == "000")
                    {
                        validating = new ValidatingPaymentModel();
                        value.Status = payResult.FraudStatus;
                        if (payResult.FraudStatus.ToLower() == "approved")
                        {
                            hotelBooking.addBookingStatus("Payment Done", bookingNum);
                            validating.Status = 0;
                        }
                        else if(payResult.FraudStatus.ToLower() == "refused")
                        {
                            hotelBooking.addBookingStatus("Payment Refused", bookingNum);
                            validating.Status = 2;
                        }
                        else if (payResult.FraudStatus.ToLower() == "suspected")
                        {
                            hotelBooking.addBookingStatus("Payment Suspected", bookingNum);
                            validating.Status = 2;
                        }
                    }
                    else
                    {
                        hotelBooking.addBookingStatus("Payment Failed", bookingNum);
                        validating = new ValidatingPaymentModel();
                        validating.Status = 1;
                        Mess = "Some thing wrong Please Search again";

                        notification.sendnotification(value, bookingNum, validating.Status.ToString(), sid, Mess, mailVal);
                        return Ok(validating);
                    }
                    // add status
                    if (validating.Status == 0)
                    { 
                  
                    var Confirm = ConfirmationBooking.GetConfirmationBooking(sid, bookingNum);
                        // if(Confirm)
                        if (Confirm.status == 0)
                        {

                            validating.Status = 0;
                            value.Status = "Booking Confirmed";
                            hotelBooking.addBookingStatus("Booking Confirmed", bookingNum);
                            ConfirmationBLL confirmation = new ConfirmationBLL();
                            var data = confirmation.GetConfirmationData(sid, bookingNum);
                            LoggingHelper.WriteToFile("SendMailobject/",  "INController" + value.Sid, "InComingData", JsonConvert.SerializeObject(data));

                            MailCLS mail = new MailCLS();
                             mailVal = mail.SendMail(data).Result;
                        }
                        else if (Confirm.status == 1)
                        {
                            validating.Status = 2;
                            value.Status = "Missing";
                            hotelBooking.addBookingStatus("Missing", bookingNum);
                            Mess = "Some thing wrong Please Call Our Call Center";
                        }
                        else if (Confirm.status == 2)
                        {
                            validating.Status = 2;
                            value.Status = "Booking Not Confirmed";
                            hotelBooking.addBookingStatus("Booking Not Confirmed", bookingNum);
                            Mess = "Some thing wrong Please Call Our Call Center";
                        }
                        else if (Confirm.status == 3)
                        {
                            validating.Status = 0;
                            value.Status = "Booking Confirmed";
                            
                        }
                        // var result = hotelBooking.ChangeBookingstatus(value);

                        notification.sendnotification(value, bookingNum, sid,validating.Status.ToString() , Mess, mailVal);
                        //validating.Status = 0;
                        return Ok(validating);
 
                     
                }
                    Mess = "Some thing wrong Please Search again";
                    notification.sendnotification(value, bookingNum, validating.Status.ToString(), sid, Mess, mailVal);

                    return Ok(validating);
            }
                validating.Status = 1;
                Mess = "Some thing wrong Please Search again";

                notification.sendnotification(value, bookingNum, validating.Status.ToString(), sid, Mess, mailVal);

                return Ok(validating);

            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("SaveBookingConfirmationController/Errors/", "ConfirmedStautsController" + "INController" + value.Sid, ex.InnerException?.Message, ex.Message + ex.StackTrace);
                if (validating == null)
                {
                    Mess = "Some thing wrong Please Search again";
                    notification.sendnotification(value, bookingNum, "3", sid, Mess, mailVal);

                    return BadRequest(ex.Message);
                }
                else
                {
                    if (validating.Status == 1)
                    {
                        Mess = "Some thing wrong Please Search again";

                        notification.sendnotification(value, bookingNum, validating.Status.ToString(), sid, Mess, mailVal);
                        return Ok(validating);
                    }
                    validating.Status = 2;
                    Mess = "Some thing wrong Please call our Call center";

                    notification.sendnotification(value, bookingNum, validating.Status.ToString(), sid, Mess, mailVal);
                    return Ok(validating);
                }
            }
        }

    }
}
