using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IDAL
{
   public interface ISearchstatisticDA
    {
        List<SearchStatistic> SearchStatisticTransaction(DateTime fromDate, DateTime toDate);
        void AddMetaSearchStatistic(CheckOutData data ,string BN );


    }
}
