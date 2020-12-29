using HotelBedsIntegration.Controller;
using HotelBedsIntegration.Management;
using HotelBedsIntegration.Models;
using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Hotels.DAL;
using IntegrationTotalStay.Controller;
using IntegrationTotalStay.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBOIntegration.Management;
using TBOIntegration.Services;

namespace Hotels.ProviderManagment
{

   public  class ConfirmationManager
    {
    public  static HotelBedsIntegration.Models.BookingStatus CallProviders(ConfirmData confirm,string BN )
        {
            HotelBedsIntegration.Models.BookingStatus confirmed = new HotelBedsIntegration.Models.BookingStatus();
            try
            {
                //TBO
                if (confirm.Pid == "5")
                {
                    //var bookingObj = TBOConfirmationManager.prepareObject(confirm);
                    TBORepo repo = new TBORepo();
                    var TBOreq = repo.GetBookReqData(confirm.hotelsBooking.SessionId, confirm.hotelsBooking.Booking_No);
                    //if (TBOreq != null && confirm.Rooms.Count == TBOreq.HotelRooms.Count)
                    //{
                        //cacluate total of rooms sup curency is different 
                        //decimal total = 0;
                        //foreach (var item in TBOreq.HotelRooms)
                        //{
                        //    total += item.RoomRate.TotalFare;
                        //    if (item.Supplements!= null)
                        //    {
                        //        foreach (var sup in item.Supplements)
                        //        {
                        //            total += sup.Price;
                        //        }
                        //    }
                        //}
                        //map genral tbo req to provider req
                        var req = TBOIntegration.Mapper.BookMapper.MapBookReq(TBOreq);
                            
                        var confirmedTBO = BookService.Booking(req, confirm.hotelsBooking.SessionId);
                        if (confirmedTBO.BookingStatus.ToString() != "Vouchered" && confirmedTBO.Status.StatusCode != "01")
                        {
                            BookingConfirmationData bookingCon = new BookingConfirmationData();
                            bookingCon.BookingNum = BN;
                            bookingCon.ClientReference = req.ClientReferenceNumber;
                            bookingCon.Cost =(decimal) TBOreq.TotalNet;//need edits
                            bookingCon.Currency = "USD";
                            bookingCon.CreationDate = DateTime.UtcNow;
                            bookingCon.HolderFirstName = req.Guests.FirstOrDefault(g => g.LeadGuest == true).FirstName;
                            bookingCon.HolderLastName = req.Guests.FirstOrDefault(g=>g.LeadGuest ==true).LastName;
                            bookingCon.ProviderId = 5;
                            bookingCon.HotelCode = confirm.hotelsBooking.Hotel_ID;
                            bookingCon.Reference = confirmedTBO.ConfirmationNo;
                            bookingCon.SessionID = confirm.hotelsBooking.SessionId;
                            bookingCon.Status = "done";
                            bookingCon.supplerReference = confirmedTBO.ConfirmationNo;
                        // map confirmedTBO to confimed
                        confirmed.booking.clientReference= req.ClientReferenceNumber;
                        confirmed.booking.totalNet = TBOreq.TotalNet??default(double);
                        confirmed.booking.currency = "USD";
                        confirmed.booking.creationDate = DateTime.UtcNow.ToShortDateString();
                        confirmed.booking.holder.name = req.Guests.FirstOrDefault(g => g.LeadGuest == true).FirstName;
                        confirmed.booking.holder.surname= req.Guests.FirstOrDefault(g => g.LeadGuest == true).LastName;
                        confirmed.booking.reference = confirmedTBO.ConfirmationNo;
                        //
                        var saveDB = ConfirmationDate.saveBookingConfirmation(bookingCon);
                            //if (saveDB == null)
                            //{
                             //   confirmed.status = 1;
                            //}
                            //else
                            //{
                                confirmed.status = 0;
                            //}
                        //}


                        if (confirmed != null)
                        {
                            return confirmed;
                        }
                    }
                }
                //
                if (confirm.Pid == "4")
                {
                var bookingObj = ConfirmationManagerHB.prepareObject(confirm);

                    if (bookingObj != null &&confirm.Rooms.Count==bookingObj.rooms.Count )
                    {
                          confirmed = BookingFlow.BookingRooms(bookingObj, confirm.hotelsBooking.SessionId, BN).Result;
                       if (confirmed.booking != null &&confirmed.status!=2)
                        {
                            BookingConfirmationData bookingCon = new BookingConfirmationData();
                            bookingCon.BookingNum = BN;
                            bookingCon.ClientReference = confirmed.booking.clientReference;
                            bookingCon.Cost =(decimal) confirmed.booking.totalNet;
                            bookingCon.Currency = confirmed.booking.currency;
                            bookingCon.CreationDate = DateTime.Parse( confirmed.booking.creationDate);
                            bookingCon.HolderFirstName = confirmed.booking.holder.name;
                            bookingCon.HolderLastName = confirmed.booking.holder.surname;
                            bookingCon.ProviderId = 4;
                            bookingCon.HotelCode = confirm.hotelsBooking.Hotel_ID;
                            bookingCon.Reference = confirmed.booking.reference;
                            bookingCon.SessionID = confirm.hotelsBooking.SessionId;
                            bookingCon.Status = "done";

                            var saveDB = ConfirmationDate.saveBookingConfirmation(bookingCon);
                            if (saveDB == null)
                            {
                                confirmed.status = 1;
                            }
                            else
                            {
                              //  confirmed.status = 0;
                            }
                        }
                        

                        if (confirmed != null)
                        {
                            return confirmed;
                        }
                    }
                }
                if (confirm.Pid == "2")
                {

                    if ( confirm != null)
                    {
                   var reqObj=     BookingManager.prepareSearchObj(confirm, "");
                        if (reqObj == null)
                        {
                            confirmed.status = 2;
                            return confirmed;
                        }
                    var    confirmedTS = ConfirmationTS.GetTSConfirmations(reqObj, confirm.hotelsBooking.SessionId, BN);
                        if (confirmedTS != null)
                        {
                            BookingConfirmationData bookingCon = new BookingConfirmationData();
                            bookingCon.BookingNum = BN;
                            bookingCon.ClientReference = confirmedTS.TradeReference;
                            bookingCon.Cost =decimal.Parse( confirmedTS.TotalPrice);
                            bookingCon.Currency = "USD";
                            bookingCon.HolderFirstName = reqObj.BookingDetails.LeadGuestFirstName;
                            bookingCon.HolderLastName = reqObj.BookingDetails.LeadGuestLastName;
                            bookingCon.HotelCode = confirm.hotelsBooking.Hotel_ID;
                            bookingCon.ProviderId = 2;
                            bookingCon.Reference = confirmedTS.BookingReference;
                            bookingCon.SessionID = confirm.hotelsBooking.SessionId;
                            bookingCon.Status = "done";
                           var saveDB= ConfirmationDate.saveBookingConfirmation(bookingCon);
                            if (saveDB == null)
                            {
                                confirmed.status = 1;
                            }
                            else {  
                            confirmed.status = 0;
                             }
                            return confirmed;
                        }
                    }
                }
                //confirmed.status = 2; 

                return confirmed;
            }catch(Exception ex)
            {
                LoggingHelper.WriteToFile("SaveBookingConfirmationController/Errors/", "ConfirmationManager CallProvider" + "ProviderManager" + confirm.hotelsBooking.SessionId, ex.InnerException?.Message, ex.Message + ex.StackTrace);

                if (confirmed.status != 2)
                {
                    confirmed.status = 1;
                }
                return confirmed;

            }
        }
    }
}
