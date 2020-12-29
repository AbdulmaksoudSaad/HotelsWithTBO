using Hotels.Common.Helpers;
using IntegrationTotalStay.Model;
using IntegrationTotalStay.Model.Availability;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace IntegrationTotalStay.Service
{
    class CheckAvailabilty
    {

        public static PreBookResponse getTsHotelAvailability(PreBookRequest request)
        {

            PreBookResponse bKR = new PreBookResponse();
            try
            {
                XmlSerializer xsSubmit = new XmlSerializer(typeof(PreBookRequest));

                var xml = "";

                using (var sww = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(sww))
                    {
                        xsSubmit.Serialize(writer, request);

                        xml = sww.ToString(); // Your XML
                        xml = xml.Remove(0, 39);
                        xml = xml.Remove(9, 99);// 
                       xml = xml.Replace("<PreBookRchema\">", "<PreBookRequest>");
                        xml = xml.Replace(@"<PreBookRtance"">", " <PreBookRequest>");

                        /*     //for (int i = 1; i <= data.RequestParameters.Insureds.Insured.Count; i++)
                             //{ <PreBookRtance">
                             //    xml = xml.Replace("ID=\"1\"", "ID='1'");
                             //}
                             xml = xml.Replace("ID=\"1\"", "ID='1'");
                             xml = xml.Replace("<Request>Parameters>", "<RequestParameters>");
                             */
                        //  xml = xml.Replace("string", "");
                    }
                }
                Stopwatch timer1 = new Stopwatch();
                timer1.Start();
                HttpWebRequest client = (HttpWebRequest)HttpWebRequest.Create("http://xmlintegrations.jactravel.com/xml/book.aspx");
                WebResponse Rsp = null;
                //client = HttpWebRequest.CreateHttp(strEndPoint);

                client.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; MS Web Services Client Protocol 4.0.30319.18408)";
                client.ReadWriteTimeout = 600000;
                //string action = strAction;
                client.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                client.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                //client.Headers.Add("SOAPAction:" + action);
                client.ContentType = "application/x-www-form-urlencoded";
                client.KeepAlive = true;
                client.Method = "post";
                StreamWriter xStream;
                xStream = new StreamWriter(client.GetRequestStream());
                xStream.WriteLine("data=" + xml);
                LoggingHelper.WriteToFile("TsServise/Request/", "Availability", "TSServiceREq", xml);

                xStream.Close();
                //  var fileData = System.Configuration.ConfigurationManager.AppSettings["ui"];

                //    Helper.Logger.WriteToFile(fileData + "/Logs/InsuranceRequests", "Request_Insurance" + data.Authentication.Session, "Request", Newtonsoft.Json.JsonConvert.SerializeObject(xml));
                Rsp = client.GetResponse();


                StreamReader sr = new StreamReader(Rsp.GetResponseStream());
                string result = sr.ReadToEnd();
                sr.Close();
                var y = timer1.Elapsed.ToString();
                LoggingHelper.WriteToFile("TsServise/Response/", "availability", "TSServiceREs", result);

                XmlSerializer serializer = new XmlSerializer(typeof(PreBookResponse));
                StringReader rdr = new StringReader(result);
                PreBookResponse Result1 = (PreBookResponse)serializer.Deserialize(rdr);

                bKR = Result1;
                //  var fileData2 = System.Configuration.ConfigurationManager.AppSettings["ui"];

                //  Helper.Logger.WriteToFile(fileData2 + "/Logs/InsuranceResponse", "Response_Insurance" + data.Authentication.Session, "Response", Newtonsoft.Json.JsonConvert.SerializeObject(InsuranceResult1));

                //  bKR.Res = null;
                if (bKR.ReturnStatus.Success == "true")
                    return bKR ;

                return null;

            }
            catch (Exception ex)
            {
                //   var fileData3 = System.Configuration.ConfigurationManager.AppSettings["ui"];

                //  Helper.Logger.WriteExceptionToFile(fileData3 + "/Logs/InsuranceResponse", "Response_Insurance" + data.Authentication.Session, "Response", Newtonsoft.Json.JsonConvert.SerializeObject(ex));

                return null;
            }

        }
    }
}
