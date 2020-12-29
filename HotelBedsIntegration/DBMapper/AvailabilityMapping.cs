using HotelBedsIntegration.Helper;
using HotelBedsIntegration.Models;
using HotelBedsIntegration.Models.Availability;
using Hotels.Common.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBedsIntegration.DBMapper
{
    class AvailabilityMapping
    {
        public static void MapRequestToDB(AvailabilityReq value, string SessionID)
        {
            try
            {
                HotelBedEntity db = new HotelBedEntity();
                foreach (var item in value.rooms)
                {
                    AvailabilityRequest request = new AvailabilityRequest();
                    request.SessionID = SessionID;
                    request.rate = item.rateKey;
                    db.AvailabilityRequests.Add(request);
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggingHelper.WriteToFile("HBLogs/AvailabilityException", "AvailabilityException_" + SessionID, "AvailabilityException", requestData);

            }
        }
        public static void MapResponseToDB(AvailabilityRes value, string SessionID)
        {
            try
            {

                HotelBedEntity db = new HotelBedEntity();
                AvailabilityResponse availability = new AvailabilityResponse();
                availability.Currency = value?.hotel?.currency;
                availability.TotalPrice = value?.hotel?.totalNet;
                availability.response = Newtonsoft.Json.JsonConvert.SerializeObject(value?.hotel);
                availability.SearchID = SessionID;
                var hotel = db.SearchHotelDatas.FirstOrDefault(a => a.SessionID == SessionID);
                availability.SearchHotelID = hotel?.Id;
                db.AvailabilityResponses.Add(availability);
                //db.SaveChanges();   XX
                if (value?.hotel?.rooms != null)
                {

                    foreach (var room in value?.hotel?.rooms)
                    {

                        foreach (var rate in room?.rates)
                        {
                            SearchRoom searchRoom = new SearchRoom();
                            searchRoom.adults = rate.adults;
                            searchRoom.boardCode = rate.boardCode;
                            searchRoom.boardName = rate.boardName;
                            searchRoom.children = rate.children;
                            searchRoom.childrenAges = rate.childrenAges;
                            searchRoom.hotelMandatory = rate.hotelMandatory.ToString();
                            searchRoom.netPrice = rate.net;
                            searchRoom.packaging = rate.packaging.ToString();
                            searchRoom.paymentType = rate.paymentType;
                            searchRoom.rateClass = rate.rateClass;
                            searchRoom.rateKey = rate.rateKey;
                            searchRoom.rateType = rate.rateType;
                            searchRoom.ResponseType = "availability";
                            searchRoom.RoomCode = room?.code;
                            searchRoom.RoomName = room?.name;
                            searchRoom.rooms = rate.rooms;
                            searchRoom.SearchHotelID = availability.ID;
                            searchRoom.sessionID = SessionID;
                            searchRoom.sellingRate = rate.sellingRate;
                            db.SearchRooms.Add(searchRoom);
                            //db.SaveChanges(); XX
                            foreach (var p in rate.cancellationPolicies)
                            {
                                RoomPolicy policy = new RoomPolicy();
                                policy.amount = p.amount;
                                policy.fromDate = p.from.ToString(); ;
                                policy.RoomID = searchRoom.Id;
                                policy.CallingType = "check";
                                db.RoomPolicies.Add(policy);
                            }
                            //db.SaveChanges(); XX

                        }
                    }
                }

                //MG
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggingHelper.WriteToFile("HBLogs/AvailabilityException", "AvailabilityException_" + SessionID, "AvailabilityException", requestData);

            }
        }
    }
}
