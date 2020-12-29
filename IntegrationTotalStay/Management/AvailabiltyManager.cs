using Hotels.Common;
using Hotels.Common.Models;
using IntegrationTotalStay.Model.Availability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTotalStay.Management
{
   public class AvailabiltyManager
    {
        public static PreBookRequest prepareAvailabilityObj(List<SearchRoomResult> rooms ,string hotelcode,string arrivalData ,string Dur)
        {
            PreBookRequest preBook = new PreBookRequest();
            preBook.BookingDetails.ArrivalDate = arrivalData;
            preBook.BookingDetails.Duration = Dur;
            preBook.BookingDetails.PropertyID = hotelcode;
            foreach (var item in rooms)
            {
                RoomBooking room = new RoomBooking();
                room.Adults = item.Adults.ToString();
                room.Children = item.Childern.ToString();
                if(item.RoomReference != "0")
                {
                    room.PropertyRoomTypeID = item.RoomReference;
                }
                else
                {
                    room.BookingToken = item.ProviderBookingKey;
                }
                room.MealBasisID = item.MealId;
                room.Infants = "0";
                for (int i = 0; i < item.Childern; i++)
                {
                    ChildAge childAge = new ChildAge();
                    childAge.Age = "5";
                    room.ChildAges.ChildAge.Add(childAge);
                }
                preBook.BookingDetails.RoomBookings.RoomBooking.Add(room);
            }

            return preBook;
        }
    }
}
