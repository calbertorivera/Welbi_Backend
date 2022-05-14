using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaxesYOtros.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri, string token = "");

        Task<TResult> PostAsync<TResult>(string uri, string jsonData, string token = "", string header = "");

        Task<TResult> PutAsync<TResult>(string uri, string jsonData, string token = "", string header = "");

        Task DeleteAsync(string uri, string token = "");
        Task<string> GetJsonAsync(String url, FormUrlEncodedContent formContent = null, String Method = "GET");
        Task<T> ExecuteAsync<T>(RestRequest request) where T : class;

        Task<string> ExecuteAsyncUploadFile(RestRequest request);

    }
}
