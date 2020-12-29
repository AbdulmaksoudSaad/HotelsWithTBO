using HotelBedsIntegration.DBMapper;
using HotelBedsIntegration.Helper;
using HotelBedsIntegration.Models;
using HotelBedsIntegration.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBedsIntegration.Controller
{
   public class SearchHotel
    {
        public static async Task<List<Hotel>> SearchHotels(HBSearchData value, string SessionID)
        {
           try
           {
                //yyyy-mm-dd
                SearchMapping.MapRequestToDB(value, SessionID);
            var hotels=  await  HotelSearchSer.SearchHotelService(value,SessionID);


                if (hotels == null)
                {
                    return new List<Hotel>();
                }
                SearchMapping.MapResponseToDB(hotels.hotels, SessionID);
                return hotels.hotels.hotels;
             }
            catch (Exception ex)
            {
               var requestData = JsonConvert.SerializeObject(ex);

              LogData.WriteToFile("c:/HotelsB2C/Logs/HBLogs/SearchException", "SearchException_" + SessionID, "SearchException", requestData);
                return new List<Hotel>();
            }

        }
    }
}
