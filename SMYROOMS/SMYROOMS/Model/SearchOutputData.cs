using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{

    public class SearchOutputData
    {

        public string supplierCode { get; set; }
        public string accessCode { get; set; }
        public string market { get; set; }
        public string hotelCode { get; set; }
        public string hotelCodeSupplier { get; set; }
        public string hotelName { get; set; }
        public string boardCode { get; set; }
        public string boardCodeSupplier { get; set; }
        public string id { get; set; }
        public string token { get; set; }
        public List<PaxBack> occupancies { get; set; }
        public List<Room> rooms { get; set; }
        public Price price { get; set; }
        public List<Supplement> supplements { get; set; }
        public List<Surcharge> surcharges { get; set; }
        public CancelPolicy cancelPolicy { get; set; }
        public string remarks { get; set; }
        public string paymentType { get; set; }
        public string status { get; set; }
        
        public SearchOutputData()
        {
            cancelPolicy = new CancelPolicy();
            occupancies = new List<PaxBack>();
            supplements = new List<Supplement>();
            surcharges = new List<Surcharge>();
            price = new Price();
            rooms = new List<Room>();
        }

    }

}
