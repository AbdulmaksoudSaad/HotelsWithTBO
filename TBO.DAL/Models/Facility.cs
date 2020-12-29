using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBO.DAL.Models
{
    public class Facility
    {
        public int FacilityId { get; set; }
        public string FacilityVal { get; set; }
        public string HotelCode { get; set; }

        public int HotelDetailId { get; set; }

        public virtual HotelDetail HotelDetail { get; set; }
    }
}
