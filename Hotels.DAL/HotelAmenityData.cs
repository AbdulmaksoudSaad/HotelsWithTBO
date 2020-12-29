using Hotels.Common;
using Hotels.Common.Helpers;
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
  public  class HotelAmenityData : IHotelAmenity
    {
        public List<HotelAmenity> GetHotelAmenities(string hotelId)
        {
            try
            {
                //need to pass provider Id
                //hotelsDBEntities hotelsDB = new hotelsDBEntities();
                TBOContext tBOContext = new TBOContext();
                List<HotelAmenity> amentiesHotel = new List<HotelAmenity>();
                //List<HotelAmenity> amenties = (from A in hotelsDB.HotelsAmenities
                //                               where A.hotelID == hotelId
                //                               select new HotelAmenity()
                //                               {
                //                                   Amenity = A.amenitie,
                //                                   HotelCode = A.hotelID,
                //                                   status = A.Status
                //                               }).ToList();
                List<HotelAmenity> amenties = (from A in tBOContext.Facilities
                                               where A.HotelCode == hotelId
                                               select new HotelAmenity()
                                               {
                                                   Amenity = A.FacilityVal,
                                                   HotelCode = A.HotelCode,
                                               }).ToList();
                var hotelAmenities = amenties.Where(a => a.HotelCode == hotelId).Distinct().ToList();
                foreach (var am in hotelAmenities)
                {
                    am.Amenity = am.Amenity.Replace("  ", String.Empty);
                    if (amentiesHotel.FirstOrDefault(x => x.Amenity == am.Amenity) == null)
                    {

                        amentiesHotel.Add(am);
                    }
                }
                return amentiesHotel;
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("SearchController/Errors/", "GetHotelAmenities" + "INController" + hotelId, ex.InnerException?.Message, ex.Message + ex.StackTrace);

                return new List<HotelAmenity>();
            }
        }
    }
}
