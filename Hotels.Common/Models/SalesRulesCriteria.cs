using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
   public class SalesRulesCriteria
    {
        public string criteriaName { set; get; }
        public string operation { set; get; }
        public string value { set; get; }
        public string textValue { set; get; }
    }
}
