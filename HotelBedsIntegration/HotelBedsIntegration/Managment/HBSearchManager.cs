using HotelBedsIntegration.Models;
using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBedsIntegration.Managment
{
   public class HBSearchManager
    {
        public SearchData searchData { set; get; }
        public List<string> HotelIds { set; get; }
        public HBSearchManager()
        {
            searchData = new SearchData();
            HotelIds = new List<string>();
        }
        public HBSearchData PreparesSearchObj()
        {
            SearchInputData searchInputData = new SearchInputData();
            searchInputData.checkin = Convert.ToDateTime(searchData.DateFrom).ToString("yyyy-MM-dd");
            searchInputData.checkout = Convert.ToDateTime(searchData.DateTo).ToString("yyyy-MM-dd");
            searchInputData.currency = "KWD";
            searchInputData.Nationality = "EG";
            searchInputData.occupancies = new List<Occupancy>();
            searchInputData.hotels = new List<string>();
            for (int i = 0; i < searchData.SearchRooms.Count; i++)
            {
                Occupancy occupancy = new Occupancy()
                {
                    paxes = new List<Pax>()

                };
                for (int j = 0; j < searchData.SearchRooms[i].Adult; j++)
                {
                    occupancy.paxes.Add(new Pax { age = 30 });
                }
                for (int k = 0; k < searchData.SearchRooms[i].Child.Count; k++)
                {
                    occupancy.paxes.Add(new Pax { age = searchData.SearchRooms[i].Child[k] });
                }
                searchInputData.occupancies.Add(occupancy);
            }
            searchInputData.hotels = HotelIds;
            // searchInputData.hotels.Add("1");
            //searchInputData.hotels.Add("180");
            return searchInputData;
        }

        public void GetSearchResult()
        {
            SearchInputData searchInputData = PreparesSearchObj();
            var SMRresult = SMCLSSearch.SearchHotels(searchInputData, searchData.sID).Result;
            MapSearchResult(SMRresult);

        }
        void MapSearchResult(List<SearchOutputData> searchOutputs)
        {

        }
    }
}
