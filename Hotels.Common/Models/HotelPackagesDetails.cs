using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class HotelPackagesDetails
    {
        
        public string hotelCode { get; set; }
       
        public string hotelName { get; set; }
      
        public double hotelRate { get; set; }
       
        public string providerID { get; set; }
       
        public string providerHotelCode { get; set; }
       
        public string providerHotelID { get; set; }
         
        public string hotelThumb { get; set; }
       
        public string hotelDescription { get; set; }
     
        public string shortcutHotelDescription { get; set; }
        
        public List<string> hotelImages { get; set; }
          
        public string Location { get; set; }
     
        public int hotelStars { get; set; }
         
        public string Lat { get; set; }
    
        public string Lng { get; set; }
        
        public string City { get; set; }
         
        public string Country { get; set; }
        
        public string ZipCode { get; set; }
        
        public string Address { get; set; }
       
        public List<RoomPackages> packages { get; set; }

        public HotelPackagesDetails()
        {
             packages = new List<RoomPackages>();
        }
    }
}
