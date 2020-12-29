using Hotels.Common.Models;
using Hotels.DAL;
using Hotels.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.BLL
{
  public  class SearchstatisticData : ISearchStatisticBL
    {
        public List<SearchStatistic> SearchStatisticTransaction(DateTime fromDate, DateTime toDate)
        {
            SearchstatisticDA searchstatistic = new SearchstatisticDA();
          return  searchstatistic.SearchStatisticTransaction(fromDate, toDate);
            
        }
    }
}
