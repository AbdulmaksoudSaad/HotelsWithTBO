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
    public class AdminSearchBLL : IAdminSearchBL
    {
        public bool CheckBooking(string BN)
        {
            AdminSearchData adminSearch = new AdminSearchData();
         return   adminSearch.CheckBooking(BN);
        }

        public void EditBookingPaxes(BookingPassenger passenger)
        {
            AdminSearchData adminSearch = new AdminSearchData();
            adminSearch.EditBookingPaxes(passenger);
        }

        public List<CustomerData> GetAllCustomer()
        {
            AdminSearchData adminSearch = new AdminSearchData();
            return adminSearch.GetAllCustomer();
        }

        public AdminBookingDetails GetBookingDetails(string BN)
        {
            AdminSearchData adminSearch = new AdminSearchData();
            return adminSearch.GetBookingDetails(BN);
             
        }

        public List<HotelBookingStatusValue> GetBookingStautsDetails()
        {
          
            AdminSearchData adminSearch = new AdminSearchData();
           return adminSearch.GetBookingStautsDetails();
        }

        public List<AdminRequiredData> GetHotelAdimSearchData(string Key)
        {
            AdminSearchData adminSearch = new AdminSearchData();
            return adminSearch.GetHotelAdimSearchData(Key);
        }

        public List<ClsProvider> GetProviderDetails()
        {
            AdminSearchData adminSearch = new AdminSearchData();
            return adminSearch.GetProviderDetails();
        }

        public List<AdminRequiredData> QueueTransaction(DateTime fromDate, DateTime toDate)
        {
            AdminSearchData adminSearch = new AdminSearchData();
            return adminSearch.QueueTransaction(fromDate,toDate);
        }

        public bool UpdateBookingStatus(string BN, string Status)
        {
            AdminSearchData adminSearch = new AdminSearchData();
            return adminSearch.UpdateBookingStatus(BN, Status);
        }
    }
}
