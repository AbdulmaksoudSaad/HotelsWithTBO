using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.TableMapper
{
   public class CreateTable
    {
       public static DataTable CreateHotelRate()
        {
            DataTable dt_Hotel1 = new DataTable();
            DataColumn Ref_ID = new DataColumn("Ref_ID");
            DataColumn supplierCode = new DataColumn("supplierCode");
            DataColumn accessCode = new DataColumn("accessCode");
            DataColumn market = new DataColumn("market");
            DataColumn hotelCode = new DataColumn("hotelCode");
            DataColumn hotelCodeSupplier = new DataColumn("hotelCodeSupplier");
            DataColumn boardCode = new DataColumn("boardCode");
            DataColumn boardCodeSupplier = new DataColumn("boardCodeSupplier");
            DataColumn Token = new DataColumn("Token");
            DataColumn remarks = new DataColumn("remarks");
            DataColumn paymentType = new DataColumn("paymentType");
            DataColumn status = new DataColumn("status");
            DataColumn Currency = new DataColumn("Currency");
            DataColumn net = new DataColumn("net");

            DataColumn gross = new DataColumn("gross");
            
            DataColumn sessionId = new DataColumn("sessionId");
            DataColumn PolicyRefundable = new DataColumn("PolicyRefundable");
            gross.DataType = typeof(decimal);
            net.DataType = typeof(decimal);
            

            dt_Hotel1.Columns.Add(Ref_ID);
            dt_Hotel1.Columns.Add(supplierCode);
            dt_Hotel1.Columns.Add(accessCode);
            dt_Hotel1.Columns.Add(market);
            dt_Hotel1.Columns.Add(hotelCode);
            dt_Hotel1.Columns.Add(hotelCodeSupplier);
            dt_Hotel1.Columns.Add(boardCode);
            dt_Hotel1.Columns.Add(boardCodeSupplier);
            dt_Hotel1.Columns.Add(Token);
            dt_Hotel1.Columns.Add(remarks);
            dt_Hotel1.Columns.Add(paymentType);
            dt_Hotel1.Columns.Add(status);
            dt_Hotel1.Columns.Add(Currency);
            dt_Hotel1.Columns.Add(net);
            dt_Hotel1.Columns.Add(gross);
           
           
            dt_Hotel1.Columns.Add(sessionId);
            dt_Hotel1.Columns.Add(PolicyRefundable);
            return dt_Hotel1;
        }
        public static DataTable CreateRoomTable()
        {
            DataTable dt_Room1 = new DataTable();
            DataColumn occupancyRefId = new DataColumn("occupancyRefId");
            DataColumn code = new DataColumn("code");
            DataColumn description = new DataColumn("description");
            DataColumn units = new DataColumn("units");
            DataColumn currency = new DataColumn("currency");
            DataColumn netPrice = new DataColumn("netPrice");
            DataColumn grossPrice = new DataColumn("grossPrice");
            DataColumn currencyChange = new DataColumn("currencyChange");
            DataColumn rateChange = new DataColumn("rateChange");
            DataColumn BedDescription = new DataColumn("BedDescription");
            DataColumn BedType = new DataColumn("BedType");
            DataColumn Bedcount = new DataColumn("Bedcount");
            DataColumn Bedshared = new DataColumn("Bedshared");
            DataColumn refundable = new DataColumn("refundable");
          
            DataColumn HotelRateID = new DataColumn("HotelRateID");
            DataColumn sessionIdr = new DataColumn("sessionId");
            units.DataType = typeof(int);
            netPrice.DataType = typeof(decimal);
            grossPrice.DataType = typeof(decimal);
           
            Bedcount.DataType = typeof(int);
            dt_Room1.Columns.Add(occupancyRefId);
            dt_Room1.Columns.Add(code);
            dt_Room1.Columns.Add(description);
            dt_Room1.Columns.Add(units);
            dt_Room1.Columns.Add(currency);
            dt_Room1.Columns.Add(netPrice);
            dt_Room1.Columns.Add(grossPrice);
            dt_Room1.Columns.Add(currencyChange);
            dt_Room1.Columns.Add(rateChange);
            dt_Room1.Columns.Add(BedDescription);
            dt_Room1.Columns.Add(BedType);
            dt_Room1.Columns.Add(Bedcount);
            dt_Room1.Columns.Add(Bedshared);
            dt_Room1.Columns.Add(refundable);
            dt_Room1.Columns.Add(HotelRateID);
        
            dt_Room1.Columns.Add(sessionIdr);
            return dt_Room1;
        }
        public static DataTable CreatePolicyTable()
        {
            DataTable dt_Policy1 = new DataTable();
            DataColumn CurrencyP = new DataColumn("Currency");
            DataColumn Type = new DataColumn("Type");
            DataColumn Value = new DataColumn("Value");
            DataColumn hoursBefore = new DataColumn("hoursBefore");
            DataColumn HotelRateIDP = new DataColumn("HotelRateID");
            DataColumn SID = new DataColumn("SessionID");
        

            Value.DataType = typeof(decimal);
            hoursBefore.DataType = typeof(int);
           
            dt_Policy1.Columns.Add(CurrencyP);
            dt_Policy1.Columns.Add(Type);
            dt_Policy1.Columns.Add(Value);
            dt_Policy1.Columns.Add(hoursBefore);
            dt_Policy1.Columns.Add(HotelRateIDP);
            dt_Policy1.Columns.Add(SID);
          
            return dt_Policy1;
            
        }
    }
}
