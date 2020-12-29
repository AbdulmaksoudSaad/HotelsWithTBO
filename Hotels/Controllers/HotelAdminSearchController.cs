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
    public class HotelAdminSearchController : ApiController
    {
        

        
        public IHttpActionResult Get(string searchKey)
        {
            LoggingHelper.WriteToFile("AdminPannelController/DataIncoming/", "HotelAdminSearchController" + "INController" + searchKey, "Data", "Search key"+searchKey);

            try
            {
                AdminSearchBLL adminSearchBLL = new AdminSearchBLL();
                var requiredData = adminSearchBLL.GetHotelAdimSearchData(searchKey);

                return Ok(requiredData);
            }
            catch(Exception ex)
            {
                LoggingHelper.WriteToFile("AdminPannelController/ERROR/", "HotelAdminSearchController" + "INController" + searchKey, "Data", "Sid is " + searchKey + " and key is" + searchKey + ex.InnerException?.Message + ex.Message + ex.StackTrace);

                return BadRequest(ex.Message);
            }
        }
        [Route("api/QueueTransaction")]
        public IHttpActionResult Get(DateTime fromDate,DateTime toDate)
        {
            LoggingHelper.WriteToFile("AdminPannelController/DataIncoming/", "QueueTransactionController" + "INController" , "Data", "From" + fromDate +"To"+toDate);

            try
            {
                AdminSearchBLL adminSearchBLL = new AdminSearchBLL();
                var requiredData = adminSearchBLL.QueueTransaction(fromDate, toDate);

                return Ok(requiredData);
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("AdminPannelController/ERROR/", "QueueTransactionController" + "INController" , "Data", "from is " + fromDate + " and to is" + toDate + ex.InnerException?.Message + ex.Message + ex.StackTrace);

                return BadRequest(ex.Message);

            }
        }
        [Route("api/GetBookingDetails")]
        public IHttpActionResult GetBookingDetails(string bookingNum )
        {
            LoggingHelper.WriteToFile("AdminPannelController/DataIncoming/", "HotelAdminSearchController" + "INController" + bookingNum, "Data", "Search key" + bookingNum);

            try
            {
                AdminSearchBLL adminSearchBLL = new AdminSearchBLL();
                var requiredData = adminSearchBLL.GetBookingDetails(bookingNum);

                return Ok(requiredData);
            }catch(Exception ex)
            {
                LoggingHelper.WriteToFile("AdminPannelController/ERROR/", "GetBookingDetails" + "INController" + bookingNum, "Data", "bookin is " + bookingNum + " and booking is" + bookingNum +  ex.Message + ex.StackTrace);

                return BadRequest(ex.Message);
            }
        }
        [Route("api/BookingStatus")]
        public IHttpActionResult Get( )
        {
           // LoggingHelper.WriteToFile("AdminPannelController/DataIncoming/", "HotelAdminSearchController" + "INController" + searchKey, "Data", "Search key" + searchKey);

            try
            {
                AdminSearchBLL adminSearchBLL = new AdminSearchBLL();
                var requiredData = adminSearchBLL.GetBookingStautsDetails();

                return Ok(requiredData);
            }catch(Exception ex)
            {
                LoggingHelper.WriteToFile("AdminPannelController/ERROR/", "BookingStatus" + "INController" , "Data",    ex.InnerException?.Message + ex.Message + ex.StackTrace);

                return BadRequest(ex.Message);
            }
        }
        [Route("api/GetProviders")]
        public IHttpActionResult GetProvidersDetails()
        {
         //   LoggingHelper.WriteToFile("AdminPannelController/DataIncoming/", "HotelAdminSearchController" + "INController" + searchKey, "Data", "Search key" + searchKey);

            try
            {
                AdminSearchBLL adminSearchBLL = new AdminSearchBLL();
                var requiredData = adminSearchBLL.GetProviderDetails();

                return Ok(requiredData);
            }catch(Exception ex)
            {
                LoggingHelper.WriteToFile("AdminPannelController/ERROR/", "GetProviders" + "INController", "Data", ex.InnerException?.Message + ex.Message + ex.StackTrace);

                return BadRequest(ex.Message);
            }
        }

        [Route("api/ChangeBookingStatus")]
        public IHttpActionResult Get(string BookingNum,string Status)
        {
            LoggingHelper.WriteToFile("AdminPannelController/DataIncoming/", "ChangeBookingStatusController" + "INController" + BookingNum, "Data", "BookingNum" + BookingNum +"and status"+Status) ;

            try
            {
                AdminSearchBLL adminSearchBLL = new AdminSearchBLL();
                var requiredData = adminSearchBLL.UpdateBookingStatus(BookingNum, Status);
                if (requiredData == true)
                    return Ok();

                return Ok("No Data Found");
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("AdminPannelController/ERROR/", "ChangeBookingStatus" + "INController" + BookingNum, "Data", "bookin is " + BookingNum + " and booking is" + BookingNum + ex.InnerException?.Message + ex.Message + ex.StackTrace);

                return BadRequest(ex.Message);
            }
        }
    }
}
