using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Hotels.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.DAL
{
    public class HotelBookingDAl : IHotelBookingStatus
    {
         

        public string addBookingStatus(string status, string BN)
        {
           

            HotelBookingDBEntities db = new HotelBookingDBEntities();
            HotelBookingStatu bookingStatus = new HotelBookingStatu();
            bookingStatus.Booking_No = BN;
            bookingStatus.Booking_Status = status;
            bookingStatus.Status_Time = DateTime.Now;
            db.HotelBookingStatus.Add(bookingStatus);
            db.SaveChanges();
            return "done";
        }

        public string ChangeBookingStatus(BookingStatus bookingStatus)
        {
            try {
                
                HotelBookingDBEntities db = new HotelBookingDBEntities();
                var BookingData = db.HotelsBookings.FirstOrDefault(x => x.Booking_No == bookingStatus.BookingNum && x.SessionId == bookingStatus.Sid);
                BookingData.Booking_Status = bookingStatus.Status;
              db.SaveChanges();
               
                 
                    return "done";
                

            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("SaveBookingConfirmationController/Errors/", "ConfirmedController" + "INController" + bookingStatus.Sid, ex.InnerException?.Message, ex.Message + ex.StackTrace);

                return null;
            }
        }

        public HotelsBooking ChangeBookingstatusAndGetNotificationData(BookingStatus bookingStatus, string BN, string Sid, MailObj mail)
        {
            HotelBookingDBEntities db = new HotelBookingDBEntities();
            var BookingData = db.HotelsBookings.FirstOrDefault(x => x.Booking_No == bookingStatus.BookingNum && x.SessionId == bookingStatus.Sid);
            BookingData.Booking_Status = bookingStatus.Status;
            BookingData.voucherPdf = mail.confirmationPDF;
            BookingData.InvoicePdf = mail.salesInvoicePDF;
            db.SaveChanges();
            return BookingData;
        }
    }
    }
 
