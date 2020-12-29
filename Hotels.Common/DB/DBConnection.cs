using Hotels.Common.Helpers;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.DB
{
   public class DBConnection :IDisposable
    {
        private string HDB_CS { set; get; }
        private string BDB_CS { set; get; }
        private string SDB_CS { set; get; }
        private string HB_CS { set; get; }

        private string db_connection { set; get; }
        private SqlConnection Db_Con { set; get; }
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public DBConnection()
        {
            HDB_CS = "Data Source=cbdev3;Initial Catalog=hotelsDB;persist security info=True;user id=bery;password=CityBookers;";
            BDB_CS = "Data Source=CBDEV3;Initial Catalog=BusinessRulesDB;persist security info=True;user id=bery;password=CityBookers;";
            SDB_CS = "Data Source=CBDEV3;Initial Catalog=SearchDB;persist security info=True;user id=bery;password=CityBookers;";
            HB_CS = "Data Source=CBDEV3;Initial Catalog=HotelBookingDB;persist security info=True;user id=bery;password=CityBookers;";
        }

        private void SelectConnectionStringByDBName(string DBName)
        {
            if (DBName != null && DBName != "")
            {
                switch (DBName)
                {
                    case "HDB":
                        db_connection =   ConfigurationSettings.AppSettings["HDB_CS"];
                        break;
                    case "ODB":
                        db_connection =  ConfigurationSettings.AppSettings["BDB_CS"];

                        break;
                    case "SDB":
                        db_connection = ConfigurationSettings.AppSettings["SDB_CS"];
                        break;
                    case "HB":
                        db_connection = ConfigurationSettings.AppSettings["HB_CS"];
                        break;

                }
            }
        }

        public DataSet FillDs(string str)
        {
            try
            {

                DataSet DS = new DataSet();
                DS.Clear();
                SqlDataAdapter DA = new SqlDataAdapter();

                using (SqlCommand com = new SqlCommand())
                {
                    com.Connection = Db_Con;
                    com.CommandType = CommandType.Text;
                    com.CommandText = str;

                    DA.SelectCommand = com;
                    DA.Fill(DS);
                    com.Dispose();
                    DA.Dispose();
                }
                return DS;

            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("DBConnection/Errors/", "FillDs", ex.InnerException?.Message, ex.Message);

                return new DataSet();
            }
        }

        public async Task<DataSet> FillDs(string str, Dictionary<string, object> Parameters)
        {
            try
            {
                DataSet DS = new DataSet();
                SqlDataAdapter DA = new SqlDataAdapter();

                DS.Clear();
                SqlCommand com = new SqlCommand();
                com.Connection = Db_Con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = str;
                foreach (var item in Parameters)
                {
                    com.Parameters.Add(new SqlParameter(item.Key, item.Value));
                }

                DA.SelectCommand = com;
                DA.Fill(DS);
                com.Dispose();
                DA.Dispose();
                return DS;
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("DBConnection/Errors/", "FillDs", ex.InnerException?.Message, ex.Message);

                return new DataSet();
            }
        }

        public async Task<DataSet> FillDs(string str, DataTable dt, string strTbl)
        {
            try
            {
                DataSet DS = new DataSet();
                SqlDataAdapter DA = new SqlDataAdapter();

                DS.Clear();
                SqlCommand com = new SqlCommand();
                com.Connection = Db_Con;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = str;
                com.Parameters.AddWithValue(strTbl, dt);
                DA.SelectCommand = com;
                DA.Fill(DS);
                com.Dispose();
                DA.Dispose();
                return DS;
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("DBConnection/Errors/", "FillDs", ex.InnerException?.Message, ex.Message);

                return new DataSet();
            }
        }
        public async Task SaveTable_Async(DataTable dt, string strProc, string strTbl)
        {
            try
            {

                using (SqlCommand cmd = new SqlCommand(strProc, Db_Con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(strTbl, dt);
                    cmd.ExecuteNonQuery();

                }


            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("DBConnection/Errors/", "SaveTable_Async" + " SP = " + strProc, ex.InnerException?.Message, ex.Message + ex.StackTrace);

            }
        }

        public object GetScalarVal_Sp(string strProc, Dictionary<string, object> Parameters, string OutParameterName, object OutParameterValue, SqlDbType outType)
        {
            try
            {
                object returnData;
                using (SqlCommand cmd = new SqlCommand(strProc, Db_Con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (var item in Parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));
                    }

                    returnData = cmd.ExecuteScalar();

                    return returnData;

                }
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("DBConnection/Errors/", "GetScalarVal_Sp", ex.InnerException?.Message, ex.Message);

                return new object();
            }
        }

        public async Task SaveSP_Async(string StoredProc, Dictionary<string, object> Parameters)
        {
            try
            {
                using (SqlCommand save_Com = new SqlCommand())
                {
                    save_Com.Connection = Db_Con;
                    save_Com.CommandType = CommandType.StoredProcedure;
                    save_Com.CommandText = StoredProc;
                    foreach (var item in Parameters)
                    {
                        save_Com.Parameters.Add(new SqlParameter(item.Key, item.Value));
                    }

                    save_Com.ExecuteNonQuery();


                }
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("DBConnection/Errors/", "SaveSP_Async", ex.InnerException?.Message, ex.Message);
                throw ex;
            }

        }

        public async Task Save(string SQL)
        {
            using (SqlCommand save_Com = new SqlCommand())
            {

                save_Com.Connection = Db_Con;
                save_Com.CommandType = CommandType.Text;
                save_Com.CommandText = SQL;
                save_Com.ExecuteNonQuery();


            }
        }

        public async void DB_OpenConnection(string DBName)
        {
            SelectConnectionStringByDBName(DBName);

            Db_Con = new SqlConnection(db_connection);

            if (Db_Con.State != ConnectionState.Open)
            {
                Db_Con.Open();
            }



        }

        public async void DB_CloseConnection()
        {
            if (Db_Con != null)
            {
                if (Db_Con.State != ConnectionState.Closed)
                {
                    Db_Con.Close();
                    Db_Con.Dispose();
                }
            }
        }

        public void Dispose()
        {
            handle.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

