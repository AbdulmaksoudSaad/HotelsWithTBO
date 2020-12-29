using System;
using System.Collections.Generic;
using System.Text;

namespace Hotels.ErrorHandling.ErrorCodes
{
    public static class HotelSearchInputErrorCodes
    {
        public static string InvalidDateFormat = "1001";  //MMMM dd yyyy
        public static string DateTimeOutOfSchedule = "1002";
        public static string InvalidPassengerNo = "1003"; //Max 9
        public static string SearchIdNotFound = "1004";
        public static string CityNameNotFound = "1005";
        public static string CurrencyNotFound = "1006";
        public static string LangNotFound = "1007";
        public static string NationalityNotFound = "1008";
        public static string POSNotFound = "1009";
        public static string SourceNotFound = "1010";
        public static string RoomNotFound = "1011";

        

    }
}
