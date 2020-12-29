using HotelBedsIntegration.Controller;
using HotelBedsIntegration.Models;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBedsIntegration.Management
{
   public class HBSearchManager
    {
        public SearchData searchData { set; get; }
        public List<int> HotelIds { set; get; }
        public List<Hotel> searchOutputs { set; get; }
        public HBSearchManager()
        {
            searchData = new SearchData();
            HotelIds = new List<int>();
            searchOutputs = new List<Hotel>();
        }
        public HBSearchData PreparesSearchObj()
        {
            try
            {
                HBSearchData searchInputData = new HBSearchData();
                searchInputData.stay.checkIn = Convert.ToDateTime(searchData.DateFrom).ToString("yyyy-MM-dd");
                searchInputData.stay.checkOut = Convert.ToDateTime(searchData.DateTo).ToString("yyyy-MM-dd");
                //  searchInputData.stay.shiftDays = "2";
                searchInputData.stay.allowOnlyShift = false;
                searchInputData.occupancies = new List<Occupancy>();
                searchInputData.hotels = new HotelsReq();
                searchInputData.hotels.hotel = HotelIds;
                for (int i = 0; i < searchData.SearchRooms.Count; i++)
                {
                    Occupancy occupancy = new Occupancy()
                    {
                        rooms = 1,
                        adults = searchData.SearchRooms[i].Adult,
                        children = searchData.SearchRooms[i].Child.Count,
                        paxes = new List<PaxReq>()

                    };

                    for (int k = 0; k < searchData.SearchRooms[i].Child.Count; k++)
                    {
                        occupancy.paxes.Add(new PaxReq { age = searchData.SearchRooms[i].Child[k], type = "CH" });
                    }
                    searchInputData.occupancies.Add(occupancy);
                }
                return searchInputData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetSearchResult()
        {
            try
            {
                HBSearchData searchInputData = PreparesSearchObj();
                searchOutputs = SearchHotel.SearchHotels(searchInputData, searchData.sID).Result;
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("HBLogs/SearchController/Errors/", "HBIntegrationManagement" + "INControoler" + searchData.sID, ex.InnerException?.Message, ex.Message + ex.StackTrace);

                throw ex;
            }
        }
        void MapSearchResult(List<Hotel> searchOutputs)
        {

        }
    }
}
