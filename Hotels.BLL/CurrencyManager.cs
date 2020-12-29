using Hotels.Common;
using Hotels.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.BLL
{
    public class CurrencyManager
    {
        public double GetCurrencyConversion(string From, string To , string SID) {
            CurrencyRepo repo = new CurrencyRepo();
            EveryDayCurrenciesConversion conversion= repo.GetEveryDayCurrenciesConversion(From, To,SID,DateTime.Now).Result;
           return conversion.Customer_Sell_Rate;
        }
    }
}
