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
    public class HotelAmenityBL : IHotelAmenityBL
    {
        public List<HotelAmenity> GetHotelAmenities(string hotelId)
        {
            HotelAmenityData amenityData = new HotelAmenityData();
           return amenityData.GetHotelAmenities(hotelId);
           
        }
    }
}
