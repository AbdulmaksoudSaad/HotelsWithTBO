using Hotels.Common;
using IntegrationTotalStay.Model.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBedsIntegration.Models
{
    public class BookingStatus
    {
        public int status { get; set; }
        public Booking booking { get; set; }
        public BookingConfirmationData confirmationData { get; set; }
        public BookingStatus() {
            booking = new Booking();
            confirmationData = new BookingConfirmationData();
        }
    }
}
