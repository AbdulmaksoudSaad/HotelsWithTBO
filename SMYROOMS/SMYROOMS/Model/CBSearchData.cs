using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
   public class CBSearchData
    {
        public string sID { set; get; }
        public string cityID { set; get; }
        public string city { set; get; }
        public string dateFrom { set; get; }
        public string dateTo { set; get; }
        public string Nationality { set; get; }
        public int rooms { set; get; }
        public string[] adults { set; get; }
        public string[] childs { set; get; }
        public string[] ages { set; get; }
        public string sortOrder { set; get; }
        public string command { set; get; }
        public string hotelName { set; get; }
        public string sAdults { set; get; }
        public string sChilds { set; get; }
        public string sAges { set; get; }
        public string IsLogin { set; get; }
        public string UserEmail { set; get; }
        public string roomsData { set; get; }
        public string Lang { set; get; }
        public string Currency { set; get; }


    }
}
