using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBO.WSDL.hotelServiceRef;
using TBOIntegration.Models.Book.Req;

namespace TBOIntegration.Mapper
{
    public static class BookMapper
    {
        public static HotelBookRequest MapBookReq(Hotels.Common.Models.TBOBookReq bookReq)
        {
            List<TBO.WSDL.hotelServiceRef.Guest> guests = new List<TBO.WSDL.hotelServiceRef.Guest>();
            if (bookReq.PaymentInfo!=null)
            {
                Enum.TryParse(bookReq.PaymentInfo.PaymentModeType, out PaymentModeType PayModeTyp);
            }

            foreach (var item in bookReq.Guests)
            {
                Enum.TryParse(item.GuestType, out GuestType Guesttype);
                guests.Add(new TBO.WSDL.hotelServiceRef.Guest()
                {
                    Age = item.Age,
                    FirstName = item.FirstName,
                    GuestInRoom = item.GuestInRoom,
                    GuestType = Guesttype,
                    LastName = item.LastName,
                    LeadGuest = item.LeadGuest,
                    Title = item.Title
                });
            }
            List<TBO.WSDL.hotelServiceRef.RequestedRooms> rooms = new List<TBO.WSDL.hotelServiceRef.RequestedRooms>();
            foreach (var item in bookReq.HotelRooms)
            {
                List<TBO.WSDL.hotelServiceRef.SuppInfo> suppInfos = new List<TBO.WSDL.hotelServiceRef.SuppInfo>();
                if (item.Supplements != null)
                {
                    foreach (var sup in item.Supplements)
                    {
                        Enum.TryParse(sup.SuppChargeType, out SuppChargeType ChargTyp);

                        suppInfos.Add(new TBO.WSDL.hotelServiceRef.SuppInfo()
                        {
                            Price = sup.Price,
                            SuppID = sup.SuppID,
                            SuppChargeType = ChargTyp,
                            SuppIsSelected = sup.SuppIsSelected

                        });
                    }
                }

                //pass the same room combination that you have passed in AvailabilityAndPricing 
                //with same roomtypename , roomplancode and rateplancode.
                rooms.Add(new RequestedRooms()
                {
                    RatePlanCode = item.RatePlanCode,
                    RoomIndex = item.RoomIndex,
                    RoomRate = new TBO.WSDL.hotelServiceRef.Rate
                    {
                        RoomFare = item.RoomRate.RoomFare,
                        RoomTax = item.RoomRate.RoomTax,
                        TotalFare = item.RoomRate.TotalFare,

                    },
                    RoomTypeCode = item.RoomTypeCode,
                    RoomTypeName = item.RoomTypeName,
                    Supplements = suppInfos.ToArray()
                });
            }
            HotelBookRequest TBOReq = new HotelBookRequest
            {
                Guests = guests.ToArray(),
                HotelRooms = rooms.ToArray(),
                AddressInfo = new TBO.WSDL.hotelServiceRef.AddressInfo
                {
                    AddressLine1 = bookReq.AddressInfo?.AddressLine1,
                    AddressLine2 = bookReq.AddressInfo?.AddressLine2,
                    AreaCode = bookReq.AddressInfo?.AreaCode,
                    City = bookReq.AddressInfo?.City,
                    Country = bookReq.AddressInfo?.Country,
                    CountryCode = bookReq.AddressInfo?.CountryCode,
                    Email = bookReq.AddressInfo?.Email,
                    PhoneNo = bookReq.AddressInfo?.PhoneNo,
                    State = bookReq.AddressInfo?.State,
                    ZipCode = bookReq.AddressInfo?.ZipCode
                },
                ClientReferenceNumber = "070817125855789" + "#" + RandomString(4),//RandomNumber(15)+"#" + RandomString(4),
                GuestNationality = bookReq.GuestNationality,
                HotelCode = bookReq.HotelCode,
                HotelName = bookReq.HotelName,
                NoOfRooms = bookReq.NoOfRooms,
                PaymentInfo = new TBO.WSDL.hotelServiceRef.PaymentInfo
                {
                    VoucherBooking =true,// bookReq.PaymentInfo.VoucherBooking,
                    //PaymentModeType = PayModeTyp
                },
                ResultIndex = bookReq.ResultIndex,
                SessionId = bookReq.SessionId,
            };

            return TBOReq;
        }
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomNumber(int length)
        {

            //CultureInfo provider = CultureInfo.InvariantCulture;
            //string dateString = "08082010";
            //string format = "MMddyyyy";
            //DateTime result = DateTime.ParseExact(dateString, format, provider);

            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
