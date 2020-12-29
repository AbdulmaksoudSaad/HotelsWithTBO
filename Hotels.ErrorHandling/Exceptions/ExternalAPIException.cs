using System;
using System.Collections.Generic;
using System.Text;

namespace Hotels.ErrorHandling.Exceptions
{
    public class ExternalAPIException:Exception
    {
        public ExternalAPIException(string code, string reason, string Msg):base(Msg)
        {
            Code = code;
            Reason = reason;
        }

        public string Code { get; }
        public string Reason { get; }
    }
}
