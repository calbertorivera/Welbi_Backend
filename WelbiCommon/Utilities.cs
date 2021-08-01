using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WelbiCommon
{
    public class Utilities
    {
       
        public static String CallRestService(String url, String rawBody = "", String method = "POST")
        {
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = method;
                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";

                var data = rawBody;

                if (!String.IsNullOrEmpty(rawBody))
                {
                    using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                    {
                        streamWriter.Write(data);
                    }
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception exc)
            {
                
                throw new Exception("There was an error calling the web service");
            
            }


        
            
        }
    }
}
