using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBOIntegration.Mapper;
using TBOIntegration.Models.Book.Req;

namespace TBOIntegration.Management
{
    public static class TBOConfirmationManager
    {
        //public static HotelBookReq prepareObject(ConfirmData data)
        //{
        //    //map to tbo general req 
             
        //    List<HotelRoom> hotelRooms = new List<HotelRoom>();
        //    List<Guest> guests = new List<Guest>();
        //    foreach (var room in data.Rooms)
        //    {
        //        hotelRooms.Add(new HotelRoom
        //        {
        //            RoomTypeCode = room.roomResult.RoomReference,
        //            RatePlanCode = room.roomResult.RoomCode,
        //            RoomRate = new RoomRate
        //            {
        //                //AgentMarkUp = room.roomResult.MarkupVal
        //                Currency =room.roomResult.SellCurrency,
        //                RoomFare =(decimal) room.roomResult.SellPrice,
        //               // RoomTax = room.roomResult.
        //               TotalFare = (decimal)room.roomResult.SellPrice
                        
        //            },
        //            RoomTypeName = room.roomResult.roomType,
        //            RoomIndex = room.RoomN??default(int)
        //            //Supplements  //need save in db
                    
        //        });
        //        foreach (var guest in room.bookingPaxes)
        //        {
        //            guests.Add(new Guest
        //            {
        //                Age = CalculateAge(Convert.ToDateTime(guest.DateOfBirth)),
        //                GuestInRoom = room.roomResult.PaxSQty ?? default(int),
        //                FirstName = guest.First_name,
        //                GuestType = guest.Pax_Type,
        //                LastName = guest.Last_Name,
        //                //LeadGuest = guest.
        //                Title = guest.Salutations
        //            });
        //        }
               
        //    }
        //    HotelBookReq req = new HotelBookReq
        //    {
        //        //AddressInfo = data.hotelsBooking.ad,
        //        GuestNationality = data.Rooms[0].bookingPaxes[0].Nationality,
        //        HotelCode = data.hotelsBooking.HotelProviderID,
        //        //HotelName = data.hotelsBooking.h
        //         HotelRooms = hotelRooms,
        //         Guests =guests,
        //         //AddressInfo=
        //         //NoOfRooms = data.
        //    };
        //    //map to tbo req
        //    return req;
        //}



        /// <summary>  
        /// For calculating only age  
        /// </summary>  
        /// <param name="dateOfBirth">Date of birth</param>  
        /// <returns> age e.g. 26</returns>  
        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }
    }
}
