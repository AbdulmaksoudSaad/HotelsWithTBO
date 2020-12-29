using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.DAL
{
   public class SalesRulesRepo
    {
        public string ConnectionString { set; get; }
        public SalesRulesRepo()
        {
            ConnectionString = new BusinessRulesDBEntities().Database.Connection.ConnectionString;

        }
        //  call to sales rules ////////////////////////////////////////////////////////////////
        public async Task<SalesRules> GetMarkupandDiscount(string Category, string POS, string Service)
        {
                try
                {
                    string path = ConfigurationSettings.AppSettings["SaleRuleUrl"];
                    var client = new HttpClient();
                    var url = path + "/api/salesRules?category=hotel&pos=" + POS + "&service="+Service;

                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    var response = client.GetAsync(url).Result;
                    stopWatch.Stop(); TimeSpan ts = stopWatch.Elapsed;
                    LoggingHelper.WriteToFile("GetSaleRuleForGateWay/", "InGateway", "Response Data For "+Service, " response time " + ts.ToString() + " Status " + response.StatusCode +" URL "+ url);

                    if (response.IsSuccessStatusCode)
                    {
                        var Data = await response.Content.ReadAsAsync<SalesRules>();
                        return Data;
                    }
                    else
                    {

                        //    LogData.WriteToFile("c:/HotelsB2C/Logs/HBLogs/SearchResponses", "SearchResponses_" + SessionID, "SearchResponses", Newtonsoft.Json.JsonConvert.SerializeObject(response));
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    //var requestData = JsonConvert.SerializeObject(ex);
                    // LogData.WriteToFile("c:/HotelsB2C/Logs/HBLogs/SearchException", "SearchException_" + sid, "SearchException", requestData);
                    return null;
                }
        }
        // first call to sales rules ////////////////////////////////////////////////////////////////
        public SalesRules GetSalesRules(string Category, string POS, string Service)
        {

            SalesRules salesRules = new SalesRules();
            try
            {
                var salesRulesData = GetMarkupandDiscount(Category, POS, Service).Result;
                if (salesRulesData != null)
                {
                    salesRules.MarkupList = salesRulesData.MarkupList;
                    salesRules.DiscountList = salesRulesData.DiscountList;
                }
            }
            catch (Exception ex)
            {
               // CBExceptionsLog.RecordException(ex);
            }
            return salesRules;

        }
        ///////Un used XXXXXXXXXXXXXXXXXXXXX /////////////////////////////////////////////
        private List<MarkupRule> GetMarkup(string Category, string POS, string service)
        {
            List<MarkupRule> list = new List<MarkupRule>();
            if (string.IsNullOrEmpty(Category))
            {
                Category = "Hotel";
            }
            if (string.IsNullOrEmpty(POS))
            {
                POS = "KW";
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                DataSet DS = new DataSet();
                SqlDataAdapter DA = new SqlDataAdapter();
                DS.Clear();
                SqlCommand com = new SqlCommand();
                com.Connection = connection;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "[dbo].[GetSalesMarkup]";
                com.Parameters.Add(new SqlParameter("pos", POS));
                com.Parameters.Add(new SqlParameter("category", Category));
                com.Parameters.Add(new SqlParameter("Service", service));
                DA.SelectCommand = com;
                DA.Fill(DS);
                com.Dispose();
                DA.Dispose();
                connection.Close();
                if (DS.Tables.Count > 0)
                {
                    list = DS.Tables[0].AsEnumerable().Select(row => new MarkupRule()
                    {
                        ID = row.Field<int>("ID"),
                        Name = row.Field<string>("MarkupName"),
                        Base = row.Field<string>("MarkupBase"),
                        chargeId = row.Field<int>("ChargeId"),
                        Branch = row.Field<string>("MarkupBranch"),
                        commType = row.Field<string>("CommType"),
                        commAmount = row.Field<double>("CommAmt"),
                        Product = row.Field<string>("MarkupProduct"),
                        commRelatedUnit = row.Field<string>("CommRelatedUnit"),
                        StartDate = row.Field<DateTime>("MarkupStartDate"),
                        EndDate = row.Field<DateTime>("MarkupEndDate"),
                        Status = row.Field<bool>("MarkupStatus"),
                        Priority = row.Field<int>("MarkupPriority"),
                        commRound = row.Field<string>("CommRound"),


                    }).ToList();

                    List<SalesMarkupCriteria> criteriaList = DS.Tables[1].AsEnumerable().Select(row => new SalesMarkupCriteria()
                    {
                        MarkupID = row.Field<string>("MarkupID"),
                        MarkupCriteria = row.Field<string>("criteriaName"),
                        operation = row.Field<string>("operation"),
                        CriteriaValue = row.Field<string>("CriteriaValue"),
                        CriteriaValueText = row.Field<string>("CriteriaValueText"),

                    }).ToList();
                    List<SalesRulesFlightOption> flighoption = new List<SalesRulesFlightOption>();
                    if (Category.ToLower() == "flight")
                    {
                        using (BusinessRulesDBEntities db = new BusinessRulesDBEntities())
                        {
                            List<string> markupids = list.Select(a => a.Name).ToList();
                            flighoption = db.SalesRulesFlightOptions.Where(a => a.Category.ToLower() == "markup" && markupids.Contains(a.CategotyId)).ToList();
                        }
                    }
                    foreach (var item in list)
                    {
                        if (Category.ToLower() == "flight")
                        {
                            var flighoptiondata = flighoption.Where(a => a.CategotyId == item.Name).FirstOrDefault();
                            if (flighoptiondata != null)
                            {
                                item.AllowMultiAirlines = flighoptiondata.AllowMultiAirlines.Value;
                                item.AllowMultiDestination = flighoptiondata.AllowMultiDestination.Value;
                            }
                        }
                        item.CriteriaList = criteriaList.Where(a => a.MarkupID == item.Name).Select(a => new SalesRulesCriteria
                        {
                            criteriaName = a.MarkupCriteria,
                            operation = a.operation,
                            textValue = a.CriteriaValueText,
                            value = a.CriteriaValue

                        }).ToList();
                        if (item.CriteriaList.Count == 0)
                        {
                            item.IsGeneric = true;
                        }
                    }
                }
                return list;
            }
        }
        private List<DiscountRule> GetDiscount(string Product, string POS, string service)
        {

            List<DiscountRule> list = new List<DiscountRule>();
            if (string.IsNullOrEmpty(Product))
            {
                Product = "Flight";
            }
            if (string.IsNullOrEmpty(POS))
            {
                POS = "KW";
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                DataSet DS = new DataSet();
                SqlDataAdapter DA = new SqlDataAdapter();
                DS.Clear();
                SqlCommand com = new SqlCommand();
                com.Connection = connection;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "[dbo].[GetSalesDiscount]";
                com.Parameters.Add(new SqlParameter("pos", POS));
                com.Parameters.Add(new SqlParameter("category", Product));
                com.Parameters.Add(new SqlParameter("Service", service));
                DA.SelectCommand = com;
                DA.Fill(DS);
                com.Dispose();
                DA.Dispose();
                connection.Close();
                if (DS.Tables.Count > 0)
                {
                    list = DS.Tables[0].AsEnumerable().Select(row => new DiscountRule()
                    {
                        ID = row.Field<int>("ID"),
                        Name = row.Field<string>("DiscountName"),
                        Base = row.Field<string>("DiscountBase"),
                        chargeId = row.Field<int>("ChargeId"),
                        Branch = row.Field<string>("DiscountBranch"),
                        commType = row.Field<string>("CommType"),
                        commAmount = row.Field<double>("CommAmt"),
                        Product = row.Field<string>("DiscountProduct"),
                        commRelatedUnit = row.Field<string>("CommRelatedUnit"),
                        StartDate = row.Field<DateTime>("DiscountStartDate"),
                        EndDate = row.Field<DateTime>("DiscountEndDate"),
                        Status = row.Field<bool>("DiscountStatus"),
                        Priority = row.Field<int>("DiscountPriority"),
                        commRound = row.Field<string>("commRound"),


                    }).ToList();
                    List<SalesRulesFlightOption> flighoption = new List<SalesRulesFlightOption>();
                    if (Product.ToLower() == "flight")
                    {
                        using (BusinessRulesDBEntities db = new BusinessRulesDBEntities())
                        {
                            List<string> markupids = list.Select(a => a.Name).ToList();
                            flighoption = db.SalesRulesFlightOptions.Where(a => a.Category.ToLower() == "discount" && markupids.Contains(a.CategotyId)).ToList();
                        }
                    }
                    List<SalesDiscountCriteria> criteriaList = DS.Tables[1].AsEnumerable().Select(row => new SalesDiscountCriteria()
                    {
                        DiscountID = row.Field<string>("DiscountID"),
                        DiscountCriteria = row.Field<string>("criteriaName"),
                        operation = row.Field<string>("operation"),
                        CriteriaValue = row.Field<string>("CriteriaValue"),
                        CriteriaValueText = row.Field<string>("CriteriaValueText"),

                    }).ToList();
                    foreach (var item in list)
                    {
                        if (Product.ToLower() == "flight")
                        {
                            var flighoptiondata = flighoption.Where(a => a.CategotyId == item.Name).FirstOrDefault();
                            if (flighoptiondata != null)
                            {
                                item.AllowMultiAirlines = flighoptiondata.AllowMultiAirlines.Value;
                                item.AllowMultiDestination = flighoptiondata.AllowMultiDestination.Value;
                            }
                        }
                        item.CriteriaList = criteriaList.Where(a => a.DiscountID == item.Name).Select(a => new SalesRulesCriteria
                        {
                            criteriaName = a.DiscountCriteria,
                            operation = a.operation,
                            textValue = a.CriteriaValueText,
                            value = a.CriteriaValue

                        }).ToList();
                        if (item.CriteriaList.Count == 0)
                        {
                            item.IsGeneric = true;
                        }
                    }
                }
                return list;

            }
        }

        public SalesRule GetSalesRulesByID(int MarkupID, int DiscountID)
        {
            SalesRule salesRule = new SalesRule();
            try
            {

                salesRule.Markup = GetMarkupByID(MarkupID);
                salesRule.Discount = GetDiscountByID(DiscountID);
            }
            catch (Exception ex)
            {
               // CBExceptionsLog.RecordException(ex);
            }
            return salesRule;

        }

        public MarkupRule GetMarkupByID(int MarkupID)
        {
            MarkupRule markup = new MarkupRule();
            using (BusinessRulesDBEntities db = new BusinessRulesDBEntities())
            {
                SalesMarkup salesMarkup = db.SalesMarkups.Where(a => a.ID == MarkupID).FirstOrDefault();
                if (salesMarkup != null)
                {
                    markup.ID = salesMarkup.ID;
                    markup.Name = salesMarkup.MarkupName;
                    markup.Base = salesMarkup.MarkupBase;
                    markup.commAmount = salesMarkup.CommAmt.Value;
                    markup.commRelatedUnit = salesMarkup.CommRelatedUnit;
                    markup.commRound = salesMarkup.CommRound;
                    markup.commType = salesMarkup.CommType;
                    markup.EndDate = salesMarkup.MarkupEndDate.Value;
                    markup.Priority = salesMarkup.MarkupPriority.Value;
                    markup.StartDate = salesMarkup.MarkupStartDate.Value;
                    List<SalesMarkupCriteria> markupCriteria = db.SalesMarkupCriterias.Where(a => a.MarkupID == markup.Name).ToList();
                    if (markupCriteria.Count > 0)
                    {
                        markup.CriteriaList = (from mc in markupCriteria
                                               join c in db.Criterias_Difinition on mc.MarkupCriteria equals c.ID.ToString()
                                               select new SalesRulesCriteria
                                               {
                                                   criteriaName = c.criteriaName,
                                                   operation = mc.operation,
                                                   textValue = mc.CriteriaValueText,
                                                   value = mc.CriteriaValue

                                               }).ToList();



                    }
                }
            }
            return markup;
        }
        public DiscountRule GetDiscountByID(int DiscountID)
        {
            DiscountRule discount = new DiscountRule();
            using (BusinessRulesDBEntities db = new BusinessRulesDBEntities ())
            {
                SalesDiscount salesDiscount = db.SalesDiscounts.Where(a => a.ID == DiscountID).FirstOrDefault();
                if (salesDiscount != null)
                {
                    discount.ID = salesDiscount.ID;
                    discount.Name = salesDiscount.DiscountName;
                    discount.Base = salesDiscount.DiscountBase;
                    discount.commAmount = salesDiscount.CommAmt.Value;
                    discount.commRelatedUnit = salesDiscount.CommRelatedUnit;
                    discount.commRound = salesDiscount.CommRound;
                    discount.commType = salesDiscount.CommType;
                    discount.EndDate = salesDiscount.DiscountEndDate.Value;
                    discount.Priority = salesDiscount.DiscountPriority.Value;
                    discount.StartDate = salesDiscount.DiscountStartDate.Value;
                    List<SalesDiscountCriteria> discountCriteria = db.SalesDiscountCriterias.Where(a => a.DiscountID == discount.Name).ToList();
                    if (discountCriteria.Count > 0)
                    {
                        discount.CriteriaList = (from dc in discountCriteria
                                                 join c in db.Criterias_Difinition on dc.DiscountCriteria equals c.ID.ToString()
                                                 select new SalesRulesCriteria
                                                 {
                                                     criteriaName = c.criteriaName,
                                                     operation = dc.operation,
                                                     textValue = dc.CriteriaValueText,
                                                     value = dc.CriteriaValue

                                                 }).ToList();
                    }
                }
            }
            return discount;

        }
    }
}
