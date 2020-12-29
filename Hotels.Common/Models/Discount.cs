using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    public class Discount
    {
        public int ID { set; get; }
        public string DiscountName { set; get; }
        public int DiscountPriority { set; get; }
        public string commrelatedunit { set; get; }
        public string commround { set; get; }
        public string commtype { set; get; }
        public string DiscountBase { set; get; }
        public double CommAmt { set; get; }
        public List<DiscountCriteria> DiscountCriterias { set; get; }
        public Discount()
        {
            DiscountCriterias = new List<DiscountCriteria>();
        }

    }
}
