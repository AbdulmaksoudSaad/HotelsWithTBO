
using Hotels.BLL;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Hotels.DAL;
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
    public class HotelCheckAvailabilityController : ApiController
    {
        [HttpOptions]
        public IHttpActionResult Post()
        {
            return Ok();
        }

        public IHttpActionResult Get(string sid, string bookingnum)//
        {
            LoggingHelper.WriteToFile("SaveBookingConfirmationController/InComingConfirmationData/", "ConfirmedController" + "INController" + sid, "InComingData", "sid" + sid + " , BookingNum" + bookingnum);

            try
            {
                // get avail data from bl then dl
                var value = CheckAvailabilityRepo.GetAvailabilityFromDB(sid, bookingnum);
                if (value != null && value.availabilityReq.Rooms.Count > 0)
                {
                    var CheckAvailabilitydata = CheckAvailabillity.GetCheckAvailabilityResult(value.availabilityReq, value.Curr, "checkAvailabilty");
                    if (CheckAvailabilitydata != null)
                    {
                        // map to validation model 

                        var result = PaymentMapper.GetVaildationModel(CheckAvailabilitydata, value.Curr, sid);
                        result.PaymentFareDetails.CustomerPaymentCurrency = value.Curr;
                        return Ok(result);
                    }
                }

                return Ok("No Data Found");

            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("AvailabilityController/Errors/", "AvailabilityController" + "INController" + sid, ex.InnerException?.Message, ex.Message + ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }
        // also call check availability
        [HttpGet]
        [Route("api/getcancelpolicy")]
        public IHttpActionResult GetCancel(string sid, string hotelcode, string roomindex, string pid)
        {
            try
            {
                // check cancel in DB in case provider 4
                if (pid == "4" || pid =="5")
                {
                    var CancelData = CheckAvailabilityRepo.GetCancelFromDB(sid, hotelcode, roomindex, pid);
                    if (CancelData.Count > 0)
                    {
                        return Ok(CancelData);
                    }
                }
                // get avail data from bl then dl
                var value = CheckAvailabilityRepo.GetAvailabilityForCancelFromDB(sid, hotelcode, roomindex, pid);
                if (value != null && value.Rooms.Count > 0)
                {
                    var CheckAvailabilitydata = CheckAvailabillity.GetCheckAvailabilityResult(value, null, "policy");
                    if (CheckAvailabilitydata != null)
                    {
                        // map to validation model
                        return Ok(CheckAvailabilitydata.Result[0].cancellationRules);
                    }
                }

                return Ok("No Data Found");

            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("AvailabilityController/Errors/", "AvailabilityController" + "INControllerGEtCancel" + sid, ex.InnerException?.Message, ex.Message + ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        // POST: api/HotelCheckAvailability  XXXXXXXXXXXXXXXXXXXxxx// not used !
        public IHttpActionResult Post([FromBody]Hotels.Common.Models.AvailabilityReq value)
        {
            try
            {
                LoggingHelper.WriteToFile("AvailabilityController/Request/", "AvailabilityReq" + "INController" + value.Sid, "RequestObject", JsonConvert.SerializeObject(value));

                var CheckAvailabilitydata = CheckAvailabillity.GetCheckAvailabilityResult(value, null, "");
                if (CheckAvailabilitydata != null)
                {
                    return Ok(CheckAvailabilitydata);
                }
                return Ok("No Availability data found");

            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("AvailabilityController/Errors/", "AvailabilityController" + "INControoler" + value.Sid, ex.InnerException?.Message, ex.Message + ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }
    }
}

//    // GET: api/HotelCheckAvailability/5
//    public IHttpActionResult Get(string sid ,string hotelCode, string rooms ,string pid ,string refNum)
//    {
//        AvailabilityData availability = new AvailabilityData();
//        availability.HotelCode = hotelCode;
//        availability.PID = pid;
//        availability.RefNUm = refNum;
//        availability.Rooms = rooms;
//        availability.Sid = sid;
////     var CheckAvailabilitydata=   CheckAvailabillity.GetCheckAvailabilityResult(availability);
//      //  return Ok(CheckAvailabilitydata);
//    }
