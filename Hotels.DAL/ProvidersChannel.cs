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
    public class ProvidersChannel : IProvidersChannel
    {
        public HotelPackagesDetails GetHotelPackages(string pid, string sid, string hid)
        {
            HotelPackagesDetails hotelPackages = new HotelPackagesDetails();
            SearchDBEntities searchDB = new SearchDBEntities();
            hotelsDBEntities hotelsDB = new hotelsDBEntities();
            var hotelSearchData = hotelsDB.hotels.FirstOrDefault(a => a.hotelID == hid);

            var hotelSearchpro = hotelsDB.hotelsProviders.FirstOrDefault(a => a.hotelID == hotelSearchData.ID.ToString() && a.providerID == pid);
            var hotelSearchDesc = hotelsDB.HotelsDescriptions.FirstOrDefault(a => a.hotelID == hid);
               var Imgs= hotelsDB.HotelsImages.Where(a => a.HotelID == hid).ToList();
            List<string> images = Imgs.Select(x => x.URL).ToList();
            hotelPackages.providerHotelID = hotelSearchpro.providerHotelID;
            hotelPackages.hotelCode = hotelSearchData.hotelID;
            hotelPackages.Address = hotelSearchData.address;
            hotelPackages.Lng = hotelSearchData.Lng;
            hotelPackages.Lat = hotelSearchData.Lat;

            hotelPackages.hotelStars = int.Parse(hotelSearchData.rating);//
            hotelPackages.hotelName = hotelSearchData.hotelName;
            hotelPackages.providerID = hotelSearchpro.providerID;
            hotelPackages.Location = hotelSearchData.location;

            hotelPackages.hotelDescription = hotelSearchDesc.Description1;
            hotelPackages.shortcutHotelDescription = hotelSearchDesc.Description2;
            hotelPackages.City = hotelSearchData.cityName;
            hotelPackages.Country = hotelSearchData.countryName;
            hotelPackages.hotelImages = images;

              var packages=   searchDB.HotelPackages.Where(a => a.Session == sid && a.Hotelid == hid && a.Provider.ToString() == pid).ToList();
                var    Rooms=searchDB.SearchRoomResults.Where(a => a.sID == sid && a.HotelCode == hid && a.ProviderId.ToString() == pid).ToList();
            RoomPackages roomPackage=new RoomPackages();
            foreach (var item in packages)
            {
           //    var availableCate = hotelPackages.packages.FirstOrDefault(a => a.RoomCategory == item.Category);
                if (roomPackage.RoomCategory != item.Category)
                {
                    if (roomPackage.roomPackages.Count != 0)
                    {
                        roomPackage.PackagesNo = roomPackage.roomPackages.Count;
                        hotelPackages.packages.Add(roomPackage);
                    }
                    roomPackage = new RoomPackages();
                    roomPackage.RoomCategory = item.Category;
                }
                
                
                RoomPackage roomPack = new RoomPackage();
                roomPack.No = item.PackageNum.Value;
                roomPack.PricePerAllNight = item.PricePerAllNight.Value;
                roomPack.PricePerNight = item.PricePerNight.Value;
                
              var specPack= Rooms.Where(x => x.PackageNo == item.PackageNum).ToList();
                foreach (var r in specPack)
                {
                    RoomResult result = new RoomResult();
                    result.CostPrice = r.costPrice.Value;
                    result.DiscountId = r.DiscountId.Value;
                    result.PackageNO = r.PackageNo.Value;
                    result.DiscountValue =r.DiscountVal.Value;
                   result.Images = Imgs.Where(x=>x.Category.ToLower()== "hotel rooms").Select(a => a.URL).ToList();
                    result.MarkupId = r.MarkupId.Value;
                    result.MarkupValue = r.MarkupVal.Value;
                    result.Paxs = r.PaxSQty.Value;
                    result.RoomCode = r.roomType;
                    result.RoomIndex = int.Parse(r.RoomCode);
                    result.RoomMeal = r.meal;
                    result.RoomReference = r.RoomReference;
                    result.RoomType = r.RoomName;
                    result.TotalSellPrice = r.SellPrice.Value;
                    result.Adult = r.Adults.Value;
                    result.Child = r.Childern.Value;
                    if (r.IsRefundable.Value != null)
                    {
                        result.IsRefundable = r.IsRefundable.Value;
                    }
                    roomPack.roomResults.Add(result);

                }
                roomPackage.roomPackages.Add(roomPack);
                //hotelPackages.packages.Add(roomPackage);
            }
            if (roomPackage.roomPackages.Count != 0)
                hotelPackages.packages.Add(roomPackage);
            return hotelPackages;

        }

        public List<hotelsProvider> GetHotelProvides(List<string> Hotelids)
        {
            hotelsDBEntities db = new hotelsDBEntities();
         var Hotels=   db.hotelsProviders.Where(a => Hotelids.Contains(a.hotelID)).ToList();

            return Hotels;
        }

        public string GetSearchData(List<string> Hotelids)
        {
            string cityID = null;
            hotelsDBEntities db = new hotelsDBEntities();
            var id = Hotelids[0];
           var hoteldata= db.hotels.FirstOrDefault(x => x.ID.ToString() == id);
         var country =   db.Countries.FirstOrDefault(a => a.CountryCode == hoteldata.countryCode);
           var city= db.Cities.FirstOrDefault(a => a.City1 == hoteldata.cityName && a.countryCode == country.code);
            if (city != null)
                cityID = city.ID.ToString();
            if (city == null)
            {
                var city1 = db.Cities.FirstOrDefault(a => a.City1 == hoteldata.cityName  );
                cityID = city1.ID.ToString();
            }
            return cityID; 
        }

        public void SaveSearchResult(List<HotelChannelResult> results , string sID )
        {
            try
            {
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

                    CostPrice.DataType = typeof(double);
                    SellPrice.DataType = typeof(double);

                    MarkUpVal.DataType = typeof(double);
                    DiscountVal.DataType = typeof(double);
                    ProviderID.DataType = typeof(int);
                    MarkUpID.DataType = typeof(int);
                    DiscountID.DataType = typeof(int);

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
                    DataColumn rPKN = new DataColumn("PackageNo");

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
                    rPKN.DataType = typeof(int);
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
                    dr.Columns.Add(rPKN);


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

                    DataTable dPK = new DataTable();
                    DataColumn PksID = new DataColumn("sID");
                    DataColumn pkProviderID = new DataColumn("ProviderId");
                    DataColumn pkHotelId = new DataColumn("HotelId");
                    DataColumn pkPackageNo = new DataColumn("PackageNo");
                    DataColumn pkCate = new DataColumn("Cate");
                    DataColumn pkNightPrice = new DataColumn("NightPrice");
                    DataColumn pkSellPrice = new DataColumn("SellPrice");
                    DataColumn pkSellCurrency = new DataColumn("SellCurrency");
                    pkNightPrice.DataType = typeof(float);
                    pkPackageNo.DataType = typeof(int);
                    pkProviderID.DataType = typeof(int);
                    pkSellPrice.DataType = typeof(float);

                    dPK.Columns.Add(PksID);
                    dPK.Columns.Add(pkProviderID);
                    dPK.Columns.Add(pkHotelId);
                    dPK.Columns.Add(pkPackageNo);
                    dPK.Columns.Add(pkCate);
                    dPK.Columns.Add(pkNightPrice);
                    dPK.Columns.Add(pkSellPrice);
                    dPK.Columns.Add(pkSellCurrency);



                    for (int i = 0; i < results.Count; i++)
                    {
                        DataRow DtHr = dt.NewRow();
                        DtHr["sID"] = sID;
                        DtHr["ProviderID"] = int.Parse(results[i].providerID);
                        DtHr["HotelCode"] = results[i].hotelId;
                        DtHr["ProviderHotelId"] = results[i].providerHotelID;
                        DtHr["ProviderHotelCode"] = results[i].providerHotelCode;
                        //   DtHr["CostPrice"] = results[i].costPrice.ToString();
                        DtHr["SellPrice"] = results[i].PricePerAllNight;
                        DtHr["SellCurrency"] = results[i].sellCurrency;


                        dt.Rows.Add(DtHr);


                        for (int j = 0; j < results[i].packages.Count; j++)
                        {
                            var eee = results[i].packages[0].roomPackages[0].roomResults[0].IsRefundable;
                            for (int p = 0; p < results[i].packages[j].roomPackages.Count; p++)
                            {
                                //fill package
                                DataRow DHpk = dPK.NewRow();
                                //  CancelPolicy policy = new CancelPolicy();
                                DHpk["ProviderId"] = int.Parse(results[i].providerID);
                                DHpk["HotelId"] = results[i].hotelId;
                                DHpk["PackageNo"] = results[i].packages[j].roomPackages[p].No;
                                DHpk["Cate"] = results[i].packages[j].RoomCategory;
                                DHpk["NightPrice"] = results[i].packages[j].roomPackages[p].PricePerNight;
                                DHpk["SellPrice"] = results[i].packages[j].roomPackages[p].PricePerAllNight;
                                DHpk["sID"] = sID;//*
                                DHpk["SellCurrency"] = results[i].sellCurrency;
                                dPK.Rows.Add(DHpk);
                                for (int r = 0; r < results[i].packages[j].roomPackages[p].roomResults.Count; r++)
                                {
                                    DataRow DrHr = dr.NewRow();
                                    DrHr["costPrice"] = results[i].packages[j].roomPackages[p].roomResults[r].CostPrice;
                                    DrHr["DiscountId"] = results[i].packages[j].roomPackages[p].roomResults[r].DiscountId;
                                    DrHr["DiscountVal"] = results[i].packages[j].roomPackages[p].roomResults[r].DiscountValue;
                                    DrHr["HotelCode"] = results[i].hotelId;
                                    DrHr["MarkupId"] = results[i].packages[j].roomPackages[p].roomResults[r].MarkupId;
                                    DrHr["MarkupVal"] = results[i].packages[j].roomPackages[p].roomResults[r].MarkupValue;
                                    DrHr["ProviderId"] = int.Parse(results[i].providerID);
                                    DrHr["RoomRef"] = results[i].packages[j].roomPackages[p].roomResults[r].RoomReference;
                                    DrHr["RoomCode"] = results[i].packages[j].roomPackages[p].roomResults[r].RoomIndex;
                                    DrHr["SellCurrency"] = results[i].sellCurrency;
                                    DrHr["SellPrice"] = results[i].packages[j].roomPackages[p].roomResults[r].TotalSellPrice;
                                    DrHr["sID"] = sID;
                                    DrHr["meal"] = results[i].packages[j].roomPackages[p].roomResults[r].RoomMeal;
                                    DrHr["type"] = results[i].packages[j].roomPackages[p].roomResults[r].RoomCode;
                                    DrHr["name"] = results[i].packages[j].roomPackages[p].roomResults[r].RoomType;
                                    DrHr["paxes"] = results[i].packages[j].roomPackages[p].roomResults[r].Paxs;
                                    DrHr["Child"] = results[i].packages[j].roomPackages[p].roomResults[r].Child;
                                    DrHr["Adult"] = results[i].packages[j].roomPackages[p].roomResults[r].Adult;
                                    DrHr["MealID"] = results[i].packages[j].roomPackages[p].roomResults[r].MealID;
                                    DrHr["Refund"] = results[i].packages[j].roomPackages[p].roomResults[r].IsRefundable;
                                    DrHr["TokenData"] = results[i].packages[j].roomPackages[p].roomResults[r].BookingKeyTS;
                                    DrHr["PackageNo"] = results[i].packages[j].roomPackages[p].No;

                                    dr.Rows.Add(DrHr);

                                    if (results[i].packages[j].roomPackages[p].roomResults[r].cancellationRules != null)
                                    {
                                        foreach (var cancel in results[i].packages[j].roomPackages[p].roomResults[r].cancellationRules)
                                        {
                                            if (cancel.FromDate != null)
                                            {
                                                DataRow DpHp = dP.NewRow();
                                                //  CancelPolicy policy = new CancelPolicy();
                                                DpHp["costPrice"] = (decimal)cancel.Cost;
                                                DpHp["FromDate"] = Convert.ToDateTime(cancel.FromDate);
                                                DpHp["HotelCode"] = results[i].hotelId;
                                                DpHp["ProviderId"] = results[i].providerID;
                                                DpHp["RoomCode"] = j + 1;
                                                DpHp["SellPrice"] = (decimal)cancel.Price;
                                                DpHp["sID"] = sID;
                                                DpHp["SellCurrency"] = results[i].sellCurrency;
                                                if (cancel.ToDate != null)
                                                {
                                                    DpHp["ToDate"] = Convert.ToDateTime(cancel.ToDate);
                                                }

                                                dP.Rows.Add(DpHp);
                                            }
                                        }

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
                    keyValues.Add("Packages", dPK);
                    db.SaveSP_Async("SaveSearchResultForChannel", keyValues);
                }
            } catch(Exception ex)
            {
                LoggingHelper.WriteToFile("WegoHotelSearch/Errors/", "INSaveData_" +  sID, ex.Message, ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);

            }
        }
    }
}
