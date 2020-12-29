using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IBLL
{
    public interface ISalesRulesManager
    {
        void PrepareSearchCriteriaDic(SearchData searchCriteria);
        AppliedSalesRule ApplySalesRules(string Category);
        void SetResultCriteria(string HotelName, int starRate, double Fare, string PID);
        void SetPaymentType(string PaymentType);
    }
}
