 
using Hotels.Common.Models;
using IntegrationTotalStay.Controller;
using IntegrationTotalStay.Model.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guest = IntegrationTotalStay.Model.Booking.Guest;

namespace IntegrationTotalStay.Management
{
  public  class BookingManager
    {
        public static BookRequest prepareSearchObj(ConfirmData data, string city)
        {
            //2019-07-27
            BookRequest Request = new BookRequest();
            Request.BookingDetails.ArrivalDate = data.fromDate;
            Request.BookingDetails.CCAmount = "0";
            Request.BookingDetails.CCCardTypeID = "0";
            Request.BookingDetails.CCIssueNumber = "0";
            Request.BookingDetails.ContractArrangementID = "0";
            Request.BookingDetails.ContractSpecialOfferID = "0";
            Request.BookingDetails.Duration = data.Dur;
            Request.BookingDetails.LeadGuestAddress1 = "" ;
            Request.BookingDetails.LeadGuestBookingCountryID = "0";
            Request.BookingDetails.LeadGuestEmail = "";
            var holder = data.hotelsBooking.Pax_Name.Split(' ');
            Request.BookingDetails.LeadGuestFirstName =  holder[0];
            Request.BookingDetails.LeadGuestLastName = holder[1];
            Request.BookingDetails.LeadGuestPhone = "0";
            Request.BookingDetails.LeadGuestPostcode ="0"   ;
            Request.BookingDetails.LeadGuestTitle ="Mr" ;
             
            Request.BookingDetails.PropertyID = data.PropertyTS;
            Request.BookingDetails.Request = "";
            Request.BookingDetails.TradeReference = data.hotelsBooking.Booking_No;
           var availRooms= data.Rooms.Select(a => a.roomResult).ToList();
       var availReqobj=AvailabiltyManager.prepareAvailabilityObj(availRooms, data.PropertyTS, data.fromDate, data.Dur);
          var availResp=  CheckAvailability.GetTSAvailability(availReqobj, data.hotelsBooking.SessionId);
            if (availResp == null)
            {
                return null;
            }
            Request.BookingDetails.PreBookingToken = availResp.PreBookingToken;
            foreach (var item in data.Rooms)
            {
                RoomBooking roomBooking = new RoomBooking();
                roomBooking.Adults = item.roomResult.Adults.Value.ToString();
                roomBooking.Children = item.roomResult.Childern.Value.ToString();
                roomBooking.Infants = "0";
                roomBooking.MealBasisID = item.roomResult.MealId;
                roomBooking.OptionalSupplements = "";
                roomBooking.PropertyRoomTypeID = item.roomResult.RoomReference;
               
                if (item.roomResult.RoomReference == "0")
                {
                    roomBooking.BookingToken = item.roomResult.ProviderBookingKey;
                }
                foreach (var pax in item.bookingPaxes)
                {
                    Guest guest = new Guest();
                    guest.Age = "0";
                    guest.DateOfBirth = "0001-01-01T00:00:00";
                    guest.Nationality = "";
                    guest.FirstName = pax.First_name;
                    guest.LastName = pax.Last_Name;
                    guest.Title = pax.Salutations;
                    guest.Type = pax.Pax_Type;
                    roomBooking.Guests.Guest.Add(guest);

                }

                Request.BookingDetails.RoomBookings.RoomBooking.Add(roomBooking);
            }
            return Request;
        }
    }
}
