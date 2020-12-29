using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
   public class Country
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string phoneCode { get; set; }
        public string region { get; set; }
        public string regionCode { get; set; }
    }
}
