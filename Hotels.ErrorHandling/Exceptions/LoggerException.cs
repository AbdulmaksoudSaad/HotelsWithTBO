using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.ErrorHandling.Exceptions
{
    public class LoggerException:Exception
    {
        public LoggerException(string code, string reason, string msg):base(msg)
        {
            Code = code;
            Reason = reason;
        }

        public string Code { get; }
        public string Reason { get; }
    }
}
