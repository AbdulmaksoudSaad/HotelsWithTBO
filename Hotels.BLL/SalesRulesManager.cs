using Hotels.Common.Models;
using Hotels.DAL;
using Hotels.IBLL;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.BLL
{
    public class SalesRulesManager : ISalesRulesManager, IDisposable
    {
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public SalesRules salesRules { set; get; }
        public List<MarkUp> MarkupList { set; get; }
        public List<Discount> DiscountList { set; get; }
        public Dictionary<string, string> userSearchCriteria { set; get; }
        public SalesRulesManager() {
            MarkupList = new List<MarkUp>();
            DiscountList = new List<Discount>();
        }
        //search 
        public void FillSalesRules(string POS,string Category,string ServiceType) {
            SalesRulesRepo repo = new SalesRulesRepo();
             salesRules = repo.GetSalesRules(Category, POS, ServiceType);
            for (int i = 0; i < salesRules.MarkupList.Count; i++)
            {
                MarkUp markUp = new MarkUp() {
                    CommAmt= salesRules.MarkupList[i].commAmount,
                    ID= salesRules.MarkupList[i].ID,
                    MarkupPriority= salesRules.MarkupList[i].Priority,
                    CommRound= salesRules.MarkupList[i].commRound,
                    CommRelatedUnit= salesRules.MarkupList[i].commRelatedUnit,
                    CommType= salesRules.MarkupList[i].commType,
                    markupbase= salesRules.MarkupList[i].Base,
                    markupname= salesRules.MarkupList[i].Name,
                   
                };
                markUp.MarkupCriterias = salesRules.MarkupList[i].CriteriaList.Select(a => new MarkupCriteria()
                {
                    criterianame=a.criteriaName,
                    criteriavalue=a.value,
                    operation=a.operation,
                    markupid= salesRules.MarkupList[i].ID.ToString(),
                    CriteriaValueText=a.textValue
                }).ToList();
                MarkupList.Add(markUp);
            }
            for (int i = 0; i < salesRules.DiscountList.Count; i++)
            {
                Discount discount = new Discount()
                {
                    CommAmt = salesRules.DiscountList[i].commAmount,
                    ID = salesRules.DiscountList[i].ID,
                    DiscountPriority = salesRules.DiscountList[i].Priority,
                    commround = salesRules.DiscountList[i].commRound,
                    commrelatedunit = salesRules.DiscountList[i].commRelatedUnit,
                    commtype = salesRules.DiscountList[i].commType,
                    DiscountBase = salesRules.DiscountList[i].Base,
                    DiscountName = salesRules.DiscountList[i].Name,

                };
                discount.DiscountCriterias = salesRules.DiscountList[i].CriteriaList.Select(a => new DiscountCriteria()
                {
                    criterianame = a.criteriaName,
                    criteriavalue = a.value,
                    operation = a.operation,
                    DiscountID = salesRules.DiscountList[i].ID.ToString(),
                    CriteriaValueText= a.textValue
                }).ToList();
                DiscountList.Add(discount);
            }
        }
        public void FillPaySaleRules(SalesRules PaysalesRules)
        {
       //   SalesRulesRepo repo = new SalesRulesRepo();
            //salesRules = repo.GetSalesRules(Category, POS, ServiceType);
            for (int i = 0; i < PaysalesRules.MarkupList.Count; i++)
            {
                MarkUp markUp = new MarkUp()
                {
                    CommAmt = PaysalesRules.MarkupList[i].commAmount,
                    ID = PaysalesRules.MarkupList[i].ID,
                    MarkupPriority = PaysalesRules.MarkupList[i].Priority,
                    CommRound = PaysalesRules.MarkupList[i].commRound,
                    CommRelatedUnit = PaysalesRules.MarkupList[i].commRelatedUnit,
                    CommType = PaysalesRules.MarkupList[i].commType,
                    markupbase = PaysalesRules.MarkupList[i].Base,
                    markupname = PaysalesRules.MarkupList[i].Name,

                };
                markUp.MarkupCriterias = PaysalesRules.MarkupList[i].CriteriaList.Select(a => new MarkupCriteria()
                {
                    criterianame = a.criteriaName,
                    criteriavalue = a.value,
                    operation = a.operation,
                    markupid = PaysalesRules.MarkupList[i].ID.ToString(),
                    CriteriaValueText = a.textValue
                }).ToList();
                MarkupList.Add(markUp);
            }
           /* for (int i = 0; i < salesRules.DiscountList.Count; i++)
            {
                Discount discount = new Discount()
                {
                    CommAmt = salesRules.DiscountList[i].commAmount,
                    ID = salesRules.DiscountList[i].ID,
                    DiscountPriority = salesRules.DiscountList[i].Priority,
                    commround = salesRules.DiscountList[i].commRound,
                    commrelatedunit = salesRules.DiscountList[i].commRelatedUnit,
                    commtype = salesRules.DiscountList[i].commType,
                    DiscountBase = salesRules.DiscountList[i].Base,
                    DiscountName = salesRules.DiscountList[i].Name,

                };
                discount.DiscountCriterias = salesRules.DiscountList[i].CriteriaList.Select(a => new DiscountCriteria()
                {
                    criterianame = a.criteriaName,
                    criteriavalue = a.value,
                    operation = a.operation,
                    DiscountID = salesRules.DiscountList[i].ID.ToString(),
                    CriteriaValueText = a.textValue
                }).ToList();
                DiscountList.Add(discount);
            }*/
        }
        public void PrepareSearchCriteriaDic(SearchData searchCriteria)
        {
            int AdultCount = 0;
            int childCount = 0;
            searchCriteria.SearchRooms.ForEach(a => {
                AdultCount += a.Adult;
                childCount += a.Child.Count();
            });

            string Nat = searchCriteria.Nat;
            string source = searchCriteria.Source.ToLower() == "wego" ? "3" : "4";
            userSearchCriteria = new Dictionary<string, string>();
            userSearchCriteria.Add("Meta Search", source);
            userSearchCriteria.Add("City", searchCriteria.CityName);
            userSearchCriteria.Add("CheckIn", searchCriteria.DateFrom.ToString("MMM-dd-yyyy"));
            userSearchCriteria.Add("CheckOut", searchCriteria.DateTo.ToString("MMM-dd-yyyy"));
            userSearchCriteria.Add("PaxNo-Adult", AdultCount.ToString());
            userSearchCriteria.Add("PaxNo-Child", childCount.ToString());
            userSearchCriteria.Add("Nationality", Nat);
            userSearchCriteria.Add("Currency", searchCriteria.Currency);
            userSearchCriteria.Add("BookingTime", DateTime.Now.Date.ToString("MMM-dd-yyyy"));
            userSearchCriteria.Add("RoomsCount", searchCriteria.SearchRooms.Count.ToString());
            userSearchCriteria.Add("Duration", Convert.ToInt32((searchCriteria.DateTo - searchCriteria.DateFrom).TotalDays).ToString());
            userSearchCriteria.Add("Hotel Supplier", "");
            userSearchCriteria.Add("Star Rate", "0");
            userSearchCriteria.Add("Hotel Name", "");
            userSearchCriteria.Add("Fare", "0");

        }

        public void SetResultCriteria(string HotelName, int starRate, double Fare, string PID)
        {
            userSearchCriteria["Hotel Supplier"] = PID;
            userSearchCriteria["Star Rate"] = starRate.ToString();
            userSearchCriteria["Hotel Supplier"] = PID;
            userSearchCriteria["Fare"] = Fare.ToString();
            userSearchCriteria["Hotel Name"] = HotelName;
        }
        public void SetResultCriteriaForpay(string HotelName, int starRate, double Fare, string PID,string gateway)
        {
            userSearchCriteria["Hotel Supplier"] = PID;
            userSearchCriteria["Star Rate"] = starRate.ToString();
            userSearchCriteria["Hotel Supplier"] = PID;
            userSearchCriteria["Fare"] = Fare.ToString();
            userSearchCriteria["Hotel Name"] = HotelName;
            userSearchCriteria["Payment Gateway"] = gateway;
        }
        public void SetPaymentType(string PaymentType)
        {
            userSearchCriteria.Add("Payment Gateway", PaymentType);
        }

        public AppliedSalesRule ApplySalesRules(string Category)
        {
            AppliedSalesRule appliedSalesRule = new AppliedSalesRule();
            switch (Category.ToLower())
            {
                case "markup":
                    appliedSalesRule = ApplyMarkup();
                    break;
                case "discount":
                    appliedSalesRule = ApplyDiscount();
                    break;
            }
            return appliedSalesRule;
        }

        private AppliedSalesRule ApplyMarkup()
        {
            AppliedSalesRule appliedMarkup = new AppliedSalesRule();
            for (int i = 0; i < MarkupList.Count; i++)
            {
                bool IsAppliedMarkup = true;
                for (int j = 0; j < MarkupList[i].MarkupCriterias.Count; j++)
                {
                    IsAppliedMarkup = IsAppliedCriteria(MarkupList[i].MarkupCriterias[j].criteriavalue,
                        userSearchCriteria[MarkupList[i].MarkupCriterias[j].criterianame],
                        MarkupList[i].MarkupCriterias[j].operation);
                    if (!IsAppliedMarkup)
                        break;
                }
                if (IsAppliedMarkup)
                {
                    appliedMarkup.ID = MarkupList[i].ID;
                    appliedMarkup.Name = MarkupList[i].markupname;
                    // here  // apply markup
                    appliedMarkup.Value = GetSalesRulesValue(MarkupList[i].CommRelatedUnit,
                      MarkupList[i].CommType, MarkupList[i].CommRound, MarkupList[i].CommAmt);
                    break;
                }

            }

            return appliedMarkup;
        }

        private AppliedSalesRule ApplyDiscount()
        {
            AppliedSalesRule appliedDiscount = new AppliedSalesRule();
            for (int i = 0; i < DiscountList.Count; i++)
            {
                bool IsAppliedMarkup = true;
                for (int j = 0; j < DiscountList[i].DiscountCriterias.Count; j++)
                {
                    IsAppliedMarkup = IsAppliedCriteria(DiscountList[i].DiscountCriterias[j].criteriavalue,
                        userSearchCriteria[DiscountList[i].DiscountCriterias[j].criterianame],
                       DiscountList[i].DiscountCriterias[j].operation);
                    if (!IsAppliedMarkup)
                        break;
                }
                if (IsAppliedMarkup)
                {
                    appliedDiscount.ID = DiscountList[i].ID;
                    appliedDiscount.Name = DiscountList[i].DiscountName;
                    appliedDiscount.Value = GetSalesRulesValue(DiscountList[i].commrelatedunit,
                        DiscountList[i].commtype, DiscountList[i].commround, DiscountList[i].CommAmt);
                    break;
                }
            }
            return appliedDiscount;
        }

        private bool IsAppliedCriteria(string SalesRuleCriteriaValue, string UserValue, string operation)
        {

            bool IsApplied = false;
            double CriteriaValue = 0;
            double userValue = 0;
            bool IsNum = false;
            bool IsDate = false;
            DateTime CriteriaDate = new DateTime();
            DateTime UserDate = new DateTime();
            switch (operation.ToLower())
            {
                case "=":
                    IsApplied = SalesRuleCriteriaValue.ToLower() == UserValue.ToLower() ? true : false;
                    break;
                case "!=":
                    IsApplied = SalesRuleCriteriaValue.ToLower() != UserValue.ToLower() ? true : false;

                    break;
                case ">=":

                    IsNum = double.TryParse(SalesRuleCriteriaValue, out CriteriaValue);
                    double.TryParse(UserValue, out userValue);


                    IsDate = DateTime.TryParse(SalesRuleCriteriaValue, out CriteriaDate);

                    DateTime.TryParse(UserValue, out UserDate);
                    IsApplied = IsNum ? (userValue >= CriteriaValue ? true : false) :
                        (UserDate >= CriteriaDate ? true : false);

                    break;
                case "<=":

                    IsNum = double.TryParse(SalesRuleCriteriaValue, out CriteriaValue);
                    double.TryParse(UserValue, out userValue);

                    IsDate = DateTime.TryParse(SalesRuleCriteriaValue, out CriteriaDate);
                    DateTime.TryParse(UserValue, out UserDate);

                    IsApplied = IsNum ? (userValue >= CriteriaValue ? true : false) :
                        (userValue >= CriteriaValue ? true : false);
                    IsApplied = SalesRuleCriteriaValue.ToLower() == UserValue.ToLower() ? true : false;
                    break;

                case "include":
                    IsApplied = SalesRuleCriteriaValue.ToLower().Contains(UserValue.ToLower()) ? true : false;

                    break;
                case "range":
                    DateTime DateFrom = new DateTime();
                    DateTime DateTo = new DateTime();
                    if (SalesRuleCriteriaValue.ToLower().Contains("to"))
                    {
                        var result = SalesRuleCriteriaValue.Split('T');
                        DateFrom = DateTime.Parse(result[0]);
                        result[1] = result[1].Remove(0, 1);
                        DateTo = DateTime.Parse(result[1]);
                        UserDate = DateTime.Parse(UserValue);

                    }
                    IsApplied = UserDate >= DateFrom && UserDate <= DateTo ? true : false;

                    break;
            }
            return IsApplied;
        }

        private double GetSalesRulesValue(string RelatedUnit, string type, string Round, double Value)
        {
            int Duration = int.Parse(userSearchCriteria["Duration"]);
            double fare = double.Parse(userSearchCriteria["Fare"]);
            switch (type.ToLower())
            {
                case "fixed":
                    break;
                case "percentage":
                    Value = (fare * Value) / 100;
                    break;
            }
            switch (RelatedUnit.ToLower())
            {
                case "night":
                    break;
                case "all":
                    Value = Value / Duration;
                    break;
            }

            switch (Round.ToLower())
            {
                case "down":
                    Value = Math.Floor(Value);
                    break;
                case "up":
                    Value = Math.Ceiling(Value);
                    break;

            }
            return Value;
        }

        //MG sales rules for payment gateway
        public AppliedSalesRule ApplyMarkupForPayGateway()
        {
            AppliedSalesRule appliedMarkup = new AppliedSalesRule();
            for (int i = 0; i < MarkupList.Count; i++)
            {
                bool IsAppliedMarkup = true;
                for (int j = 0; j < MarkupList[i].MarkupCriterias.Count; j++)
                {
                    IsAppliedMarkup = IsAppliedCriteria(MarkupList[i].MarkupCriterias[j].criteriavalue,
                        userSearchCriteria[MarkupList[i].MarkupCriterias[j].criterianame],
                        MarkupList[i].MarkupCriterias[j].operation);
                    if (!IsAppliedMarkup)
                        break;
                }
                if (IsAppliedMarkup)
                {
                    appliedMarkup.ID = MarkupList[i].ID;
                    appliedMarkup.Name = MarkupList[i].markupname;
                    // here  // apply markup
                    appliedMarkup.Value = GetSalesRulesForPayGateway(MarkupList[i].CommRelatedUnit,
                      MarkupList[i].CommType, MarkupList[i].CommRound, MarkupList[i].CommAmt);
                    break;
                }

            }

            return appliedMarkup;
        }

        private double GetSalesRulesForPayGateway(string RelatedUnit, string type, string Round, double Value)
        {
            double fare = double.Parse(userSearchCriteria["Fare"]);
            switch (type.ToLower())
            {
                case "fixed":
                    break;
                case "percentage":
                    Value = (fare * Value) / 100;
                    break;
            }

            switch (Round.ToLower())
            {
                case "down":
                    Value = Math.Floor(Value);
                    break;
                case "up":
                    Value = Math.Ceiling(Value);
                    break;

            }
            return Value;
        }
        public void Dispose()
        {
            handle.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
