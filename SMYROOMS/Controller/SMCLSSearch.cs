using Newtonsoft.Json;
using SMYROOMS.DB;
using SMYROOMS.Helpers;
using SMYROOMS.Model;
using SMYROOMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Controller
{
    public class SMCLSSearch
    {
        public static async Task<List<SearchOutputData>> SearchHotels(SearchInputData value ,string SessionID)
        {
            try {
                //yyyy-mm-dd
             //  var searchdata=   SearchMapper.MapInputData(value);
                SearchService service = new SearchService();
                var data = await service.GetHotelsData(value,SessionID);
                List<SearchOutputData> OutPutData = new List<SearchOutputData>();
                List<SearchOutputData> OutPutHotel = new List<SearchOutputData>();
                ResponseMapper map = new ResponseMapper();
                if (data.Data.hotelX.search.errors == null )
                {
                    OutPutData = map.MappingData(data,SessionID);
                    DataEntry db = new DataEntry();
                    db.SaveSMRHotelRates(OutPutData,SessionID);
                }
                else
                    return new List<SearchOutputData>();
                foreach (var item in value.hotels)
                {
                    var lstofHotels = OutPutData.Where(a => a.hotelCode == item).ToList();
                    var lstofSortedHotel = lstofHotels.Where(a => a.price.net == lstofHotels.Min(x => x.price.net)).FirstOrDefault();
                    if (lstofSortedHotel != null)
                    {
                        OutPutHotel.Add(lstofSortedHotel);
                    }
                }
                return OutPutHotel;
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggerHelper.WriteToFile("SMLogs/SearchException", "SearchException_" + SessionID, "SearchException", requestData);
                return new List<SearchOutputData>();
            }
            
        }
    }
}
