using Newtonsoft.Json;
using SMYROOMS.Helpers;
using SMYROOMS.TableMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.DB
{
    public class QuoteDataEntry
    {
        DB_Connection db = new DB_Connection();
        public async void SaveSMQuote(string ref_id, string SessionId, string hotel)
        {
            try
            {
                db.DB_OpenConnection();
                DataTable dt_QueteRate = new DataTable();
                dt_QueteRate = CreateBookingTable.CreateQuoteTB(); ;
                DataRow dr = dt_QueteRate.NewRow();
                dr["Ref_ID"] = ref_id;
                dr["SessionID"] = SessionId;
                dr["HotelID"] = hotel;
                dt_QueteRate.Rows.Add(dr);
                // consume sp to get bookingid
                await db.SaveQuote_Async(dt_QueteRate);
                db.DB_CloseConnection();
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggerHelper.WriteToFile("c:/HotelsB2C/Logs/SMLogs/ValidationDBException", "ValidationDBException_" + SessionId, "ValidationDBException", requestData);
                throw;
            }
        }
    }
}
