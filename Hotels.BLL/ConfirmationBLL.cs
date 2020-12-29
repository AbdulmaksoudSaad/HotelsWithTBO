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
    public class ConfirmationBLL : IConfimationBL
    {
        public ConfirmationModel GetConfirmationData(string sid, string BN)
        {
            ConfirmationDAL confirmation = new ConfirmationDAL();

            return confirmation.GetConfirmationData(sid, BN);
        }

        public UpcomingHistory GetUpcomingAndHistoryData(string mail)
        {
            ConfirmationDAL confirmation = new ConfirmationDAL();
            return confirmation.GetUpcomingAndHistoryData(mail);
        }
    }
}
