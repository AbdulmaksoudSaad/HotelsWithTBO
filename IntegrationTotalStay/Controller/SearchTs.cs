using IntegrationTotalStay.Model;
using IntegrationTotalStay.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTotalStay.Controller
{
   public  class SearchTs
    {
        public static List<PropertyResult> GetTSHotels(SearchRequest search)
        {

            search.LoginDetails.Login = "Citybookerstest";
            search.LoginDetails.Password = "xmltest";
            var res = SearchServ.getTsSearhHotel(search);
            if (res != null)
            {
                return res;
            }
            return null;
        }
    }
}
