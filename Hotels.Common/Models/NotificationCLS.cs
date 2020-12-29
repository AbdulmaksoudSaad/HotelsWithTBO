 
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

public class NotificationData
{
    public string BookingNumber { get; set; }
    public string Language { get; set; }
    public string NotificationToken { get; set; }
    public string ConfirmationStatus { get; set; }
    public string Message { get; set; }
    public string Title { get; set; }
    public string PDFURL { get; set; }
    public string Type { get; set; } = "hotel";
    public string SId { get; set; }
    
}

namespace B2C.Hotel.Common.AdminPanleServices.BookingFlowServices
{
    public class NotificationHelper
    {
        public class NontificationSetting
        {
            public string ServerKey { get; set; }
            public string SenderId { get; set; }
        }
        public class RootObject
        {
            public NontificationSetting NontificationSetting { get; set; }
        }
        public static NontificationSetting GetNontificationSetting()
        {
            NontificationSetting NontificationSetting = new NontificationSetting();
                var serverID = ConfigurationSettings.AppSettings["MobServerID"];
            var serverKey = ConfigurationSettings.AppSettings["MobServerKey"];


            NontificationSetting.SenderId = serverID;
            NontificationSetting.ServerKey = serverKey;


            return NontificationSetting;
        }
        public static void SendPushNotification(NotificationData notificationData)
        {
            NontificationSetting NontificationSetting = GetNontificationSetting();
            string serverKey = NontificationSetting.ServerKey;/*"AAAAkz00EBc:APA91bGCQCoykat89T4nRrz9F0Hfek8k9aHPm64CRErPOt_Xp6qxNRA8td1kse4CtNRscS604qXFczg_tCqglc-YTTEN-fnLgETdM0RB050tAqHHlNhwBINrp_VbiLJMmSz9V1ax2BTL"; // Something very long*/
            string senderId = NontificationSetting.SenderId; /*"632387014679";*/
            string deviceId = notificationData.NotificationToken;

            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");

            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            var jsondata = new
            {
                to = deviceId,
                //{ "PDFURL", notificationData.PDFURL },
                data = new Dictionary<string, string>() { { "Language", notificationData.Language }, { "SId", notificationData.SId }, { "Title", notificationData.Title }, { "Message", notificationData.Message }, { "Type", notificationData.Type }, { "BN", notificationData.BookingNumber }, { "ConfirmationStatus", notificationData.ConfirmationStatus } },
                notification = new
                {
                    body = notificationData.Message,
                    title = notificationData.Title,
                    sound = "Enabled",
                },
            };

            if (string.IsNullOrEmpty(notificationData.PDFURL))
            {
                notificationData.PDFURL = "";
            }
            jsondata.data.Add("PDFURL", notificationData.PDFURL);

            var json = JsonConvert.SerializeObject(jsondata);
            Byte[] byteArray = Encoding.UTF8.GetBytes(json);
            tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
            tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            tRequest.ContentLength = byteArray.Length;

            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String sResponseFromServer = tReader.ReadToEnd();
                            var response = sResponseFromServer;
                        }
                    }
                }
            }
        }

    }
}
