using Hotels.Common.Models;
using Hotels.DAL;
using Hotels.IBLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.BLL
{
    public class PayGateway  
    {
      /*  public List<PaymentGateWay> GetPayGateWay(string BN, string Sid)
        {
            try
            {
                // get Some data from DB like Currency and pos
                PayLinkDB DB = new PayLinkDB();
                string pos;
                List<string> gates = new List<string>();
                List<PaymentGateWay> paymentGateWays = new List<PaymentGateWay>();
                SalesRuleGateway salesRule = new SalesRuleGateway();
                var curr = DB.GetDataForGatewayDA(BN, Sid, out pos);
                //call selected Gateway
                //basecurr
                var BaseCur = ConfigurationSettings.AppSettings["BaseCur"];
                // exchangeRate
                CurrencyRepo currencyManager = new CurrencyRepo();

                double ExcahngeRate = currencyManager.GetEveryDayCurrenciesConversion(BaseCur, curr, DateTime.Now).Customer_Sell_Rate;

                if (curr != null & pos != null)
                {
                    gates = GateWays.GetPaymentGatewaysAsync(curr).Result;
                    if (gates.Count == 0)
                    {
                        return null;
                    }
                }
                else
                    return null;
                // call sales Rules 
                salesRule = SaleRule.GetSaleRuleForGateAsync(pos).Result;
                if (salesRule == null)
                {
                    return null;
                }
                if (salesRule.MarkupList.Count == 0)
                {
                    return null;
                }

                // map 
                foreach (var item in salesRule.MarkupList)
                {
                    foreach (var gate in item.CriteriaList)
                    {
                        foreach (var g in gates)
                        {


                            if (g.ToLower() == gate.value.ToLower())
                            {
                                PaymentGateWay gateWay = new PaymentGateWay();
                                gateWay.paymentMethod = g;
                                gateWay.currency = BaseCur;
                                gateWay.amount = item.commAmount;
                                gateWay.exchangeRate = ExcahngeRate;
                                paymentGateWays.Add(gateWay);
                            }
                        }
                    }
                }
                return paymentGateWays;
            }
            catch (Exception ex)
            {
                return null;
            }
        }*/
    }
}
