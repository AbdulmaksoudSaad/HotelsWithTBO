using Hotels.Common;
using Hotels.Common.Models;
using Hotels.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBO.DAL.Models.Context;
namespace Hotels.DAL
{
    public class TBORepo : ITBORepo
    {
        private TBOContext _context;



        public List<string> GetHotelIdsByCityCode(string city)
        {
            using (_context = new TBOContext())
            {
                return _context.HotelDetails.Where(cty => cty.CityCode == city).Select(hot => hot.HotelCode).Take(250).ToList();
            }
        }


        public List<HotelDetails> GetHotelDetails(List<string> HotelProviderIds)
        {
            List<TBO.DAL.Models.HotelDetail> TBODetails = new List<TBO.DAL.Models.HotelDetail>();
            using (_context = new TBOContext())
            {
                foreach (var Id in HotelProviderIds)
                {
                    var HotelDet = _context.HotelDetails.Where(d => d.HotelCode == Id).FirstOrDefault();
                    TBODetails.Add(HotelDet);

                }
            }

            // map TBO DB Table HotelDetails to general
            List<HotelDetails> hotelDetails = new List<HotelDetails>();
            foreach (var det in TBODetails)
            {
                Enum.TryParse(det.HotelRating, out HotelStars stars);
                HotelDetails hotel = new HotelDetails
                {
                    Address = det.Address,
                    City = det.CityName,
                    Country = det.CountryName,
                    HotelId = det.HotelCode,
                    ProviderHotelId = det.HotelCode,
                    HotelName = det.HotelName,
                    Lat = det.Map,
                    Location = det.HotelLocation,
                    LongDescriptin = det.Description,
                    Zipcode = det.PinCode,
                    Rating = ((int)stars + 1).ToString(),
                    ShortDescription = det.Attraction,
                    //hotelAmenities=
                    //Images=
                };
                hotelDetails.Add(hotel);

            }

            return hotelDetails;
        }


        public TBOBookReq GetBookReqData(string SID, string BN)
        {

            HotelBookingDBEntities db = new HotelBookingDBEntities();
            SearchDBEntities SDB = new SearchDBEntities();

            var psid = SDB.ProviderSessions.FirstOrDefault(ss => ss.SearchId == SID).PSession;
            var searchcriteria = SDB.SearchCriterias.FirstOrDefault(sc => sc.sID == SID);

            var paxes = db.HotelBookingPaxs.Where(px => px.SID == SID).ToList();
            var bookdata = db.HotelsBookings.FirstOrDefault(px => px.SessionId == SID && px.Booking_No == BN);

            //var searchRooms = SDB.SearchRoomResults.Where(r => r.sID == SID && r.HotelCode == bookdata.Hotel_ID).ToList() ;
            var searchHotelRes = SDB.SearchHotelResults.FirstOrDefault(h => h.sID == SID && h.HotelCode == bookdata.Hotel_ID);
            var mainTraveller = db.Customers.FirstOrDefault(cus => cus.ID.ToString() == bookdata.Customer_ID);
            var Bookingrooms = db.HotelBookingRooms.Where(rom => rom.SID == SID).ToList();

            // get rooms indexes
            List<SearchRoomResult> Roomrates = new List<SearchRoomResult>();
            var Paxes = db.HotelBookingPaxs.Where(a => a.SID == SID && a.Booking_No == BN).ToList();
            //list of room indx
            var rooms = Paxes.Where(x => x.PaxNo == 1).Select(a => a.Room_No.Value.ToString()).ToList();

            foreach (var item in rooms)
            {
                Roomrates.Add(SDB.SearchRoomResults.Where(a => a.RoomCode == item && a.sID == SID && a.HotelCode == bookdata.Hotel_ID).FirstOrDefault());
            }
            //

            List<Guest> guests = new List<Guest>();
            //foreach (var room in Bookingrooms)
            //{
            int c = 0;
            foreach (var pax in paxes)
            {


                int age = 0;
                if (pax.Pax_Type.ToLower() == "adult")
                {
                    c = 0;  //case next was not chd to reset chd ages

                    age = 30;
                    guests.Add(new Guest
                    {
                        Age = age, //TBORepo.CalculateAge(Convert.ToDateTime(pax.DateOfBirth)),
                        FirstName = pax.First_name,
                        LeadGuest = pax.Lead,
                        GuestInRoom = pax.RoomRef ?? default(int),
                        GuestType = pax.Pax_Type,
                        LastName = pax.Last_Name,
                        Title = pax.Salutations

                    });
                }
                else
                {
                    var PCHDAges = SDB.ProviderSessions.FirstOrDefault(pp => pp.SearchId == SID && pp.RoomRef == pax.RoomRef);
                    string[] values = PCHDAges.ChildAges.Split(',');
                    if (c > 1)
                    {
                        c = 0;
                    }

                    age = int.Parse(values[c]);
                    guests.Add(new Guest
                    {
                        Age = age, //TBORepo.CalculateAge(Convert.ToDateTime(pax.DateOfBirth)),
                        FirstName = pax.First_name,
                        LeadGuest = pax.Lead,
                        GuestInRoom = pax.RoomRef ?? default(int),
                        GuestType = pax.Pax_Type,
                        LastName = pax.Last_Name,
                        Title = pax.Salutations

                    });
                    if (values.Length > 1)
                    {
                        c++;
                    }
                }

            }

            //}

            //calc total in usd curr
            double? total = 0;
            List<Hotels.Common.Models.HotelRoom> Rooms = new List<Hotels.Common.Models.HotelRoom>();
            foreach (var room in Roomrates)
            {
                var roomcod = int.Parse(room.RoomCode);
                //suplement
                List<Hotels.Common.Models.Supplement> supplements = new List<Hotels.Common.Models.Supplement>();
                var supps = SDB.Supplements.Where(sup => sup.SID == SID && sup.HotelCode == bookdata.Hotel_ID
                     && sup.RoomIndex == roomcod);
                if (supps != null)
                {
                    foreach (var sp in supps)
                    {
                        supplements.Add(new Common.Models.Supplement
                        {
                            Price = (decimal)sp.Price,
                            SuppChargeType = sp.ChargeType,
                            //SuppID =sp.
                            SuppIsSelected = sp.IsSelected ?? default(bool)
                        });
                        total += sp.Price;
                    }

                }
                Rooms.Add(new Common.Models.HotelRoom
                {
                    RoomIndex = int.Parse(room.RoomCode),
                    Supplements = supplements,
                    RoomTypeName = room.RoomName,
                    RatePlanCode = room.RoomReference,
                    RoomTypeCode = room.roomType,
                    RoomRate = new RoomRate
                    {
                        RoomTax = decimal.Parse(room.rateType),  // tax
                        RoomFare = decimal.Parse(room.rateClass), //base fare
                        TotalFare = (decimal)room.costPrice,
                    }

                });
                total += room.costPrice;
            }
            //map to geral tbo req
            TBOBookReq req = new TBOBookReq
            {
                SessionId = psid,
                Guests = guests,
                //AddressInfo = new AddressInfo
                //{
                //},
                GuestNationality = searchcriteria.passengerNationality,
                HotelRooms = Rooms,
                HotelCode = bookdata.Hotel_ID,
                //HotelName =bookdata.na
                NoOfRooms = Rooms.Count,

                ResultIndex = searchHotelRes.ResIndex ?? default(int),

                TotalNet = total
            };

            return req;
        }
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
