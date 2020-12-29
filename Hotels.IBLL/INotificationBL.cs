using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IBLL
{
   public interface INotificationBL
    {
        int sendnotification(BookingStatus bookingStatus,string BN, string Sid, string Status, string message,MailObj mail);
    }
}
