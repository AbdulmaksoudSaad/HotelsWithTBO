using HotelBedsIntegration.Models;
using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBedsIntegration.Management
{
    public class ConfirmationManagerHB
    {
       public static  BookingReq prepareObject(ConfirmData data)
        {
            BookingReq booking = new BookingReq();
          var FullName=   data.hotelsBooking.Pax_Name.Split(' ');
            booking.holder = new Holder();
            booking.rooms = new List<RoomB>();
            booking.holder.name = FullName[0];
            booking.holder.surname = FullName[1];
            booking.paymentData = null;
            booking.clientReference = data.hotelsBooking.Booking_No;
            for (int i = 0; i < data.Rooms.Count; i++)
            {
                RoomB room = new RoomB();
                room.paxes = new List<PaxB>();

                room.rateKey = data.Rooms[i].Rate;
                foreach (var item in data.Rooms[i].bookingPaxes)
                {
                    PaxB pax = new PaxB();
                    pax.roomId = "1";
                    pax.name = item.First_name;
                    pax.surname = item.Last_Name;
                    if (item.Pax_Type.ToLower() == "adult")
                    {
                        pax.type = "AD";
                    }
                    else
                    {
                        pax.type = "CH";
                    }
                    room.paxes.Add(pax);
                }
                booking.rooms.Add(room);
            }
            return booking;
        }
    }
}
