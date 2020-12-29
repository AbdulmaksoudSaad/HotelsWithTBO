using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelWegolayer.Model
{
    public class WegoSearch
    {
        [Required]
        public List< string> HotelsId { set; get; } //sid
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
        public string sID { set; get; }
        [Required]
        public string Lang { set; get; }
        [Required]
        public List<SearchRoom> SearchRooms { set; get; }
        public WegoSearch()
        {
            SearchRooms = new List<SearchRoom>();
        }
    }
}
