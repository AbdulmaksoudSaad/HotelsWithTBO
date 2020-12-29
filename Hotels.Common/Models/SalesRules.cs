using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    public class SalesRules
    {

        public List<MarkupRule> MarkupList { set; get; }
        public List<DiscountRule> DiscountList { set; get; }

        public SalesRules()
        {
            MarkupList = new List<MarkupRule>();
            DiscountList = new List<DiscountRule>();
        }
    }
}
