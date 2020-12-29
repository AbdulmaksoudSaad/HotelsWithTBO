using Hotels.Common;
using Hotels.Common.Models;
using Hotels.DAL;
using Hotels.ProviderManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelWegolayer.Model
{
 public   class Wego : IChannelSearch<WegoSearch>
    {
        public SearchData searchData { set; get; }
        public List<HotelChannelResult> hotelChannelResults { set; get; }
        List<hotelsProvider> HotelprovidersID { get; set; }
        ProvidersChannel DB;
        public Wego()
        {
            searchData = new SearchData();
            hotelChannelResults = new List<HotelChannelResult>();
            HotelprovidersID = new  List<hotelsProvider>();
            DB = new ProvidersChannel();
        }
        public void GetHotelSearchResultForAllProviders()
        {
            using (ProviderManager pm = new ProviderManager())
            {
                pm.WegoHotels = HotelprovidersID;
                pm.searchData = searchData;
                //F
                pm.GetHotelSearchResultForAllProviders();
                hotelChannelResults = pm.hotelChannelResults;
                //**** GetLowestPrice(searchResults);
            }

        }

        public void GetHotelsProviderID(List<string> Hotelid)
        {
            
            HotelprovidersID= DB.GetHotelProvides(Hotelid);
             
        }

        public void SaveMetaSearchData(WegoSearch Data)
        {
           string cid= DB.GetSearchData(Data.HotelsId);
            searchData.CityName = cid;
            searchData.Currency = Data.Currency;
            searchData.DateFrom = Data.DateFrom;
            searchData.DateTo = Data.DateTo;
            searchData.Lang = Data.Lang;
            searchData.Nat = Data.Nat;
            searchData.POS = Data.POS;
            searchData.SearchRooms = Data.SearchRooms;
            searchData.sID = Data.sID;
            searchData.Source = Data.Source;
            // call service to Get CityId;
             // save data by call statistic
            
        }

        public void SaveSearchResultForAllProviders()
        {
            DB.SaveSearchResult(hotelChannelResults, searchData.sID);
            
        }
    }
}
