using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Hotels.IDAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.DAL
{
    public class HotelRepo : IHotelRepo
    {
        public List<HotelDetails> GetHotelData(List<string> HotelProvidersIds, string providerId)
        {
            try
            {

                DataTable dh = new DataTable();
                List<Image> images = new List<Image>();
                List<HotelAmenity> amenties = new List<HotelAmenity>();
                List<HotelDetails> list = new List<HotelDetails>();
                DataColumn hCode = new DataColumn("PHotelId");
                dh.Columns.Add(hCode);
                foreach (var item in HotelProvidersIds)
                {
                    DataRow DtH = dh.NewRow();
                    // Model.Hotel hotel = new Model.Hotel();
                    DtH["PHotelId"] = item;
                    dh.Rows.Add(DtH);
                }
                var connStr = ConfigurationSettings.AppSettings["HDB_CS"];
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetHotelsImagesforSearch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("providerHotelsID", dh));
                // need to pass providerId instead of "4"
                cmd.Parameters.Add(new SqlParameter("PID", "4"));
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    Image img = new Image();
                    img.Category = dr["Category"].ToString();
                    img.HotelId = dr["HotelID"].ToString();
                    img.Thum = dr["Thum"].ToString();
                    img.Url = dr["URL"].ToString();
                    images.Add(img);
                }
                dr.Close();
                conn.Close();
                conn.Open();
                /*     SqlCommand cmd2 = new SqlCommand("GetHotelsAmenityforSearch", conn);
                     cmd2.CommandType = CommandType.StoredProcedure;
                     cmd2.Parameters.Add(new SqlParameter("providerHotelsID", dh));
                     cmd2.Parameters.Add(new SqlParameter("PID", "4"));
                     SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

                     while (dr2.Read())
                     {
                         HotelAmenity AM = new HotelAmenity();
                         AM.Amenity = dr2["amenitie"].ToString();
                         AM.HotelCode = dr2["hotelID"].ToString();
                         AM.status = dr2["Status"].ToString();
                         amenties.Add(AM);

                     }*/
                //close DataReader
                //  dr2.Close();
                conn.Close();
                conn.Open();
                SqlCommand cmd3 = new SqlCommand("GetHotelsDataforSearch", conn);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.Add(new SqlParameter("providerHotelsID", dh));
                cmd3.Parameters.Add(new SqlParameter("PID", "4"));
                SqlDataReader dr3 = cmd3.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr3.Read())
                {
                    HotelDetails hotel = new HotelDetails();
                    hotel.Address = dr3["address"].ToString();
                    hotel.City = dr3["cityName"].ToString();
                    hotel.Country = dr3["countryName"].ToString();
                    hotel.HotelId = dr3["hotelID"].ToString();
                    hotel.HotelName = dr3["hotelName"].ToString();
                    hotel.ID = int.Parse(dr3["ID"].ToString());
                    hotel.Lat = dr3["Lat"].ToString();
                    hotel.Lng = dr3["Lng"].ToString();
                    hotel.Location = dr3["location"].ToString();
                    hotel.LongDescriptin = dr3["Description1"].ToString();
                    hotel.ProviderHotelId = dr3["providerHotelID"].ToString();
                    hotel.ProviderID = dr3["providerID"].ToString();
                    hotel.Rating = dr3["rating"].ToString();
                    hotel.ShortDescription = dr3["Description2"].ToString();
                    hotel.Zipcode = dr3["zipCode"].ToString();

                    list.Add(hotel);

                }
                //close DataReader
                dr3.Close();
                conn.Close();
                // using (hotelsDBEntities db = new hotelsDBEntities())
                //  {

                /*  List<Image> images = (from h in db.hotels
                                          join p in db.hotelsProviders on h.ID.ToString() equals p.hotelID
                                          join i in db.HotelsImages on h.hotelID equals i.HotelID
                                          where HotelProvidersIds.Contains(p.providerHotelID.ToString()) && p.providerID == providerId
                                          select new Image()
                                          {
                                              Thum = i.Thum,
                                              HotelId = h.hotelID,
                                              Category = i.Category,
                                              Url = i.URL
                                              
                                          }).ToList();*/

                /*    List<HotelAmenity> amenties = (from h in db.hotels
                                                   join p in db.hotelsProviders on h.ID.ToString() equals p.hotelID
                                                   join A in db.HotelsAmenities on h.hotelID equals A.hotelID
                                                   where HotelProvidersIds.Contains(p.providerHotelID.ToString()) && p.providerID == providerId 
                                                   select new HotelAmenity()
                                                   {
                                                       Amenity = A.amenitie,
                                                       HotelCode = h.hotelID,
                                                       status = A.Status 
                                                   }).ToList();*/

                /*  list = (from h in db.hotels
                          join p in db.hotelsProviders on h.ID.ToString() equals p.hotelID
                          join d in db.HotelsDescriptions on h.hotelID equals d.hotelID
                          where HotelProvidersIds.Contains(p.providerHotelID.ToString()) && p.providerID == providerId
                          select new HotelDetails()
                          {
                              ID = h.ID,
                              HotelId = h.hotelID,
                              Address = h.address,
                              Rating = h.rating,
                              HotelName = h.hotelName,
                              Zipcode = h.zipCode,
                              ProviderHotelId = p.providerHotelID,
                              ProviderID = p.providerID,
                              Location = h.location,
                              ShortDescription = d.Description2,
                              LongDescriptin = d.Description1,
                              City = h.cityName,
                              Country = h.countryName,
                              Lat = h.Lat,
                              Lng = h.Lng,
                              Location1 = h.location1,Location2=h.location2,Location3=h.location3


                              //{ images.Where(a => a.HotelId == h.hotelID).ToList() }
                          }).Distinct().ToList();*/
                //foreach (var item in list)
                //    {
                //        List<HotelAmenity> ams = new List<HotelAmenity>();
                //        var hotelAmenities = amenties.Where(a => a.HotelCode == item.HotelId).Distinct().ToList();
                //        foreach (var am in hotelAmenities)
                //        {

                //            am.Amenity = am.Amenity.Replace("  ", String.Empty);
                //            if (ams.FirstOrDefault(x=>x.Amenity==am.Amenity)==null)
                //            {
                //                ams.Add(am);
                //            }
                //        }
                //        item.hotelAmenities = ams;
                //    }

                list.ForEach(a => a.Images = images.Where(h => h.HotelId == a.HotelId).ToList());
                return list;
            }

            catch (Exception ex)
            {
                //log exp 
                LoggingHelper.WriteToFile("SearchController/MapSearchResultHB/GetHotelDataFromDB/Errors/", "GetHotelDataFromDB_", ex.Message, ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);

                return new List<HotelDetails>();
            }

            // }
        }

        public List<HotelProviderData> GetHotelIdsForActiveProviders(int CityId)
        {
            try
            {
                using (hotelsDBEntities db = new hotelsDBEntities())
                {
                    string cityname;
                    var cityData = db.Cities.FirstOrDefault(a => a.ID == CityId);

                    cityname = cityData.City1;

                    return db.GetHotelsIDAndProvidersByCityName(cityname).Select(a => new HotelProviderData
                    {
                        HotelProviderId = a.providerHotelID,
                        ProviderId = a.providerID
                    }).ToList();

                }
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("SearchController/Errors/", "HotelRepo" + "GetHotelIdsForActiveProviders", ex.InnerException?.Message, ex.Message + ex.StackTrace);
                throw ex;
            }
        }
        public List<CitiesID> GetCitiesForActiveProviders(int CityId)
        {
            try
            {
                using (hotelsDBEntities db = new hotelsDBEntities())
                {

                    return db.CitiesIDs.Where(a => a.cityID == CityId).ToList();

                }
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("SearchController/Errors/", "HotelRepo" + "GetHotelIdsForActiveProviders", ex.InnerException?.Message, ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        public CheckAvailabilityForTs GetHotelDataForTsAvailability(AvailabilityReq roomsReq)
        {
            try
            {
                SearchDBEntities dBEntities = new SearchDBEntities();
                CheckAvailabilityForTs checkAvailability = new CheckAvailabilityForTs();
                checkAvailability.HodelData = dBEntities.SearchHotelResults.FirstOrDefault(a => a.sID == roomsReq.Sid && a.HotelCode == roomsReq.HotelCode && a.ProviderID.Value.ToString() == roomsReq.PID);
                var RoomsData = roomsReq.Rooms.Select(a => a.RoomId.ToString()).ToList();
                checkAvailability.roomResults = dBEntities.SearchRoomResults.Where(a => a.sID == roomsReq.Sid && a.HotelCode == roomsReq.HotelCode && a.ProviderId.Value.ToString() == roomsReq.PID && RoomsData.Contains(a.RoomCode)).ToList();

                return checkAvailability;
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("AvailabilityController/Errors/", "HotelRepo" + "GetHotelDataForTsAvailability" + roomsReq.Sid, ex.InnerException?.Message, ex.Message + ex.StackTrace);

                return null;
            }
        }

        public List<HotelDetails> GetChannelHotelData(List<string> HotelProvidersIds, string providerId)
        {
            try
            {
                DataTable dh = new DataTable();
                List<HotelDetails> list = new List<HotelDetails>();
                DataColumn hCode = new DataColumn("PHotelId");
                dh.Columns.Add(hCode);
                foreach (var item in HotelProvidersIds)
                {
                    DataRow DtH = dh.NewRow();
                    // Model.Hotel hotel = new Model.Hotel();
                    DtH["PHotelId"] = item;
                    dh.Rows.Add(DtH);
                }
                var connStr = ConfigurationSettings.AppSettings["HDB_CS"];
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand cmd3 = new SqlCommand("GetHotelsDataforSearch", conn);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.Add(new SqlParameter("providerHotelsID", dh));
                cmd3.Parameters.Add(new SqlParameter("PID", providerId));
                SqlDataReader dr3 = cmd3.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr3.Read())
                {
                    HotelDetails hotel = new HotelDetails();
                    hotel.Address = dr3["address"].ToString();
                    hotel.City = dr3["cityName"].ToString();
                    hotel.Country = dr3["countryName"].ToString();
                    hotel.HotelId = dr3["hotelID"].ToString();
                    hotel.HotelName = dr3["hotelName"].ToString();
                    hotel.ID = int.Parse(dr3["ID"].ToString());
                    hotel.Lat = dr3["Lat"].ToString();
                    hotel.Lng = dr3["Lng"].ToString();
                    hotel.Location = dr3["location"].ToString();
                    hotel.LongDescriptin = dr3["Description1"].ToString();
                    hotel.ProviderHotelId = dr3["providerHotelID"].ToString();
                    hotel.ProviderID = dr3["providerID"].ToString();
                    hotel.Rating = dr3["rating"].ToString();
                    hotel.ShortDescription = dr3["Description2"].ToString();
                    hotel.Zipcode = dr3["zipCode"].ToString();

                    list.Add(hotel);

                }
                //close DataReader
                dr3.Close();
                conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                //log exp 
                LoggingHelper.WriteToFile("SearchController/MapSearchResultHB/GetHotelDataFromDB/Errors/", "GetHotelDataFromDB_", ex.Message, ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);

                return new List<HotelDetails>();
            }
        }
    }
}
