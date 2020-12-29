using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    public class MarkUp
    {
        public int ID { set; get; }
        public string markupname { set; get; }
        public string markupbase { set; get; }
        public string CommType { set; get; }
        public double CommAmt { set; get; }
        public string CommRelatedUnit { set; get; }
        public string CommRound { set; get; }
        public int MarkupPriority { set; get; }
        public List<MarkupCriteria> MarkupCriterias { set; get; }
        public MarkUp()
        {
            MarkupCriterias = new List<MarkupCriteria>();
        }
    }
}
