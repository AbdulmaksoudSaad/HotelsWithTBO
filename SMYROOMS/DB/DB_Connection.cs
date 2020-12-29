using SMYROOMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.DB
{
  public class DB_Connection
    {
       SqlConnection Db_Con;
        public async void DB_OpenConnection()
        {
            // SelectConnectionStringByDBName(DBName);
            //data source=CBDEV3;initial catalog=hotelsDB;persist security info=True;user id=bery;password=CityBookers;multipleactiveresultsets=True;application name=EntityFramework&quot;" providerName="System.Data.EntityClient"
            Db_Con = new SqlConnection("server=CBDEV3;Initial Catalog=SMyRooms;persist security info=True;user id=bery;password=CityBookers;multipleactiveresultsets=True;"  );

            if (Db_Con.State != ConnectionState.Open)
            {
                Db_Con.Open();
            }
        }
        public async void DB_CloseConnection()
        {
            Db_Con.Close();
            Db_Con.Dispose();
        }
        public async  Task SaveTable_Async(DataTable dt)
        {
                using (SqlCommand cmd = new SqlCommand("SMR_Hotels_Rooms_Results", Db_Con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("tmpMSRoomsResults", dt);
                    cmd.Parameters.AddWithValue("sessionID", dt.Rows[0]["sessionId"].ToString());
                    cmd.ExecuteNonQuery();

                }     
        }
        public async Task SaveRooms_Async(DataTable dt)
        {
            using (SqlCommand cmd = new SqlCommand("MS_Rooms_Results", Db_Con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("tmpMSRooms", dt);
                cmd.Parameters.AddWithValue("sessionID", dt.Rows[0]["sessionId"].ToString());
                cmd.ExecuteNonQuery();

            }
        }
        public async Task SavePolicy_Async(DataTable dt)
        {
            using (SqlCommand cmd = new SqlCommand("MS_CancelPenalty", Db_Con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("tmpMSCancelPenalty", dt);
                cmd.Parameters.AddWithValue("sessionID", dt.Rows[0]["sessionId"].ToString());
                cmd.ExecuteNonQuery();

            }
        }
        public async Task<List<Room>> GETRoomDetails(string HotelId,string SessionID)
        {
            List<Room> Rooms = new List<Room>();
            using (SqlCommand cmd = new SqlCommand("MS_SelectRooms", Db_Con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("sessionID", SessionID);
                cmd.Parameters.AddWithValue("HotelID", HotelId);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                  Room room = new Room();
                     room.description     = reader["description"].ToString();
                     room.code = reader["code"].ToString();
                    room.occupancyRefId =int.Parse(reader["occupancyRefId"].ToString());
                    room.refundable = bool.Parse(reader["refundable"].ToString());
                    room.units =int.Parse(reader["units"].ToString());
                    room.roomPrice.price.currency = reader["currency"].ToString();
                    room.roomPrice.price.net =decimal.Parse(reader["netPrice"].ToString());
                    room.roomPrice.price.gross = decimal.Parse(reader["grossPrice"].ToString());
                    room.roomPrice.price.exchange.currency =  reader["currencyChange"].ToString();
                    room.roomPrice.price.exchange.rate = float.Parse(reader["rateChange"].ToString());
                    room.beds.count = int.Parse(reader["Bedcount"].ToString());
                    room.beds.description = reader["BedDescription"].ToString();
                    room.beds.type =  reader["BedType"].ToString();
                    Rooms.Add(room);
                    //    TestList.Add(test);
                }
               
            }
            return Rooms;
        }
        public async Task<CancelPolicy> GETPolicyDetails(string HotelId, string SessionID)
        {
            CancelPolicy policy = new CancelPolicy();
            using (SqlCommand cmd = new SqlCommand("MS_SelectCancelPolicy", Db_Con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("sessionID", SessionID);
                cmd.Parameters.AddWithValue("HotelID", HotelId);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CancelPenalty cancel = new CancelPenalty();
                    cancel.currency = reader["Currency"].ToString();
                    cancel.hoursBefore = int.Parse(reader["hoursBefore"].ToString());
                    cancel.penaltyType = reader["Type"].ToString();
                    cancel.value = float.Parse(reader["Value"].ToString());
                    policy.refundable  = bool.Parse(reader["PolicyRefundable"].ToString());
                 
                   
                    policy.cancelPenalties.Add(cancel);
                    //    TestList.Add(test);
                }

            }
            return policy ;
        }
        public async Task<int> SaveBooking_Async(DataTable dt)
        {
            int bookingid;
            using (SqlCommand cmd = new SqlCommand("SM_Booking_Results", Db_Con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("BookingData", dt);
                cmd.Parameters.AddWithValue("sessionID", dt.Rows[0]["SessionId"].ToString());
                SqlParameter outputIdParam = new SqlParameter("@IDOUT", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);
                cmd.ExecuteNonQuery();
                  bookingid =(int)outputIdParam.Value;

            }
            return bookingid;
        }
        public async Task SaveBookingRoom_Async(DataTable dt)
        {
            using (SqlCommand cmd = new SqlCommand("SM_Booking_Rooms", Db_Con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("BookingData", dt);
                cmd.ExecuteNonQuery();
            }
        }
        public async Task SavePolicyBooking_Async(DataTable dt)
        {
            using (SqlCommand cmd = new SqlCommand("SM_Booking_Policy", Db_Con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("BookingData", dt);
                cmd.ExecuteNonQuery();
            }
        }
        public async Task<int> SavePaxROOMBooking_Async(int? roomID, int BookingID)
        {
            int room=0;
            using (SqlCommand cmd = new SqlCommand("SM_Booking_Policy", Db_Con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Room_id", roomID);
                cmd.Parameters.AddWithValue("BookingData", BookingID);
                SqlParameter outputIdParam = new SqlParameter("@IDOUT", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);
                cmd.ExecuteNonQuery();
                room=(int)outputIdParam.Value;
            }
            return room;
        }
        public async Task SavePaxesBooking_Async(DataTable dt)
        {
            using (SqlCommand cmd = new SqlCommand("SM_Booking_PAX", Db_Con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("BookingPaxes ", dt);
                 
                cmd.ExecuteNonQuery();
            }
        }
        public async Task SaveQuote_Async(DataTable dt)
        {
            using (SqlCommand cmd = new SqlCommand("MS_Quete", Db_Con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("tmpMSQuote", dt);
                cmd.Parameters.AddWithValue("sessionID", dt.Rows[0]["sessionId"].ToString());
                cmd.ExecuteNonQuery();

            }
        }

    }
}
