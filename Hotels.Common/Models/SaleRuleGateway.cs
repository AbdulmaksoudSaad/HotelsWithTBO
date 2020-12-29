using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{

    public class MarkupAndDiscount
    {
        public bool AllowMultiDestination { get; set; }
        public bool AllowMultiAirlines { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Branch { get; set; }
        public string Product { get; set; }
        /// <summary>
        /// CommType (fixed , percentage)
        /// </summary>
        public string CommType { get; set; }
        /// <summary>
        /// related unit (segment, itinerary, journey)
        /// </summary>
        public double CommAmount { get; set; }

        public string CommRelatedUnit { get; set; }
        /// <summary>
        ///  Round value (up, exact,down )
        /// </summary>
        public string CommRound { get; set; }
        public int Priority { get; set; }
        public int ChargeId { get; set; }
        public string ChargeName { get; set; }
        /// <summary>
        /// Base(Total cost, Fare cost)
        /// </summary>
        public string Base { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsGeneric { get; set; }
        public List<Criteria> CriteriaList { get; set; }
    }


    public class DiscountList
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Branch { get; set; }
        public string Product { get; set; }
        public string commType { get; set; }
        public int commAmount { get; set; }
        public string commRelatedUnit { get; set; }
        public string commRound { get; set; }
        public int Priority { get; set; }
        public int chargeId { get; set; }
        public object chargeName { get; set; }
        public string Base { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsGeneric { get; set; }
        public bool AllowMultiDestination { get; set; }
        public bool AllowMultiAirlines { get; set; }
      //  public List<Criteria> CriteriaList { get; set; }
    }

    public class SalesRuleGateway
    {

        public List<MarkupAndDiscount> MarkupList { get; set; }
        public List<MarkupAndDiscount> DiscountList
        {
            get; set;
        }

    }
    public class Criteria
    {
        public string CriteriaName { get; set; }
        public string Operation { get; set; }
        public string Value { get; set; }
        public string TextValue { get; set; }
    }

}
