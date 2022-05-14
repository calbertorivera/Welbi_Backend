using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxesYOtros.Classes;
using TaxesYOtros.Models;
using TaxesYOtros.Models.Responses;

namespace TaxesYOtros.Services.User
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(string username, string password, string deviceId);
        Task<LoginResponse> ConfirmOTPAsync(string email, string device, string value);
        Task<ServiceStatusResponse> ForgotPasswordAsync(string value);
        Task<ServiceStatusResponse> ResetPasswordAsync(string username, string password, string token);
        Task<RegistrationResponse> RegisterAsync(string value1, string value2, string value3, string value4, string value5, string value6, string value7);
        Task<JObject> GetUser( string token, string session_id);
    }
}
