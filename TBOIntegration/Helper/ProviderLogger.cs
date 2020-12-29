using Hotels.ErrorHandling.ErrorCodes;
using Hotels.ErrorHandling.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TBO.WSDL.hotelServiceRef;

namespace TBOIntegration.Helper
{
    static class ProviderLogger
    {
        static string MainPath = ConfigurationSettings.AppSettings["LogPathdata"];
        // log XML req and rsp for provider 
        public static void LogSearchProviderReq(HotelSearchRequest SearchReq, string SearchId)
        {
            string path = MainPath + @"\TBOLogs\" + DateTime.UtcNow.Date.ToString("dd_MM_yyyy") + @"\" + SearchId + @"\SearchRequest\";
            Directory.CreateDirectory(path);
            path = path + "HotelSearchRequest.txt";
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(SearchReq.GetType());
                using (StreamWriter streamwriter = new StreamWriter(path, append: false))
                {
                    xmlSerializer.Serialize(streamwriter, SearchReq);
                    streamwriter.Close();
                }
            }
            catch (FileNotFoundException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileNotFound, LoggerErrorCodes.LoggerFileNotFound + " -This path (" + path + ") doesn't exist with file name " + Ex.FileName + " .", Ex.Message);
            }
            catch (IOException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileINProcess, LoggerErrorCodes.LoggerFileINProcess + " -This Logger File (" + path + ") in another process you can not access it right now.", Ex.Message);
            }
            catch (Exception Ex)
            {
                throw new LoggerException(LoggerErrorCodes.FailedLogIntoLogger, LoggerErrorCodes.FailedLogIntoLogger + " -Failed Log Into Logger with path (" + path + ").", Ex.Message);
            }

        }
        public static void LogSearchProviderRsp(HotelSearchResponse SearchRsp, string SearchId, string time)
        {
            string path = MainPath + @"\TBOLogs\" + DateTime.UtcNow.Date.ToString("dd_MM_yyyy") + @"\" + SearchId + @"\SearchResponse\";
            Directory.CreateDirectory(path);
            path = path + "HotelSearchResponse.txt";
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(SearchRsp.GetType());
                using (StreamWriter streamwriter = new StreamWriter(path, append: false))
                {
                    streamwriter.WriteLine($"Response Time is {time}");
                    streamwriter.WriteLine("");
                    xmlSerializer.Serialize(streamwriter, SearchRsp);
                    streamwriter.Close();
                }
            }
            catch (FileNotFoundException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileNotFound, LoggerErrorCodes.LoggerFileNotFound + " -This path (" + path + ") doesn't exist with file name " + Ex.FileName + " .", Ex.Message);
            }
            catch (IOException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileINProcess, LoggerErrorCodes.LoggerFileINProcess + " -This Logger File (" + path + ") in another process you can not access it right now.", Ex.Message);
            }
            catch (Exception Ex)
            {
                throw new LoggerException(LoggerErrorCodes.FailedLogIntoLogger, LoggerErrorCodes.FailedLogIntoLogger + " -Failed Log Into Logger with path (" + path + ").", Ex.Message);
            }
        }


        public static void LogRoomAvailabiltyReq(HotelRoomAvailabilityRequest AvailabiltyReq, string SearchId)
        {

            string path = MainPath + @"\TBOLogs\" + DateTime.UtcNow.Date.ToString("dd_MM_yyyy") + @"\" + SearchId + @"\RoomAvailabiltyReq\";
            Directory.CreateDirectory(path);
            path = path + "RoomAvailabiltyReq.txt";
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(AvailabiltyReq.GetType());
                using (StreamWriter streamwriter = new StreamWriter(path, append: false))
                {
                    xmlSerializer.Serialize(streamwriter, AvailabiltyReq);
                    streamwriter.Close();
                }
            }
            catch (FileNotFoundException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileNotFound, LoggerErrorCodes.LoggerFileNotFound + " -This path (" + path + ") doesn't exist with file name " + Ex.FileName + " .", Ex.Message);
            }
            catch (IOException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileINProcess, LoggerErrorCodes.LoggerFileINProcess + " -This Logger File (" + path + ") in another process you can not access it right now.", Ex.Message);
            }
            catch (Exception Ex)
            {
                throw new LoggerException(LoggerErrorCodes.FailedLogIntoLogger, LoggerErrorCodes.FailedLogIntoLogger + " -Failed Log Into Logger with path (" + path + ").", Ex.Message);
            }
        }
        public static void LogRoomAvailabilityProviderRsp(HotelRoomAvailabilityResponse AvailabilityRsp, string SearchId)
        {
            string path = MainPath + @"\TBOLogs\" + DateTime.UtcNow.Date.ToString("dd_MM_yyyy") + @"\" + SearchId + @"\RoomAvailabilityResp\";
            Directory.CreateDirectory(path);
            path = path + "RoomAvailabilityResp.txt";
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(AvailabilityRsp.GetType());
                using (StreamWriter streamwriter = new StreamWriter(path, append: false))
                {
                    xmlSerializer.Serialize(streamwriter, AvailabilityRsp);
                    streamwriter.Close();
                }
            }
            catch (FileNotFoundException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileNotFound, LoggerErrorCodes.LoggerFileNotFound + " -This path (" + path + ") doesn't exist with file name " + Ex.FileName + " .", Ex.Message);
            }
            catch (IOException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileINProcess, LoggerErrorCodes.LoggerFileINProcess + " -This Logger File (" + path + ") in another process you can not access it right now.", Ex.Message);
            }
            catch (Exception Ex)
            {
                throw new LoggerException(LoggerErrorCodes.FailedLogIntoLogger, LoggerErrorCodes.FailedLogIntoLogger + " -Failed Log Into Logger with path (" + path + ").", Ex.Message);
            }
        }


        public static void LogAvailablityPricingReq(AvailabilityAndPricingRequest AvailablityPricingReq, string SearchId)
        {
            string path = MainPath + @"\TBOLogs\" + DateTime.UtcNow.Date.ToString("dd_MM_yyyy") + @"\" + SearchId + @"\AvailablityPricingReq\";
            Directory.CreateDirectory(path);
            path = path + "AvailablityPricingReq.txt";
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(AvailablityPricingReq.GetType());
                using (StreamWriter streamwriter = new StreamWriter(path, append: false))
                {
                    xmlSerializer.Serialize(streamwriter, AvailablityPricingReq);
                    streamwriter.Close();
                }
            }
            catch (FileNotFoundException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileNotFound, LoggerErrorCodes.LoggerFileNotFound + " -This path (" + path + ") doesn't exist with file name " + Ex.FileName + " .", Ex.Message);
            }
            catch (IOException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileINProcess, LoggerErrorCodes.LoggerFileINProcess + " -This Logger File (" + path + ") in another process you can not access it right now.", Ex.Message);
            }
            catch (Exception Ex)
            {
                throw new LoggerException(LoggerErrorCodes.FailedLogIntoLogger, LoggerErrorCodes.FailedLogIntoLogger + " -Failed Log Into Logger with path (" + path + ").", Ex.Message);
            }
        }
        public static void LogAvailablityPricingProviderRsp(AvailabilityAndPricingResponse AvailablityPricingRsp, string SearchId)
        {
            string path = MainPath + @"\TBOLogs\" + DateTime.UtcNow.Date.ToString("dd_MM_yyyy") + @"\" + SearchId + @"\AvailablityPricingResp\";
            Directory.CreateDirectory(path);
            path = path + "AvailablityPricingRsp.txt";
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(AvailablityPricingRsp.GetType());
                using (StreamWriter streamwriter = new StreamWriter(path, append: false))
                {
                    xmlSerializer.Serialize(streamwriter, AvailablityPricingRsp);
                    streamwriter.Close();
                }
            }
            catch (FileNotFoundException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileNotFound, LoggerErrorCodes.LoggerFileNotFound + " -This path (" + path + ") doesn't exist with file name " + Ex.FileName + " .", Ex.Message);
            }
            catch (IOException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileINProcess, LoggerErrorCodes.LoggerFileINProcess + " -This Logger File (" + path + ") in another process you can not access it right now.", Ex.Message);
            }
            catch (Exception Ex)
            {
                throw new LoggerException(LoggerErrorCodes.FailedLogIntoLogger, LoggerErrorCodes.FailedLogIntoLogger + " -Failed Log Into Logger with path (" + path + ").", Ex.Message);
            }
        }


        public static void LogHotelBookReq(HotelBookRequest HotelBookReq, string SearchId)
        {
            string path = MainPath + @"\TBOLogs\" + DateTime.UtcNow.Date.ToString("dd_MM_yyyy") + @"\" + SearchId + @"\HotelBookReq\";
            Directory.CreateDirectory(path);
            path = path + "HotelBookReq.txt";
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(HotelBookReq.GetType());
                using (StreamWriter streamwriter = new StreamWriter(path, append: false))
                {
                    xmlSerializer.Serialize(streamwriter, HotelBookReq);
                    streamwriter.Close();
                }
            }
            catch (FileNotFoundException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileNotFound, LoggerErrorCodes.LoggerFileNotFound + " -This path (" + path + ") doesn't exist with file name " + Ex.FileName + " .", Ex.Message);
            }
            catch (IOException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileINProcess, LoggerErrorCodes.LoggerFileINProcess + " -This Logger File (" + path + ") in another process you can not access it right now.", Ex.Message);
            }
            catch (Exception Ex)
            {
                throw new LoggerException(LoggerErrorCodes.FailedLogIntoLogger, LoggerErrorCodes.FailedLogIntoLogger + " -Failed Log Into Logger with path (" + path + ").", Ex.Message);
            }
        }
        public static void LogHotelBookResp(HotelBookResponse HotelBookResp, string SearchId)
        {

            string path = MainPath + @"\TBOLogs\" + DateTime.UtcNow.Date.ToString("dd_MM_yyyy") + @"\" + SearchId + @"\HotelBookResp\";
            Directory.CreateDirectory(path);
            path = path + "HotelBookResp.txt";
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(HotelBookResp.GetType());
                using (StreamWriter streamwriter = new StreamWriter(path, append: false))
                {
                    xmlSerializer.Serialize(streamwriter, HotelBookResp);
                    streamwriter.Close();
                }
            }
            catch (FileNotFoundException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileNotFound, LoggerErrorCodes.LoggerFileNotFound + " -This path (" + path + ") doesn't exist with file name " + Ex.FileName + " .", Ex.Message);
            }
            catch (IOException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileINProcess, LoggerErrorCodes.LoggerFileINProcess + " -This Logger File (" + path + ") in another process you can not access it right now.", Ex.Message);
            }
            catch (Exception Ex)
            {
                throw new LoggerException(LoggerErrorCodes.FailedLogIntoLogger, LoggerErrorCodes.FailedLogIntoLogger + " -Failed Log Into Logger with path (" + path + ").", Ex.Message);
            }
        }


        public static void LogBookingDetailReq(HotelBookingDetailRequest BookDetailReq, string SearchId)
        {
            string path = MainPath + @"\TBOLogs\" + DateTime.UtcNow.Date.ToString("dd_MM_yyyy") + @"\" + SearchId + @"\BookDetailReq\";
            Directory.CreateDirectory(path);
            path = path + "BookDetailReq.txt";
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(BookDetailReq.GetType());
                using (StreamWriter streamwriter = new StreamWriter(path, append: false))
                {
                    xmlSerializer.Serialize(streamwriter, BookDetailReq);
                    streamwriter.Close();
                }
            }
            catch (FileNotFoundException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileNotFound, LoggerErrorCodes.LoggerFileNotFound + " -This path (" + path + ") doesn't exist with file name " + Ex.FileName + " .", Ex.Message);
            }
            catch (IOException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileINProcess, LoggerErrorCodes.LoggerFileINProcess + " -This Logger File (" + path + ") in another process you can not access it right now.", Ex.Message);
            }
            catch (Exception Ex)
            {
                throw new LoggerException(LoggerErrorCodes.FailedLogIntoLogger, LoggerErrorCodes.FailedLogIntoLogger + " -Failed Log Into Logger with path (" + path + ").", Ex.Message);
            }
        }
        public static void LogBookingDetailRsp(HotelBookingDetailResponse BookDetailResp, string SearchId)
        {
            string path = MainPath + @"\TBOLogs\" + DateTime.UtcNow.Date.ToString("dd_MM_yyyy") + @"\" + SearchId + @"\BookDetailResp\";
            Directory.CreateDirectory(path);
            path = path + "BookDetailResp.txt";
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(BookDetailResp.GetType());
                using (StreamWriter streamwriter = new StreamWriter(path, append: false))
                {
                    xmlSerializer.Serialize(streamwriter, BookDetailResp);
                    streamwriter.Close();
                }
            }
            catch (FileNotFoundException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileNotFound, LoggerErrorCodes.LoggerFileNotFound + " -This path (" + path + ") doesn't exist with file name " + Ex.FileName + " .", Ex.Message);
            }
            catch (IOException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileINProcess, LoggerErrorCodes.LoggerFileINProcess + " -This Logger File (" + path + ") in another process you can not access it right now.", Ex.Message);
            }
            catch (Exception Ex)
            {
                throw new LoggerException(LoggerErrorCodes.FailedLogIntoLogger, LoggerErrorCodes.FailedLogIntoLogger + " -Failed Log Into Logger with path (" + path + ").", Ex.Message);
            }
        }

        public static void LogHotelCancelReq(HotelCancelRequest HotelCancelReq, string SearchId)
        {
            string path = MainPath + @"\TBOLogs\" + DateTime.UtcNow.Date.ToString("dd_MM_yyyy") + @"\" + SearchId + @"\CancelReq\";
            Directory.CreateDirectory(path);
            path = path + "HotelCancelReq.txt";
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(HotelCancelReq.GetType());
                using (StreamWriter streamwriter = new StreamWriter(path, append: false))
                {
                    xmlSerializer.Serialize(streamwriter, HotelCancelReq);
                    streamwriter.Close();
                }
            }
            catch (FileNotFoundException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileNotFound, LoggerErrorCodes.LoggerFileNotFound + " -This path (" + path + ") doesn't exist with file name " + Ex.FileName + " .", Ex.Message);
            }
            catch (IOException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileINProcess, LoggerErrorCodes.LoggerFileINProcess + " -This Logger File (" + path + ") in another process you can not access it right now.", Ex.Message);
            }
            catch (Exception Ex)
            {
                throw new LoggerException(LoggerErrorCodes.FailedLogIntoLogger, LoggerErrorCodes.FailedLogIntoLogger + " -Failed Log Into Logger with path (" + path + ").", Ex.Message);
            }
        }
        public static void LogHotelCancelResp(HotelCancelResponse HotelCancelResp, string SearchId)
        {
            string path = MainPath + @"\TBOLogs\" + DateTime.UtcNow.Date.ToString("dd_MM_yyyy") + @"\" + SearchId + @"\CancelResp\";
            Directory.CreateDirectory(path);
            path = path + "HotelCancelResp.txt";
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(HotelCancelResp.GetType());
                using (StreamWriter streamwriter = new StreamWriter(path, append: false))
                {
                    xmlSerializer.Serialize(streamwriter, HotelCancelResp);
                    streamwriter.Close();
                }
            }
            catch (FileNotFoundException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileNotFound, LoggerErrorCodes.LoggerFileNotFound + " -This path (" + path + ") doesn't exist with file name " + Ex.FileName + " .", Ex.Message);
            }
            catch (IOException Ex)
            {
                throw new LoggerException(LoggerErrorCodes.LoggerFileINProcess, LoggerErrorCodes.LoggerFileINProcess + " -This Logger File (" + path + ") in another process you can not access it right now.", Ex.Message);
            }
            catch (Exception Ex)
            {
                throw new LoggerException(LoggerErrorCodes.FailedLogIntoLogger, LoggerErrorCodes.FailedLogIntoLogger + " -Failed Log Into Logger with path (" + path + ").", Ex.Message);
            }
        }

    }
}
