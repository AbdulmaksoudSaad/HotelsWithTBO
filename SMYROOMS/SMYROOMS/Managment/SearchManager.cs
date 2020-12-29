using Hotels.BLL;
using Hotels.Common.Models;
using SMYROOMS.Controller;
using SMYROOMS.DB;
using SMYROOMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Managment
{
    public class SearchManager
    {
        public SearchData searchData { set; get; }
        public List<string> HotelIds { set; get; }
        public List<BoardCode> boardCodes { set; get; }
        public List<SearchOutputData> searchOutputs { set; get; }

        public SearchManager()
        {
            searchData = new SearchData();
            HotelIds = new List<string>();
            boardCodes = new List<BoardCode>();
            searchOutputs = new List<SearchOutputData>();
        }
        public SearchInputData PreparesSearchObj()
        {
            SearchInputData searchInputData = new SearchInputData();
            searchInputData.checkin = Convert.ToDateTime(searchData.DateFrom).ToString("yyyy-MM-dd");
            searchInputData.checkout = Convert.ToDateTime(searchData.DateTo).ToString("yyyy-MM-dd");
            searchInputData.currency = "KWD";
            searchInputData.Nationality = searchData.Nat;
            searchInputData.occupancies = new List<Occupancy>();
            searchInputData.hotels = HotelIds;
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
            return searchInputData;
        }

        public void GetSearchResult()
        {
            SearchInputData searchInputData = PreparesSearchObj();
            searchOutputs = SMCLSSearch.SearchHotels(searchInputData, searchData.sID).Result;
            MealDataEntry mealDataEntry = new MealDataEntry();
           boardCodes = mealDataEntry.getSMRBoardCode();
        }
        //public List<HotelSearchResult> MapSearchResult(List<SearchOutputData> searchOutputs)
        //{
        //    //save result
        //    //apply salesRules
        //    MealDataEntry mealDataEntry = new MealDataEntry();
        //    List<BoardCode> boardCodes = mealDataEntry.getSMRBoardCode();
        //    List<string> hotelIds = searchOutputs.Select(a => a.hotelCode).ToList();
        //    HotelManager manager = new HotelManager();

        //    List<HotelDetails> HotelDataList = manager.GetHotelData(hotelIds, "3");
        //    List<HotelSearchResult> results = new List<HotelSearchResult>();
        //    DateTime CheckInDate = searchData.DateFrom;
        //    for (int i = 0; i < searchOutputs.Count; i++)
        //    {
        //        BoardCode boardCode = boardCodes.Where(a => a.Code == searchOutputs[i].boardCode).FirstOrDefault();
        //        HotelDetails hotelData = HotelDataList.Where(a => a.ProviderHotelId == searchOutputs[i].hotelCode).FirstOrDefault();
        //        HotelSearchResult hotel = new HotelSearchResult();
        //        hotel.hotelCode = searchOutputs[i].hotelCode;
        //        hotel.City = hotelData.City;
        //        hotel.hotelName = hotelData.HotelName;
        //        hotel.Country = hotelData.Country;
        //        hotel.hotelStars = int.Parse(hotelData.Rating) - 558;
        //        var images = hotelData.Images.FirstOrDefault();
        //        if (images != null)
        //        {
        //            hotel.hotelThumb = images.Thum;
        //        }
        //        hotel.hotelImages = hotelData.Images.Select(a => a.Thum).ToList();
        //        hotel.Lat = hotelData.Lat;
        //        hotel.Lng = hotelData.Lng;
        //        hotel.providerID = "3";
        //        hotel.hotelDescription = hotelData.LongDescriptin;
        //        hotel.shortcutHotelDescription = hotelData.ShortDescription;
        //        hotel.ZipCode = hotelData.Zipcode;
        //        hotel.Location = hotelData.Location;
        //        hotel.Address = hotelData.Address;

        //        hotel.sellCurrency = searchData.Currency;
        //        hotel.costPrice = double.Parse(searchOutputs[i].price.net.Value.ToString());
        //        hotel.hotelRate = hotel.costPrice;
        //        hotel.costCurrency = searchOutputs[i].price.currency;
        //        hotel.rooms = new List<RoomResult>();
        //        for (int j = 0; j < searchOutputs[i].rooms.Count; j++)
        //        {
        //            RoomResult room = new RoomResult();
        //            room.IsRefundable = searchOutputs[i].rooms[j].refundable;
        //            room.RatePerNight = double.Parse(searchOutputs[i].rooms[j].roomPrice.price.net.Value.ToString());
        //            room.RoomIndex = j + 1;
        //            room.RoomType = searchOutputs[i].rooms[j].description;
        //            room.RoomMeal = boardCode.Name;
        //            room.Images = hotelData.Images.Where(a => a.Category.ToLower() == "hotel rooms").Select(a => a.Thum).ToList();
        //            room.cancellationRules = searchOutputs[i].cancelPolicy.cancelPenalties.Select(a => new CancellationRule
        //            {
        //                ToDate = CheckInDate.AddHours(-a.hoursBefore.Value).ToString("dd MMM YYYY"),
        //                Cost=a.value.Value
        //            }).ToList();
        //            hotel.rooms.Add(room);
        //        }
        //        results.Add(hotel);
        //    }
        //    return results;
        //}
    }
}
