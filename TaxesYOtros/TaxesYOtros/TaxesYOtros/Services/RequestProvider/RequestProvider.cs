using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaxesYOtros.Core;
using TaxesYOtros.Exceptions;
using TaxesYOtros.Models.Responses;

namespace TaxesYOtros.Services.RequestProvider
{
    public class RequestProvider : IRequestProvider
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public RequestProvider()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
        {
            HttpClient httpClient = CreateHttpClient(token);
            HttpResponseMessage response;
            response = await httpClient.GetAsync(uri);
            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return result;
        }

        public async Task<TResult> PostAsync<TResult>(string uri, string jsonData, string token = "", string header = "")
        {
            HttpClient httpClient = CreateHttpClient(token);

            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(httpClient, header);
            }

            var content = new StringContent(jsonData); //JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return result;
        }


        public async Task<TResult> PutAsync<TResult>(string uri, string jsonData, string token = "", string header = "")
        {
            HttpClient httpClient = CreateHttpClient(token);

            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(httpClient, header);
            }

            var content = new StringContent(jsonData);
            //var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PutAsync(uri, content);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return result;
        }



        public async Task DeleteAsync(string uri, string token = "")
        {
            HttpClient httpClient = CreateHttpClient(token);
            await httpClient.DeleteAsync(uri);
        }

        private HttpClient CreateHttpClient(string token = "")
        {
            HttpClient httpClient;
            httpClient = new HttpClient();

            httpClient.Timeout = new TimeSpan(0, 0, 0, GlobalSetting.Instance.AsyncTimeoutMillisecond);
            httpClient.BaseAddress = new Uri(GlobalSetting.Instance.BaseEndpoint);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return httpClient;
        }

        private void AddHeaderParameter(HttpClient httpClient, string parameter)
        {
            if (httpClient == null)
                return;

            if (string.IsNullOrEmpty(parameter))
                return;

            httpClient.DefaultRequestHeaders.Add(parameter, Guid.NewGuid().ToString());
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(content);
                }

                throw new HttpRequestExceptionEx(response.StatusCode, content);
            }
        }

        public async Task<T> ExecuteAsync<T>(RestRequest request)
            where T : class
        {
            var cancellationTokenSource = new CancellationTokenSource();
            Console.WriteLine($"*** SERVICE ACTION => {request.Resource}");
                                          
            var response = await App.ServiceClient.ExecuteAsync<T>(request, cancellationTokenSource.Token);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (typeof(T) == typeof(string))
                {
                    return (T)Convert.ChangeType(response.Content, typeof(T));
                }

                var serviceData = JsonConvert.DeserializeObject<T>(response.Content);
                               
                return serviceData;

            }
            return null;
        }

        public async Task<string> ExecuteAsyncUploadFile(RestRequest request)
        {

            return null;
        }


        public async Task<string> GetJsonAsync(String url, FormUrlEncodedContent formC = null, String Method = "GET")
        {
            var cancellationTokenSource = new CancellationTokenSource();
            HttpClient myClient = new HttpClient();

            if (Method=="GET")
            {
                var response = await myClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
                return null;
            }
            else if (Method == "POST")
            {
                var response = await myClient.PostAsync(url,formC);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
                return null;
            }
            return null;
            
        }

    }
}
