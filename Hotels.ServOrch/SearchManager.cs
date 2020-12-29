using Hotels.BLL;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Hotels.DAL;
using Hotels.ErrorHandling.ErrorCodes;
using Hotels.ErrorHandling.Exceptions;
using Hotels.ProviderManagment;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Hotels.ServOrch
{
    public class SearchManager : IDisposable
    {
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public SearchData PrepareSearchData(SearchData searchData)
        {
            try
            {
                if (string.IsNullOrEmpty(searchData.Currency))
                    searchData.Currency = "KWD";
                if (string.IsNullOrEmpty(searchData.POS))
                    searchData.POS = "KW";
                if (string.IsNullOrEmpty(searchData.Nat))
                    searchData.Nat = "KW";

                var tasks = new List<Task>();
                var tokenSource1 = new CancellationTokenSource();
                var tokenSource2 = new CancellationTokenSource();
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    //create session  sure if session Not exists///////////////////******
                    SessionManager sessionManager = new SessionManager();
                    sessionManager.CreateSession(searchData.sID);
                }, tokenSource1.Token));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    //save search data in db
                    SearchResultData searchResultData = new SearchResultData();
                    searchResultData.SaveSearchData(searchData);
                }, tokenSource2.Token));
                return searchData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public HotelSearchResponse GetSearchResult(SearchData searchData)
        {
            try
            {
                var tasks = new List<Task>();
                var tokenSource1 = new CancellationTokenSource();
                var tokenSource2 = new CancellationTokenSource();
                List<HotelSearchResult> searchResults = new List<HotelSearchResult>();
                HotelSearchResponse searchResponse = new HotelSearchResponse();
                SessionManager sessionManager = new SessionManager();

                // add validation  
                if (!ValidateSearchData(searchData))
                {
                    searchResponse.Status = 1; // invalid data 
                    return searchResponse;
                }

                // sure if session vaild ********
                if (sessionManager.ValidSession(searchData))
                {
                    // get hotel data From DB
                    SessionRepo sessionRepo = new SessionRepo();
                    var HotelSRes = sessionRepo.GetDataBySession(searchData);
                    if (HotelSRes != null)
                    {
                        return HotelSRes;
                    }
                    return null;
                }

                //MG data not found in db new SID 
                searchData = PrepareSearchData(searchData);
                if (searchData != null)
                {
                    using (ProviderManager pm = new ProviderManager())
                    {
                        pm.searchData = searchData;
                        pm.GetHotelSearchResultForAllProviders();
                        //  searchResults = pm.HotelSearchResults;
                        //  searchResults = GetLowestPrice(searchResults);
                        searchResponse = pm.searchResponse;
                        //searchResults = GetLowestPrice(searchResponse.HotelResult);
                        searchResults = searchResponse.HotelResult;

                        searchResponse.HotelResult = searchResults;
                        searchResponse.Locations = searchResponse.HotelResult.GroupBy(x => x.Location).Select(x => x.FirstOrDefault()).Select(a => a.Location).ToList();

                        tasks.Add(Task.Factory.StartNew(() =>
                        {

                            if (searchResults.Count > 0)
                            {
                                SaveSearchResult(searchResults, searchData.sID);/////////////////
                            }

                        }, tokenSource1.Token));
                        Task.WaitAll(tasks.ToArray());
                    }
                }
                return searchResponse;
            }
            catch (HotelSearchInputException EX)
            {
                return new HotelSearchResponse()
                {
                    Status = 1,  //"Invalid"
                    ResultException = new ResultException()
                    {
                        Code = EX.Code,
                        ExceptionMessage = EX.Message
                    }
                };
            }
            catch (Exception ex)
            {
                LoggingHelper.WriteToFile("SearchController/Errors/", "SearchController" + "INServOrch" + searchData.sID, ex.InnerException?.Message, ex.Message + ex.StackTrace);
                return new HotelSearchResponse();
            }
        }

        #region Old validation
        //bool ValidateSearchData(SearchData searchData)
        //{
        //    int Paxes = 0;
        //    if (string.IsNullOrEmpty(searchData.CityName))
        //        return false;
        //    if (string.IsNullOrEmpty(searchData.Currency))
        //        return false;
        //    if (string.IsNullOrEmpty(searchData.Lang))
        //        return false;
        //    if (string.IsNullOrEmpty(searchData.Nat))
        //        return false;
        //    if (string.IsNullOrEmpty(searchData.POS))
        //        return false;
        //    if (string.IsNullOrEmpty(searchData.Source))
        //        return false;
        //    if (string.IsNullOrEmpty(searchData.sID))
        //        return false;

        //    if (searchData.DateFrom.Date < DateTime.Now.Date)
        //        return false;

        //    if (searchData.DateTo.Date < DateTime.Now.Date)
        //        return false;
        //    if (searchData.DateTo.Date < searchData.DateFrom)
        //        return false;
        //    if (searchData.SearchRooms == null)
        //        return false;
        //    foreach (var item in searchData.SearchRooms)
        //    {
        //        Paxes = Paxes + item.Adult+item.Child.Count;
        //        if (item.Adult > 5)
        //            return false;
        //        if (item.Child.Count > 2)
        //        {
        //            return false;
        //        }

        //    }
        //    if(Paxes>9)
        //        return false;
        //    if (searchData.SearchRooms.Count < 1)
        //        return false;
        //    return true;
        //} 
        #endregion

        bool ValidateSearchData(SearchData searchData)
        {
            int Paxes = 0;
            if (searchData.DateFrom.Date < DateTime.Now.Date || searchData.DateTo.Date < DateTime.Now.Date)
                throw new HotelSearchInputException(HotelSearchInputErrorCodes.DateTimeOutOfSchedule, searchData.sID, "Invalid Date Time , Enter Date start from today! ");
            if (searchData.DateTo.Date < searchData.DateFrom)
                throw new HotelSearchInputException(HotelSearchInputErrorCodes.DateTimeOutOfSchedule, searchData.sID, "Invalid Date Time , Date From after Date to! ");

            foreach (var item in searchData.SearchRooms)
            {
                Paxes = Paxes + item.Adult + item.Child.Count;
                if (item.Adult > 5)
                    throw new HotelSearchInputException(HotelSearchInputErrorCodes.InvalidPassengerNo, searchData.sID, "Invalid ADT NO in Room Max is 5! ");
                if (item.Child.Count > 2)
                    throw new HotelSearchInputException(HotelSearchInputErrorCodes.InvalidPassengerNo, searchData.sID, "Invalid CHD NO Max is 3! ");
            }
            if (Paxes > 9)
                throw new HotelSearchInputException(HotelSearchInputErrorCodes.InvalidPassengerNo, searchData.sID, "Invalid Passenger NO Max is 9! ");
            if (searchData.SearchRooms.Count < 1 || searchData.SearchRooms == null)
                throw new HotelSearchInputException(HotelSearchInputErrorCodes.RoomNotFound, searchData.sID, "Room is Required");
            return true;

            //if (!DateTime.TryParseExact(searchData.DateFrom.Date.ToString(), "MMMM dd, yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime DateFrom)
            //    || !DateTime.TryParseExact(searchData.DateTo.Date.ToString(), "MMMM dd, yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime DateTo))
            //    throw new HotelSearchInputException(HotelSearchInputErrorCodes.DateTimeOutOfSchedule, searchData.sID, "Invalid Date Time Format must be MMMM dd, yyyy! ");

        }

        List<HotelSearchResult> GetLowestPrice(List<HotelSearchResult> searchResults)
        {
            var CountedList = searchResults.GroupBy(x => x.hotelCode).Select(x => new
            {
                Count = x.Count(),
                ID = x.Key,
            }).Where(x => x.Count > 1).ToList();
            for (int i = 0; i < CountedList.Count; i++)
            {

                var Repeated = searchResults.Where(a => a.hotelCode == CountedList[i].ID).ToList();
                var minRepeatedHotel = Repeated.Where(a => a.hotelRate == Repeated.Min(x => x.hotelRate)).FirstOrDefault();
                Repeated.Remove(minRepeatedHotel);
                for (int r = 0; r < Repeated.Count; r++)
                {
                    searchResults.Remove(Repeated[r]);
                }
            }
            var hotelsZero = searchResults.Where(a => a.TotalSellPrice == 0).ToList();
            if (hotelsZero.Count > 0)
            {
                for (int j = 0; j < hotelsZero.Count; j++)
                {
                    searchResults.Remove(hotelsZero[j]);
                }
            }
            return searchResults;
        }
        public void SaveSearchResult(List<HotelSearchResult> HotelSearchResults, string sID)
        {
            try
            {
                SearchResultData search = new SearchResultData();
                search.SaveSearchResult(HotelSearchResults, sID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Dispose()
        {
            handle.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
