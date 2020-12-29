using Hotels.Common.Helpers;
using Hotels.DAL;
using HotelWegolayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hotels.Controllers
{
    public class WegoHotelSearchController : ApiController
    {
        // GET: api/WegoHotelSearch
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/WegoHotelSearch/5
        //public IHttpActionResult Get(string pid,string hotelid,string sid)
        //{
        //    ProvidersChannel packs = new ProvidersChannel();
        //   var Packages=  packs.GetHotelPackages(pid, sid, hotelid);
        //    return Ok(Packages);
        //}

        //// POST: api/WegoHotelSearch
        //public IHttpActionResult Post([FromBody]WegoSearch value)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            int Paxes = 0;
        //            foreach (var item in value.SearchRooms)
        //            {
        //                Paxes = Paxes + item.Adult + item.Child.Count;
        //                if (item.Adult > 5)
        //                    return BadRequest("Maximum Number Of Adult In Room 5");
        //                if (item.Child.Count > 2)
        //                {
        //                    return BadRequest("Maximum Number Of Childern In Room 5");
        //                }

        //            }
        //            if (Paxes > 9)
        //                return BadRequest("Maximum Number Of Paxes In Room 5");
        //            if (value.HotelsId.Count == 0)
        //                return BadRequest("Your Request Has No Hotels");
        //            Wego wego = new Wego();
        //            wego.GetHotelsProviderID(value.HotelsId);
        //            wego.SaveMetaSearchData(value);
        //            wego.GetHotelSearchResultForAllProviders();
        //            wego.SaveSearchResultForAllProviders();
        //            if(wego.hotelChannelResults.Count>0)
        //            return Ok(wego.hotelChannelResults);

        //            return NotFound();
        //        }
        //        return BadRequest(ModelState);
        //    }catch(Exception ex)
        //    {
        //        LoggingHelper.WriteToFile("WegoHotelSearch/Errors/", "INController_" + value.sID, ex.Message, ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);
        //        return BadRequest(ex.Message);
        //    }
        //}

      
    }
}
