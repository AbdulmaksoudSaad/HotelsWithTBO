using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class ClsProvider
    { 
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public bool status { get; set; }
        public string Currency { get; set; }

    }
}
