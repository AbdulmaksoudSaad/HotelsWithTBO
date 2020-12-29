using Hotels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IDAL
{
    public interface IProviderRepo
    {
        List<GetActiveProviders_Result> GetActiveProvider();
       

    }
}
