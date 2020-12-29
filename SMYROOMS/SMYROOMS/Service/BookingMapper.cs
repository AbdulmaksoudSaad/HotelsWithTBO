using GraphQL.Common.Response;
using Newtonsoft.Json;
using SMYROOMS.Helpers;
using SMYROOMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Service
{
   public class BookingMapper
    {
        public Bookingback MapResponseofBooking(dynamic OutPutData ,string sessionId)
        {
            try
            {
                var BackData = new Bookingback();
                BackData.reference.client = OutPutData.reference.client;
                BackData.reference.supplier = OutPutData.reference.supplier;
                if (OutPutData.holder == null)
                    BackData.holder = null;
                else
                {
                    BackData.holder.name = OutPutData.holder.name;
                    BackData.holder.surname = OutPutData.holder.surname;
                }
                if (OutPutData.hotel == null)
                    BackData.hotel = null;
                else
                {
                    BackData.hotel.boardCode = OutPutData.hotel.boardCode;
                    BackData.hotel.checkIn = OutPutData.hotel.checkIn;
                    BackData.hotel.checkOut = OutPutData.hotel.checkOut;
                    BackData.hotel.creationDate = OutPutData.hotel.creationDate;
                    BackData.hotel.hotelCode = OutPutData.hotel.hotelCode;
                    BackData.hotel.hotelName = OutPutData.hotel.hotelName;
                    if (OutPutData.hotel.rooms != null)
                    {
                        foreach (var item in OutPutData.hotel.rooms)
                        {
                            BookingRoom room = new BookingRoom();
                            room.code = item.code;
                            room.description = item.description;
                            room.occupancyRefId = item.occupancyRefId;
                            room.price.currency = item.price.currency;
                            room.price.net = item.price.net;
                            room.price.gross = item.price.gross;
                            room.price.binding = item.price.binding;
                            room.price.exchange.currency = item.price.exchange.currency;
                            room.price.exchange.rate = item.price.exchange.rate;
                            BackData.hotel.rooms.Add(room);
                        }
                    }

                    if (OutPutData.hotel.occupancies != null)
                    {
                        foreach (var p in OutPutData.hotel.occupancies)
                        {
                            PaxBack roomPax = new PaxBack();
                            roomPax.id = p.id;
                            foreach (var x in p.paxes)
                            {
                                Pax pax = new Pax();
                                pax.age = x.age;
                                roomPax.paxes.Add(pax);
                            }
                            BackData.hotel.occupancies.Add(roomPax);
                        }
                    }
                }
                BackData.price.currency = OutPutData.price.currency;
                BackData.price.net = OutPutData.price.net;
                BackData.price.gross = OutPutData.price.gross;
                BackData.status = OutPutData.status;
                BackData.remarks = OutPutData.remarks;
                BackData.payable = OutPutData.payable;

                if (OutPutData.cancelPolicy != null)
                {
                    BackData.cancelPolicy.refundable = OutPutData.cancelPolicy.refundable;
                    foreach (var c in OutPutData.cancelPolicy.cancelPenalties)
                    {
                        CancelPenalty cancelPenalty = new CancelPenalty();
                        cancelPenalty.currency = c.currency;
                        cancelPenalty.hoursBefore = c.hoursBefore;
                        cancelPenalty.value = c.value;
                        cancelPenalty.penaltyType = c.penaltyType;
                        BackData.cancelPolicy.cancelPenalties.Add(cancelPenalty);
                    }
                }


                return BackData;
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggerHelper.WriteToFile("c:/HotelsB2C/Logs/SMLogs/BookingMapException", "BookingMapException_" + sessionId, "BookingMapException", requestData);
                throw;
            }
        }
    }
}
