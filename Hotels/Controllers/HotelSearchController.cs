using Hotels.BLL;
using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
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
    public class HotelSearchController : ApiController
    {
        [HttpOptions]
        public IHttpActionResult Post()
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]SearchData searchdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SearchManager search = new SearchManager())
                    {
                        LoggingHelper.WriteToFile("SearchController/SearchDataIncoming/", "INController" + searchdata.sID, "SearchData", JsonConvert.SerializeObject(searchdata));

                        var data = search.GetSearchResult(searchdata);
                        if (data.HotelResult.Count > 0)
                        {
                            data.Status = 0;
                            return Ok(data);
                        }
                        else if (data.Status == 1) //not valid search criteria
                        {
                            //data.ResultException = new ResultException
                            //{
                            //    ExceptionMessage = "Invalid Search Critreia"
                            //};
                            return Ok(data);
                        }
                        data.Status = 2; // error from provider
                        //data.ResultException = new ResultException
                        //{
                        //    ExceptionMessage = "NO Result"
                        //};
                        return Ok(data);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {

                LoggingHelper.WriteToFile("SearchController/Errors/", "INController" + searchdata.sID, ex.InnerException?.Message, ex.Message + ex.StackTrace);
                return Ok(new HotelSearchResponse { Status = 3, Locations = new List<string>(), HotelResult = new List<HotelSearchResult>() });
                //return BadRequest(ex.Message);
            }
        }
        [Route("api/getAmenities")]
        [HttpGet]
        public IHttpActionResult GetHotelAmenities(string id)
        {
            try
            {
                HotelAmenityBL hotelAmenityBL = new HotelAmenityBL();
                //   LoggingHelper.WriteToFile("SearchController/SearchDataIncoming/", "SearchController" + "INController" + , "SearchData", JsonConvert.SerializeObject(searchdata));

                var amenities = hotelAmenityBL.GetHotelAmenities(id);
                return Ok(amenities);

            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("SearchController/Errors/", "GetHotelAmenities" + "INController" + id, ex.InnerException?.Message, ex.Message + ex.StackTrace);

                return BadRequest(ex.Message);
            }
        }

        //add Api return Search Criteria
        //public IHttpActionResult GetSearchCriteria(string SID)
        //{
        //   var SearchData = SearchRepo.GetSearchDataBySession(availability.Sid);

        //}
    }
}
