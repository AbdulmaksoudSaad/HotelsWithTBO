using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class BookingTraveller
    {
        
        [Required]
        public int roomNo { get; set; } 
       
        public string paxType { get; set; }

        public bool Main { get; set; } // 

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Must be at least 3 characters long.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        public string firstName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Must be at least 3 characters long.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        public string lastName { get; set; }
        [Required]
        public string salutation { get; set; }
        
        public string nationality { get; set; }
         
        public DateTime DateOfBirth { get; set; }
        public string BirthOfDate_string { get; set; }
        public int TravellerId { set; get; } // 1  must be one 
        public string phone { get; set; }
        public string phoneCode { get; set; }
        public int roomRef { get; set; }
    }
}
