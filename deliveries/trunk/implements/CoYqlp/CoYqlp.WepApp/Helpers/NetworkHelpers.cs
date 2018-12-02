using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace CoYqlp.WepApp.Helpers
{
    public class NetworkHelpers
    {
        /// <summary>
        /// Gui request dang post
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="RequestString"></param>
        /// <returns></returns>
        public static string SendPOSTRequest(string Url, string RequestString)
        {
            try
            {
                UTF8Encoding encoding = new UTF8Encoding();
                string strResult = string.Empty;
                byte[] data = encoding.GetBytes(RequestString);
                // declare httpwebrequet wrt url defined above
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(Url);
                // set method as post
                webrequest.Method = "POST";
                webrequest.Timeout = 9999999;
                // set content type
                webrequest.ContentType = "application/x-www-form-urlencoded";
                // set content length
                webrequest.ContentLength = data.Length;
                // get stream data out of webrequest object
                Stream newStream = webrequest.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                // declare & read response from service
                HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();

                // set utf8 encoding
                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                // read response stream from response object
                StreamReader loResponseStream =
                 new StreamReader(webresponse.GetResponseStream(), enc);
                // read string from stream data
                strResult = loResponseStream.ReadToEnd();
                // close the stream object
                loResponseStream.Close();
                // close the response object
                webresponse.Close();
                return strResult;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Gui request dang get
        /// </summary>
        /// <param name="RequestUrl"></param>
        /// <returns></returns>
        public static string SendGetRequest(string RequestUrl)
        {
            Uri address = new Uri(RequestUrl);
            HttpWebRequest request;
            HttpWebResponse response = null;
            StreamReader reader;
            if (address == null) { throw new ArgumentNullException("address"); }
            try
            {
                // Create and initialize the web request  
                request = WebRequest.Create(address) as HttpWebRequest;
                request.UserAgent = ".NET Sample";
                request.KeepAlive = false;
                // Set timeout to 15 seconds  
                request.Timeout = 15 * 1000;
                // Get response  
                response = request.GetResponse() as HttpWebResponse;
                if (request.HaveResponse == true && response != null)
                {
                    // Get the response stream  
                    reader = new StreamReader(response.GetResponseStream());
                    // Read it into a StringBuilder  
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
            catch (WebException wex)
            {
                // This exception will be raised if the server didn't return 200 - OK  
                // Try to retrieve more information about the network error  
                if (wex.Response != null)
                {
                    using (HttpWebResponse errorResponse = (HttpWebResponse)wex.Response)
                    {
                        Console.WriteLine(
                         "The server returned '{0}' with the status code {1} ({2:d}).",
                         errorResponse.StatusDescription, errorResponse.StatusCode,
                         errorResponse.StatusCode);
                    }
                }
            }
            finally
            {
                if (response != null) { response.Close(); }
            }
            return null;

        }

        /// <summary>
        /// Lay dia chi ip client
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddressClient()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            //string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            string ipAddress = context.Request.UserHostAddress;

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        /// <summary>
        /// Lay domain server hien tai
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHostName(string url)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
            {
                return $"{HttpContext.Current.Request.Url.Scheme}://{HttpContext.Current.Request.Url.Authority}";
            }
            else
            {
                Uri request = new Uri(url);
                return $"{request.Scheme}://{request.Authority}";
            }
        }
    }
}