using Hotels.Common;
using Hotels.Common.Models;
using Hotels.DAL;
using Hotels.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.BLL
{
    public class SearchResultData : IResultData
    {
        public SearchResultDatadb GetSearchData;
        public SearchRequiredData requiredData;
        public  SearchRequiredData GetAllCountriesAndCity()
        {
            GetSearchData = new SearchResultDatadb();
            requiredData = new SearchRequiredData();

             requiredData.cities = GetSearchData.GetAllCitiesForHotels();
            requiredData.countries = GetSearchData.GetAllCounty();
            return requiredData ;
        }
        public List<Common.Models.Country> GetAllCountries()
        {
            GetSearchData = new SearchResultDatadb();
            requiredData = new SearchRequiredData();

           // requiredData.cities = GetSearchData.GetAllCities();
            requiredData.countries = GetSearchData.GetAllCounty();
            return requiredData.countries;
        }
        public List<CityWithCountryName> GetAllCities(string code)
        {
            GetSearchData = new SearchResultDatadb();
                var cities    = GetSearchData.GetAllCities(code);
          //  requiredData.countries = GetSearchData.GetAllCounty();
            return cities;
        }

        public List<CityWithCountryName> AllCities()
        {
            GetSearchData = new SearchResultDatadb();
            var cities = GetSearchData.AllCities();
            //  requiredData.countries = GetSearchData.GetAllCounty();
            return cities;
        }

        public List<GetActiveProviders_Result> GetActiveProvidersData() {
            ProviderRepo repo = new ProviderRepo();
            return repo.GetActiveProvider();
        }
        //
        public List<HotelProviderData> GetProviderHotelIdsForActiveProviders(int CityId)
        {
            try
            {
                HotelRepo repo = new HotelRepo();
                return repo.GetHotelIdsForActiveProviders(CityId);
            }catch(Exception ex)
            {
                return  new List<HotelProviderData>();
            }
        }
        //
        public List<CitiesID> GetProviderCitiessForActiveProviders(int CityId)
        {
            try
            {
                HotelRepo repo = new HotelRepo();
                return repo.GetCitiesForActiveProviders(CityId);
            }
            catch (Exception ex)
            {
                return new List<CitiesID>();
            }
        }
        //
        public void SaveSearchData(SearchData searchData) {
            try
            {
                SearchRepo repo = new SearchRepo();
                repo.SaveSearchData(searchData);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public void SaveSearchResult(List<HotelSearchResult> hotelSearchResults, string sID)
        {
            try
            {
                SearchRepo repo = new SearchRepo();
                repo.SaveSearchResult(hotelSearchResults, sID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        SearchRequiredData IResultData.GetAllCitiesAndCountries()
        {
            throw new NotImplementedException();
        }

        void IResultData.SaveSearchData(SearchData searchData)
        {
            throw new NotImplementedException();
        }

        void IResultData.SaveSearchResult(List<HotelSearchResult> hotelSearchResults, string sID)
        {
            throw new NotImplementedException();
        }

       
    }
}
