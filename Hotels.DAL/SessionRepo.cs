using Hotels.Common;
using Hotels.Common.Models;
using Hotels.IBLL;
using Hotels.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBO.DAL.Models.Context;

namespace Hotels.DAL
{
    public class SessionRepo : ISessionRepo
    {
        public bool ChechSessionStatus(SearchData searchData)
        {
            using (SearchDBEntities db = new SearchDBEntities()) {

              var validSession=db.SearchCriterias.FirstOrDefault(a => a.sID == searchData.sID && a.dateFrom ==searchData.DateFrom &&a.source==searchData.Source );
            
                if (validSession == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

         
        public void CreateSession(string sID)
        {
            using (SearchDBEntities db = new SearchDBEntities())
            {
                SearchSession existisSession = db.SearchSessions.Where(a => a.Session_Id == sID).FirstOrDefault();
                if (existisSession != null)
                {
                  
                    db.SearchSessions.Remove(existisSession);
                }

                db.SearchSessions.Add(new SearchSession()
                {
                    Session_Id = sID,
                    Status = "Created"
                });

                db.SaveChanges();

            }

        }

        public HotelSearchResponse GetDataBySession(SearchData searchData)
        {
            try
            {
                #region Hotelbeds pid 5
                HotelSearchResponse hotelsSearch = new HotelSearchResponse();
                HotelRepo repo = new HotelRepo();
                hotelsDBEntities dbh = new hotelsDBEntities();
                
                using (SearchDBEntities db = new SearchDBEntities())
                {
                    int duration = Convert.ToInt32((searchData.DateTo - searchData.DateFrom).TotalDays);
                    var HotelResults = db.SearchHotelResults.Where(a => a.sID == searchData.sID).ToList();
                    var providers = HotelResults.GroupBy(x => x.ProviderID).Select(x => x.FirstOrDefault()).Select(a => a.ProviderID.Value).ToList();

                    var hotelIds = HotelResults.Select(a => a.HotelCode).ToList();

                    //List<HotelDetails> HotelDataList = repo.GetHotelData(hotelIds, "4");///
                    List<HotelDetails> HotelDataList = new List<HotelDetails>();
                    hotelsSearch.CheckIn = searchData.DateFrom;
                    hotelsSearch.CheckOut = searchData.DateTo;

                    using (var _TBOContext = new TBOContext())
                    {




                        List<Image> imagesdata = (from i in _TBOContext.HotelImages

                                                      //  join i in dbh.HotelsImages on h.hotelID equals i.HotelID
                                                  where hotelIds.Contains(i.HotelCode)
                                                  select new Image()
                                                  {
                                                      Thum = i.URL,
                                                      HotelId = i.HotelCode,
                                                      Category = "",
                                                      Url = i.URL

                                                  }).ToList();

                        //List<HotelsAmenity> imagesdata = (from i in _TBOContext.Facilities

                        //                              //  join i in dbh.HotelsImages on h.hotelID equals i.HotelID
                        //                          where hotelIds.Contains(i.HotelCode)
                        //                          select new Image()
                        //                          {
                                                     

                        //                          }).ToList();
                        /*     List<HotelAmenity> amenties = (from h in dbh.hotels

                                                            join A in dbh.HotelsAmenities on h.hotelID equals A.hotelID
                                                            where hotelIds.Contains(h.hotelID)
                                                            select new HotelAmenity()
                                                            {
                                                                Amenity = A.amenitie,
                                                                HotelCode = h.hotelID,
                                                                status = A.Status
                                                            }).ToList();*/
                        var list = (from h in _TBOContext.HotelDetails
                        
                                    where hotelIds.Contains(h.HotelCode)
                                    select new HotelDetails()
                                    {
                                        ID = h.HotelDetailId,
                                        HotelId = h.HotelCode,
                                        Address = h.Address,
                                        Rating = h.HotelRating,
                                        HotelName = h.HotelName,
                                        Zipcode = h.PinCode,
                                           //ProviderHotelId = h.h,
                                        //  ProviderID = p.providerID,
                                        Location = h.HotelLocation,
                                        ShortDescription = h.Description,
                                        LongDescriptin = h.Description,
                                        City = h.CityName,
                                        Country = h.CountryName,
                                        //Lat = h.,
                                        //Lng = h.,
                                        Location1 = h.HotelLocation,
                                        Location2 = h.HotelLocation,
                                        Location3 = h.HotelLocation


                                        //{ images.Where(a => a.HotelId == h.hotelID).ToList() }
                                    }).Distinct().ToList();
                        /*    foreach (var item in list)
                            {
                                List<HotelAmenity> ams = new List<HotelAmenity>();
                                var hotelAmenities = amenties.Where(a => a.HotelCode == item.HotelId).Distinct().ToList();
                                foreach (var am in hotelAmenities)
                                {

                                    am.Amenity = am.Amenity.Replace(" ", String.Empty);
                                    if (ams.FirstOrDefault(x => x.Amenity == am.Amenity) == null)
                                    {
                                        ams.Add(am);
                                    }
                                }
                                item.hotelAmenities = ams;
                            }*/
                        var stars = new List<string> { "OneStar", "TwoStar","ThreeStar" , "FourStar",  "FiveStar" };

                        list.ForEach(a => a.Images = imagesdata.Where(h => h.HotelId == a.HotelId).ToList());
                        HotelDataList.AddRange(list);
                        foreach (var item in HotelResults)
                        {
                            HotelSearchResult hotel = new HotelSearchResult();
                            HotelDetails hotelData = HotelDataList.Where(a => a.HotelId == item.HotelCode).FirstOrDefault();

                            if (hotelData != null)
                            {


                                hotel.providerHotelCode = item.ProviderHotelCode;
                                hotel.City = hotelData.City;
                                hotel.hotelName = hotelData.HotelName;

                                hotel.Country = hotelData.Country;
                                hotel.hotelStars = stars.IndexOf(hotelData.Rating);
                                var images = hotelData.Images.FirstOrDefault();
                                if (images != null)
                                {
                                    hotel.hotelThumb = images.Thum;
                                }
                                //***   hotel.hotelImages = hotelData.Images.Select(a => a.Url).ToList();
                                //**     hotel.Amenities = hotelData.hotelAmenities;
                                hotel.Lat = hotelData.Lat??"";
                                hotel.Lng = hotelData.Lng??"";
                                hotel.providerID = item.ProviderID.Value.ToString();
                                hotel.hotelDescription = hotelData.LongDescriptin;
                                hotel.shortcutHotelDescription = hotelData.ShortDescription;
                                hotel.ZipCode = hotelData.Zipcode??"";
                                hotel.Location = hotelData.Location;
                                hotel.Address = hotelData.Address;
                                hotel.providerHotelID = item.ProviderHotelId;
                                hotel.hotelCode = hotelData.HotelId;
                                hotel.sellCurrency = searchData.Currency;

                                hotel.hotelRate = item.SellPrice.Value / duration;
                                hotel.costPrice = item.CostPrice.Value;
                                hotel.TotalSellPrice = item.SellPrice.Value;
                                hotel.rooms = new List<RoomResult>();
                                hotelsSearch.HotelResult.Add(hotel);
                                
                            }
                        }
                    }
                }
                hotelsSearch.Locations = hotelsSearch.HotelResult.GroupBy(x => x.Location).Select(x => x.FirstOrDefault()).Select(a => a.Location).ToList();
          
                return hotelsSearch; 
                #endregion
            }catch(Exception ex)
            {

                return null;
            }
        }


        //TBO
        public void SaveSessions(int pid, string PSession, string SearchId, List<SearchRoom> rooms)
        {
            try
            {
                SearchRepo repo = new SearchRepo();
                repo.SaveSessions(pid, PSession, SearchId,rooms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    }
 
