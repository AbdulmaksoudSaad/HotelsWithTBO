using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Hotels.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.BLL
{
    public class SessionManager
    {
        public void CreateSession(string sID) {
            try
            {
                SessionRepo sessionRepo = new SessionRepo();
                sessionRepo.CreateSession(sID);
            }
            catch (Exception ex) {
                LoggingHelper.WriteToFile("SessionManager/Errors/", "CreateSession_" + sID, ex.InnerException?.Message, ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);

            }
        }
        public bool ValidSession(SearchData SD)
        {
            try
            {
                SessionRepo sessionRepo = new SessionRepo();
            return sessionRepo.ChechSessionStatus(SD);
                 
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("SessionManager/Errors/", "CreateSession_" + SD.sID, ex.InnerException?.Message, ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);
                return false;

            }
        }
    }
}
