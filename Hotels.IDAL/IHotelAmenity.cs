using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IDAL
{
  public interface IHotelAmenity
    {
        List<HotelAmenity> GetHotelAmenities(string hotelId);
    }
}
