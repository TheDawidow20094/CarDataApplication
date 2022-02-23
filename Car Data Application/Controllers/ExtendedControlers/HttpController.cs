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

            catch (WebException) { source = "BadRequest"; }
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
            string ResponseFromServer = string.Empty;

            try
            {
                DataStream = request.GetRequestStream();
                DataStream.Write(ByteArray, 0, ByteArray.Length);
                DataStream.Close();
            }
            catch (WebException) { ResponseFromServer = "BadRequest"; }
            catch (Exception) { }

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

        public string HttpCheckRequest(string request)
        {
            string data = request;

            if (data == "No user found")
            {
                MessageBox.Show("Wrong login or password");
                data = "false";
            }
            if (data == "BadRequest")
            {
                MessageBox.Show("Request Error, Check your network connection");
                data = "false";
            }
            if (data == "" || data == "No user found" || data == "No data!")
            {
                MessageBox.Show("Unexpected Error");
                data = "false";
            }
            if (data == "False")
            {
                MessageBox.Show("Your request failed, please try again");
                data = "false";
            }
            if (data == "Wrong Old Password!")
            {
                MessageBox.Show("Your typed wrong password");
                data = "false";
            }

            return data;
        }

        //string url = "https://localhost:7074/api/adduser?dbpassword=" + PDbPassword;
        //var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
        //var result = await response.Content.ReadAsStringAsync();
    }
}
