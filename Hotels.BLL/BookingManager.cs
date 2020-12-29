using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Hotels.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotels.BLL
{
  public  class BookingManager
    {
        static CheckOutData fillAllTrivellers(CheckOutData data)
        {
            try
            {
                if (data.Pid == "5")
                {
                    //foreach (var item in data.Travellers)
                    //{
                    //    item.Main = true;
                    //}


                    //  CheckOutData outData = new CheckOutData();
                    string[] demydata = new string[] { "dy", "di", "sy", "hm", "lm" };
                    string[] demydataCh = new string[] { "dyc", "dci", "syc", "hmc" };
                    int numOfRooms = data.Travellers.Count;
                    List<BookingTraveller> LeadTraveller = new List<BookingTraveller>();
                    LeadTraveller.AddRange(data.Travellers);
                    SearchDBEntities searchDB = new SearchDBEntities();
                    var CountPax = searchDB.ProviderSessions.Where(ss => ss.SearchId == data.Sid).ToList();
                    var Rooms = GetRooms.GetRoomsForTraveller(data.Sid, data.HotelID, data.Pid, data.Travellers.Select(a => a.roomNo.ToString()).ToList());
                    for (int i = 0; i < CountPax.Count; i++)
                    {
                        //foreach (var item in CountPax)
                        //{
                            data.Travellers[i].roomRef = i + 1;
                            var room = Rooms.FirstOrDefault(a => a.RoomCode == LeadTraveller[i].roomNo.ToString());
                            if (CountPax[i].Adult > 1)
                            {
                                for (int j = 0; j < CountPax[i].Adult - 1; j++)
                                {
                                    BookingTraveller traveller = new BookingTraveller();
                                    traveller.firstName = data.Travellers.FirstOrDefault(x => x.roomNo.ToString() == room.RoomCode).firstName + demydata[j];
                                    traveller.lastName = data.Travellers.FirstOrDefault(x => x.roomNo.ToString() == room.RoomCode).lastName;
                                    traveller.Main = false;
                                    traveller.paxType = "adult";
                                    traveller.roomNo = int.Parse(room.RoomCode);
                                    traveller.salutation = data.Travellers.FirstOrDefault(x => x.roomNo.ToString() == room.RoomCode).salutation;
                                    traveller.TravellerId = j + 2;
                                    traveller.roomRef = i + 1;
                                    data.Travellers.Add(traveller);
                                }


                            }
                            if (CountPax[i].Child > 0)
                            {
                                for (int c = 0; c < CountPax[i].Child; c++)
                                {
                                    BookingTraveller traveller = new BookingTraveller();
                                    traveller.firstName = data.Travellers.FirstOrDefault(x => x.roomNo.ToString() == room.RoomCode).firstName + demydataCh[c];
                                    traveller.lastName = data.Travellers.FirstOrDefault(x => x.roomNo.ToString() == room.RoomCode).lastName;
                                    traveller.Main = false;
                                    traveller.paxType = "Child";
                                    traveller.roomNo = int.Parse(room.RoomCode);
                                    traveller.salutation = "mr";
                                    traveller.TravellerId = c + room.Adults.Value + 2;
                                    traveller.roomRef = i + 1;
                                    data.Travellers.Add(traveller);
                                }
                            }
                        //}
                        
                    }
                }
                else
                {
                    //  CheckOutData outData = new CheckOutData();
                    string[] demydata = new string[] { "dy", "di", "sy", "hm", "lm" };
                    string[] demydataCh = new string[] { "dyc", "dci", "syc", "hmc" };
                    int numOfRooms = data.Travellers.Count;
                    List<BookingTraveller> LeadTraveller = new List<BookingTraveller>();
                    LeadTraveller.AddRange(data.Travellers);
                    var Rooms = GetRooms.GetRoomsForTraveller(data.Sid, data.HotelID, data.Pid, data.Travellers.Select(a => a.roomNo.ToString()).ToList());
                    for (int i = 0; i < numOfRooms; i++)
                    {
                        data.Travellers[i].roomRef = i + 1;
                        var room = Rooms.FirstOrDefault(a => a.RoomCode == LeadTraveller[i].roomNo.ToString());
                        if (room.Adults > 1)
                        {
                            for (int j = 0; j < room.Adults - 1; j++)
                            {
                                BookingTraveller traveller = new BookingTraveller();
                                traveller.firstName = data.Travellers.FirstOrDefault(x => x.roomNo.ToString() == room.RoomCode).firstName + demydata[j];
                                traveller.lastName = data.Travellers.FirstOrDefault(x => x.roomNo.ToString() == room.RoomCode).lastName;
                                traveller.Main = false;
                                traveller.paxType = "adult";
                                traveller.roomNo = int.Parse(room.RoomCode);
                                traveller.salutation = data.Travellers.FirstOrDefault(x => x.roomNo.ToString() == room.RoomCode).salutation;
                                traveller.TravellerId = j + 2;
                                traveller.roomRef = i + 1;
                                data.Travellers.Add(traveller);
                            }


                        }
                        if (room.Childern > 0)
                        {
                            for (int c = 0; c < room.Childern; c++)
                            {
                                BookingTraveller traveller = new BookingTraveller();
                                traveller.firstName = data.Travellers.FirstOrDefault(x => x.roomNo.ToString() == room.RoomCode).firstName + demydataCh[c];
                                traveller.lastName = data.Travellers.FirstOrDefault(x => x.roomNo.ToString() == room.RoomCode).lastName;
                                traveller.Main = false;
                                traveller.paxType = "Child";
                                traveller.roomNo = int.Parse(room.RoomCode);
                                traveller.salutation = "mr";
                                traveller.TravellerId = c + room.Adults.Value + 1;
                                traveller.roomRef = i + 1;
                                data.Travellers.Add(traveller);
                            }
                        }

                    }
                }
              


                return data;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public static string GetBookingNumberAndManageBooking(CheckOutData data)
        {
            try
            {
                CurrencyManager currencyManager = new CurrencyManager();
                var BaseCur = ConfigurationSettings.AppSettings["BaseCur"];

                double ExcahngeRate = currencyManager.GetCurrencyConversion(BaseCur, data.Currency, data.Sid);
                data.SellPrice = Math.Round(data.SellPrice * ExcahngeRate, 3);
                string BookingNum = null;
                BookingNum = BookingRepo.checkbookingnumberavailability(data.Sid);
                if(BookingNum==null)
                {
                    BookingNum = "";
                   
                  
                    BookingNum = BookingRepo.GetBookingNumber(data.Sid,data.Src, data.Pid);
                    
                    if (BookingNum != null )
                    {
                        var tasks = new List<Task>();
                        var tokenSource1 = new CancellationTokenSource();
                        var tokenSource2 = new CancellationTokenSource();
                        var token1 = tokenSource1.Token;
                        var token2 = tokenSource2.Token;

                        tasks.Add(Task.Factory.StartNew(() =>
                        {
                            data = fillAllTrivellers(data);
                            BookingRepo.SaveBookingResult(data, BookingNum);
                         }
                                    , token1));
                        //tasks.Add(Task.Factory.StartNew(() =>
                        //{
                        //    SearchstatisticDA searchstatistic = new SearchstatisticDA();
                        //searchstatistic.AddMetaSearchStatistic(data, BookingNum);
                        //}
                        //            , token2));
                        //call save Delivary*****************
                        Task.WaitAll(tasks.ToArray());

                    }

                    return BookingNum;
                }

                else
                {
                  BookingRepo.EditBookingPaxes(data, BookingNum);
                    
                        return BookingNum;
                    
                     
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("SaveBookingController/Errors/", "SaveBookingController" + "BLL" + data.Sid, ex.InnerException?.Message, ex.Message + ex.StackTrace);

                return null;
            }
        }
    }
}
