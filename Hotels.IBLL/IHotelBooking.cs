using Hotels.Common;
using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IBLL
{
  public  interface IHotelBooking
    {
        String ChangeBookingstatus(BookingStatus  bookingStatus);
        String addBookingStatus(string status, string BN);
        HotelsBooking ChangeBookingstatusAndSendNotification(BookingStatus bookingStatus, string BN, string Sid, MailObj mail);

    }
}
