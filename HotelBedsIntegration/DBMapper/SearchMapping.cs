using HotelBedsIntegration.Helper;
using HotelBedsIntegration.Models;
using Hotels.Common.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBedsIntegration.DBMapper
{
    class SearchMapping
    {
        public static void MapRequestToDB(HBSearchData value, string SessionID)
        {
            try
            {
                HotelBedEntity db = new HotelBedEntity();
                SearchRequest search = new SearchRequest();
                search.SessionId = SessionID;
                search.Req = Newtonsoft.Json.JsonConvert.SerializeObject(value);
                db.SearchRequests.Add(search);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggingHelper.WriteToFile("HBLogs/SearchException", "SearchException_" + SessionID, "SearchException", requestData);
                 
            }
        }
        //public static void MapResponseToDB(HotelBedsIntegration.Models.Hotels value, string SessionID)
        //{
        //    try {
        //        HotelBedEntity db = new HotelBedEntity();
        //        foreach (var item in value.hotels)
        //        {
        //            SearchHotelData searchHotel = new SearchHotelData();
        //            searchHotel.CategoryCode = item.categoryCode;
        //            searchHotel.CategoryName = item.categoryName;
        //            searchHotel.checkIn = value.checkIn;
        //            searchHotel.checkOut = value.checkOut;
        //            searchHotel.Code = item.code;
        //            searchHotel.currency = item.currency;
        //            searchHotel.destinationCode = item.destinationCode;
        //            searchHotel.DestinationName = item.destinationName;
        //            searchHotel.latitude = item.latitude;
        //            searchHotel.longitude = item.longitude;
        //            searchHotel.maxRate = item.maxRate;
        //            searchHotel.minRate = item.minRate;
        //            searchHotel.Name = item.name;
        //            searchHotel.SessionID = SessionID;
        //            searchHotel.total = value.total.ToString();
        //            searchHotel.ZoneCode = item.zoneCode.ToString();
        //            searchHotel.ZoneName = item.zoneName;
        //            searchHotel.Request = Newtonsoft.Json.JsonConvert.SerializeObject(value);
        //            db.SearchHotelDatas.Add(searchHotel);
        //            db.SaveChanges();

        //            foreach (var room in item.rooms)
        //            {
        //                foreach (var rate in room.rates)
        //                {
        //                    SearchRoom searchRoom = new SearchRoom();
        //                    searchRoom.adults = rate.adults;
        //                    searchRoom.allotment = rate.allotment;
        //                    searchRoom.boardCode = rate.boardCode;
        //                    searchRoom.boardName = rate.boardName;
        //                    searchRoom.children = rate.children;
        //                    searchRoom.childrenAges = rate.childrenAges;
        //                    searchRoom.hotelMandatory = rate.hotelMandatory.ToString();
        //                    searchRoom.netPrice = rate.net;
        //                    searchRoom.packaging = rate.packaging.ToString();
        //                    searchRoom.paymentType = rate.paymentType;
        //                    searchRoom.rateClass = rate.rateClass;
        //                    searchRoom.rateKey = rate.rateKey;
        //                    searchRoom.rateType = rate.rateType;
        //                    searchRoom.ResponseType = "search";
        //                    searchRoom.RoomCode = room.code;
        //                    searchRoom.RoomName = room.name;
        //                    searchRoom.rooms = rate.rooms;
        //                    searchRoom.SearchHotelID = searchHotel.Id;
        //                    searchRoom.sessionID = SessionID;
        //                    searchRoom.sellingRate = rate.sellingRate;
        //                    db.SearchRooms.Add(searchRoom);
        //                    db.SaveChanges();
        //                    if (rate.taxes != null)
        //                    {
        //                        foreach (var tax in rate.taxes.taxes)
        //                        {
        //                            RoomTax roomTax = new RoomTax();
        //                            roomTax.amount = tax.amount;
        //                            roomTax.Currency = tax.currency;
        //                            roomTax.RoomId = searchRoom.Id;
        //                            roomTax.included = tax.included.ToString();
        //                            db.RoomTaxes.Add(roomTax);
        //                        }
        //                    }
        //                    if (rate.cancellationPolicies != null)
        //                    {
        //                        foreach (var policy in rate.cancellationPolicies)
        //                        {
        //                            RoomPolicy roomPol = new RoomPolicy();
        //                            roomPol.amount = policy.amount;
        //                            roomPol.CallingType = "Search";
        //                            roomPol.Currency = searchHotel.currency;
        //                            roomPol.fromDate = policy.from.ToString();
        //                            roomPol.RoomID = searchRoom.Id;
        //                            db.RoomPolicies.Add(roomPol);
        //                        }
        //                    }
        //                    db.SaveChanges();
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var requestData = JsonConvert.SerializeObject(ex);

        //        LoggingHelper.WriteToFile("HBLogs/SearchException", "SearchException_" + SessionID, "SearchException", requestData);

        //    }
        //}

    }
}



