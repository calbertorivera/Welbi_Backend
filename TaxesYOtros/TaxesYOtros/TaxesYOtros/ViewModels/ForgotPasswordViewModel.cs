using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TaxesYOtros.Classes;
using TaxesYOtros.Models.Responses;
using TaxesYOtros.Services.User;
using TaxesYOtros.Views;
using Xamarin.Forms;

namespace TaxesYOtros.ViewModels
{
    internal class ForgotPasswordViewModel : BaseViewModel
    {
        #region Constructor
        public ForgotPasswordViewModel()
          : base("ForgotPasswordViewModel")
        {
            base.ExecuteMethod("LoginViewModel", delegate ()
            {
                this.userService = DependencyService.Get<IUserService>();
                this.email = new ValidatableObject<string>();
                AddValidations();
                Email.Value = "calbertorivera@hotmail.com";
            });
            LoginCommand = new Command(OnLoginClicked);
            SendRecoveryEmailCommand = new Command(OnSendRecoveryEmailClicked);
        }
        #endregion
        #region Private properties  
        private ValidatableObject<string> email;
        private string emailError;
        private IUserService userService;
        IDevice device;
        #endregion

        #region Public properties


        public ValidatableObject<string> Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        public string EmailError
        {
            get => emailError;
            set => SetProperty(ref emailError, value);
        }

        #endregion
        #region Commands
        public Command LoginCommand { get; }
        public ICommand ValidateEmailCommand => new Command(() =>
        {
            EmailError = !email.HasValidData() ? email.Errors.FirstOrDefault() : "";
        });

       public Command SendRecoveryEmailCommand { get; }
        #endregion

        #region Protected methods
        protected void AddValidations()
        {
            email.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "El correo electronico es requerido"
            });
            email.Validations.Add(new EmailRule<string>
            {
                ValidationMessage = "El correo electronico no es valido"
            });
          
        }


        private async void OnSendRecoveryEmailClicked(object obj)
        {
            await base.ExecuteMethodAsync("OnLoginClicked", async delegate ()
            {
                if (Validate())
                {

                    device = DependencyService.Get<IDevice>();
                    string Identifier = device.GetIdentifier();
                    ServiceStatusResponse response = await userService.ForgotPasswordAsync(email.Value);

                    if (response.message == "OTP_SENT")
                    {
                        App.Current.MainPage = new ResetPassword();
                    }
                    else if (response.message == "USER_NOT_VALID")
                    {
                        emailError = "EL usuario ingresado no existe en nuestra base de datos";
                    }                    
                    else
                    {
                        emailError = "Hubo un error, por favor inténtelo más tarde";

                    }
                }
            });

        }

        private bool Validate()
        {

            bool isValidUser = ValidateUserName();
            if (!isValidUser)
            {
                EmailError = !email.HasValidData() ? email.Errors.FirstOrDefault() : "";

            }
            return isValidUser;

        }

        private bool ValidateUserName()
        {
            return email.HasValidData();
        }
        private async void OnLoginClicked(object obj)
        {
            App.Current.MainPage = new LoginPage();

        }

        #endregion

    }
}