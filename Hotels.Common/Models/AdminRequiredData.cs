using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class AdminRequiredData
    {
        public string bookingNumber { get; set; }
        public string status { get; set; }
        public string customerEmail { get; set; }
        public DateTime creationDate { get; set; }
        public string proveider { get; set; }
        public DateTime checkin { get; set; }
        public DateTime checkout { get; set; }
        public string LeadFirstName { get; set; }
        public string LeadLastName { get; set; }
        public string proveiderName { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }



    }
}
