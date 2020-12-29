using Hotels.Common;
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
    public class HotelBookingCls : IHotelBooking
    {
        public string addBookingStatus(string status, string BN)
        {
            HotelBookingDAl hotelBooking = new HotelBookingDAl();
            return hotelBooking.addBookingStatus(status,BN );
        }

        public string ChangeBookingstatus(BookingStatus bookingStatus)
        {
            HotelBookingDAl hotelBooking = new HotelBookingDAl();
            return hotelBooking.ChangeBookingStatus(bookingStatus);
            
        }

        public HotelsBooking ChangeBookingstatusAndSendNotification(BookingStatus bookingStatus, string BN, string Sid, MailObj mail)
        {
            throw new NotImplementedException();
        }
    }
}
