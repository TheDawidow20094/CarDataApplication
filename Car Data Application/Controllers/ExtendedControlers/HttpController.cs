using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Car_Data_Application.Controllers.ExtendedControlers
{
    class HttpController
    {
        public string PDbPassword = "dUmv9Fq/8D6y9Rwh";

        public static string HttpGet(string url)
        {
            string source = string.Empty;

            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";
                req.KeepAlive = true;

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                Stream dataStream = response.GetResponseStream();

                if (dataStream != null)
                {
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        source = reader.ReadToEnd();
                    }
                }
            }

            catch (Exception) { }

            return source;
        }

        public static string HttpPost(string url, string data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.KeepAlive = true;

            string PostData = data;
            byte[] ByteArray = Encoding.UTF8.GetBytes(PostData);
            request.ContentType = "application/json";
            request.ContentLength = ByteArray.Length;  

            Stream DataStream = null;

            try
            {
                DataStream = request.GetRequestStream();
                DataStream.Write(ByteArray, 0, ByteArray.Length);
                DataStream.Close();
            }
            catch (Exception) { }

            string ResponseFromServer = string.Empty;
            StreamReader Reader = null;
            HttpWebResponse Response = null;

            try
            {
                Response = (HttpWebResponse)request.GetResponse();
                DataStream = Response.GetResponseStream();

                if (DataStream != null)
                {
                    Reader = new StreamReader(DataStream);
                    ResponseFromServer = Reader.ReadToEnd();
                }
            }
            catch (Exception) { }

            return ResponseFromServer;
        }

        //string url = "https://localhost:7074/api/adduser?dbpassword=" + PDbPassword;
        //var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
        //var result = await response.Content.ReadAsStringAsync();
    }
}
