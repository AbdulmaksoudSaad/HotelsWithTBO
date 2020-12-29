using HotelBedsIntegration.DBMapper;
using HotelBedsIntegration.Helper;
using HotelBedsIntegration.Models;
using HotelBedsIntegration.Services;
using Hotels.Common.Helpers;
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
            List<Hotel> HotelOutREsult = new List<Hotel>();
           try
           {
                //yyyy-mm-dd
                SearchMapping.MapRequestToDB(value, SessionID);
            var hotels=  await  HotelSearchSer.SearchHotelService(value,SessionID);


                if (hotels == null)
                {
                    return new List<Hotel>();
                }
            //    SearchMapping.MapResponseToDB(hotels.hotels, SessionID);
                //foreach (var item in hotels.hotels.hotels)
                //{
                //    foreach (var r in item.rooms)
                //    {
                //  var room=  r.rates.FirstOrDefault(a => a.sellingRate == r.rates.Min(x => x.sellingRate));
                    
                //    }
                //}

              //  var lstofSortedHotel = lstofHotels.Where(a => a.price.net == lstofHotels.Min(x => x.price.net)).FirstOrDefault();
                return hotels.hotels.hotels;
             }
            catch (Exception ex)
            {
               var requestData = JsonConvert.SerializeObject(ex);

                LoggingHelper.WriteToFile("HBLogs/SearchException", "SearchException_" + SessionID, "SearchException", requestData);
                return new List<Hotel>();
            }

        }
    }
}
