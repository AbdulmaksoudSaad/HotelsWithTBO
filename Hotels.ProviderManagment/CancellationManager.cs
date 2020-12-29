using HotelBedsIntegration.Controller;
using Hotels.BLL;
using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.Common.Models;
using Hotels.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBOIntegration.Services;

namespace Hotels.ProviderManagment
{
  public class CancellationManager
    {
      public  CancelDetailes cancelData;
        public void CancelForAllProviders( string BN)
        {
            cancelData = new CancelDetailes();
            SearchResultData searchResult = new SearchResultData();
            List<GetActiveProviders_Result> providers_Results = searchResult.GetActiveProvidersData();
            CancellationBookingRepo cancellationrepo = new CancellationBookingRepo();
          var BookingData=  cancellationrepo.GetBookingReference(BN);
            // for (int i = 0; i < providers_Results.Count; i++)
            //{
            //  int providerid = providers_Results[i].Provider_ID.Value;
            switch (BookingData.ProviderId.Value)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                    try
                    {

                        var CancelDetailes = Cancellation.CancelBooking(BookingData.Reference, BN).Result;
                        if(CancelDetailes != null)
                        {
                            cancellationrepo.SaveCancellationBookingData(CancelDetailes, BN, BookingData.Reference);
                           
                            if (CancelDetailes.booking.status.ToLower() == "CANCELLED".ToLower())
                            {
                                cancelData.Status = true;
                                cancelData.CancelReference = CancelDetailes.booking.cancellationReference;
                                cancelData.BookingNum = BN;
                            }
                            else
                            {
                                cancelData.Status = false;
                                cancelData.BookingNum = BN;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        cancelData.Status = false;
                        cancelData.BookingNum = BN;
                        LoggingHelper.WriteToFile("/ProviderCancelManager/Errors/", "HotelSearchSMR_" + BN, ex.Message, ex.Message + " Sourse :" + ex.Source + " Stack Trace :" + ex.StackTrace);
                    }

                    break;
                // }
                case 5:
                    
                    var CancelTBO = CancelationService.Cancel(BookingData.supplerReference); 
                    if (CancelTBO != null)
                    {
                        //cancellationrepo.SaveCancellationBookingData(CancelTBO, BN, BookingData.Reference);
                        //TBO.RequestStatus
                        //UnProcessed,Pending, InProgress, Processed, Rejected  RefundAwaited
                        if (CancelTBO.RequestStatus.ToString().ToLower() == "Processed".ToLower())
                        {
                            cancelData.Status = true;
                            //cancelData.CancelReference = CancelDetailes.booking.cancellationReference;
                            cancelData.BookingNum = BN;
                        }
                        else
                        {
                            cancelData.Status = false;
                            cancelData.BookingNum = BN;
                        }
                    }
                    break;
            }
         }

    }
}
