using System;
using System.Collections.Generic;
using System.Text;

namespace Hotels.ErrorHandling.Exceptions
{
    public class HotelSearchInputException:Exception
    {
        public HotelSearchInputException(string code, string searchId, string msg):base(msg)
        {
            Code = code;
            SearchId = searchId;
        }

        public string Code { get; set; }
        public string SearchId { get; set; }
    }
}
