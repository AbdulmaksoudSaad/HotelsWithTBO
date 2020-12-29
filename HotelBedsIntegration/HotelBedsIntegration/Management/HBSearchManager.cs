using HotelBedsIntegration.Controller;
using HotelBedsIntegration.Models;
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
        public HBSearchManager()
        {
            searchData = new SearchData();
            HotelIds = new List<int>();
        }
        public HBSearchData PreparesSearchObj()
        {
            HBSearchData searchInputData = new HBSearchData();
            searchInputData.stay.checkIn = Convert.ToDateTime(searchData.DateFrom).ToString("yyyy-MM-dd");
            searchInputData.stay.checkOut = Convert.ToDateTime(searchData.DateTo).ToString("yyyy-MM-dd");
            searchInputData.stay.shiftDays = "2";
         
            searchInputData.occupancies = new List<Occupancy>();
            searchInputData.hotels = new HotelsReq();
            searchInputData.hotels.hotel = HotelIds;
            for (int i = 0; i < searchData.SearchRooms.Count; i++)
            {
                Occupancy occupancy = new Occupancy()
                {
                    rooms = 1, adults = searchData.SearchRooms[i].Adult,
                    children = searchData.SearchRooms[i].Child.Count,
                    paxes = new List<PaxReq>()

                };
                
                for (int k = 0; k < searchData.SearchRooms[i].Child.Count; k++)
                {
                    occupancy.paxes.Add(new PaxReq { age = searchData.SearchRooms[i].Child[k] ,type="CH"});
                }
                searchInputData.occupancies.Add(occupancy);
            }
            return searchInputData;
        }

        public void GetSearchResult()
        {
            HBSearchData searchInputData = PreparesSearchObj();
            var SMRresult = SearchHotel.SearchHotels(searchInputData, searchData.sID).Result;
            MapSearchResult(SMRresult);

        }
        void MapSearchResult(List<Hotel> searchOutputs)
        {

        }
    }
}
