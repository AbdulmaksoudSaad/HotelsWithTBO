using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL.Common.Request;
using GraphQL.Client;
using GraphQL.Common.Response;
using SMYROOMS.Model;
using Newtonsoft.Json;
using SMYROOMS.Helpers;
using System.Xml.Linq;

namespace SMYROOMS.Service
{
   public class ValidationService
    {
        public  async Task<GraphQLResponse> ValidationData(string id,string SessionID)
        {
            try
            {
                var ValidationRequest = new GraphQLRequest
                {
                    //"01[180823[180824[1[14[1[EN[GB[en[EUR[0[1[422[1[14[1[0[1 | 30#30|1|2018-08-23|1|143250|143278|14|0|0[1[30#30[H4sIAAAAAAAA/8pNLUpOTMmPdvWLdq0oyCxKdUksSY02MtE3sNA3MjC0AAAAAP//AQAA//+mSODzIAAAAA=="

                    Query = @"query variabledata($data:String! )
{
  hotelX {
    quote(criteria: {optionRefId:$data }, 
settings: {client: ""Demo_Client"", testMode: true, context: ""HOTELTEST""}) {
      errors {
                    code
        description
                }
      warnings {
                    code
        description
                }
      optionQuote {
                    optionRefId
        status
        price {
                    currency
          binding
          net
          gross
          exchange {
                    currency
            rate
                }
             }
         cancelPolicy{
          refundable
          cancelPenalties{
            currency
            hoursBefore
            penaltyType
            value
          }
        }
      }
    }
}


}",
                    OperationName = "variabledata",
                    Variables = new
                    {
                        data = id,

                    }
                };
                string path = @"C:\Users\Dev User\source\repos\Hotels2\SMYROOMS\ConfigData.xml";
                XDocument doc = XDocument.Load(path);
                var url = doc.Element("MetaData").Element("URL").Value;
                var KeyData = doc.Element("MetaData").Element("TokenKEY").Value;
                var requestData = JsonConvert.SerializeObject(ValidationRequest);

                LoggerHelper.WriteToFile("SMLogs/ValidationRequests", "ValidationRequest_" + SessionID, "ValidationRequest", requestData);
                var graphQLClient = new GraphQLClient(url);
                graphQLClient.DefaultRequestHeaders.Add("Authorization", KeyData);

                var graphQLResponse =   graphQLClient.PostAsync(ValidationRequest).Result;
                var ResponseData = JsonConvert.SerializeObject(graphQLResponse);

                LoggerHelper.WriteToFile("SMLogs/ValidationResponse", "ValidationResponse_" + SessionID, "ValidationResponse", ResponseData);
                return graphQLResponse;
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggerHelper.WriteToFile("SMLogs/ValidationQueryException", "ValidationQueryException_" + SessionID, "ValidationQueryException", requestData);
                throw;
            }
        }
    }
}
