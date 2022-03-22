using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxesYOtros.Classes;
using TaxesYOtros.Models;
using TaxesYOtros.Models.Responses;
using TaxesYOtros.Services.RequestProvider;
using Xamarin.Forms;

namespace TaxesYOtros.Services.User
{
    public class UserService : IUserService
    {
        public async Task<LoginResponse> ConfirmOTPAsync(string email, IDevice device, string value)
        {
            var request = new RestRequest(GlobalSetting.Instance.ConfirmOTPEndpoint, Method.Post);
            request.AddParameter("username", email, ParameterType.GetOrPost);
            request.AddParameter("trustedDevice", device, ParameterType.GetOrPost);
            request.AddParameter("codeDoubleFA", value, ParameterType.GetOrPost);

            var response = await DependencyService.Get<IRequestProvider>().ExecuteAsync<LoginResponse>(request);

            return response;
        }

        public async Task<ServiceStatusResponse> ForgotPasswordAsync(string username)
        {
            var request = new RestRequest(GlobalSetting.Instance.ForgotPasswordEndpoint, Method.Post);
            request.AddParameter("username", username, ParameterType.GetOrPost);         

            var response = await DependencyService.Get<IRequestProvider>().ExecuteAsync<ServiceStatusResponse>(request);

            return response;
        }

        public async Task<LoginResponse> LoginAsync(string username, string password, string deviceId)
        {
            var request = new RestRequest(GlobalSetting.Instance.LoginEndpoint,Method.Post);
            request.AddParameter("username", username, ParameterType.GetOrPost);
            request.AddParameter("password", password, ParameterType.GetOrPost);
            request.AddParameter("deviceId", deviceId, ParameterType.GetOrPost);
           
            var response = await DependencyService.Get<IRequestProvider>().ExecuteAsync<LoginResponse>(request);
            
            return response;
        }

        public async Task<RegistrationResponse> RegisterAsync(string email, string firstName, string lastName, string address, string phone, string password, string confirmPassword)
        {
            var request = new RestRequest(GlobalSetting.Instance.RegisterPasswordEndpoint, Method.Post);
            request.AddParameter("email", email, ParameterType.GetOrPost);
            request.AddParameter("new-password", password, ParameterType.GetOrPost);
            request.AddParameter("confirm-password", confirmPassword, ParameterType.GetOrPost);
            request.AddParameter("first_name", firstName, ParameterType.GetOrPost);
            request.AddParameter("last_name", lastName, ParameterType.GetOrPost);
            request.AddParameter("address", address, ParameterType.GetOrPost);
            request.AddParameter("phone", phone, ParameterType.GetOrPost);


            var response = await DependencyService.Get<IRequestProvider>().ExecuteAsync<RegistrationResponse>(request);

            return response;
        }

        public async Task<ServiceStatusResponse> ResetPasswordAsync(string username, string password, string token)
        {
            var request = new RestRequest(GlobalSetting.Instance.ResetPasswordEndpoint, Method.Post);
            request.AddParameter("username", username, ParameterType.GetOrPost);
            request.AddParameter("password", password, ParameterType.GetOrPost);
            request.AddParameter("otp", token, ParameterType.GetOrPost);

            var response = await DependencyService.Get<IRequestProvider>().ExecuteAsync<ServiceStatusResponse>(request);

            return response;
        }
    }
}
