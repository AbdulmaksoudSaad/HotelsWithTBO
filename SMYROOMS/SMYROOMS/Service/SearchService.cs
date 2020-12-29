using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL.Common.Request;
using GraphQL.Client;
using GraphQL.Common.Response;
using SMYROOMS.Model;
using SMYROOMS.Helpers;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.IO;
using System.Web;
using System.Net.Http;

namespace SMYROOMS.Service
{
    public class SearchService
    {

        public async Task<GraphQLResponse> GetHotelsData(SearchInputData data, string SessionID)
        {
            try {
                var HotelRequest = new GraphQLRequest
                {
                    //client: ""citybookers"", testMode: true, context: ""LOGI""
                    Query = @"
               query VariableData($hotelsdata:[String!]!,$ch_in: Date!,$ch_out:Date!,$paxesdata:[RoomInput!]!,$nat:Country,$Money:Currency )
                    {
                  hotelX {
                 search(criteria: {checkIn:$ch_in, checkOut:$ch_out,hotels:$hotelsdata ,nationality:$nat,currency:$Money,
                occupancies:$paxesdata}, settings: {client: ""Demo_Client"", testMode: true, context: ""HOTELTEST""}) {
                      errors {
                         code
                        type
                        description
                              }
                      warnings {
                        code
                        type
                        description
                                }
                      options {
                         id
                        supplierCode
                        hotelCode
                        hotelName remarks token
                        boardCode
                        accessCode market hotelCodeSupplier boardCodeSupplier paymentType status 
          
                 occupancies{
                  id paxes{
                   age
                      }
                         }     
                rooms{
             code occupancyRefId
               beds {
                 type 
            description  
                count 
            shared 
             } description refundable units 
         roomPrice{
             price {
             net
           currency
             gross
          exchange{
            rate
            currency
          }
                   }
                   }
          }
           supplements{
          code 
         name 
         description 
         supplementType 
         chargeType 
         mandatory 
         durationType 
       quantity 
        unit 
      effectiveDate 
      expireDate 
        resort {
         name
          }
             price{
            net
          currency
          
              }      
            }       surcharges{
                    chargeType description mandatory
                  price{net currency}
                }
          cancelPolicy{
          refundable
          cancelPenalties{
            hoursBefore currency penaltyType value
          }
        }         
                        price {
                                    net
                          currency
                gross
          exchange{
            rate
            currency
          }
                                }
                            }
                    }
                    }
                }
                ",
                    OperationName = "VariableData",
                    Variables = new
                    {
                        hotelsdata = data.hotels,
                        ch_in = data.checkin,
                        ch_out = data.checkout,
                        paxesdata = data.occupancies,
                        nat = data.Nationality,
                        Money = data.currency
                    }
                };

                string path = @"E:\hotelproviders\SMYROOMS\SMYROOMS\ConfigData.xml";
                XDocument doc = XDocument.Load(path);

                var url = doc.Element("MetaData").Element("URL").Value;
                var KeyData = doc.Element("MetaData").Element("TokenKEY").Value;
                var requestData = JsonConvert.SerializeObject(HotelRequest);

                LoggerHelper.WriteToFile("c:/HotelsB2C/Logs/SMLogs/SearchRequests", "SearchRequest_" + SessionID, "SearchRequest", requestData);

                var graphQLClient = new GraphQLClient(url);
                graphQLClient.DefaultRequestHeaders.Add("Authorization", KeyData);

                var graphQLResponse =  graphQLClient.PostAsync(HotelRequest).Result;
                var ResponseData = JsonConvert.SerializeObject(graphQLResponse);

                LoggerHelper.WriteToFile("c:/HotelsB2C/Logs/SMLogs/SearchResponses", "SearchResponse_" + SessionID, "SearchResponse", ResponseData);
                return graphQLResponse;
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggerHelper.WriteToFile("c:/HotelsB2C/Logs/SMLogs/SearchQUERYException", "SearchQUERYException_" + SessionID, "SearchQUERYException", requestData);
                throw;
            }
        }
    }
}
