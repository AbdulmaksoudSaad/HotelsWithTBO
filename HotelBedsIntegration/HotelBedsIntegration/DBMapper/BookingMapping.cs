using HotelBedsIntegration.Helper;
using HotelBedsIntegration.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBedsIntegration.DBMapper
{
    class BookingMapping
    {
        public static void MapRequestToDB(BookingReq value, string SessionID)
        {
            try
            {
                HotelBedEntities db = new HotelBedEntities();

                foreach (var item in value.rooms)
                {
                    BookingRequest booking = new BookingRequest();
                    booking.ClientReference = value.clientReference;
                    booking.HolderName = value.holder.name;
                    booking.SurName = value.holder.surname;
                    booking.session = SessionID;
                    booking.Roomkey = item.rateKey;
                    db.BookingRequests.Add(booking);
                    db.SaveChanges();
                    foreach (var p in item.paxes)
                    {
                        RequestPAX requestPAX = new RequestPAX();
                        requestPAX.BookingID = booking.id;
                        requestPAX.Name = p.name;
                        requestPAX.RID = p.roomId;
                        requestPAX.SurName = p.surname;
                        requestPAX.Type = p.type;
                        db.RequestPAXES.Add(requestPAX);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LogData.WriteToFile("c:/HotelsB2C/Logs/HBLogs/BookingException", "BookingException_" + SessionID, "BookingException", requestData);

            }
        }
        public static void MapResponseToDB(BookingRes value, string SessionID)
        {
            try { 
            HotelBedEntities db = new HotelBedEntities();
            BookingConfirmation booking = new BookingConfirmation();
            booking.cancellationPolicy = value.booking.modificationPolicies.cancellation.ToString();
            booking.clientReference = value.booking.clientReference;
            booking.CreationData = value.booking.creationDate;
            booking.CreationUser = value.booking.creationUser;
            booking.Currency = value.booking.currency;
            booking.HolderName = value.booking.holder.name;
            booking.HolderSurName = value.booking.holder.surname;
            booking.invoiceCompany = value.booking.invoiceCompany.company;
            booking.invoiceCompanyCode = value.booking.invoiceCompany.code;
            booking.modificationPolicy = value.booking.modificationPolicies.modification.ToString();
            booking.PendingAmount = value.booking.pendingAmount.ToString();
            booking.Reference = value.booking.reference;
            booking.RegistrationNumber = value.booking.invoiceCompany.registrationNumber;
            booking.SessionID = SessionID;
            booking.Status = value.booking.status;
            booking.supplier = value.booking.hotel.supplier.name;
            booking.totalAmount = value.booking.totalNet.ToString();
            booking.response = Newtonsoft.Json.JsonConvert.SerializeObject(value.booking);
            var hotel = db.SearchHotelDatas.FirstOrDefault(a => a.SessionID == SessionID);
            booking.HotelSearchID = hotel.Id;
            db.BookingConfirmations.Add(booking);
            db.SaveChanges();
            foreach (var item in value.booking.hotel.rooms)
            {

                foreach (var rate in item.rates)
                {
                    BookingRoom searchRoom = new BookingRoom();
                    searchRoom.Adult = rate.adults;
                    searchRoom.rateComments = rate.rateComments;
                    searchRoom.boardCode = rate.boardCode;
                    searchRoom.boardName = rate.boardName;
                    searchRoom.Child = rate.children;

                    searchRoom.hotelMandatory = rate.hotelMandatory.ToString();
                    searchRoom.Net = rate.net;
                    searchRoom.packaging = rate.packaging.ToString();
                    searchRoom.paymentType = rate.paymentType;
                    searchRoom.rateClass = rate.rateClass;

                    searchRoom.rooms = rate.rooms;

                    searchRoom.code = item.code;
                    searchRoom.Name = item.name;
                    searchRoom.rooms = rate.rooms;
                    searchRoom.BookingID = booking.ID;
                    searchRoom.SearchId = SessionID;
                    searchRoom.sellingRate = rate.sellingRate;
                    db.BookingRooms.Add(searchRoom);
                    db.SaveChanges();
                    foreach (var P in item.paxes)
                    {
                        Pax pa = new Pax();
                        pa.Name = P.name;
                        pa.RoomID = searchRoom.ID;
                        pa.SurName = P.surname;
                        pa.type = P.type;
                        db.Paxes.Add(pa);



                    }
                    foreach (var policy in rate.cancellationPolicies)
                    {
                        PolicyBooking policyBooking = new PolicyBooking();
                        policyBooking.Amount = policy.amount;
                        policyBooking.BookingRoomID = searchRoom.ID;
                        policyBooking.FromDate = policy.from.ToString();
                        db.PolicyBookings.Add(policyBooking);
                    }
                    db.SaveChanges();
                }
            }


        }
            catch (Exception ex)
            {
                var requestData = JsonConvert.SerializeObject(ex);

                LogData.WriteToFile("c:/HotelsB2C/Logs/HBLogs/BookingException", "BookingException_" + SessionID, "BookingException", requestData);

            }
        }

        }
     }
 
