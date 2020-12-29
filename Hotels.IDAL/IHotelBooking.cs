using Hotels.Common;
using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IDAL
{
   public  interface IHotelBookingStatus
    {
        String ChangeBookingStatus(BookingStatus bookingStatus);
        String addBookingStatus(string status,string BN);
        HotelsBooking ChangeBookingstatusAndGetNotificationData(BookingStatus bookingStatus, string BN, string Sid, MailObj mail);


    }
}
