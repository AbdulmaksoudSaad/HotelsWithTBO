using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IBLL
{
   public interface IConfimationBL
    {
        ConfirmationModel GetConfirmationData(string sid,string BN);
        UpcomingHistory GetUpcomingAndHistoryData(string mail);

    }
}
