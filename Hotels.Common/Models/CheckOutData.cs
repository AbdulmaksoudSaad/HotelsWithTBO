using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class CheckOutData
    {
        [Required]
        public string Sid { get; set; }
        [Required]
        public string CityName { get; set; }
        [Required]
        public string HotelID { get; set; }
        
        public string ProviderHotelID { get; set; }
        [Required]
        public string Pid { get; set; }
        [Required]
        public string RoomQty { get; set; }
        [Required]
        public string PaxQty { get; set; }
        [Required]
        public string Src { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Mail { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public double SellPrice { get; set; }
         
        public double? totalCost { get; set; }
    //    public BookingTraveller bookingTraveller { get; set; }
       public List<BookingTraveller> Travellers { get; set; }
        public HotelBookingDeliveryModel deliveryModel { get; set; }
        public CheckOutData()
        {
         //   bookingTraveller = new BookingTraveller();
            Travellers = new List<BookingTraveller>();
            deliveryModel = new HotelBookingDeliveryModel();
        }
    }
}
