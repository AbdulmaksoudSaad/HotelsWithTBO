using Hotels.Common;
using Hotels.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.DAL
{
    public class ProviderRepo : IProviderRepo
    {
        public List<GetActiveProviders_Result> GetActiveProvider()
        {
            using (BusinessRulesDBEntities db = new BusinessRulesDBEntities())
            {
             List< GetActiveProviders_Result> providers_Results= db.GetActiveProviders().ToList();
                return providers_Results;
            }
        }
    }
}
