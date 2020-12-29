using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    public class DiscountCriteria
    {
        public string DiscountID { set; get; }
        public string criterianame { set; get; }
        public string criteriavalue { set; get; }
        public string operation { set; get; }
        public string CriteriaValueText { set; get; }
    }
}
