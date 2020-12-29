using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.BLL
{
  public  class PaymentMapper
    {
        public static ValidatingPaymentModel GetVaildationModel(CheckAvailabilityResult result ,string Curr ,string SID)
        {
            ValidatingPaymentModel paymentModel = new ValidatingPaymentModel();
            CurrencyManager currencyManager = new CurrencyManager();
            var BaseCur = ConfigurationSettings.AppSettings["BaseCur"];

            double ExcahngeRate = currencyManager.GetCurrencyConversion(BaseCur, Curr, SID);
            if (result.Status == 0)
            {
                paymentModel.Status = 0;
                paymentModel.Message = "No Price Change";
            }
           else if (result.Status == 1)
            {
                paymentModel.Status = 1;
                paymentModel.Message = "Price Change";
            }
            else
            {
                paymentModel.Status = 2;
                return paymentModel;
            }
            //foreach (var item in result.Result)
            //{
            //    paymentModel.PaymentFareDetails.FareAmount += (decimal)item.TotalSellPrice ;//(decimal)(item.TotalSellPrice/ ExcahngeRate);

            //}
            paymentModel.PaymentFareDetails.FareAmount = (decimal)result.TotalCost +(decimal).02;
            paymentModel.PaymentFareDetails.TotalAmount =(decimal) result.TotalCost + (decimal).02;

            paymentModel.PaymentFareDetails.ExchangeRate = (decimal)ExcahngeRate;
            return paymentModel;
        }
    }
}
