using HotelBedsIntegration.Models;
using Hotels.Common;
using Hotels.Common.Helpers;
using Hotels.DAL;
using Hotels.ProviderManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.ServOrch
{
    public class ConfirmationBooking
    {


        public static BookingStatus GetConfirmationBooking(string Sid, string BN)
        {
            BookingStatus confirmed = new BookingStatus();

            try
            {
                // check confirmation
                var Confirm = ConfirmationDate.CheckBookingConfirmation(BN, Sid);
                // get data from DB //
                if (Confirm != true)
                {
                    var data = ConfirmationDate.GetAllConfirmationData(Sid, BN);
                    // call provider
                    if (data != null)
                    {


                        confirmed = ConfirmationManager.CallProviders(data, BN);
                        // Change Booking status
                        if (confirmed.status == 0)
                        {
                            //  save it in DB

                            // Change Booking status
                            var ConfirmStatus = ConfirmationDate.ChangeBookingStatus(Sid, BN, "Booked");
                            if (ConfirmStatus != null)
                            {
                                return confirmed;
                            }
                            confirmed.status = 1;
                            return confirmed;
                        }
                        else if (confirmed.status == 1)
                        {
                            var ConfirmStatus = ConfirmationDate.ChangeBookingStatus(Sid, BN, "Missing");

                            if (ConfirmStatus != null)
                            {
                                return confirmed;
                            }
                            confirmed.status = 1;
                            return confirmed;

                        }
                        else if (confirmed.status == 2)
                        {
                            var ConfirmStatus = ConfirmationDate.ChangeBookingStatus(Sid, BN, "Booking Not Confirmed");

                            if (ConfirmStatus != null)
                            {
                                return confirmed;
                            }
                            confirmed.status = 2;
                            return confirmed;
                        }

                        return confirmed;
                    }
                 
                confirmed.status = 2;
                return confirmed;
            }
                else
                {
                    confirmed.status = 3;
                    return confirmed;

                }
        }catch(Exception ex)
            {
                LoggingHelper.WriteToFile("SaveBookingConfirmationController/Errors/", "ConfirmationBooking GetConfirmationBooking" + "ServOrch" + Sid, ex.InnerException?.Message, ex.Message + ex.StackTrace);

                if (confirmed.status != 2)
                {
                    confirmed.status=1;
                }
                return confirmed;
            }
        }
    }
}
