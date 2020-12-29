using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.ErrorHandling.ErrorCodes
{
    public class LoggerErrorCodes
    {
        public static string FailedLogIntoLogger => "3001";
        public static string LoggerFileNotFound => "3002";
        public static string LoggerFileINProcess => "3003";
    }
}
