using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxesYOtros.Models;
using TaxesYOtros.Models.Responses;
using TaxesYOtros.Services.RequestProvider;
using Xamarin.Forms;

namespace TaxesYOtros.Services.User
{
    public class UserService : IUserService
    {
      
        public async Task<LoginResponse> LoginAsync(string username, string password, string deviceId)
        {
            var request = new RestRequest(GlobalSetting.Instance.LoginEndpoint,Method.Post);
            request.AddParameter("username", username, ParameterType.GetOrPost);
            request.AddParameter("password", password, ParameterType.GetOrPost);
            request.AddParameter("deviceId", deviceId, ParameterType.GetOrPost);
           
            var response = await DependencyService.Get<IRequestProvider>().ExecuteAsync<LoginResponse>(request);
            
            return response;
        }
    }
}
