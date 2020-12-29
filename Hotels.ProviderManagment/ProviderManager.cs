using HotelBedsIntegration.Management;
using HotelBedsIntegration.Models;
using Hotels.BLL;
using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Hotels.DAL;
using Hotels.IBLL;
using IntegrationTotalStay.Controller;
using Microsoft.Win32.SafeHandles;
using SMYROOMS.Managment;
using SMYROOMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBOIntegration.Management;

namespace Hotels.ProviderManagment
{
    public class ProviderManager : IDisposable
    {
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public SearchData searchData { set; get; }
        public List<SearchOutputData> SMRResult { set; get; }
        public List<Hotel> HBResult { set; get; }
        public HotelSearchResponse searchResponse { set; get; }
        public List<HotelSearchResult> HotelSearchResults { set; get; }
        //prop result
        public List<hotelsProvider> WegoHotels { set; get; } //*********
        public List<HotelChannelResult> hotelChannelResults { set; get; }
        public ProviderManager()
        {
            searchData = new SearchData();
            SMRResult = new List<SearchOutputData>();
            HotelSearchResults = new List<HotelSearchResult>();
            searchResponse = new HotelSearchResponse();
            hotelChannelResults = new List<HotelChannelResult>();
        }

        public void GetHotelSearchResultForAllProviders()
        {
            SearchResultData searchResult = new SearchResultData();

            List<GetActiveProviders_Result> providers_Results = searchResult.GetActiveProvidersData();
            //use stored in hotelsDb [GetHotelsIDAndProvidersByCityName] hotelbeds
            //var HotelIdsForActiveProviders = searchResult.GetProviderHotelIdsForActiveProviders(int.Parse(searchData.CityName));
            // hotelsDb tbl CitiesID
            //var ProvidersCities = searchResult.GetProviderCitiessForActiveProviders(int.Parse(searchData.CityName));//
            var tasks = new List<Task>();
            var tokenSource1 = new CancellationTokenSource();
            var tokenSource2 = new CancellationTokenSource();
            var tokenSource3 = new CancellationTokenSource();
            CancellationTokenSource tokenSource5 = new CancellationTokenSource();
            for (int i = 0; i < providers_Results.Count; i++)
            {
                int providerid = providers_Results[i].Provider_ID.Value;
                switch (providerid)
                {
                    case 1:
                        break;
                    //case 2:
                    //    var token3 = tokenSource3.Token;
                    //    try
                    //    {
                    //        if (ProvidersCities.Count > 0)
                    //        {
                    //            tasks.Add(Task.Factory.StartNew(() =>
                    //            {
                    //                //
                    //                var tsCity = ProvidersCities.FirstOrDefault(a => a.providerID == 2);
                    //                if (tsCity != null)
                    //                {
                    //                    var searchobj = IntegrationTotalStay.Management.SearchManager.prepareSearchObj(searchData, tsCity.providerCity.ToString());
                    //                    var res = SearchTs.GetTSHotels(searchobj);


                    //                    using (TSMapper tsMapper = new TSMapper())
                    //                    {
                    //                        searchResponse.HotelResult.AddRange(tsMapper.MapSearchResult(res, searchData).HotelResult);

                    //                    }
                    //                }
                    //            }, token3));
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        LoggingHelper.WriteToFile("/ProviderManager/Errors/", "HotelSearchSMR_" + searchData.sID, ex.InnerException?.Message, ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);
                    //    }

                    //    break;
                    case 3:
                        /*     var token1 = tokenSource1.Token;
                             SearchManager smrManager = new SearchManager();
                             smrManager.searchData = searchData;
                            try
                            {
                                tasks.Add(Task.Factory.StartNew(() =>
                                {
                                    smrManager.HotelIds = HotelIdsForActiveProviders.Where(a => a.ProviderId == "3").Select(a => a.HotelProviderId).ToList();
                                    smrManager.GetSearchResult();
                                    SMRResult = smrManager.searchOutputs;


                                    using (SMRMapper sMRMapper = new SMRMapper())
                                    {
                                        HotelSearchResults.AddRange(sMRMapper.MapSearchResult(SMRResult, smrManager.boardCodes, searchData));
                                    }

                                }, token1));
                            }
                            catch (Exception ex)
                            {
                                LoggingHelper.WriteToFile("/ProviderManager/Errors/", "HotelSearchSMR_" + searchData.sID, ex.InnerException?.Message, ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);
                            }*/
                        break;
                    //case 4:
                    //    var token2 = tokenSource2.Token;
                    //    HBSearchManager HBManager = new HBSearchManager();
                    //    HBManager.searchData = searchData;
                    //    try
                    //    {
                    //        if (HotelIdsForActiveProviders.Count > 0)
                    //        {
                    //            tasks.Add(Task.Factory.StartNew(() =>
                    //            {
                    //                if (searchData.Source.ToLower() == "direct" || searchData.Source.ToLower() == "ios" || searchData.Source.ToLower() == "android")
                    //                    HBManager.HotelIds = HotelIdsForActiveProviders.Where(a => a.ProviderId == "4").Select(a => int.Parse(a.HotelProviderId)).ToList();
                    //                else
                    //                {
                    //                    var hoteslPids = WegoHotels.Where(x => x.providerID == "4").Select(x => x.providerHotelID).ToList();
                    //                    HBManager.HotelIds = HotelIdsForActiveProviders.Where(a => a.ProviderId == "4" && hoteslPids.Contains(a.HotelProviderId)).Select(a => int.Parse(a.HotelProviderId)).ToList();
                    //                    ///HBManager.HotelIds.Add(103296); HBManager.HotelIds.Add(6810); HBManager.HotelIds.Add(405928); HBManager.HotelIds.Add(7661); HBManager.HotelIds.Add(9487); HBManager.HotelIds.Add(395560);
                    //                }

                    //                HBManager.GetSearchResult();
                    //                HBResult = HBManager.searchOutputs;

                    //                if (searchData.Source.ToLower() == "direct" || searchData.Source.ToLower() == "ios" || searchData.Source.ToLower() == "android")
                    //                {
                    //                    using (HBMapper HBMapper = new HBMapper())
                    //                    {
                    //                        // HotelSearchResults.AddRange(sMRMapper.MapSearchResult(HBResult, searchData));
                    //                        // call Service Charge and Cancellation Charge Get SaleRule API
                    //                        searchResponse.HotelResult.AddRange(HBMapper.MapSearchResult(HBResult, searchData).HotelResult);
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    using (ChannelHBMapper HbMapper = new ChannelHBMapper())
                    //                    {
                    //                        hotelChannelResults.AddRange(HbMapper.MapSearchResult(HBResult, searchData));
                    //                    }
                    //                }
                    //            }
                    //                , token2));
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        LoggingHelper.WriteToFile("ProviderManager/Errors/", "HotelSearchHotelBeds_" + searchData.sID, ex.InnerException?.Message, ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);
                    //    }
                    //    break;
                    case 5:
                        var token5 = tokenSource5.Token;
                        TBOSearchManager TBOManager = new TBOSearchManager();
                        TBOManager.searchData = searchData;

                        TBORepo tBORepo = new TBORepo();
                        try
                        {
                            //if (HotelIdsForActiveProviders.Count > 0)
                            //{
                                //tasks.Add(Task.Factory.StartNew(() =>
                                //{
                                    //TBOManager.HotelIds = HotelIdsForActiveProviders.Where(a => a.ProviderId == "5").Select(a => a.HotelProviderId).ToList();
                                    TBOManager.HotelIds = tBORepo.GetHotelIdsByCityCode(searchData.CityName);
                                    //call tbo provider
                                    TBOManager.GetSearchResult();
                                    var TBOResult = TBOManager.searchOutputs;

                                    // map resp to general rsp
                                    searchResponse.HotelResult.AddRange(TBOMapper.MapSearchResult(TBOResult, searchData).HotelResult);
                                    
                               // }
                                  //  ));
                           // }
                        }
                        catch (Exception ex)
                        {
                            LoggingHelper.WriteToFile("/ProviderManager/Errors/", "HotelSearchTBO_" + searchData.sID, ex.InnerException?.Message, ex.Message + " Source :" + ex.Source + " Stack Trace :" + ex.StackTrace);
                        }
                        break;
                }
            }
            Task.WaitAll(tasks.ToArray());
        }

        public void Dispose()
        {
            handle.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
