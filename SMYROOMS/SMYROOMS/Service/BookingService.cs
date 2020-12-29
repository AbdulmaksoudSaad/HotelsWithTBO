using GraphQL.Client;
using GraphQL.Common.Request;
using GraphQL.Common.Response;
using Newtonsoft.Json;
using SMYROOMS.Helpers;
using SMYROOMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SMYROOMS.Service
{
    public class BookingService
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public async Task<GraphQLResponse> HotelBooking(HotelBookInput data,string sessionid)
        {
            GraphQLResponse graphQLResponse = new GraphQLResponse();
            var randomd = "Book" + RandomString(5);
            try
            {
                var BooKingRequest = new GraphQLRequest
                {
                    //"01[180823[180824[1[14[1[EN[GB[en[EUR[0[1[422[1[14[1[0[1 | 30#30|1|2018-08-23|1|143250|143278|14|0|0[1[30#30[H4sIAAAAAAAA/8pNLUpOTMmPdvWLdq0oyCxKdUksSY02MtE3sNA3MjC0AAAAAP//AQAA//+mSODzIAAAAA=="
                    // rooms: [{occupancyRefId: $roomid, paxes: [{name: ""Test1"", surname: ""Test1"", age: 26}, {name: ""Test2"", surname: ""Test2"", age: 30}]}]
                    Query = @"mutation Variablesdata($data:String!,$clientNum:String!,$cardtype:String!,$cardHolderName:String!,$cardHolderSurName:String!,$cardNum:String!,$cardCvc:String!,$expiredMonth:Int!,$expiredyear:Int!,$holderName:String!,$holdersurName:String!,$bookingRooms:[BookRoomInput!]!)
{
  hotelX {
    book(input: {optionRefId:$data,
      clientReference:$clientNum, deltaPrice: { amount: 10, percent: 10, applyBoth: true},
      paymentCard: {cardType:$cardtype, holder: {name:$cardHolderName, surname:$cardHolderSurName},
        number:$cardNum,
        CVC:$cardCvc, expire: {month:$expiredMonth, year:$expiredyear}},
      holder: {name:$holderName, surname:$holdersurName},
      rooms:$bookingRooms},
      settings: {client: ""Demo_Client"", testMode: true}) {
      booking {
        status
        payable
        remarks
        reference{
          supplier client
        }
        hotel {
          hotelCode
          checkIn checkOut creationDate boardCode
          rooms{
            occupancyRefId code description
            price{
              net gross currency binding exchange{rate currency}
            }
          }
          occupancies{
            id paxes{
              age 
            }
          }
        }
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
            value currency hoursBefore penaltyType
          }
        }
      }
      errors {
        code
        type
        description
      }
      warnings {
        code
        description

      }
    }
  }



}",
                    OperationName = "Variablesdata",
                    Variables = new
                    {
                        data = data.optionRefId,
                        cardtype = data.paymentCard.cardType,
                        cardHolderName = data.paymentCard.holder.name,
                        cardHolderSurName = data.paymentCard.holder.surname,
                        cardNum = data.paymentCard.number,
                        cardCvc = data.paymentCard.CVC,
                        expiredMonth = data.paymentCard.expire.month,
                        expiredyear = data.paymentCard.expire.year,
                        holderName = data.holder.name,
                        holdersurName = data.holder.surname,
                        bookingRooms = data.rooms,
                        clientNum = randomd


                    }
                };
                string path = @"C:\Users\Dev User\source\repos\SMYROOMS\SMYROOMS\ConfigData.xml";
                XDocument doc = XDocument.Load(path);
                var url = doc.Element("MetaData").Element("URL").Value;
                var KeyData = doc.Element("MetaData").Element("TokenKEY").Value;
                var requestData = JsonConvert.SerializeObject(BooKingRequest);
                LoggerHelper.WriteToFile("c:/HotelsB2C/Logs/SMLogs/BookingRequests", "BookingRequest_" + sessionid, "BookingRequest", requestData);
                var graphQLClient = new GraphQLClient(url);
                graphQLClient.DefaultRequestHeaders.Add("Authorization", KeyData);
                 graphQLResponse = await graphQLClient.PostAsync(BooKingRequest);
                var ResponseData = JsonConvert.SerializeObject(graphQLResponse);
                LoggerHelper.WriteToFile("c:/HotelsB2C/Logs/SMLogs/BookingResponse", "BookingResponse_" + sessionid, "BookingResponse", ResponseData);
                return graphQLResponse;
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LoggerHelper.WriteToFile("c:/HotelsB2C/Logs/SMLogs/BookingQueryException", "BookingQueryException_" + sessionid, "BookingQueryException", requestData);
                throw;
            }
        }
    }
}