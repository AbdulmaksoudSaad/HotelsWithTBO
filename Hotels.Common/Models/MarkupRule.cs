using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    public class MarkupRule
    {

        public int ID { set; get; }

        public string Name { set; get; }

        public bool Status { set; get; }

        public string Branch { set; get; }

        public string Product { set; get; }

        public string commType { set; get; }

        public double commAmount { set; get; }

        public string commRelatedUnit { set; get; }

        public string commRound { set; get; }

        public int Priority { set; get; }

        public int chargeId { set; get; }

        public string chargeName { set; get; }

        public string Base { set; get; }

        public DateTime StartDate { set; get; }

        public DateTime EndDate { set; get; }

        public bool IsGeneric { set; get; }

        public bool AllowMultiDestination { get; set; }

        public bool AllowMultiAirlines { get; set; }

        public List<SalesRulesCriteria> CriteriaList { get; set; }
    }
}
