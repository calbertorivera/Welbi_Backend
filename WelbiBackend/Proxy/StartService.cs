using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WelbiBackend.Proxy.RequestEntities;
using WelbiBackend.Proxy.ResponseEntities;
using WelbiCommon;

namespace WelbiBackend.Proxy
{
    public class StartService
    {   

        /// <summary>
        /// this method get the authentication token from the web service
        /// </summary>
        /// <param name="endPoint"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static String getToken(String endPoint, String email)
        {
            try {
                String response = Utilities.CallRestService(endPoint, JsonConvert.SerializeObject(new StartRequest(email)));
                String token = JObject.Parse(response)["data"].ToString();
                return JsonConvert.DeserializeObject<StartResponse>(token).token;
            }
            catch { throw new Exception("There was an exception casting the response"); }

         

        }
    }
}
