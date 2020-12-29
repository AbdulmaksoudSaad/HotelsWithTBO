using Hotels.Common;
using Hotels.Common.DB;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Hotels.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.DAL
{
    public class SearchRepo : ISearchRepo
    {
        public void SaveSearchData(SearchData searchData)
        {
            try
            {
                using (SearchDBEntities db = new SearchDBEntities())
                {

                    SearchCriteria searchCriteria = new SearchCriteria();
                    searchCriteria.cityName = searchData.CityName.ToUpper();
                    searchCriteria.currency = searchData.Currency;
                    searchCriteria.dateFrom = searchData.DateFrom;
                    searchCriteria.dateTo = searchData.DateTo;
                    searchCriteria.duration = Convert.ToInt32((searchData.DateTo - searchData.DateFrom).TotalDays);
                    searchCriteria.language = searchData.Lang;
                    searchCriteria.passengerNationality = searchData.Nat;
                    searchCriteria.pos = searchData.POS;
                    searchCriteria.roomNo = searchData.SearchRooms.Count();
                    searchCriteria.sID = searchData.sID;
                    searchCriteria.source = searchData.Source;
                    for (int i = 0; i < searchData.SearchRooms.Count; i++)
                    {
                        SearchRoomData searchRoomData = new SearchRoomData();
                        searchRoomData.sID = searchData.sID;
                        searchRoomData.childernNo = searchData.SearchRooms[i].Child.Count;

                        searchRoomData.childAge = string.Join("-", searchData.SearchRooms[i].Child);
                        searchRoomData.adultNo = searchData.SearchRooms[i].Adult;
                        searchRoomData.roomNo = i + 1;
                        db.SearchRoomDatas.Add(searchRoomData);
                    }

                    db.SearchCriterias.Add(searchCriteria);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                LoggingHelper.WriteToFile("SearchRepo/Errors/", "SaveSearchData_" + searchData.sID, ex.InnerException?.Message, ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);
                throw ex;
            }
        }
        public static SearchCriteria GetSearchDataBySession(string sid)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            using (SearchDBEntities db = new SearchDBEntities())
            {
                searchCriteria = db.SearchCriterias.FirstOrDefault(a => a.sID == sid);
            }
            return searchCriteria;
        }

        public void SaveSearchResult(List<HotelSearchResult> hotelSearchResults, string sID)
        {
            try
            {
                HotelBookingDBEntities dBEntities = new HotelBookingDBEntities();
                List<CancelPolicy> cancelPolicies = new List<CancelPolicy>();
                using (DBConnection db = new DBConnection())
                {
                    db.DB_OpenConnection("SDB");

                    //  hotel rates
                    DataTable dt = new DataTable();
                    DataColumn SID = new DataColumn("sID");
                    DataColumn HotelCode = new DataColumn("HotelCode");
                    DataColumn ProviderID = new DataColumn("ProviderID");
                    DataColumn ProviderHotelCode = new DataColumn("ProviderHotelCode");
                    DataColumn ProviderHotelId = new DataColumn("ProviderHotelId");
                    DataColumn CostPrice = new DataColumn("CostPrice");
                    DataColumn SellPrice = new DataColumn("SellPrice");
                    DataColumn SellCurrency = new DataColumn("SellCurrency");
                    DataColumn MarkUpID = new DataColumn("MarkUpID");
                    DataColumn MarkUpVal = new DataColumn("MarkUpVal");
                    DataColumn DiscountID = new DataColumn("DiscountID");
                    DataColumn DiscountVal = new DataColumn("DiscountVal");
                    DataColumn ResIndex = new DataColumn("ResIndex");


                    CostPrice.DataType = typeof(double);
                    SellPrice.DataType = typeof(double);

                    MarkUpVal.DataType = typeof(double);
                    DiscountVal.DataType = typeof(double);
                    ProviderID.DataType = typeof(int);
                    MarkUpID.DataType = typeof(int);
                    DiscountID.DataType = typeof(int);
                    ResIndex.DataType = typeof(int);

                    dt.Columns.Add(SID);
                    dt.Columns.Add(HotelCode);
                    dt.Columns.Add(ProviderID);
                    dt.Columns.Add(ProviderHotelCode);
                    dt.Columns.Add(ProviderHotelId);
                    dt.Columns.Add(CostPrice);
                    dt.Columns.Add(SellPrice);
                    dt.Columns.Add(SellCurrency);
                    dt.Columns.Add(MarkUpID);
                    dt.Columns.Add(MarkUpVal);
                    dt.Columns.Add(DiscountID);
                    dt.Columns.Add(DiscountVal);
                    dt.Columns.Add(ResIndex);


                    //// room result 

                    DataTable dr = new DataTable();
                    DataColumn rsID = new DataColumn("sID");
                    DataColumn rProviderID = new DataColumn("ProviderId");

                    DataColumn rHotelCode = new DataColumn("HotelCode");
                    DataColumn rRoomRef = new DataColumn("RoomRef");

                    DataColumn rRoomCode = new DataColumn("RoomCode");
                    DataColumn rCostPrice = new DataColumn("CostPrice");
                    DataColumn rSellPrice = new DataColumn("SellPrice");
                    DataColumn rSellCurrency = new DataColumn("SellCurrency");
                    DataColumn rMarkUpID = new DataColumn("MarkUpID");
                    DataColumn rMarkUpVal = new DataColumn("MarkUpVal");
                    DataColumn rDiscountID = new DataColumn("DiscountID");
                    DataColumn rDiscountVal = new DataColumn("DiscountVal");
                    DataColumn rmeal = new DataColumn("meal");
                    DataColumn rtype = new DataColumn("type");
                    DataColumn rname = new DataColumn("name");
                    DataColumn rpaxes = new DataColumn("paxes");
                    DataColumn rChild = new DataColumn("Child");
                    DataColumn rAdult = new DataColumn("Adult");
                    DataColumn rMealID = new DataColumn("MealID");
                    DataColumn rRefund = new DataColumn("Refund");
                    DataColumn rTokenP = new DataColumn("TokenData");
                    DataColumn rratetype = new DataColumn("ratetype");
                    DataColumn rrateClass = new DataColumn("rateClass");
                    DataColumn rboardcode = new DataColumn("boardcode");
                    DataColumn rpaytype = new DataColumn("paytype");
                    DataColumn rChildage = new DataColumn("Childage");

                    rCostPrice.DataType = typeof(double);
                    rSellPrice.DataType = typeof(double);

                    rMarkUpVal.DataType = typeof(double);
                    rDiscountVal.DataType = typeof(double);
                    rProviderID.DataType = typeof(int);

                    rMarkUpID.DataType = typeof(int);
                    rDiscountID.DataType = typeof(int);
                    rpaxes.DataType = typeof(int);
                    rChild.DataType = typeof(int);
                    rAdult.DataType = typeof(int);
                    rRefund.DataType = typeof(bool);

                    dr.Columns.Add(rsID);
                    dr.Columns.Add(rProviderID);
                    dr.Columns.Add(rHotelCode);
                    dr.Columns.Add(rRoomRef);

                    dr.Columns.Add(rRoomCode);
                    dr.Columns.Add(rCostPrice);
                    dr.Columns.Add(rSellPrice);
                    dr.Columns.Add(rSellCurrency);
                    dr.Columns.Add(rMarkUpID);
                    dr.Columns.Add(rMarkUpVal);
                    dr.Columns.Add(rDiscountID);
                    dr.Columns.Add(rDiscountVal);
                    dr.Columns.Add(rmeal);
                    dr.Columns.Add(rtype);
                    dr.Columns.Add(rname);
                    dr.Columns.Add(rpaxes);
                    dr.Columns.Add(rChild);
                    dr.Columns.Add(rAdult);
                    dr.Columns.Add(rMealID);
                    dr.Columns.Add(rRefund);
                    dr.Columns.Add(rTokenP);
                    dr.Columns.Add(rratetype);
                    dr.Columns.Add(rrateClass);
                    dr.Columns.Add(rboardcode);
                    dr.Columns.Add(rpaytype);
                    dr.Columns.Add(rChildage);

                    DataTable dP = new DataTable();
                    DataColumn psID = new DataColumn("sID");
                    DataColumn pProviderID = new DataColumn("ProviderId");

                    DataColumn pHotelCode = new DataColumn("HotelCode");


                    DataColumn pRoomCode = new DataColumn("RoomCode");
                    DataColumn pCostPrice = new DataColumn("CostPrice");
                    DataColumn pSellPrice = new DataColumn("SellPrice");
                    DataColumn pSellCurrency = new DataColumn("SellCurrency");

                    DataColumn pFromdata = new DataColumn("FromDate");
                    DataColumn ptodate = new DataColumn("ToDate");



                    pCostPrice.DataType = typeof(double);
                    pSellPrice.DataType = typeof(double);
                    pProviderID.DataType = typeof(int);


                    pFromdata.DataType = typeof(DateTime);
                    ptodate.DataType = typeof(DateTime);


                    dP.Columns.Add(psID);
                    dP.Columns.Add(pProviderID);
                    dP.Columns.Add(pHotelCode);


                    dP.Columns.Add(pRoomCode);
                    dP.Columns.Add(pCostPrice);
                    dP.Columns.Add(pSellPrice);
                    dP.Columns.Add(pSellCurrency);
                    dP.Columns.Add(pFromdata);
                    dP.Columns.Add(ptodate);


                    for (int i = 0; i < hotelSearchResults.Count; i++)
                    {
                        DataRow DtHr = dt.NewRow();
                        DtHr["sID"] = sID;
                        DtHr["ProviderID"] = int.Parse(hotelSearchResults[i].providerID);
                        DtHr["HotelCode"] = hotelSearchResults[i].hotelCode;
                        DtHr["ProviderHotelId"] = hotelSearchResults[i].providerHotelID;
                        DtHr["ProviderHotelCode"] = hotelSearchResults[i].providerHotelCode;
                        DtHr["CostPrice"] = hotelSearchResults[i].costPrice.ToString();
                        DtHr["SellPrice"] = hotelSearchResults[i].TotalSellPrice;
                        DtHr["SellCurrency"] = hotelSearchResults[i].sellCurrency;
                        DtHr["MarkUpID"] = hotelSearchResults[i].MarkupId;
                        DtHr["MarkUpVal"] = hotelSearchResults[i].MarkupValue;
                        DtHr["DiscountID"] = hotelSearchResults[i].DiscountId;
                        DtHr["DiscountVal"] = hotelSearchResults[i].DiscountValue;
                        DtHr["ResIndex"] = hotelSearchResults[i].ResIndex;

                        dt.Rows.Add(DtHr);


                        for (int j = 0; j < hotelSearchResults[i].rooms.Count; j++)
                        {
                            DataRow DrHr = dr.NewRow();
                            DrHr["costPrice"] = hotelSearchResults[i].rooms[j].CostPrice;
                            DrHr["DiscountId"] = hotelSearchResults[i].rooms[j].DiscountId;
                            DrHr["DiscountVal"] = hotelSearchResults[i].rooms[j].DiscountValue;
                            DrHr["HotelCode"] = hotelSearchResults[i].hotelCode;
                            DrHr["MarkupId"] = hotelSearchResults[i].rooms[j].MarkupId;
                            DrHr["MarkupVal"] = hotelSearchResults[i].rooms[j].MarkupValue;
                            DrHr["ProviderId"] = int.Parse(hotelSearchResults[i].providerID);
                            DrHr["RoomRef"] = hotelSearchResults[i].rooms[j].RoomReference;
                            DrHr["RoomCode"] = j + 1;
                            DrHr["SellCurrency"] = hotelSearchResults[i].sellCurrency;
                            DrHr["SellPrice"] = hotelSearchResults[i].rooms[j].TotalSellPrice;
                            DrHr["sID"] = sID;
                            DrHr["meal"] = hotelSearchResults[i].rooms[j].RoomMeal;
                            DrHr["type"] = hotelSearchResults[i].rooms[j].RoomCode;
                            DrHr["name"] = hotelSearchResults[i].rooms[j].RoomType;
                            DrHr["paxes"] = hotelSearchResults[i].rooms[j].Paxs;
                            DrHr["Child"] = hotelSearchResults[i].rooms[j].Child;
                            DrHr["Adult"] = hotelSearchResults[i].rooms[j].Adult;
                            DrHr["MealID"] = hotelSearchResults[i].rooms[j].MealID;
                            DrHr["Refund"] = hotelSearchResults[i].rooms[j].IsRefundable;
                            DrHr["TokenData"] = hotelSearchResults[i].rooms[j].BookingKeyTS;

                            DrHr["ratetype"] = hotelSearchResults[i].rooms[j].rateType;
                            DrHr["rateClass"] = hotelSearchResults[i].rooms[j].rateClass;
                            DrHr["boardcode"] = hotelSearchResults[i].rooms[j].boardCode;
                            DrHr["paytype"] = hotelSearchResults[i].rooms[j].paymentType;
                            DrHr["Childage"] = hotelSearchResults[i].rooms[j].childrenAges;

                            dr.Rows.Add(DrHr);

                            if (hotelSearchResults[i].rooms[j].cancellationRules != null)
                            {
                                foreach (var cancel in hotelSearchResults[i].rooms[j].cancellationRules)
                                {
                                    if (cancel.FromDate != null)
                                    {
                                        DataRow DpHp = dP.NewRow();
                                        //  CancelPolicy policy = new CancelPolicy();
                                        DpHp["costPrice"] = (decimal)cancel.Cost;
                                        DpHp["FromDate"] = Convert.ToDateTime(cancel.FromDate);
                                        DpHp["HotelCode"] = hotelSearchResults[i].hotelCode;
                                        DpHp["ProviderId"] = hotelSearchResults[i].providerID;
                                        DpHp["RoomCode"] = j + 1;
                                        DpHp["SellPrice"] = (decimal)cancel.Price;
                                        DpHp["sID"] = sID;
                                        DpHp["SellCurrency"] = hotelSearchResults[i].sellCurrency;

                                        if (cancel.ToDate != null)
                                        {
                                            DpHp["ToDate"] = Convert.ToDateTime(cancel.ToDate);
                                        }
                                        /*    else
                                            {
                                                DpHp["ToDate"] = ;
                                            }*/
                                        dP.Rows.Add(DpHp);
                                    }

                                }
                            }
                        }

                    }


                    // dBEntities.CancelPolicies.AddRange(cancelPolicies);
                    //  dBEntities.SaveChanges();
                    Dictionary<string, object> keyValues = new Dictionary<string, object>();
                    keyValues.Add("HotelList", dt);
                    keyValues.Add("RoomList", dr);
                    keyValues.Add("Policy", dP);
                    db.SaveSP_Async("SaveSearchResult", keyValues);
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("SearchController/SearchRepo/Errors/", "SaveSearchResult_" + sID, "ex.Message", ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);
                throw ex;
            }
        }

        //TBO
        public List<SearchRoomData> GetADTCHDNoBySid(string sid)
        {
            List<SearchRoomData> rooms = new List<SearchRoomData>();
            using (SearchDBEntities db = new SearchDBEntities())
            {
                rooms = db.SearchRoomDatas.Where(dd => dd.sID == sid).ToList();
            }
            return rooms;
        }
        //save sessions 
        public void SaveSessions(int pid, string PSession, string SearchId , List<SearchRoom> rooms)
        {
            using (SearchDBEntities db = new SearchDBEntities())
            {
                for (int i = 0; i < rooms.Count; i++)
                {
                    db.ProviderSessions.Add(new ProviderSession
                    {
                        PID = pid,
                        PSession = PSession,
                        SearchId = SearchId,
                        Adult = rooms[i].Adult,
                        Child = rooms[i].Child.Count,
                        ChildAges= string.Join(",", rooms[i].Child.Select(n => n.ToString()).ToArray()),
                        RoomRef = i+1
  
                    });
                }
                
                db.SaveChanges();
            }
        }

        //save tbo rooms
        public void SaveSearchRooms(List<RoomResult> RoomResults, string sID, string HotelCode)

        {
            try
            {
                using (SearchDBEntities db = new SearchDBEntities())
                {
                 
                    //foreach (var rooms in Separatedrooms)
                    //{

                        for (int i = 0; i < RoomResults.Count(); i++)
                        {
                        
                        if (RoomResults[i].Supplements != null)
                            {
                            var distictSupp = RoomResults[i].Supplements.GroupBy(n => n.SuppID).ToList()
                                    .Select(g => g.First()).ToList();

                            foreach (var item in distictSupp)
                                {
                                    db.Supplements.Add(new Hotels.Common.Supplement
                                    {
                                        ChargeType = item.SuppChargeType,
                                        HotelCode = HotelCode,
                                        IsSelected = item.SuppIsSelected,
                                        Price = (double)item.Price,
                                        RoomIndex = RoomResults[i].RoomIndex,
                                        SID = sID
                                    });
                                }
                            }
                        }
                    //}

                    db.SaveChanges();

                }



                //HotelBookingDBEntities dBEntities = new HotelBookingDBEntities();
                List<CancelPolicy> cancelPolicies = new List<CancelPolicy>();
                using (DBConnection db = new DBConnection())
                {
                    db.DB_OpenConnection("SDB");

                    //// room result 
                    DataTable dr = new DataTable();
                    DataColumn rsID = new DataColumn("sID");
                    DataColumn rProviderID = new DataColumn("ProviderId");
                    DataColumn rHotelCode = new DataColumn("HotelCode");
                    DataColumn rRoomRef = new DataColumn("RoomRef");
                    DataColumn rRoomCode = new DataColumn("RoomCode");
                    DataColumn rCostPrice = new DataColumn("CostPrice");
                    DataColumn rSellPrice = new DataColumn("SellPrice");
                    DataColumn rSellCurrency = new DataColumn("SellCurrency");
                    DataColumn rMarkUpID = new DataColumn("MarkUpID");
                    DataColumn rMarkUpVal = new DataColumn("MarkUpVal");
                    DataColumn rDiscountID = new DataColumn("DiscountID");
                    DataColumn rDiscountVal = new DataColumn("DiscountVal");
                    DataColumn rmeal = new DataColumn("meal");
                    DataColumn rtype = new DataColumn("type");
                    DataColumn rname = new DataColumn("name");
                    DataColumn rpaxes = new DataColumn("paxes");
                    DataColumn rChild = new DataColumn("Child");
                    DataColumn rAdult = new DataColumn("Adult");
                    DataColumn rMealID = new DataColumn("MealID");
                    DataColumn rRefund = new DataColumn("Refund");
                    DataColumn rTokenP = new DataColumn("TokenData");
                    DataColumn rratetype = new DataColumn("ratetype");
                    DataColumn rrateClass = new DataColumn("rateClass");
                    DataColumn rboardcode = new DataColumn("boardcode");
                    DataColumn rpaytype = new DataColumn("paytype");
                    DataColumn rChildage = new DataColumn("Childage");

                    rCostPrice.DataType = typeof(double);
                    rSellPrice.DataType = typeof(double);

                    rMarkUpVal.DataType = typeof(double);
                    rDiscountVal.DataType = typeof(double);
                    rProviderID.DataType = typeof(int);

                    rMarkUpID.DataType = typeof(int);
                    rDiscountID.DataType = typeof(int);
                    rpaxes.DataType = typeof(int);
                    rChild.DataType = typeof(int);
                    rAdult.DataType = typeof(int);
                    rRefund.DataType = typeof(bool);

                    dr.Columns.Add(rsID);
                    dr.Columns.Add(rProviderID);
                    dr.Columns.Add(rHotelCode);
                    dr.Columns.Add(rRoomRef);
                    dr.Columns.Add(rRoomCode);
                    dr.Columns.Add(rCostPrice);
                    dr.Columns.Add(rSellPrice);
                    dr.Columns.Add(rSellCurrency);
                    dr.Columns.Add(rMarkUpID);
                    dr.Columns.Add(rMarkUpVal);
                    dr.Columns.Add(rDiscountID);
                    dr.Columns.Add(rDiscountVal);
                    dr.Columns.Add(rmeal);
                    dr.Columns.Add(rtype);
                    dr.Columns.Add(rname);
                    dr.Columns.Add(rpaxes);
                    dr.Columns.Add(rChild);
                    dr.Columns.Add(rAdult);
                    dr.Columns.Add(rMealID);
                    dr.Columns.Add(rRefund);
                    dr.Columns.Add(rTokenP);
                    dr.Columns.Add(rratetype);
                    dr.Columns.Add(rrateClass);
                    dr.Columns.Add(rboardcode);
                    dr.Columns.Add(rpaytype);
                    dr.Columns.Add(rChildage);

                    DataTable dP = new DataTable();
                    DataColumn psID = new DataColumn("sID");
                    DataColumn pProviderID = new DataColumn("ProviderId");

                    DataColumn pHotelCode = new DataColumn("HotelCode");


                    DataColumn pRoomCode = new DataColumn("RoomCode");
                    DataColumn pCostPrice = new DataColumn("CostPrice");
                    DataColumn pSellPrice = new DataColumn("SellPrice");
                    DataColumn pSellCurrency = new DataColumn("SellCurrency");
                    DataColumn pFromdata = new DataColumn("FromDate");
                    DataColumn ptodate = new DataColumn("ToDate");
                    DataColumn ChargeType = new DataColumn("ChargeType");


                    pCostPrice.DataType = typeof(double);
                    pSellPrice.DataType = typeof(double);
                    pProviderID.DataType = typeof(int);

                    pFromdata.DataType = typeof(DateTime);
                    ptodate.DataType = typeof(DateTime);


                    dP.Columns.Add(psID);
                    dP.Columns.Add(pProviderID);
                    dP.Columns.Add(pHotelCode);


                    dP.Columns.Add(pRoomCode);
                    dP.Columns.Add(pCostPrice);
                    dP.Columns.Add(pSellPrice);
                    dP.Columns.Add(pSellCurrency);
                    dP.Columns.Add(pFromdata);
                    dP.Columns.Add(ptodate);
                    dP.Columns.Add(ChargeType);


                   // foreach (var rooms in Separatedrooms)
                    //{
                        for (int j = 0; j < RoomResults.Count(); j++)
                        {
                            DataRow DrHr = dr.NewRow();
                            DrHr["costPrice"] = RoomResults[j].CostPrice;
                            DrHr["DiscountId"] = RoomResults[j].DiscountId;
                            DrHr["DiscountVal"] = RoomResults[j].DiscountValue;
                            DrHr["HotelCode"] = HotelCode;
                            DrHr["MarkupId"] = RoomResults[j].MarkupId;
                            DrHr["MarkupVal"] = RoomResults[j].MarkupValue;
                            DrHr["ProviderId"] = 5;
                            DrHr["RoomRef"] = RoomResults[j].RoomReference;
                            DrHr["RoomCode"] = RoomResults[j].RoomIndex;
                            DrHr["SellCurrency"] = RoomResults[j].Curency;
                            DrHr["SellPrice"] = RoomResults[j].TotalSellPrice;
                            DrHr["sID"] = sID;
                            DrHr["meal"] = RoomResults[j].RoomMeal;
                            DrHr["type"] = RoomResults[j].RoomCode;
                            DrHr["name"] = RoomResults[j].RoomType;
                            DrHr["paxes"] = RoomResults[j].Paxs;
                            DrHr["Child"] = RoomResults[j].Child;
                            DrHr["Adult"] = RoomResults[j].Adult;
                            DrHr["MealID"] = RoomResults[j].MealID;
                            DrHr["Refund"] = RoomResults[j].IsRefundable;
                            DrHr["TokenData"] = RoomResults[j].BookingKeyTS;

                            DrHr["ratetype"] = RoomResults[j].rateType;
                            DrHr["rateClass"] = RoomResults[j].rateClass;
                            DrHr["boardcode"] = RoomResults[j].boardCode;
                            DrHr["paytype"] = RoomResults[j].paymentType;
                            DrHr["Childage"] = RoomResults[j].childrenAges;

                            dr.Rows.Add(DrHr);

                            if (RoomResults[j].cancellationRules != null)
                            {
                                foreach (var cancel in RoomResults[j].cancellationRules)
                                {
                                    if (cancel.FromDate != null)
                                    {
                                        DataRow DpHp = dP.NewRow();
                                        DpHp["ChargeType"] = cancel.ChargeType;

                                        DpHp["costPrice"] = (decimal)cancel.Cost;
                                        DpHp["FromDate"] = Convert.ToDateTime(cancel.FromDate);
                                        DpHp["HotelCode"] = HotelCode;
                                        DpHp["ProviderId"] = 5;
                                        DpHp["RoomCode"] = RoomResults[j].RoomIndex;
                                        DpHp["SellPrice"] = (decimal)cancel.Price;
                                        DpHp["sID"] = sID;
                                        DpHp["SellCurrency"] = RoomResults[j].Curency;
                                        if (cancel.ToDate != null)
                                        {
                                            DpHp["ToDate"] = Convert.ToDateTime(cancel.ToDate);
                                        }

                                        dP.Rows.Add(DpHp);
                                    }

                                }
                            }
                        }
                    //}
                    Dictionary<string, object> keyValues = new Dictionary<string, object>();
                    keyValues.Add("RoomList", dr);
                    keyValues.Add("Policy", dP);
                    db.SaveSP_Async("SaveRoomResult", keyValues);
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("SearchController/SearchRepo/Errors/", "SaveSearchResult_" + sID, "ex.Message", ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);
                throw ex;
            }
        }


        public HotelSearchRoom GetTBOHotelDetails(HotelSearchRoom hotelSearchRoom, string Hid, string Sid)
        {
            //List<RoomResult> roomResults = new List<RoomResult>();
            SearchDBEntities searchDB = new SearchDBEntities();

            // Hotels.DAL.
            TBO.DAL.Models.Context.TBOContext context = new TBO.DAL.Models.Context.TBOContext();

            var HImages = context.HotelImages.Where(a => a.HotelCode == Hid).ToList();
            List<string> images = HImages.Select(x => x.URL).ToList();
            var Dur = searchDB.SearchCriterias.FirstOrDefault(a => a.sID == Sid).duration;

            var hotelSearchData = context.HotelDetails.FirstOrDefault(a => a.HotelCode == Hid);

            //HotelSearchRoom hotelSearchRoom = new HotelSearchRoom();
            hotelSearchRoom.providerHotelID = hotelSearchData.HotelCode;
            hotelSearchRoom.hotelCode = hotelSearchData.HotelCode;
            hotelSearchRoom.Address = hotelSearchData.Address;
            hotelSearchRoom.Lng = hotelSearchData.Map;
            //hotelSearchRoom.Lat = hotelSearchData.Lat;

            //hotelSearchRoom.hotelStars = int.Parse(hotelSearchData.HotelRating);//
            hotelSearchRoom.hotelName = hotelSearchData.HotelName;
            hotelSearchRoom.providerID = "5";
            hotelSearchRoom.Location = hotelSearchData.HotelLocation;

            hotelSearchRoom.hotelDescription = hotelSearchData.Description;
            //hotelSearchRoom.shortcutHotelDescription = hotelSearchDesc.Description2;
            hotelSearchRoom.City = hotelSearchData.CityName;
            hotelSearchRoom.Country = hotelSearchData.CountryName;
            hotelSearchRoom.hotelImages = images;

            var Facilities = context.Facilities.Where(fa => fa.HotelCode == Hid).ToList();
            List<HotelAmenity> Amenits = new List<HotelAmenity>();
            foreach (var amenti in Facilities)
            {
                Amenits.Add(new HotelAmenity
                {
                    Amenity = amenti.FacilityVal,
                    HotelCode =amenti.HotelCode
                });
            }


            hotelSearchRoom.Amenities = Amenits;

            //if (RoomsRuslt.RoomResults.Count > 0)
            //{
            //    foreach (var item in RoomsRuslt.RoomResults)
            //    {
            //        RoomResult result = new RoomResult();
            //        result.CostPrice = item.CostPrice;
            //        result.DiscountId = item.DiscountId;
            //        result.DiscountValue = item.DiscountValue;
            //        result.MarkupId = item.MarkupId;
            //        result.MarkupValue = item.MarkupValue;
            //        //result.Paxs = item.PaxSQty;
            //        result.RatePerNight = (item.TotalSellPrice / Dur).Value;
            //        result.RoomCode = item.RoomCode;
            //        result.RoomIndex = item.RoomIndex;
            //        result.RoomMeal = item.RoomMeal;
            //        result.RoomReference = item.RoomReference;
            //        result.RoomType = item.RoomType;
            //        result.TotalSellPrice = item.TotalSellPrice;
            //        //result.Adult = item.Adults;
            //        //result.Child = item.Childern.Value;
            //        //result.IsRefundable = item.IsRefundable.Value;

            //        //var repeatedRoom = roomResults.Where(x => x.RoomReference == item.RoomReference).ToList();
            //        //if (repeatedRoom.Count < 1)
            //        roomResults.Add(result);
            //    }

            //    //var roomsDetails = searchDB.SearchRoomDatas.Where(a => a.sID == Sid).ToList();
            //    //for (int i = 0; i < roomsDetails.Count; i++)

            //    //{
            //    //    SearchRoomData roomresult = new SearchRoomData();
            //    //    roomresult = null;
            //    //    var resultsR = roomResults.ToList();// Where(x => x.Adult == roomsDetails[i].adultNo.Value && x.Child == roomsDetails[i].childernNo.Value).ToList();
            //    //    SeparatedRoom srts = new SeparatedRoom();
            //    //    srts.RoomResults.AddRange(resultsR);
            //    //    hotelSearchRoom.rooms.Add(srts);

            //    //}

            //}
            //SeparatedRoom separatedRoom = new SeparatedRoom
            //{
            //    OptionsForBookings = RoomsRuslt.OptionsForBookings,
            //    RoomResults = roomResults
            //};
            //hotelSearchRoom.rooms = new List<SeparatedRoom>();
            //hotelSearchRoom.rooms.Add(separatedRoom);
            ////return separatedRoom;
            return hotelSearchRoom;
        }


    }
}