using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.TableMapper
{
   public class CreateBookingTable
    {
        public static DataTable CreateBookingTB()
        {
            DataTable dt_BookingTB = new DataTable();
            DataColumn Supplier = new DataColumn("Supplier");
            DataColumn Client = new DataColumn("Client");
            DataColumn SessionId = new DataColumn("SessionId");
            DataColumn HolderName = new DataColumn("HolderName");
            DataColumn HolderSerName = new DataColumn("HolderSerName");
            DataColumn CurrencyPrice = new DataColumn("CurrencyPrice");
            DataColumn BindingPrice = new DataColumn("BindingPrice");
            DataColumn Net = new DataColumn("Net");
            DataColumn Gross = new DataColumn("Gross");
            DataColumn CurrencyExchange = new DataColumn("CurrencyExchange");
            DataColumn CurrencyRate = new DataColumn("CurrencyRate");
            DataColumn CancelRefundable = new DataColumn("CancelRefundable");
            DataColumn Status = new DataColumn("Status");
            DataColumn Payabe = new DataColumn("Payabe");
            DataColumn Remark = new DataColumn("Remark");
            DataColumn CreationDate = new DataColumn("CreationDate");
            DataColumn HotelCode = new DataColumn("HotelCode");
            DataColumn CheckIN = new DataColumn("CheckIN");
            DataColumn CheckOut = new DataColumn("CheckOut");
            DataColumn BoardCode = new DataColumn("BoardCode");
            Gross.DataType = typeof(decimal);
            Net.DataType = typeof(decimal);
            CheckIN.DataType = typeof(DateTime);
            CheckOut.DataType = typeof(DateTime);
            CreationDate.DataType = typeof(DateTime);
            dt_BookingTB.Columns.Add(Supplier);
            dt_BookingTB.Columns.Add(Client);
            dt_BookingTB.Columns.Add(SessionId);
            dt_BookingTB.Columns.Add(HolderName);
            dt_BookingTB.Columns.Add(HolderSerName);
            dt_BookingTB.Columns.Add(CurrencyPrice);
            dt_BookingTB.Columns.Add(BindingPrice);
            dt_BookingTB.Columns.Add(Net);
            dt_BookingTB.Columns.Add(Gross);
            dt_BookingTB.Columns.Add(CurrencyExchange);
            dt_BookingTB.Columns.Add(CurrencyRate);
            dt_BookingTB.Columns.Add(CancelRefundable);
            dt_BookingTB.Columns.Add(Status);
            dt_BookingTB.Columns.Add(Payabe);
            dt_BookingTB.Columns.Add(Remark);
            dt_BookingTB.Columns.Add(CreationDate);
            dt_BookingTB.Columns.Add(HotelCode);
            dt_BookingTB.Columns.Add(CheckIN);
            dt_BookingTB.Columns.Add(CheckOut);
            dt_BookingTB.Columns.Add(BoardCode);
            return dt_BookingTB;
        }
        public static DataTable CreateRoomBookingTB()
        {
            DataTable dt_RoomBookingTB = new DataTable();
            DataColumn occupancyRefId = new DataColumn("occupancyRefId");
            DataColumn code = new DataColumn("code");
            DataColumn description = new DataColumn("description");
            DataColumn CurrencyPrice = new DataColumn("CurrencyPrice");
            DataColumn NetPrice = new DataColumn("NetPrice");
            DataColumn GrossPrice = new DataColumn("GrossPrice");
            DataColumn ExchangeCurrency = new DataColumn("ExchangeCurrency");
            DataColumn ExchangeRate = new DataColumn("ExchangeRate");
            DataColumn Binding = new DataColumn("Binding");
            DataColumn BookingID = new DataColumn("BookingID");
            
            GrossPrice.DataType = typeof(decimal);
            NetPrice.DataType = typeof(decimal);
            BookingID.DataType = typeof(int);
            dt_RoomBookingTB.Columns.Add(occupancyRefId);
            dt_RoomBookingTB.Columns.Add(code);
            dt_RoomBookingTB.Columns.Add(description);
            dt_RoomBookingTB.Columns.Add(CurrencyPrice);
            dt_RoomBookingTB.Columns.Add(NetPrice);
            dt_RoomBookingTB.Columns.Add(GrossPrice);
            dt_RoomBookingTB.Columns.Add(ExchangeCurrency);
            dt_RoomBookingTB.Columns.Add(ExchangeRate);
            dt_RoomBookingTB.Columns.Add(Binding);
            dt_RoomBookingTB.Columns.Add(BookingID);
            
            return dt_RoomBookingTB;
        }
        public static DataTable CreatePolicyBookingTB()
        {
            DataTable dt_PolicyBookingTB = new DataTable();
            DataColumn Currency = new DataColumn("Currency");
            DataColumn Type = new DataColumn("Type");
            DataColumn Value = new DataColumn("Value");
            DataColumn hoursBefore = new DataColumn("hoursBefore");
            DataColumn BookingId = new DataColumn("BookingId");

            Value.DataType = typeof(decimal);
            hoursBefore.DataType = typeof(int);
            BookingId.DataType = typeof(int);
            dt_PolicyBookingTB.Columns.Add(Currency);
            dt_PolicyBookingTB.Columns.Add(Type);
            dt_PolicyBookingTB.Columns.Add(Value);
            dt_PolicyBookingTB.Columns.Add(hoursBefore);
            dt_PolicyBookingTB.Columns.Add(BookingId);
           
            return dt_PolicyBookingTB;
        }
        public static DataTable CreatePaxesBookingTB()
        {
            DataTable dt_PaxesBookingTB = new DataTable();
            DataColumn Age = new DataColumn("Age");
            DataColumn Room_Id = new DataColumn("Room_Id");
            

            
            Age.DataType = typeof(int);
            Room_Id.DataType = typeof(int);
            dt_PaxesBookingTB.Columns.Add(Age);
            dt_PaxesBookingTB.Columns.Add(Room_Id);
            
            return dt_PaxesBookingTB;
        }
        public static DataTable CreateQuoteTB()
        {
            DataTable dt_QuoteTB = new DataTable();
            DataColumn SessionID = new DataColumn("SessionID");
            DataColumn Ref_ID = new DataColumn("Ref_ID");
            DataColumn HotelID = new DataColumn("HotelID");

            dt_QuoteTB.Columns.Add(Ref_ID);
            dt_QuoteTB.Columns.Add(SessionID);
            dt_QuoteTB.Columns.Add(HotelID);
            return dt_QuoteTB;
        }
    }
}
