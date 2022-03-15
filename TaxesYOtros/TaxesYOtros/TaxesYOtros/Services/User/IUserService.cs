using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxesYOtros.Models;
using TaxesYOtros.Models.Responses;

namespace TaxesYOtros.Services.User
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(string username, string password, string deviceId);
        //Task<UserData> RegistrationAsync(string username, string password);
        //Task<UserData> UpdateUser(UserData currentUser);
        //Task<BasicResponse> SavePushNotificationTokenAsync(UserData response, string fCMToken);
        //Task<BasicResponse> ForgotPassword(string value, string step, string resetKey, string password);
    }
}
