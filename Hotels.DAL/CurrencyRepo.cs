using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.DAL
{
    public class CurrencyRepo
    {
        public async Task<EveryDayCurrenciesConversion> GetEveryDayCurrenciesConversion(string fromCurrency, string toCurrency, string SID, DateTime dateTime)
        {
            ConverterCurrency currencyData = new ConverterCurrency();
            using (BusinessRulesDBEntities db = new BusinessRulesDBEntities())
            {
                if (fromCurrency == null)
                {
                    fromCurrency = "KWD";
                }
                if (fromCurrency.ToLower() == "769")
                {
                    fromCurrency = "KWD";
                }
                if (fromCurrency.ToLower() == toCurrency.ToLower())
                {
                    return new EveryDayCurrenciesConversion()
                    {
                        ToCurrency = toCurrency,
                        FromCurrency = fromCurrency,
                        Customer_Purchase_Rate = 1.0,
                        Customer_Sell_Rate = 1.0,
                        Supplier_Purchase_Rate = 1.0,
                        Supplier_Sell_Rate = 1.0
                    };
                }
                if (dateTime.ToString("dd MMM yyyy").ToLower() == DateTime.Now.ToString("dd MMM yyyy").ToLower())
                {
                    var client = new HttpClient();
                    var path = ConfigurationSettings.AppSettings["CurrApi"];

                    var url = path + "/api/CurrencyApi?fromCurrency=" + fromCurrency + "&toCurrency=" + toCurrency;
                    var response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        currencyData = await response.Content.ReadAsAsync<ConverterCurrency>();
                    }
                    else
                    {

                    }
                    if (currencyData != null)  //Check Change Rate == 0
                    {
                        //Log Exchange Rate

                        LoggingHelper.WriteToFile("CurrencyExchangeRate", "SearchID " + SID, "fromCurrency = " + fromCurrency + " & toCurrency = " + toCurrency, "CurrencyExchangeRateVal " + currencyData.ExchangeRate);

                        return new EveryDayCurrenciesConversion()
                        {
                            ToCurrency = currencyData.ToCurrency,
                            FromCurrency = currencyData.FromCurrency,
                            Customer_Purchase_Rate = currencyData.ExchangeRate,
                            Customer_Sell_Rate = currencyData.ExchangeRate,
                            Supplier_Purchase_Rate = currencyData.ExchangeRate,
                            Supplier_Sell_Rate = currencyData.ExchangeRate
                        };
                    }
                    else
                    {
                        return null;
                    }
                }

                return null;
            }
        }

        /*   var todayCurrConv = db.DailyCurrencies.FirstOrDefault(e => e.FromCurrency.ToLower() == fromCurrency.ToLower()
                                                                         && e.ToCurrency.ToLower() ==
                                                                         toCurrency.ToLower());
           if (todayCurrConv != null)
               return new EveryDayCurrenciesConversion()
               {
                   ToCurrency = todayCurrConv.ToCurrency,
                   FromCurrency = todayCurrConv.FromCurrency,
                   Customer_Purchase_Rate = todayCurrConv.Customer_Purchase_Rate,
                   Customer_Sell_Rate = todayCurrConv.Customer_Sell_Rate,
                   Supplier_Purchase_Rate = todayCurrConv.Supplier_Purchase_Rate,
                   Supplier_Sell_Rate = todayCurrConv.Supplier_Sell_Rate
               };
           // Check the opposite conversion
           todayCurrConv = db.DailyCurrencies.FirstOrDefault(e => e.FromCurrency.ToLower() == toCurrency.ToLower()
                                                                     && e.ToCurrency.ToLower() == fromCurrency.ToLower());
           if (todayCurrConv != null)
               return new EveryDayCurrenciesConversion()
               {
                   ToCurrency = todayCurrConv.ToCurrency,
                   FromCurrency = todayCurrConv.FromCurrency,
                   Customer_Purchase_Rate = 1.000 / todayCurrConv.Customer_Purchase_Rate,
                   Customer_Sell_Rate = 1.000 / todayCurrConv.Customer_Sell_Rate,
                   Supplier_Purchase_Rate = 1.000 / todayCurrConv.Supplier_Purchase_Rate,
                   Supplier_Sell_Rate = 1.000 / todayCurrConv.Supplier_Sell_Rate
               };*/
        //else .. not Today
        /*   var theDay = dateTime.ToString("dd MMM yyyy").ToLower();

           var currConv = db.EveryDayCurrenciesConversions.FirstOrDefault(e =>
               e.FromCurrency.ToLower() == fromCurrency.ToLower()
               && e.ToCurrency.ToLower() == toCurrency.ToLower()
               && e.Date.ToLower() == theDay);
           if (currConv != null)
               return currConv;

           currConv = db.EveryDayCurrenciesConversions.FirstOrDefault(e => e.FromCurrency.ToLower() == toCurrency.ToLower()
                                                                              && e.ToCurrency.ToLower() ==
                                                                              fromCurrency.ToLower()
                                                                              && e.Date.ToLower() == theDay);
           if (currConv != null)
               return new EveryDayCurrenciesConversion()
               {
                   ToCurrency = currConv.ToCurrency,
                   FromCurrency = currConv.FromCurrency,
                   Customer_Purchase_Rate = 1.000 / currConv.Customer_Purchase_Rate,
                   Customer_Sell_Rate = 1.000 / currConv.Customer_Sell_Rate,
                   Supplier_Purchase_Rate = 1.000 / currConv.Supplier_Purchase_Rate,
                   Supplier_Sell_Rate = 1.000 / currConv.Supplier_Sell_Rate
               };*/



    }
}
