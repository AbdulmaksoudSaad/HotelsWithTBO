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
   public  class ResponseMapper
    {
        public List<SearchOutputData> MappingData(GraphQLResponse RoomsData ,string SessionID)
        {
            try
            {
                List<SearchOutputData> data = new List<SearchOutputData>();
                foreach (var item in RoomsData.Data.hotelX.search.options)
                {
                    var da = new SearchOutputData();
                    da.accessCode = item.accessCode;
                    da.boardCode = item.boardCode;
                    da.boardCodeSupplier = item.boardCodeSupplier;
                    da.cancelPolicy.refundable = item.cancelPolicy.refundable;
                    if (item.cancelPolicy.cancelPenalties != null)
                    {
                        foreach (var i in item.cancelPolicy.cancelPenalties)
                        {
                            var cancelData = new SMYROOMS.Model.CancelPenalty();
                            cancelData.hoursBefore = i.hoursBefore;
                            cancelData.value = i.value;
                            cancelData.currency = i.currency;
                            cancelData.penaltyType = i.penaltyType;
                            da.cancelPolicy.cancelPenalties.Add(cancelData);
                        }
                    }
                    else
                        da.cancelPolicy.cancelPenalties = null;
                    da.hotelCode = item.hotelCode;
                    da.hotelCodeSupplier = item.hotelCodeSupplier;
                    da.hotelName = item.hotelName;
                    da.id = item.id;
                    da.market = item.market;
                    foreach (var i in item.occupancies)
                    {
                        var ocupan = new SMYROOMS.Model.PaxBack();
                        ocupan.id = i.id;
                        foreach (var j in i.paxes)
                        {
                            var pax = new SMYROOMS.Model.Pax();
                            pax.age = j.age;
                            ocupan.paxes.Add(pax);
                        }
                        da.occupancies.Add(ocupan);
                    }
                    da.paymentType = item.paymentType;
                    da.price.binding = item.price.binding;
                    da.price.currency = item.price.currency;
                    da.price.gross = item.price.gross;
                    da.price.net = item.price.net;
                    //
                    if (item.price.exchange != null)
                    {
                        da.price.exchange.rate = item.price.exchange.rate;
                        da.price.exchange.currency = item.price.exchange.currency;
                    }
                    //
                    da.remarks = item.remarks;
                    foreach (var i in item.rooms)
                    {
                        var room = new SMYROOMS.Model.Room();
                        //room.beds.count = i.beds.count;
                        //room.beds.description = i.beds.description;
                        //room.beds.shared = i.beds.shared;
                        //room.beds.type = i.beds.type;
                        room.beds = null;
                        room.code = i.code;
                        room.description = i.description;
                        room.occupancyRefId = i.occupancyRefId;
                        room.refundable = false;
                        room.units = 0;
                        room.roomPrice.price.binding = i.roomPrice.price.binding;
                        room.roomPrice.price.currency = i.roomPrice.price.currency;
                        room.roomPrice.price.net = i.roomPrice.price.net;
                       room.roomPrice.price.gross = i.roomPrice.price.gross;
                        da.rooms.Add(room);

                    }
                    da.status = item.status;
                    if (item.supplements != null)
                    {
                        foreach (var i in item.supplements)
                        {
                            var supplement = new SMYROOMS.Model.Supplement();
                            supplement.effectiveDat = i.effectiveDat;
                            supplement.code = i.code;
                            supplement.description = i.description;
                            supplement.expireDate = i.expireDate;
                            supplement.mandatory = i.effectiveDat;
                            supplement.name = i.name;
                            supplement.quantity = i.quantity;
                            supplement.price.currency = i.price.currency;
                            supplement.price.net = i.price.net;
                            supplement.price.gross = i.price.gross;

                            da.supplements.Add(supplement);
                        }
                    }
                    else
                        da.supplements = null;
                    da.supplierCode = item.supplierCode;
                    if (item.surcharges != null)
                    {

                        foreach (var i in item.surcharges)
                        {
                            var surcharge = new SMYROOMS.Model.Surcharge();
                            surcharge.chargeType = i.chargeType;
                            surcharge.description = i.description;
                            surcharge.mandator = i.mandator;
                            surcharge.price.currency = i.price.currency;
                            surcharge.price.net = i.price.net;
                            surcharge.price.gross = i.price.gross;

                        }
                    }
                    else
                        da.surcharges = null;
                    da.token = item.token;
                    data.Add(da);
                }
                return data;
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggerHelper.WriteToFile("SMLogs/SearchResponseMapException", "SearchResponseMapException_" + SessionID, "SearchResponseMapException", requestData);
                throw;
            }
        }
    }
}
