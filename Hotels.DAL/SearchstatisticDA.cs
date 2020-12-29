using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Hotels.IDAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.DAL
{
  public  class SearchstatisticDA : ISearchstatisticDA
    {
        
        public async void  AddMetaSearchStatistic(CheckOutData data, string BN)
        {
            try
            {
                SearchDBEntities searchDB = new SearchDBEntities();
                hotelsDBEntities db = new hotelsDBEntities();
                SearchStatistic metaData = new SearchStatistic();
                var SearchData = searchDB.SearchCriterias.FirstOrDefault(x => x.sID == data.Sid);
                var hotel = db.hotels.FirstOrDefault(h => h.hotelID == data.HotelID);
             //   var PosData= db.
                if (hotel != null && SearchData != null)
                {
                    metaData.BookingNo = BN;
                    metaData.BookingStatus = "New Booking";
                    metaData.CheckOut = SearchData.dateTo.Value;
                    metaData.ChekIn = SearchData.dateFrom.Value;
                    metaData.CityName = hotel.cityName;
                    metaData.Date = SearchData.sessionCreatedAt.Value;
                    metaData.HotelID = data.HotelID;
                    metaData.HotelName = hotel.hotelName;
                    metaData.POS = SearchData.pos;
                    metaData.ProvideID = data.Pid;
                    metaData.sID = data.Sid;
                    metaData.Source = SearchData.source;
                }
                string path = ConfigurationSettings.AppSettings["metaData"];

                var client = new HttpClient();
                var url = path + "/api/MetaSearchStatistics/POST";
                var response = client.PostAsJsonAsync(url,metaData).Result;

                LoggingHelper.WriteToFile("AddMetaSearchStatistic/", "InSearchstatisticDA ", "ResponseData", response.StatusCode.ToString());

                //if (response.IsSuccessStatusCode)
                //{



                //}
                //else
                //{


                //}
            }catch(Exception ex)
            {
                LoggingHelper.WriteToFile("AddMetaSearchStatistic/Errors/", "InSearchstatisticDA" +   data.Sid, ex.Message, ex.Message + ex.StackTrace);

            }
        }

        public List<SearchStatistic> SearchStatisticTransaction(DateTime fromDate, DateTime toDate)
        {
            try
            {
                HotelBookingDBEntities BookingDB = new HotelBookingDBEntities();
                SearchDBEntities searchDB = new SearchDBEntities();
                hotelsDBEntities db = new hotelsDBEntities();
                List<SearchStatistic> searchStatistics = new List<SearchStatistic>();
                var SearchesData = searchDB.SearchCriterias.Where(x => x.sessionCreatedAt >= fromDate && x.sessionCreatedAt <= toDate).ToList();
                foreach (var item in SearchesData)
                {
                    SearchStatistic searchStatistic = new SearchStatistic();
                    searchStatistic.ChekIn = item.dateFrom.Value;
                    searchStatistic.CheckOut = item.dateTo.Value;
                    searchStatistic.Date = item.sessionCreatedAt.Value;
                    searchStatistic.POS = item.pos;
                    searchStatistic.sID = item.sID;
                    searchStatistic.Source = item.source;
                    var cityD = db.Cities.FirstOrDefault(x => x.ID.ToString() == item.cityName);
                    if (cityD != null)
                        searchStatistic.CityName = cityD.City1;
                    var BookingData = BookingDB.HotelsBookings.FirstOrDefault(a => a.SessionId == item.sID);
                    if (BookingData != null)
                    {
                        searchStatistic.BookingNo = BookingData.Booking_No;
                        searchStatistic.BookingStatus = BookingData.Booking_Status;
                        searchStatistic.HotelID = BookingData.Hotel_ID;
                        searchStatistic.ProvideID = BookingData.Provider_ID;
                        var hotel = db.hotels.FirstOrDefault(h => h.hotelID == BookingData.Hotel_ID);
                        if (hotel != null)
                            searchStatistic.HotelName = hotel.hotelName;
                    }
                    searchStatistics.Add(searchStatistic);
                }
                return searchStatistics;
            }catch(Exception ex)
            {
                LoggingHelper.WriteToFile("StatisticController/Errors/", "INDAL", "FromData" + fromDate +"to"+toDate, ex.InnerException?.Message + "//" + ex.Message + ex.StackTrace);
                return new List<SearchStatistic>();
            }
        }
    }
}
