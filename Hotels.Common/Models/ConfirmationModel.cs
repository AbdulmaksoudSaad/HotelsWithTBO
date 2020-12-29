using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class ConfirmationModel
    {
        public string status { set; get; }
        public string mailStatus { set; get; }
        public DateTime BookingTime { set; get; }
        public string confirmationPDF { get; set; }
        public string salesInvoicePDF { get; set; }
        public string bookingNum { set; get; }
        public string mail { set; get; }
        public string ProviderConfirmation { set; get; }
        public string PayableNote { set; get; }
        public ConfirmedHotel hotel { set; get; }
        public List<ConfirmedTraveller> travellers { set; get; }
        public List<ConfirmedRoom> rooms { get; set; }

        public ConfirmationModel()
        {
            hotel = new ConfirmedHotel();
            travellers = new List<ConfirmedTraveller>();
            rooms = new List<ConfirmedRoom>();
        }




    }
}
