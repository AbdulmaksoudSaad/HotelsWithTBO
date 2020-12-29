using Hotels.Common.Models;
using IntegrationTotalStay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTotalStay.Management
{
  public  class SearchManager
    {
        public static SearchRequest prepareSearchObj(SearchData data,string city)
        {
            //2019-07-27
            SearchRequest searchRequest = new SearchRequest();
            searchRequest.SearchDetails.ArrivalDate = Convert.ToDateTime(data.DateFrom).ToString("yyyy-MM-dd");
            searchRequest.SearchDetails.Duration = Convert.ToInt32((data.DateTo - data.DateFrom).TotalDays).ToString();
            searchRequest.SearchDetails.MealBasisID = "0";
            searchRequest.SearchDetails.MinStarRating = "0";
            searchRequest.SearchDetails.RegionID = city;
            foreach (var item in data.SearchRooms)
            {
                RoomRequest room = new RoomRequest();
                room.Adults = item.Adult.ToString();
                room.Children = item.Child.Count.ToString();
                room.Infants = "0";
                if (item.Child.Count > 0)
                {
                    foreach (var ch in item.Child)
                    {
                        room.ChildAges.ChildAge.Add(new ChildAge { Age = "5" });
                    }
                   
                }
                searchRequest.SearchDetails.RoomRequests.RoomRequest.Add(room);
            }
    
            return searchRequest;
        }
    }
}
