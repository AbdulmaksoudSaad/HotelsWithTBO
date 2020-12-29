using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    public class ResultException
    {
        public string Code { get; set; } = "";
        public string ExceptionMessage { get; set; } = "";
    }
}
