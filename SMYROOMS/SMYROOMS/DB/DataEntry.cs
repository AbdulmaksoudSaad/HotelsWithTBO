using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SMYROOMS.Helpers;
using SMYROOMS.Model;
using SMYROOMS.TableMapper;

namespace SMYROOMS.DB
{
   public class DataEntry
    {
        DB_Connection db= new DB_Connection();
        public async void SaveSMRHotelRates(List<SearchOutputData> result, string SessionId)
        {
            try
            {
                db.DB_OpenConnection();
                DataTable dt_Hotel = new DataTable();
                dt_Hotel = CreateTable.CreateHotelRate();
                DataTable dt_Room = new DataTable();
                dt_Room = CreateTable.CreateRoomTable();
                DataTable dt_Policy = new DataTable();
                dt_Policy = CreateTable.CreatePolicyTable();
                //var Hotels = result.Hotels.Hotel;
                for (int i = 0; i < result.Count; i++)
                {
                    DataRow dr = dt_Hotel.NewRow();
                    dr["Ref_ID"] = result[i].id;
                    dr["supplierCode"] = result[i].supplierCode;
                    dr["accessCode"] = result[i].accessCode;
                    dr["market"] = result[i].market;
                    dr["hotelCode"] = result[i].hotelCode;
                    dr["hotelCodeSupplier"] = result[i].hotelCodeSupplier;
                    dr["boardCode"] = result[i].boardCode;
                    dr["boardCodeSupplier"] = result[0].boardCodeSupplier;
                    dr["Token"] = result[i].token;
                    dr["remarks"] = result[i].remarks;
                    dr["paymentType"] = result[i].paymentType;
                    dr["status"] = result[i].status;
                    dr["Currency"] = result[i].price.currency;
                    dr["net"] = result[i].price.net;
                    dr["gross"] =result[i].price.gross;

                    dr["sessionId"] = SessionId;
                    dr["PolicyRefundable"] = result[i].cancelPolicy.refundable;
                    dt_Hotel.Rows.Add(dr);
                    for (int j = 0; j < result[i].rooms.Count; j++)
                    {
                        DataRow drr = dt_Room.NewRow();
                        drr["occupancyRefId"] = result[i].rooms[j].occupancyRefId;
                        drr["code"] = result[i].rooms[j].code;
                        drr["description"] = result[i].rooms[j].description;
                        drr["units"] = result[i].rooms[j].units;
                        drr["currency"] = result[i].rooms[j].roomPrice.price.currency;
                        drr["netPrice"] = result[i].rooms[j].roomPrice.price.net;
                        drr["grossPrice"] = result[i].rooms[j].roomPrice.price.gross;
                        drr["currencyChange"] = result[i].rooms[j].roomPrice.price.exchange.currency;
                        drr["rateChange"] = result[i].rooms[j].roomPrice.price.exchange.rate;
                        if (result[i].rooms[j].beds == null)
                        {
                            drr["BedDescription"] = "no des";
                            drr["BedType"] = "unknown";
                            drr["Bedcount"] = 0;
                            drr["Bedshared"] = "unknown";
                        }
                        else
                        {
                            drr["BedDescription"] = result[i].rooms[j].beds.description;
                            drr["BedType"] = result[i].rooms[j].beds.type;
                            drr["Bedcount"] = result[i].rooms[j].beds.count;
                            drr["Bedshared"] = result[i].rooms[j].beds.shared;
                        }
                        drr["refundable"] = result[i].rooms[j].refundable;
                        drr["HotelRateID"] = result[i].id;

                        drr["sessionId"] = SessionId;
                        dt_Room.Rows.Add(drr);
                    }
                    if (result[i].cancelPolicy.cancelPenalties != null)
                    {
                        for (int j = 0; j < result[i].cancelPolicy.cancelPenalties.Count; j++)
                        {
                            DataRow drp = dt_Policy.NewRow();
                            drp["Currency"] = result[i].cancelPolicy.cancelPenalties[j].currency;
                            drp["Type"] = result[i].cancelPolicy.cancelPenalties[j].penaltyType;
                            drp["Value"] = result[i].cancelPolicy.cancelPenalties[j].value;
                            drp["hoursBefore"] = result[i].cancelPolicy.cancelPenalties[j].hoursBefore;
                            drp["HotelRateID"] = result[i].id;
                            drp["SessionID"] = SessionId;

                            dt_Policy.Rows.Add(drp);
                        }
                    }
                }
                // db.Save("delete from tmpDotwHotelsRates where sessionID =" + "'" + searchCriteria.sID + "'"); //delete repeated data 
                var ff = dt_Hotel.Rows[0];
                await db.SaveTable_Async(dt_Hotel);
                await db.SaveRooms_Async(dt_Room);
                await db.SavePolicy_Async(dt_Policy);
                db.DB_CloseConnection();

            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);
                LoggerHelper.WriteToFile("c:/HotelsB2C/Logs/SMLogs/SearchDBexception", "SearchDBException_" + SessionId, "SearchDBException", requestData);
                throw;
            }
        }
           public async  Task<List<Room>> GEtRoomData(string HotelID,string SessionId)
        {
            List<Room> rooms = new  List<Room>();
            rooms = await db.GETRoomDetails(HotelID, SessionId);
            return rooms;
        }
        public async Task<CancelPolicy> GEtPolicyData(string HotelID, string SessionId)
        {
            CancelPolicy cancelPolicy = new CancelPolicy();
            cancelPolicy = await db.GETPolicyDetails(HotelID, SessionId);
            return cancelPolicy;
        }

    }
}
