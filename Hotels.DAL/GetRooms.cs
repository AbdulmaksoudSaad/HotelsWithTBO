using Hotels.Common;
using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBO.DAL.Models.Context;

namespace Hotels.DAL
{
    public class GetRooms
    {
        public static HotelSearchRoom GetRoomsByHotelIDAndProvide(string Sid, string pid, string Hid)
        {
            try
            {
                SearchDBEntities searchDB = new SearchDBEntities();
                List<RoomResult> roomResults = new List<RoomResult>();
                var RoomsRuslt = searchDB.SearchRoomResults.Where(a => a.HotelCode == Hid && a.sID == Sid && a.ProviderId.ToString() == pid).ToList();
                var Dur = searchDB.SearchCriterias.FirstOrDefault(a => a.sID == Sid).duration;

                if (pid == "5")
                {
                    TBO.DAL.Models.Context.TBOContext context = new TBO.DAL.Models.Context.TBOContext();

                    var HImages = context.HotelImages.Where(a => a.HotelCode == Hid).ToList();
                    List<string> images = HImages.Select(x => x.URL).ToList();

                    var hotelSearchData = context.HotelDetails.FirstOrDefault(a => a.HotelCode == Hid);
                    var facilties = context.Facilities.Where(ff => ff.HotelCode == Hid).ToList();
                    HotelSearchRoom hotelSearchRoom = new HotelSearchRoom();
                    //facilties 
                    foreach (var item in facilties)
                    {
                        hotelSearchRoom.Amenities.Add(new HotelAmenity
                        {
                            Amenity = item.FacilityVal,
                            HotelCode = item.HotelCode
                        });

                    }
                    hotelSearchRoom.providerHotelID = hotelSearchData.HotelCode;
                    hotelSearchRoom.hotelCode = hotelSearchData.HotelCode;
                    hotelSearchRoom.Address = hotelSearchData.Address;
                    hotelSearchRoom.Lng = hotelSearchData.Map;
                    //hotelSearchRoom.Lat = hotelSearchData.Lat;

                    //hotelSearchRoom.hotelStars = int.Parse(hotelSearchData.HotelRating);//
                    hotelSearchRoom.hotelName = hotelSearchData.HotelName;
                    hotelSearchRoom.providerID = "5";
                    hotelSearchRoom.Location = hotelSearchData.HotelLocation;

                    hotelSearchRoom.hotelDescription = hotelSearchData.Description;
                    //hotelSearchRoom.shortcutHotelDescription = hotelSearchDesc.Description2;
                    hotelSearchRoom.City = hotelSearchData.CityName;
                    hotelSearchRoom.Country = hotelSearchData.CountryName;
                    hotelSearchRoom.hotelImages = images;

                    #region instead call available hotel room api to get tbo rooms
                    //if (RoomsRuslt.Count > 0)
                    //{
                    //    foreach (var item in RoomsRuslt)
                    //    {
                    //        RoomResult result = new RoomResult();
                    //        result.CostPrice = item.costPrice.Value;
                    //        result.DiscountId = item.DiscountId.Value;
                    //        result.DiscountValue = item.DiscountVal.Value;
                    //        //result.Images = roomsImages.Select(a => a.URL).ToList();
                    //        //   result.IsRefundable=item.
                    //        result.MarkupId = item.MarkupId.Value;
                    //        result.MarkupValue = item.MarkupVal.Value;
                    //        result.Paxs = item.PaxSQty.Value;
                    //        result.RatePerNight = (item.SellPrice / Dur).Value;
                    //        result.RoomCode = item.roomType;
                    //        result.RoomIndex = int.Parse(item.RoomCode);
                    //        result.RoomMeal = item.meal;
                    //        result.RoomReference = item.RoomReference;
                    //        result.RoomType = item.RoomName;
                    //        result.TotalSellPrice = item.SellPrice.Value;
                    //        result.Adult = item.Adults.Value;
                    //        result.Child = item.Childern.Value;
                    //        result.IsRefundable = item.IsRefundable.Value;

                    //        //var repeatedRoom = roomResults.Where(x => x.RoomReference == item.RoomReference).ToList();
                    //        //if (repeatedRoom.Count < 1)
                    //            roomResults.Add(result);
                    //    }

                    //    var roomsDetails = searchDB.SearchRoomDatas.Where(a => a.sID == Sid).ToList();
                    //    for (int i = 0; i < roomsDetails.Count; i++)

                    //    {
                    //        SearchRoomData roomresult = new SearchRoomData();
                    //        roomresult = null;
                    //        var resultsR = roomResults.ToList();// Where(x => x.Adult == roomsDetails[i].adultNo.Value && x.Child == roomsDetails[i].childernNo.Value).ToList();
                    //        SeparatedRoom srts = new SeparatedRoom();
                    //        srts.RoomResults.AddRange(resultsR);
                    //        hotelSearchRoom.rooms.Add(srts);

                    //    }
                    //} 
                    #endregion
                    return hotelSearchRoom;
                }
                else
                {
                    hotelsDBEntities hotelsDB = new hotelsDBEntities();
                    var HImages = hotelsDB.HotelsImages.Where(a => a.HotelID == Hid).ToList();
                    var roomsImages = HImages.Where(a => a.Category.ToLower() == "hotel rooms");

                    if (RoomsRuslt.Count > 0)
                    {
                        foreach (var item in RoomsRuslt)
                        {
                            RoomResult result = new RoomResult();
                            result.CostPrice = item.costPrice.Value;
                            result.DiscountId = item.DiscountId.Value;
                            result.DiscountValue = item.DiscountVal.Value;
                            result.Images = roomsImages.Select(a => a.URL).ToList();
                            //   result.IsRefundable=item.
                            result.MarkupId = item.MarkupId.Value;
                            result.MarkupValue = item.MarkupVal.Value;
                            result.Paxs = item.PaxSQty.Value;
                            result.RatePerNight = (item.SellPrice / Dur).Value;
                            result.RoomCode = item.roomType;
                            result.RoomIndex = int.Parse(item.RoomCode);
                            result.RoomMeal = item.meal;
                            result.RoomReference = item.RoomReference;
                            result.RoomType = item.RoomName;
                            result.TotalSellPrice = item.SellPrice.Value;
                            result.Adult = item.Adults.Value;
                            result.Child = item.Childern.Value;
                            result.IsRefundable = item.IsRefundable.Value;

                            var repeatedRoom = roomResults.Where(x => x.RoomReference == item.RoomReference).ToList();
                            if (repeatedRoom.Count < 1)
                                roomResults.Add(result);
                        }
                        //// get hotel 

                        HotelSearchRoom hotelSearchRoom = new HotelSearchRoom();

                        List<string> images = HImages.Select(x => x.URL).ToList();


                        List<HotelAmenity> amenties = (from A in hotelsDB.HotelsAmenities
                                                       where A.hotelID == Hid
                                                       select new HotelAmenity()
                                                       {
                                                           Amenity = A.amenitie,
                                                           HotelCode = A.hotelID,
                                                           status = A.Status
                                                       }).ToList();


                        var hotelSearchData = hotelsDB.hotels.FirstOrDefault(a => a.hotelID == Hid);
                        var hotelSearchpro = hotelsDB.hotelsProviders.FirstOrDefault(a => a.hotelID == hotelSearchData.ID.ToString() && a.providerID == pid);
                        var hotelSearchDesc = hotelsDB.HotelsDescriptions.FirstOrDefault(a => a.hotelID == Hid);

                        hotelSearchRoom.providerHotelID = hotelSearchpro.providerHotelID;
                        hotelSearchRoom.hotelCode = hotelSearchData.hotelID;
                        hotelSearchRoom.Address = hotelSearchData.address;
                        hotelSearchRoom.Lng = hotelSearchData.Lng;
                        hotelSearchRoom.Lat = hotelSearchData.Lat;

                        hotelSearchRoom.hotelStars = int.Parse(hotelSearchData.rating);//
                        hotelSearchRoom.hotelName = hotelSearchData.hotelName;
                        hotelSearchRoom.providerID = hotelSearchpro.providerID;
                        hotelSearchRoom.Location = hotelSearchData.location;

                        hotelSearchRoom.hotelDescription = hotelSearchDesc.Description1;
                        hotelSearchRoom.shortcutHotelDescription = hotelSearchDesc.Description2;
                        hotelSearchRoom.City = hotelSearchData.cityName;
                        hotelSearchRoom.Country = hotelSearchData.countryName;

                        var hotelAmenities = amenties.Where(a => a.HotelCode == Hid).Distinct().ToList();
                        foreach (var am in hotelAmenities)
                        {
                            am.Amenity = am.Amenity.Replace("  ", String.Empty);
                            if (hotelSearchRoom.Amenities.FirstOrDefault(x => x.Amenity == am.Amenity) == null)
                            {

                                hotelSearchRoom.Amenities.Add(am);
                            }
                        }

                        //  hotelSearchRoom.Amenities = ams;

                        hotelSearchRoom.hotelImages = images;
                        // searchDB to know list of each rooms 
                        if (pid == "4")
                        {
                            /*
                            List<RoomForSearch> searchRooms = new List<RoomForSearch>();
                            var roomsDetails = searchDB.SearchRoomDatas.Where(a => a.sID == Sid).ToList();
                            foreach (var item in roomsDetails)
                            {
                                SearchRoomData roomresult = new SearchRoomData();
                                roomresult = null;
                                var results = roomsDetails.Where(x => x.adultNo == item.adultNo && x.childernNo == item.childernNo).ToList();
                                foreach (var r in searchRooms)
                                {
                                    if (roomresult  == null )
                                    {
                                        roomresult = r.rooms.FirstOrDefault(a => a.adultNo == item.adultNo && a.childernNo == item.childernNo);
                                    }
                                }

                                if (roomresult == null)
                                {

                                        RoomForSearch roomFor = new RoomForSearch();
                                        roomFor.num = results.Count;
                                        roomFor.rooms = results;
                                        searchRooms.Add(roomFor);

                                }
                                //
                                //var ReqiredRooms=  roomResults.Where(x => x.Adult == item.adultNo && x.Child == item.childernNo).ToList();
                                //SeparatedRoom sr = new SeparatedRoom();
                                //  sr.RoomResults.AddRange(ReqiredRooms);
                                //   hotelSearchRoom.rooms.Add(sr);

                            }
                            foreach (var item in searchRooms)
                            {
                                var ReqiredRooms = roomResults.Where(x => x.Adult == item.rooms[0].adultNo && x.Child == item.rooms[0].childernNo).ToList();
                                if (item.num == 1)
                                {
                                    SeparatedRoom sr = new SeparatedRoom();
                                    sr.RoomResults.AddRange(ReqiredRooms);
                                    hotelSearchRoom.rooms.Add(sr);
                                }
                                else
                                {
                                    int roomNum = ReqiredRooms.Count / item.num;
                                    int skipNum = 0;
                                    for (int i = 0; i < item.num; i++)
                                    {
                                        SeparatedRoom sr = new SeparatedRoom();
                                        List<RoomResult> roomResultsData = new List<RoomResult>();
                                        //        var roomsReq = ReqiredRooms.Skip(skipNum).ToList();
                                        for (int j = skipNum; j < skipNum + roomNum; j++)
                                        {
                                            roomResultsData.Add(ReqiredRooms[j]);


                                        }
                                        // sr.RoomResults.AddRange(roomsReq.Take(roomNum).ToList());
                                        sr.RoomResults.AddRange(roomResultsData);
                                        skipNum = roomNum * (i + 1);
                                        hotelSearchRoom.rooms.Add(sr);
                                    }
                                }
                            }*/
                            var roomsDetails = searchDB.SearchRoomDatas.Where(a => a.sID == Sid).ToList();
                            for (int i = 0; i < roomsDetails.Count; i++)

                            {
                                SearchRoomData roomresult = new SearchRoomData();
                                roomresult = null;
                                var resultsR = roomResults.Where(x => x.Adult == roomsDetails[i].adultNo.Value && x.Child == roomsDetails[i].childernNo.Value).ToList();
                                SeparatedRoom srts = new SeparatedRoom();
                                srts.RoomResults.AddRange(resultsR);
                                hotelSearchRoom.rooms.Add(srts);

                            }
                        }
                        if (pid == "2")
                        {
                            var roomsDetails = searchDB.SearchRoomDatas.Where(a => a.sID == Sid).ToList();
                            for (int i = 0; i < roomsDetails.Count; i++)

                            {
                                SearchRoomData roomresult = new SearchRoomData();
                                roomresult = null;
                                var resultsR = roomResults.Where(x => x.RoomCode == (i + 1).ToString()).ToList();
                                SeparatedRoom srts = new SeparatedRoom();
                                srts.RoomResults.AddRange(resultsR);
                                hotelSearchRoom.rooms.Add(srts);

                            }
                        }
                        //  hotelSearchRoom.rooms= roomResults;
                        var minRoom = roomResults.Where(a => a.RatePerNight == roomResults.Min(x => x.RatePerNight)).FirstOrDefault();
                        if (minRoom != null)
                        {
                            hotelSearchRoom.hotelRate = minRoom.RatePerNight;
                            hotelSearchRoom.costPrice = minRoom.CostPrice;
                            hotelSearchRoom.TotalSellPrice = minRoom.TotalSellPrice;
                            hotelSearchRoom.MarkupId = minRoom.MarkupId;
                            hotelSearchRoom.MarkupValue = minRoom.MarkupValue;
                            hotelSearchRoom.DiscountId = minRoom.DiscountId;
                            hotelSearchRoom.DiscountValue = minRoom.DiscountValue;
                        }

                        return hotelSearchRoom;
                    }
                    return null;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static RequiredBookingData GetRoomsData(string sid, string hotel, string Pid, string rooms)
        {
            try
            {
                SearchDBEntities searchDB = new SearchDBEntities();

                RequiredBookingData requiredBooking = new RequiredBookingData();
                var data = searchDB.SearchCriterias.FirstOrDefault(a => a.sID == sid);
                var HRooms = searchDB.SearchRoomResults.Where(a => a.sID == sid && a.HotelCode == hotel && a.ProviderId.ToString() == Pid).ToList();
                requiredBooking.Currency = data?.currency;
                requiredBooking.City = data?.cityName;
                var RoomsCode = rooms.Split('-');
                foreach (var item in RoomsCode)
                {
                    var roomdata = HRooms.FirstOrDefault(a => a.RoomCode == item);

                    requiredBooking.rooms.Add(roomdata);

                }
                if (Pid == "4")
                {
                    hotelsDBEntities hotelsDB = new hotelsDBEntities();
                    var HData = hotelsDB.hotels.FirstOrDefault(d => d.hotelID == hotel);
                    var HDesc = hotelsDB.HotelsDescriptions.FirstOrDefault(x => x.hotelID == hotel);
                    var Hsearch = searchDB.SearchHotelResults.FirstOrDefault(x => x.HotelCode == hotel && x.sID == sid && x.ProviderID.ToString() == Pid);
                    var Himage = hotelsDB.HotelsImages.FirstOrDefault(v => v.HotelID == hotel);
                    requiredBooking.HotelName = HData?.hotelName;
                    requiredBooking.Hotelstar = int.Parse(HData?.rating);
                    requiredBooking.address = HData?.address;
                    requiredBooking.CheckIn = data?.dateFrom.Value.ToString();
                    requiredBooking.Checkout = data?.dateTo.Value.ToString();
                    requiredBooking.City = HData?.cityName;
                    requiredBooking.location = HData?.location;
                    requiredBooking.HotelDesc = HDesc?.Description1;
                    requiredBooking.providerHotelID = Hsearch?.ProviderHotelId;
                    requiredBooking.providerID = Pid;
                    //MG
                    requiredBooking.Lat = HData?.Lat;
                    requiredBooking.Lng = HData?.Lng;
                    if (Himage != null)
                        requiredBooking.hotelThumb = Himage.Thum;
                }
                else if (Pid == "5")
                {

                    TBOContext hotelsDB = new TBOContext();
                    var HData = hotelsDB.HotelDetails.FirstOrDefault(d => d.HotelCode == hotel);
                    var Hsearch = searchDB.SearchHotelResults.FirstOrDefault(x => x.HotelCode == hotel && x.sID == sid && x.ProviderID.ToString() == Pid);
                    var Himage = hotelsDB.HotelImages.FirstOrDefault(v => v.HotelCode == hotel);
                    Enum.TryParse(HData.HotelRating, out HotelStars stars);
                    requiredBooking.HotelName = HData?.HotelName;
                    requiredBooking.Hotelstar = (int)stars + 1;
                    requiredBooking.address = HData?.Address;
                    requiredBooking.CheckIn = data?.dateFrom.Value.ToString();
                    requiredBooking.Checkout = data?.dateTo.Value.ToString();
                    requiredBooking.City = HData?.CityName;
                    requiredBooking.location = HData?.HotelLocation;
                    requiredBooking.HotelDesc = HData.Description;
                    requiredBooking.providerHotelID = HData?.HotelCode;
                    requiredBooking.providerID = Pid;
                    //MG
                    requiredBooking.Lat = HData?.HotelLocation;
                    requiredBooking.Lng = HData?.Map;
                    if (Himage != null)
                        requiredBooking.hotelThumb = Himage.URL;

                    //instead of call pricing api 
                    //RoomResult roomResult = new RoomResult();
                    //using (HotelBookingDBEntities hotelBookingDB = new HotelBookingDBEntities())
                    //{
                    //    foreach (var item in requiredBooking.rooms)
                    //    {
                    //        var cancellations = hotelBookingDB.CancelPolicies.Where(x => x.HotelCode == hotel && x.Sid == sid && x.ProviderID.ToString() == Pid /*&& x.RoomCode ==int.Parse(item.RoomCode)*/).ToList();

                    //        //roomResult.HotelNorms = availRes.HotelCancellationPolicies?.HotelNorms;
                    //        //handel cancel policy 
                    //        foreach (var cancel in cancellations)
                    //        {
                    //            CancellationRule cancellation = new CancellationRule();
                    //            //DateTime dateFrom = Convert.ToDateTime(cancel.FromDate);
                    //            cancellation.FromDate = cancel.FromDate.ToString();// dateFrom.ToString("MMMM dd, yyyy");
                    //            cancellation.ToDate = cancel.ToDate.ToString();
                    //            cancellation.Cost = (double)cancel.Cost;
                    //            cancellation.ChargeType = cancel.ChargeType.ToString();
                    //            cancellation.Curency = cancel.Currency;
                    //            roomResult.cancellationRules.Add(cancellation);
                    //        }
                    //    }
                    //    requiredBooking.TBoRooms.Add(roomResult);
                    //}

                }

                return requiredBooking;


                //   requiredBooking.rooms = searchDB.SearchRoomResults.Where(a => RoomsCode.Contains(a.RoomCode) && a.sID == sid && a.HotelCode == hotel && a.ProviderId.ToString() == Pid).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<SearchRoomResult> GetRoomsForTraveller(string sid, string hotel, string Pid, List<string> RoomsCode)
        {
            SearchDBEntities searchDB = new SearchDBEntities();

            List<SearchRoomResult> requiredBooking = new List<SearchRoomResult>();
            requiredBooking = searchDB.SearchRoomResults.Where(a => RoomsCode.Contains(a.RoomCode) && a.sID == sid && a.HotelCode == hotel && a.ProviderId.ToString() == Pid).ToList();
            //requiredBooking = searchDB.SearchRoomDatas.Where(a => a.sID == sid).ToList();

            return requiredBooking;
        }
    }
}
