using Hotels.Common.Models;
using Hotels.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using TBO.DAL.Models.Context;

namespace Hotels.DAL
{
    public class SearchResultDatadb : IResultData
    {
        public List<CityWithCountryName> GetAllCities(string code)
        {
            List<CityWithCountryName> cities = new List<CityWithCountryName>();
            using (TBOContext tBOContext = new TBOContext())
            {
                var TBOCities = tBOContext.Cities.Where(ww=>ww.CityName.Contains(code)).Include(ww => ww.Country).AsNoTracking().ToList();
                foreach (var item in TBOCities)
                {
                    cities.Add(new CityWithCountryName
                    {
                        CityId = int.Parse(item.CityCode),
                        City = item.CityName,
                        CityWithCountry = item.CityName + "," + item.Country.CountryName,
                        Country = item.Country.CountryName
                    });
                }
            }



            //string connString = ConfigurationSettings.AppSettings["hotelsDB"];

            //using (SqlConnection conn = new SqlConnection(connString))
            //{
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand("GetCitiesForHotels", conn);
            //    // 2. set the command object so it knows to execute a stored procedure
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add(new SqlParameter("code", code));



            //    // execute the command
            //    using (SqlDataReader rdr = cmd.ExecuteReader())
            //    {
            //        // iterate through results, printing each to console
            //        while (rdr.Read())
            //        {
            //            CityWithCountryName cityWithCountry = new CityWithCountryName();
            //            cityWithCountry.CityId = int.Parse(rdr["id"].ToString());
            //            //city.City1+" , "+city.countryName
            //            cityWithCountry.CityWithCountry = rdr["city"].ToString() + "," + rdr["countryName"].ToString();
            //            cityWithCountry.City = rdr["city"].ToString();
            //            cityWithCountry.Country = rdr["countryName"].ToString();
            //            cities.Add(cityWithCountry);
            //        }
            //    }
            //}
            if (cities.Count == 0)
            {

                return null;
            }
            return cities;
        }




        public List<CityWithCountryName> AllCities( )
        {
            List<CityWithCountryName> cities = new List<CityWithCountryName>();
            using (TBOContext tBOContext = new TBOContext())
            {
                var TBOCities = tBOContext.Cities.Include(ww => ww.Country).AsNoTracking().ToList();
                foreach (var item in TBOCities)
                {
                    cities.Add(new CityWithCountryName
                    {
                        CityId = int.Parse(item.CityCode),
                        City = item.CityName,
                        CityWithCountry = item.CityName + "," + item.Country.CountryName,
                        Country = item.Country.CountryName,
                        CountryCode=item.Country.CountryCode
                    });
                }
            }



            
            if (cities.Count == 0)
            {

                return null;
            }
            return cities;
        }





        public List<Country> GetAllCounty()
        {
            List<Country> countries = new List<Country>();
            using (TBOContext tBOContext = new TBOContext())
            {
                var res = tBOContext.Countries.AsNoTracking().ToList();
                foreach (var item in res)
                {
                    countries.Add(new Country
                    {
                        code = item.CountryCode,
                        name = item.CountryName,
                        ID = item.CountryId
                    });
                }
            }
            if (countries.Count == 0)
            {
                return null;
            }
            return countries;

            #region HB
            //List<Country> countries = new List<Country>();
            //string connString = ConfigurationSettings.AppSettings["hotelsDB"];

            //using (SqlConnection conn = new SqlConnection(connString))
            //{
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand("GetCountriesForHotels", conn);

            //    // 2. set the command object so it knows to execute a stored procedure
            //    cmd.CommandType = CommandType.StoredProcedure;



            //    // execute the command
            //    using (SqlDataReader rdr = cmd.ExecuteReader())
            //    {
            //        // iterate through results, printing each to console
            //        while (rdr.Read())
            //        {
            //            Country country = new Country();
            //            country.code = rdr["code"].ToString();
            //            //city.City1+" , "+city.countryName
            //            country.name = rdr["name"].ToString();
            //            country.phoneCode = rdr["phoneCode"].ToString();
            //            country.ID = int.Parse(rdr["ID"].ToString());
            //            country.region = rdr["region"].ToString();
            //            country.regionCode = rdr["regionCode"].ToString();
            //            countries.Add(country);
            //        }
            //    }
            //}
            //if (countries.Count == 0)
            //{
            //    return null;
            //}
            //return countries; 
            #endregion
        }
        public List<CityWithCountryName> GetAllCitiesForHotels()
        {
            List<CityWithCountryName> cities = new List<CityWithCountryName>();


            using (TBOContext tBOContext = new TBOContext())
            {
                var TBOCities = tBOContext.Cities.Include(ww=>ww.Country).AsNoTracking().ToList();
                foreach (var item in TBOCities)
                {
                    cities.Add(new CityWithCountryName
                    {
                        CityId = int.Parse(item.CityCode),
                        City = item.CityName,
                        CityWithCountry =item.CityName +"," +item.Country.CountryName,
                        Country= item.Country.CountryName
                    });
                }
            }

            if (cities.Count == 0)
            {
                return null;
            }
            return cities;
        }


        #region hotelbeds
        //    List<CityWithCountryName> cities = new List<CityWithCountryName>();
        //    string connString= ConfigurationSettings.AppSettings["hotelsDB"];

        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("GetAllCitiesForHotels", conn);
        //        // 2. set the command object so it knows to execute a stored procedure
        //        cmd.CommandType = CommandType.StoredProcedure;




        //        // execute the command
        //        using (SqlDataReader rdr = cmd.ExecuteReader())
        //        {
        //            // iterate through results, printing each to console
        //            while (rdr.Read())
        //            {
        //                CityWithCountryName cityWithCountry = new CityWithCountryName();
        //                cityWithCountry.CityId = int.Parse(rdr["id"].ToString());
        //                //city.City1+" , "+city.countryName
        //                cityWithCountry.CityWithCountry = rdr["city"].ToString() + "," + rdr["countryName"].ToString();
        //                cityWithCountry.City = rdr["city"].ToString();
        //                cityWithCountry.Country = rdr["countryName"].ToString();
        //                cities.Add(cityWithCountry);
        //            }
        //        }
        //    }
        //    if (cities.Count == 0)
        //    {

        //        return null;
        //    }
        //    return cities;
        //} 
        #endregion
    }
}
