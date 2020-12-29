using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SMYROOMS.DB;
using SMYROOMS.Helpers;
using SMYROOMS.Model;
using SMYROOMS.Service;

namespace SMYROOMS.Controller
{
   public class SMCLSValidation
    {
        public static async Task<ValidationData> ValidationQuote(string id ,string SessionID)
        {
            try { 
            ResponseQuoteMapper mapSer = new ResponseQuoteMapper();
            ValidationData OutPutData = new ValidationData();
            ValidationService service = new ValidationService();
            var data = await service.ValidationData(id,SessionID);
                if (data.Data.hotelX.quote.errors == null && data.Data.hotelX.quote.warnings == null)
                {
                    OutPutData = mapSer.MappingDataQuate(data,SessionID);
                    QuoteDataEntry db = new QuoteDataEntry();
                    db.SaveSMQuote(OutPutData.optionRefId, SessionID, id);
                }
                else
                    return new ValidationData();
         return OutPutData;
            }
            catch(Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggerHelper.WriteToFile("SMLogs/ValidationException", "ValidationException_" + SessionID, "ValidationException", requestData);
                return new ValidationData();
            }
        }
    }
}
