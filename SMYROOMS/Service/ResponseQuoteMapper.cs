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
    class ResponseQuoteMapper
    {
        public ValidationData MappingDataQuate(GraphQLResponse data ,string SessionID)
        {
            try
            {
                ValidationData OutPutData = new ValidationData();
                OutPutData.cancelPolicy.refundable = data.Data.hotelX.quote.optionQuote.cancelPolicy.refundable;
                if (data.Data.hotelX.quote.optionQuote.cancelPolicy.cancelPenalties != null)
                {
                    foreach (var item in data.Data.hotelX.quote.optionQuote.cancelPolicy.cancelPenalties)
                    {
                        var cancel = new CancelPenalty();
                        cancel.currency = item.currency;
                        cancel.hoursBefore = item.hoursBefore;
                        cancel.value = item.value;
                        cancel.penaltyType = item.penaltyType;
                        OutPutData.cancelPolicy.cancelPenalties.Add(cancel);
                    }
                }
                OutPutData.optionRefId = data.Data.hotelX.quote.optionQuote.optionRefId;
                OutPutData.price.currency = data.Data.hotelX.quote.optionQuote.price.currency;
                OutPutData.price.net = data.Data.hotelX.quote.optionQuote.price.net;
                OutPutData.status = data.Data.hotelX.quote.optionQuote.status;
                return OutPutData;
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggerHelper.WriteToFile("SMLogs/ValidationMapException", "ValidationMapException_" + SessionID, "ValidationMapException", requestData);
                throw;
            }
        }
    }

}
