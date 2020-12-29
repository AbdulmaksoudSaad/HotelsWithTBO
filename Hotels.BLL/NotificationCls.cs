using B2C.Hotel.Common.AdminPanleServices.BookingFlowServices;
using Hotels.Common.Models;
using Hotels.DAL;
using Hotels.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.BLL
{
    public class NotificationCls : INotificationBL
    {
        public int sendnotification(BookingStatus bookingStatus, string BN, string Sid, string Status, string message, MailObj mail)
        {
            HotelBookingDAl hotelBooking = new HotelBookingDAl();
            var Bookingdata = hotelBooking.ChangeBookingstatusAndGetNotificationData(bookingStatus, BN, Sid, mail);

            if (Bookingdata?.Sales_Channel?.ToLower() != "direct")
            {
                //Direct
                NotificationData notificationData = new NotificationData();
                notificationData.BookingNumber = BN;
                notificationData.ConfirmationStatus = Status;
                notificationData.Language = "en";
                notificationData.Message = message;
                notificationData.NotificationToken = Bookingdata?.NotificationKey;

                notificationData.PDFURL = mail.confirmationPDF;
                notificationData.SId = bookingStatus.Sid;
                notificationData.Type = "hotel";
                NotificationHelper.SendPushNotification(notificationData);
                return 10;
            }
            return 11;
        }
    }
}
