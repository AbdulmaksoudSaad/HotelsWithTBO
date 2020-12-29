using Hotels.Common;
using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IBLL
{
   public interface IAdminSearchBL
    {
        List<AdminRequiredData> GetHotelAdimSearchData(string Key);
        List<AdminRequiredData> QueueTransaction(DateTime fromDate, DateTime toDate);
        AdminBookingDetails GetBookingDetails(String BN);
        List<HotelBookingStatusValue> GetBookingStautsDetails();

        List<ClsProvider> GetProviderDetails();
        bool UpdateBookingStatus(string BN, string Status);
        void EditBookingPaxes(BookingPassenger passenger);

        List<CustomerData> GetAllCustomer();
        bool CheckBooking(String BN);

    }
}
