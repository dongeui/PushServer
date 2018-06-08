using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ADTPush
{
    public class SendMessage
    {
        public bool Send(string deviceList, string message)
        {
            string SERVER_API_KEY = "AAAA3EnShoY:APA91bEvDbFQJVKzsZQ1Q4LTnCiJ6juOZRH66mrB3wwk6vPUyNDA4IauSeTsiLWBGv_ZcgscVKIX8ckW6OZRcdwXiGPndDVi5TUUuiwarwU9MvrXCkV6kEB_yYSJfkKkZbZwa3JPbPV9";

            bool sendResult = true;
            string senderId = "946131338886";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            request.Method = "POST";
            request.ContentType = "application/json;charset=utf-8;";
            request.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));
            request.Headers.Add(string.Format("Sender: Id={0}", senderId));
            request.KeepAlive = false;

            string deviceId = "cA64WBuL2SI:APA91bESyd2JFtmLOxLzm7ORCGbr1MRszZHAE9JAoO6C1KYSkI6DXGGIkSX4hEf42L0k_IGxSYCs6Gy7GmspyvS1LkUDycRcx9mo64D5xJiLLQdfGJQwoQ1drKmvzAzZNFttME7vZrAY";

            //IOS
            var IOSData = new
            {
                notification = new
                {
                    title = "ADT",
                    body = message,
                },
                content_available = true,
                priority = "high",
                to = deviceId
            };

            //ANDROID
            var AndData = new
            {
                data = new
                {
                    title = "ADT",
                    body = message,
                },
                content_available = true,
                priority = "high",
                to = deviceId
            };

            string contentMsg = JsonConvert.SerializeObject(IOSData);

            Byte[] byteArray = Encoding.UTF8.GetBytes(contentMsg);
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            //result 를 sendReuslt에 넣어주기
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            //try
            //{
            //    WebResponse response = request.GetResponse();
            //    Stream responseStream = response.GetResponseStream();
            //    StreamReader reader = new StreamReader(responseStream);
            //    resultStr = reader.ReadToEnd();
            //    reader.Close();
            //    responseStream.Close();
            //    response.Close();

            //    //여기서 디비에 결과값과 함께 메세지 타입, 보낸시간 저장
            //}

            //catch (Exception e)
            //{
            //    resultStr = "";
            //}

            return sendResult;
        }
    }
}
