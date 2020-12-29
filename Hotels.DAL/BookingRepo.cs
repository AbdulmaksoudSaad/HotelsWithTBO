using Hotels.Common;
using Hotels.Common.DB;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.DAL
{
    public class BookingRepo
    {

        public static void SaveBookingResult(CheckOutData BookingResults, string BNNum)
        {
            List<int> rooms = new List<int>();

            try
            {
                var BaseCur = ConfigurationSettings.AppSettings["BaseCur"];
               
                foreach (var item in BookingResults.Travellers)
                {
                    if (!rooms.Contains(item.roomNo))
                    {
                        rooms.Add(item.roomNo);
                    }
                }
                using (DBConnection db = new DBConnection())
                {
                    db.DB_OpenConnection("HB");
                    SearchDBEntities searchDB = new SearchDBEntities();

                    #region old code save use SP
                    //  hotel rates
                    //DataTable dt = new DataTable();
                    //DataColumn SessionId = new DataColumn("SessionId");
                    //DataColumn Booking_No = new DataColumn("Booking_No");
                    //DataColumn City = new DataColumn("City");
                    //DataColumn HotelId = new DataColumn("HotelId");
                    //DataColumn HotelPId = new DataColumn("HotelPId");
                    //DataColumn Rooms_Qty = new DataColumn("Rooms_Qty");
                    //DataColumn Pax_Qty = new DataColumn("Pax_Qty");
                    //DataColumn Booking_Status = new DataColumn("Booking_Status");
                    //DataColumn Customer_ID = new DataColumn("Customer_ID");
                    //DataColumn Provider_ID = new DataColumn("Provider_ID");
                    //DataColumn Sell_Price = new DataColumn("Sell_Price");
                    //DataColumn Sell_Currency = new DataColumn("Sell_Currency");
                    //DataColumn Booking_Phone_Code = new DataColumn("Booking_Phone_Code");
                    //DataColumn Booking_Phone = new DataColumn("Booking_Phone");
                    //DataColumn Booking_Email = new DataColumn("Booking_Email");
                    //DataColumn Pax_Name = new DataColumn("Pax_Name");
                    //DataColumn Foreign_Amount = new DataColumn("Foreign_Amount");
                    //DataColumn Total_Cost_main = new DataColumn("Total_Cost_main");
                    //DataColumn Bookingsrc = new DataColumn("src");
                    //Rooms_Qty.DataType = typeof(int);
                    //Pax_Qty.DataType = typeof(int);

                    //Sell_Price.DataType = typeof(double);
                    //Foreign_Amount.DataType = typeof(double);
                    //Total_Cost_main.DataType = typeof(int);


                    //dt.Columns.Add(SessionId);
                    //dt.Columns.Add(Booking_No);
                    //dt.Columns.Add(City);
                    //dt.Columns.Add(HotelId);
                    //dt.Columns.Add(HotelPId);
                    //dt.Columns.Add(Rooms_Qty);
                    //dt.Columns.Add(Pax_Qty);
                    //dt.Columns.Add(Booking_Status);
                    //dt.Columns.Add(Customer_ID);
                    //dt.Columns.Add(Provider_ID);
                    //dt.Columns.Add(Sell_Price);
                    //dt.Columns.Add(Sell_Currency);
                    //dt.Columns.Add(Booking_Phone_Code);
                    //dt.Columns.Add(Booking_Phone);
                    //dt.Columns.Add(Booking_Email);
                    //dt.Columns.Add(Pax_Name);
                    //dt.Columns.Add(Foreign_Amount);
                    //dt.Columns.Add(Total_Cost_main);
                    //dt.Columns.Add(Bookingsrc);
                    //// room result 

                    //DataTable dr = new DataTable();
                    //DataColumn rsID = new DataColumn("sID");
                    //DataColumn rProviderID = new DataColumn("ProviderId");

                    //DataColumn rBooking_No = new DataColumn("Booking_No");
                    //DataColumn rRoomNo = new DataColumn("RoomNo");

                    //DataColumn rRoomCat = new DataColumn("RoomCat");
                    //DataColumn rRoomType = new DataColumn("RoomType");
                    //DataColumn rMeal = new DataColumn("Meal");
                    //DataColumn rPaxesQty = new DataColumn("PaxesQty");
                    //DataColumn rRefund = new DataColumn("Refund");


                    //rProviderID.DataType = typeof(int);
                    //rRoomNo.DataType = typeof(int);

                    //rPaxesQty.DataType = typeof(int);


                    //dr.Columns.Add(rsID);
                    //dr.Columns.Add(rProviderID);
                    //dr.Columns.Add(rBooking_No);
                    //dr.Columns.Add(rRoomNo);

                    //dr.Columns.Add(rRoomCat);
                    //dr.Columns.Add(rRoomType);
                    //dr.Columns.Add(rMeal);
                    //dr.Columns.Add(rPaxesQty);
                    //dr.Columns.Add(rRefund);

                    //DataTable dn = new DataTable();
                    //DataColumn nBooking_No = new DataColumn("Booking_No");
                    //DataColumn nNdate = new DataColumn("Ndate");

                    //DataColumn nrate = new DataColumn("rate");
                    //DataColumn nCurrency = new DataColumn("Currency");

                    //DataColumn nRoomNo = new DataColumn("Room_No");
                    //DataColumn nExchangeRate = new DataColumn("ExchangeRate");



                    //nNdate.DataType = typeof(DateTime);
                    //nrate.DataType = typeof(double);
                    //nRoomNo.DataType = typeof(int);
                    //nExchangeRate.DataType = typeof(double);


                    //dn.Columns.Add(nBooking_No);
                    //dn.Columns.Add(nNdate);
                    //dn.Columns.Add(nrate);
                    //dn.Columns.Add(nCurrency);

                    //dn.Columns.Add(nRoomNo);
                    //dn.Columns.Add(nExchangeRate);

                    //DataTable dp = new DataTable();
                    //DataColumn pBooking_No = new DataColumn("Booking_No");
                    //DataColumn psID = new DataColumn("sID");

                    //DataColumn pRoomNo = new DataColumn("RoomNo");
                    //DataColumn ppaxtype = new DataColumn("paxtype");

                    //DataColumn pFirstName = new DataColumn("FirstName");
                    //DataColumn pLastName = new DataColumn("LastName");
                    //DataColumn saluation = new DataColumn("saluation");

                    //DataColumn pBD = new DataColumn("BD");
                    //DataColumn pNationality = new DataColumn("Nationality");

                    //DataColumn pphone = new DataColumn("phone");
                    //DataColumn pphoneCode = new DataColumn("phoneCode");
                    //DataColumn PPaxNo = new DataColumn("PaxNo");
                    //DataColumn PRoomRef = new DataColumn("Ref");


                    //pRoomNo.DataType = typeof(int);

                    //PPaxNo.DataType = typeof(int);
                    //PRoomRef.DataType = typeof(int);
                    //dp.Columns.Add(psID);
                    //dp.Columns.Add(pBooking_No);
                    //dp.Columns.Add(pRoomNo);
                    //dp.Columns.Add(ppaxtype);
                    //dp.Columns.Add(pFirstName);
                    //dp.Columns.Add(pLastName);
                    //dp.Columns.Add(saluation);
                    //dp.Columns.Add(pBD);
                    //dp.Columns.Add(pNationality);
                    //dp.Columns.Add(pphone);
                    //dp.Columns.Add(pphoneCode);
                    //dp.Columns.Add(PPaxNo);
                    //dp.Columns.Add(PRoomRef);




                    //DataRow DtHr = dt.NewRow();
                    //DtHr["SessionId"] = BookingResults.Sid;
                    //DtHr["Booking_No"] = BNNum;//////////*********************
                    //DtHr["City"] = BookingResults.CityName;
                    //DtHr["HotelId"] = BookingResults.HotelID;
                    //DtHr["HotelPId"] = BookingResults.ProviderHotelID;
                    //DtHr["Rooms_Qty"] = int.Parse(BookingResults.RoomQty);
                    //DtHr["Pax_Qty"] = BookingResults.Travellers.Count;
                    //DtHr["Booking_Status"] = "NewBooking"; 
                    #endregion

                    HotelBookingDBEntities hotelBookingDB = new HotelBookingDBEntities();
                    var traveller = BookingResults.Travellers.FirstOrDefault(a => a.Main == true);

                    var customer = hotelBookingDB.Customers.FirstOrDefault(a => a.Email == BookingResults.Mail);
                    var CusID = 0;
                    if (customer != null)
                    {
                        //DtHr["Customer_ID"] = customer.ID;
                        CusID= customer.ID;
                    }
                    else
                    {
                        Customer cus = new Customer();
                        cus.Country = traveller.nationality;
                        cus.Date_Of_Birth = traveller.DateOfBirth;
                        cus.Email = BookingResults.Mail;
                        cus.First_Name = traveller.firstName;
                        cus.Last_Name = traveller.lastName;
                        cus.Phone = traveller.phone;
                        cus.Phone_Code = traveller.phoneCode;
                        cus.Salutations = traveller.salutation;
                        hotelBookingDB.Customers.Add(cus);
                        hotelBookingDB.SaveChanges();
                        //DtHr["Customer_ID"] = cus.ID;
                        CusID = cus.ID;

                    }

                    //DtHr["Provider_ID"] = BookingResults.Pid;
                    //DtHr["Sell_Price"] = BookingResults.SellPrice;
                    //DtHr["Sell_Currency"] = BookingResults.Currency;
                    //DtHr["Booking_Phone_Code"] = traveller.phoneCode;
                    //DtHr["Booking_Phone"] = traveller.phone;
                    //DtHr["Booking_Email"] = BookingResults.Mail;
                    traveller.lastName = traveller.lastName.Replace(" ", String.Empty);

                    //DtHr["Pax_Name"] = traveller.firstName + " " + traveller.lastName;
                    //DtHr["Foreign_Amount"] = BookingResults.totalCost;
                    //DtHr["Total_Cost_main"] = BookingResults.totalCost;
                    //DtHr["src"] = BookingResults.Src;
                    //TBO 

                    HotelsBooking hotelsBooking = new HotelsBooking
                    {
                        //Booking_Conf =
                        Booking_Email = BookingResults.Mail,
                        Booking_No = BNNum,
                        Booking_phone = traveller.phone,
                        Booking_Phone_Code = traveller.phoneCode,
                        Booking_Status = "NewBooking",
                        Booking_Time = DateTime.UtcNow,
                        City = BookingResults.CityName,
                        Customer_ID = CusID.ToString(),
                        Foreign_Amount = BookingResults.totalCost,
                        //Form_Of_Payment
                        //HotelConfirmationNo
                        HotelProviderID = BookingResults.ProviderHotelID,
                        Hotel_ID = BookingResults.HotelID,
                        //InvoicePdf
                        //NotificationKey
                        Pax_Name = traveller.firstName + " " + traveller.lastName,
                        Pax_Qty = BookingResults.Travellers.Count,
                        //PromoCode_Amount
                        Provider_ID = BookingResults.Pid,
                        Rooms_Qty = int.Parse(BookingResults.RoomQty),
                        Sell_Currency = BookingResults.Currency,
                        Sell_Price = BookingResults.SellPrice,
                        Total_Cost_Main_Currency = BookingResults.totalCost,
                        SessionId = BookingResults.Sid,
                        Sales_Channel = BookingResults.Src
                    };
                    hotelBookingDB.HotelsBookings.Add(hotelsBooking);

                    //


                    //dt.Rows.Add(DtHr);
                    var searchData = searchDB.SearchCriterias.FirstOrDefault(a => a.sID == BookingResults.Sid);
                    int duration = Convert.ToInt32((searchData.dateTo - searchData.dateFrom).Value.TotalDays);
                    var roomsDB = searchDB.SearchRoomResults.Where(a => a.sID == BookingResults.Sid && a.HotelCode == BookingResults.HotelID);
                    var Bookingrooms = BookingResults.Travellers.Select(x => x.roomRef).ToList().Distinct();
                    foreach (var item in Bookingrooms)
                    {
                        var specificRoom = BookingResults.Travellers.FirstOrDefault(x => x.roomRef == item);
                        var roomdata = roomsDB.FirstOrDefault(a => a.RoomCode == specificRoom.roomNo.ToString() && a.HotelCode == BookingResults.HotelID);
                        HotelBookingRoom room = new HotelBookingRoom
                        {
                            Booking_No= BNNum,
                            Meal = roomdata.meal,
                            Paxs_Qty = roomdata.PaxSQty,
                            PID = int.Parse(BookingResults.Pid),
                            //Room_Category = roomdata.RoomName,
                            Room_No = specificRoom.roomNo,
                            //Room_Type = roomdata.roomType,
                            SID = BookingResults.Sid,
                        };
                        hotelBookingDB.HotelBookingRooms.Add(room);
                        //DataRow DRR = dr.NewRow();
                        //DRR["sID"] = BookingResults.Sid;
                        //DRR["ProviderId"] = int.Parse(BookingResults.Pid);//////////
                        //DRR["Booking_No"] = BNNum;////////////////////////////*************
                        //DRR["RoomNo"] = specificRoom.roomNo;
                        //DRR["RoomCat"] = roomdata.RoomName;
                        //DRR["RoomType"] = roomdata.roomType;
                        //DRR["Meal"] = roomdata.meal;
                        //DRR["PaxesQty"] = roomdata.PaxSQty;
                        ////  DRR["Refund"] = "";//////////////
                        //dr.Rows.Add(DRR);
                        for (int j = 0; j < duration; j++)
                        {
                            HotelBookingNight night = new HotelBookingNight
                            {
                                Booking_No = BNNum,
                                Currency = roomdata.SellCurrency,
                                NightDate = searchData.dateFrom.Value.AddDays(j + 1),
                                Rate = roomdata.SellPrice.Value / duration,
                                Room_No = specificRoom.roomNo
                            };
                            hotelBookingDB.HotelBookingNights.Add(night);
                            //DataRow DrN = dn.NewRow();
                            //DrN["Booking_No"] = BNNum;//***************************
                            //DrN["Ndate"] = searchData.dateFrom.Value.AddDays(j + 1);
                            //DrN["rate"] = roomdata.SellPrice.Value / duration;
                            //DrN["Currency"] = roomdata.SellCurrency;
                            //DrN["Room_No"] = specificRoom.roomNo;


                            //dn.Rows.Add(DrN);

                        }
                        HotelBookingRoomsStatu roomsStatus = new HotelBookingRoomsStatu();
                        roomsStatus.Booking_No = BNNum;
                        roomsStatus.Room_No = specificRoom.roomNo;
                        roomsStatus.Room_Status = 1;
                        hotelBookingDB.HotelBookingRoomsStatus.Add(roomsStatus);
                    }

                    foreach (var item in BookingResults.Travellers)
                    {
                        HotelBookingPax pax = new HotelBookingPax
                        {
                            Booking_No = BNNum,
                            DateOfBirth = item.DateOfBirth.ToString(),
                            First_name = item.firstName,
                            Last_Name = item.lastName,
                            Nationality = item.nationality,
                            PaxNo = item.TravellerId,
                            Pax_Type = item.paxType,
                            Phone = item.phone,
                            Phone_Code = item.phoneCode,
                            RoomRef = item.roomRef,
                            Room_No = item.roomNo,  //room index
                            Salutations = item.salutation,
                            SID = BookingResults.Sid,
                            Lead = item.Main
                        };
                        hotelBookingDB.HotelBookingPaxs.Add(pax);
                        //DataRow Dpri = dp.NewRow();
                        //Dpri["sID"] = BookingResults.Sid;
                        //Dpri["Booking_No"] = BNNum;//////////****************
                        //Dpri["RoomNo"] = item.roomNo;////////////////////////////
                        //Dpri["paxtype"] = item.paxType;
                        //Dpri["FirstName"] = item.firstName;
                        //Dpri["LastName"] = item.lastName;
                        //Dpri["saluation"] = item.salutation;
                        //Dpri["BD"] = item.DateOfBirth.ToString();
                        //Dpri["Nationality"] = item.nationality;
                        //Dpri["phone"] = item.phone;
                        //Dpri["phoneCode"] = item.phoneCode;
                        //Dpri["PaxNo"] = item.TravellerId;
                        //Dpri["Ref"] = item.roomRef;
                        //dp.Rows.Add(Dpri);
                    }
                    HotelBookingStatu bookingStatus = new HotelBookingStatu();
                    bookingStatus.Booking_No = BNNum;
                    bookingStatus.Booking_Status = "New Booking";
                    bookingStatus.Status_Time = DateTime.Now;
                    hotelBookingDB.HotelBookingStatus.Add(bookingStatus);

                    //Dictionary<string, object> keyValues = new Dictionary<string, object>();
                    //keyValues.Add("Bookindata", dt);
                    //keyValues.Add("RoomList", dr);
                    //keyValues.Add("nigtData", dn);
                    //keyValues.Add("paxes", dp);

                   // db.SaveSP_Async("SaveBookingResult", keyValues);

                    hotelBookingDB.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("SaveBookingController/Errors/", "SaveSearchResult_" + BookingResults.Sid, "", ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);
                throw ex;
            }
        }

        public static string GetBookingNumber(string sid, string Src, string Pid)
        {
            try
            {
                SearchDBEntities searchDB = new SearchDBEntities();

                //     HotelBookingDBEntitiy dB = new HotelBookingDBEntitiy();
                var searchData = searchDB.SearchCriterias.FirstOrDefault(a => a.sID == sid);
                if (searchData != null)
                {
                    string BookingNum;
                    string connstring = ConfigurationSettings.AppSettings["HB_CS"];
                    string BookNoPrefix = ConfigurationSettings.AppSettings["BookNoPrefix"];

                    using (SqlConnection conn = new SqlConnection(connstring))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("CreateBookingNum", conn);
                        // 2. set the command object so it knows to execute a stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        // execute the command

                        cmd.Parameters.Add(new SqlParameter("Bn", BookNoPrefix));//  3 
                        cmd.Parameters.Add(new SqlParameter("User", sid));
                        cmd.Parameters.Add(new SqlParameter("lang", searchData.language));
                        cmd.Parameters.Add(new SqlParameter("pos", searchData.pos));
                        cmd.Parameters.Add(new SqlParameter("sc", Src));
                        cmd.Parameters.Add(new SqlParameter("pid ", Pid));
                        cmd.Parameters.Add(new SqlParameter("status", "Bending"));

                        cmd.Parameters.Add("@newBN", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        // read output value from @NewId
                        string BookingNumberId = Convert.ToString(cmd.Parameters["@newBN"].Value);
                        return BookingNumberId;
                    }


                }
                return null;
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("SaveBookingController/Errors/", "SaveController", "GetBookingNumber" + sid, ex.InnerException?.Message + "//" + ex.Message + ex.StackTrace);

                return null;
            }
        }
        public static string checkbookingnumberavailability(string sessionId)
        {
            try
            {
                HotelBookingDBEntities dBEntitiy = new HotelBookingDBEntities();
                var booking = dBEntitiy.HotelsBookings.FirstOrDefault(a => a.SessionId == sessionId);
                if (booking == null)
                {
                    return null;

                }
                return booking.Booking_No;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int EditBookingPaxes(CheckOutData checkOutData, string BN)
        {
            try
            {
                int EffectedRows = 0;
                int indx = 1;
                HotelBookingDBEntities dBEntitiy = new HotelBookingDBEntities();
                for (int i = 0; i < checkOutData.Travellers.Count; i++)
                {
                    checkOutData.Travellers[i].roomRef = i + 1;
                }
                var booking = dBEntitiy.HotelBookingPaxs.Where(a => a.SID == checkOutData.Sid && a.Booking_No == BN && a.PaxNo == 1);
                if (booking != null)
                {
                    foreach (var item in booking)
                    {
                        var pax = checkOutData.Travellers.FirstOrDefault(x => x.roomNo == item.Room_No && x.roomRef == indx && x.TravellerId == item.PaxNo);
                        if (pax != null)
                        {
                            item.DateOfBirth = pax.DateOfBirth.ToString();
                            item.First_name = pax.firstName;
                            item.Last_Name = pax.lastName;
                            item.Nationality = pax.nationality;
                            item.Pax_Type = pax.paxType;
                            item.Phone = pax.phone;
                            item.Phone_Code = pax.phoneCode;
                            item.Salutations = pax.salutation;

                        }
                        indx = indx + 1;

                    }
                    var BookingData = dBEntitiy.HotelsBookings.FirstOrDefault(a => a.Booking_No == BN && a.SessionId == checkOutData.Sid);
                    var traveller = checkOutData.Travellers.FirstOrDefault(a => a.Main == true);
                    var customer = dBEntitiy.Customers.FirstOrDefault(a => a.Email == checkOutData.Mail);
                    if (customer != null)
                    {
                        BookingData.Customer_ID = customer.ID.ToString();
                    }
                    else
                    {
                        Customer cus = new Customer();
                        cus.Country = traveller.nationality;
                        cus.Date_Of_Birth = traveller.DateOfBirth;
                        cus.Email = checkOutData.Mail;
                        cus.First_Name = traveller.firstName;
                        cus.Last_Name = traveller.lastName;
                        cus.Phone = traveller.phone;
                        cus.Phone_Code = traveller.phoneCode;
                        cus.Salutations = traveller.salutation;
                        dBEntitiy.Customers.Add(cus
                        );
                        dBEntitiy.SaveChanges();
                        BookingData.Customer_ID = cus.ID.ToString();
                    }
                    BookingData.Pax_Name = traveller.firstName + " " + traveller.lastName;
                    BookingData.Booking_Email = checkOutData.Mail;
                    BookingData.Booking_phone = traveller.phone;
                    BookingData.Booking_Phone_Code = traveller.phoneCode;
                    EffectedRows = dBEntitiy.SaveChanges();

                    return EffectedRows;

                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
