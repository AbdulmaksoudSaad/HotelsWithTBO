using Newtonsoft.Json;
using SMYROOMS.Helpers;
using SMYROOMS.Model;
using SMYROOMS.TableMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.DB
{
    public class BookingDataEntry
    {
        DB_Connection db = new DB_Connection();
        public async void SaveSMRBooking(Bookingback result, string SessionId)
        {
            try
            {
                db.DB_OpenConnection();
                DataTable dt_BookingRate = new DataTable();
                dt_BookingRate = CreateBookingTable.CreateBookingTB();
                DataRow dr = dt_BookingRate.NewRow();
                dr["supplier"] = result.reference.supplier;
                dr["client"] = result.reference.client;
                dr["SessionId"] = SessionId;
                if (result.holder != null)
                {
                    dr["HolderName"] = result.holder.name;
                    dr["HolderSerName"] = result.holder.surname;
                }
                dr["CurrencyPrice"] = result.price.currency;
                dr["Net"] = result.price.net;
                dr["Gross"] = result.price.gross;
                dr["BindingPrice"] = result.price.binding;
                dr["CurrencyExchange"] = result.price.exchange.currency;
                dr["CurrencyRate"] = result.price.exchange.rate;
                dr["CancelRefundable"] = result.cancelPolicy.refundable;
                dr["Status"] = result.status;
                dr["Payabe"] = result.payable;
                dr["Remark"] = result.remarks;
                if (result.hotel != null)
                {
                    dr["CreationDate"] = result.hotel.creationDate;
                    dr["HotelCode"] = result.hotel.hotelCode;
                    dr["CheckIN"] = result.hotel.checkIn;
                    dr["CheckOut"] = result.hotel.checkOut;
                    dr["BoardCode"] = result.hotel.boardCode;
                }
                dt_BookingRate.Rows.Add(dr);
                // consume sp to get bookingid
                int bookingID = await db.SaveBooking_Async(dt_BookingRate);
                DataTable dt_Room = new DataTable();
                dt_Room = CreateBookingTable.CreateRoomBookingTB();
                if (result.hotel != null)
                {
                    foreach (var item in result.hotel.rooms)
                    {
                        DataRow drr = dt_Room.NewRow();
                        drr["occupancyRefId"] = item.occupancyRefId;
                        drr["code"] = item.code;
                        drr["description"] = item.description;
                        drr["CurrencyPrice"] = item.price.currency;
                        drr["NetPrice"] = item.price.net;
                        drr["GrossPrice"] = item.price.gross;
                        drr["Binding"] = item.price.binding;
                        drr["ExchangeCurrency"] = item.price.exchange.currency;
                        drr["ExchangeRate"] = item.price.exchange.rate;
                        drr["BookingID"] = bookingID;
                        dt_Room.Rows.Add(drr);
                    }
                    await db.SaveBookingRoom_Async(dt_Room);
                }
                // 
                DataTable dt_Policy = new DataTable();
                dt_Policy = CreateBookingTable.CreatePolicyBookingTB();
                if (result.cancelPolicy.cancelPenalties != null)
                {
                    foreach (var item in result.cancelPolicy.cancelPenalties)
                    {
                        DataRow drp = dt_Policy.NewRow();
                        drp["Currency"] = item.currency;
                        drp["Type"] = item.penaltyType;
                        drp["Value"] = item.value;
                        drp["hoursBefore"] = item.hoursBefore;
                        drp["BookingId"] = bookingID;

                        dt_Policy.Rows.Add(drp);
                    }

                    await db.SavePolicyBooking_Async(dt_Policy);
                }
                if (result.hotel != null)
                {
                    foreach (var R in result.hotel.occupancies)
                    {
                        var roomid = await db.SavePaxROOMBooking_Async(R.id, bookingID);
                        DataTable dt_pax = new DataTable();
                        dt_pax = CreateBookingTable.CreatePaxesBookingTB();
                        foreach (var pax in R.paxes)
                        {
                            DataRow drx = dt_pax.NewRow();
                            drx["Age"] = pax.age;
                            drx["Room_Id"] = roomid;
                            dt_pax.Rows.Add(drx);
                        }
                        await db.SavePaxesBooking_Async(dt_pax);
                    }
                }

                //
                db.DB_CloseConnection();
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggerHelper.WriteToFile("c:/HotelsB2C/Logs/SMLogs/BookingDBException", "BookingDBException_" + SessionId, "BookingDBException", requestData);
                throw;
            }
        }

    }
}
