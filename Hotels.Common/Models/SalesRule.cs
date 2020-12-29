using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    public class SalesRule
    {
        public MarkupRule Markup { set; get; }
        public DiscountRule Discount { set; get; }
    }
}
