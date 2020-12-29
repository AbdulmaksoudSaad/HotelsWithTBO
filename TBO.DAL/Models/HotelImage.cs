using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBO.DAL.Models
{
    public class HotelImage
    {
        public int HotelImageId { get; set; }
        public string URL { get; set; }
        public string HotelCode { get; set; }
        public int HotelDetailId { get; set; }
        public HotelDetail HotelDetail { get; set; }
    }
}
