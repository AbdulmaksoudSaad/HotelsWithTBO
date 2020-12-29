using Hotels.Common;
using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IDAL
{
  public  interface IAdminSearchDA
    {
        List<CustomerData> GetAllCustomer();
        List<AdminRequiredData> GetHotelAdimSearchData(string Key);
        List<AdminRequiredData> QueueTransaction(DateTime fromDate ,DateTime toDate);
        AdminBookingDetails GetBookingDetails(String BN);
        List<HotelBookingStatusValue >GetBookingStautsDetails();
        List<ClsProvider> GetProviderDetails();
        bool UpdateBookingStatus(string BN,string Status);
        void EditBookingPaxes(BookingPassenger passenger);
        bool CheckBooking(String BN );


    }
}
