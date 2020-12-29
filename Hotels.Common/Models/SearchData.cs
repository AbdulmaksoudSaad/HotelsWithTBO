using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    public class SearchData
    {
        [Required]
        public string CityName { set; get; } //city id
        [Required]
        public DateTime DateFrom { set; get; }
        [Required]
        public DateTime DateTo { set; get; }
        [Required]
        public string Currency { set; get; }
        [Required]
        public string Nat { set; get; }
        [Required]
        public string POS { set; get; }
        [Required]
        public string Source { set; get; }
        [Required]
        public string sID { set; get; } //session id all flow
        [Required]
        public string Lang { set; get; } = "en";
        [Required]
        public List<SearchRoom> SearchRooms { set; get; }
        public SearchData() {
            SearchRooms = new List<SearchRoom>();
        }
    }
}
