using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IBLL
{
   public interface ISearchStatisticBL
    {
        List<SearchStatistic> SearchStatisticTransaction(DateTime fromDate, DateTime toDate);
    }
}
