using IntegrationTotalStay.Model.Booking;
using IntegrationTotalStay.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTotalStay.Controller
{
   public class ConfirmationTS
    {
        public static BookResponse GetTSConfirmations(BookRequest book ,string BN,string session)
        {
            book.LoginDetails.Login = "Citybookerstest";
            book.LoginDetails.Password = "xmltest";
            var res = BookingSer.getTsHotelConfirmation(book);
            if (res != null)
            {
                return res;
            }
            return null;
        }
    }
}
