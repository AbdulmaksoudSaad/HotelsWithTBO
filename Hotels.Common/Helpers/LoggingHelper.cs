using Hotels.ErrorHandling.ErrorCodes;
using Hotels.ErrorHandling.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Helpers
{
    public class LoggingHelper
    {
        public static void WriteToFile(string fileDirectory, string fileName, string dataCategory, string dataToWrite)
        {
            string path = ConfigurationSettings.AppSettings["LogPathdata"];
            path = path + fileDirectory;
            Directory.CreateDirectory(@path + "/" + DateTime.UtcNow.Date.ToString("dd_MM_yyyy"));
            string filepath = @path + "/" + DateTime.UtcNow.Date.ToString("dd_MM_yyyy") + "/" + fileName + /*"_" + DateTime.UtcNow.ToString("dd-MM-yyyy--hh-mm tt") +*/ ".txt";
            try
            {
                using (StreamWriter fs = File.AppendText(filepath))
                {
                    fs.WriteLine(dataCategory);
                    fs.WriteLine(DateTime.UtcNow + " : ");
                    if (dataToWrite != null)
                    {
                        fs.Write(dataToWrite);
                        fs.WriteLine();
                    }

                    else
                    {
                        fs.WriteLine("Data is null");
                    }
                    fs.WriteLine("=========================================");
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