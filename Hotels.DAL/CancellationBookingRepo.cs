using HotelBedsIntegration.Models.Cancellation;
using Hotels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.DAL
{
  public  class CancellationBookingRepo
    {
      public BookingConfirmationData GetBookingReference(string BN)
        {
            HotelBookingDBEntities hotelBookingDB = new HotelBookingDBEntities();
          var Booking=   hotelBookingDB.BookingConfirmationDatas.FirstOrDefault(x => x.BookingNum == BN);
            return Booking;
        }

      public  void SaveCancellationBookingData(CancellationBookingData cancellation, string BN,string reference)
        {
            HotelBookingDBEntities hotelBookingDB = new HotelBookingDBEntities();
            CancellationBooking cancel  = new CancellationBooking();
            cancel.BookingCurrency = cancellation.booking.currency;
            cancel.BookingNum = BN;
            cancel.BookingReference = reference;
            cancel.BookingTotalNet = cancellation.booking.totalNet.ToString() ;
            cancel.CancellationAmount = cancellation.booking.hotel.cancellationAmount.ToString();
            cancel.cancellationFlag = cancellation.booking.modificationPolicies.cancellation.ToString();
            cancel.CancellationReference = cancellation.booking.cancellationReference;
            cancel.Cancelstatus = cancellation.booking.status;
            cancel.checkIn = cancellation.booking.hotel.checkIn;
            cancel.checkOut = cancellation.booking.hotel.checkOut;
            cancel.ClientReference = cancellation.booking.clientReference;
            cancel.CompanyregistrationNum = cancellation.booking.invoiceCompany.registrationNumber;
            cancel.creationDate = cancellation.booking.creationDate;
            cancel.creationUser = cancellation.booking.creationUser;
            cancel.Currency = cancellation.booking.hotel.currency;
            cancel.HolderFirstName = cancellation.booking.holder.name;
            cancel.HolderLastName = cancellation.booking.holder.surname;
            cancel.HotelCode = cancellation.booking.hotel.code.ToString();
            cancel.InvoiceCompany = cancellation.booking.invoiceCompany.company;
            cancel.InvoiceCompanyCode = cancellation.booking.invoiceCompany.code;
            cancel.Reference = cancellation.booking.reference;
            cancel.SupplierName = cancellation.booking.hotel.supplier.name;
            cancel.totalNet = cancellation.booking.hotel.totalNet;
            cancel.VatNumber = cancellation.booking.hotel.supplier.vatNumber;
            hotelBookingDB.CancellationBookings.Add(cancel);
            foreach (var item in cancellation.booking.hotel.rooms)
            {
                foreach (var r in item.rates)
                {
                    CancellationRoom room = new CancellationRoom();
                    room.adults=r.adults.ToString();
                    room.boardName = r.boardName;
                    room.BookingNum = BN;
                    room.children = r.children.ToString();
                    room.code = item.id.ToString();
                   
                    room.net = r.net.ToString();
                    room.rateClass = r.rateClass;
                    room.reference = reference;
                    room.RoomName = item.name;
                    room.RoomStatus = item.status;
                    room.supplierReference = item.supplierReference;
                     
                    hotelBookingDB.CancellationRooms.Add(room);

                }
            }
            hotelBookingDB.SaveChanges();
          
            
        }
    }
}
