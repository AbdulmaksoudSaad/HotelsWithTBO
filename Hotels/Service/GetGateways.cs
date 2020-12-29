using Hotels.BLL;
using Hotels.Common.Models;
using Hotels.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Hotels.Service
{
    public class GetGateways
    {
        public List<PaymentGateWay> GetPayGateWay(string BN )
        {
            try
            {
                // get Some data from DB like Currency and pos
                PayLinkDB DB = new PayLinkDB();
                SalesRulesManager ServiceChargeManager = new SalesRulesManager();
                string pos;
                List<string> gates = new List<string>();
                List<PaymentGateWay> paymentGateWays = new List<PaymentGateWay>();
                //    SalesRuleGateway salesRule = new SalesRuleGateway();
                SalesRules salesRule = new SalesRules();
                    var criterais = DB.GetDataForGatewayDA(BN  );
                //call selected Gateway
                //basecurr
                var BaseCur = ConfigurationSettings.AppSettings["BaseCur"];
                // exchangeRate
                CurrencyRepo currencyManager = new CurrencyRepo();

                double ExcahngeRate = currencyManager.GetEveryDayCurrenciesConversion(BaseCur, criterais.Curr,criterais.searchData.sID, DateTime.Now).Result.Customer_Sell_Rate;
                double  ExcahngeRateForBase = currencyManager.GetEveryDayCurrenciesConversion(criterais.Curr, BaseCur, criterais.searchData.sID, DateTime.Now).Result.Customer_Sell_Rate;

                if (criterais.Curr != null & criterais.pos != null)
                {
                    gates = GateWays.GetPaymentGatewaysAsync(criterais.Curr).Result;
                    if (gates.Count == 0)
                    {
                        return null;
                    }
                }
                else
                    return null;
                // call sales Rules 
                salesRule = GateWays.GetSaleRuleForGateAsync(criterais.pos).Result;

                if (salesRule.MarkupList.Count == 0)
                {
                    foreach (var item in gates)
                    {
                        PaymentGateWay gateWay = new PaymentGateWay();
                        gateWay.paymentMethod = item;
                        gateWay.currency = criterais.Curr;
                        gateWay.amount = 0;

                        paymentGateWays.Add(gateWay);
                    }
                    return paymentGateWays;
                }
                ServiceChargeManager.FillPaySaleRules(salesRule);
                
                ServiceChargeManager.PrepareSearchCriteriaDic(criterais.searchData);
                   
                foreach (var item in gates)
                {
                    ServiceChargeManager.SetResultCriteriaForpay(criterais.HotelName, criterais.HotelStars, criterais.cost
                                       * ExcahngeRateForBase, criterais.Pid, item);
                    //AppliedSalesRule AppliedMarkup = ServiceChargeManager.ApplySalesRules("Markup");
                    AppliedSalesRule AppliedMarkup = ServiceChargeManager.ApplyMarkupForPayGateway();
                    PaymentGateWay gateWay = new PaymentGateWay();
                    gateWay.paymentMethod = item;
                    gateWay.currency = criterais.Curr;
                    if (gateWay.currency.ToLower() =="kwd")
                        gateWay.amount = Math.Round(AppliedMarkup.Value * ExcahngeRate, 3);

                    gateWay.amount = Math.Round(AppliedMarkup.Value * ExcahngeRate, 2);


                    paymentGateWays.Add(gateWay);
                }
                return paymentGateWays;

                ///    AppliedSalesRule AppliedDiscount = ServiceChargeManager.ApplySalesRules("Discount");
                // map 
                /*  foreach (var item in salesRule.MarkupList)
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
                  }*/
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        SearchData GetSearchData(string HG,string sid)
        {
            SearchData searchData = new SearchData();

            return searchData;
        }

        RestCriteraiData GetRestCriteriaData(string HG, string sid)
        {
            RestCriteraiData CriteraiData = new RestCriteraiData();

            return CriteraiData;
        }
    }
}