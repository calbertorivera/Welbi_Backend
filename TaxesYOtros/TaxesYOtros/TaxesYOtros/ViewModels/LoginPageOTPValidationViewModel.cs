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
    internal class LoginPageOTPValidationViewModel : BaseViewModel
    {
        #region Constructor
        public LoginPageOTPValidationViewModel()
          : base("LoginPageOTPValidationViewModel")
        {
            base.ExecuteMethod("LoginViewModel", delegate ()
            {
                this.userService = DependencyService.Get<IUserService>();
                //this._email = new ValidatableObject<string>();
                this.code = new ValidatableObject<string>();
                AddValidations();

            
                //Password.Value = "Ju4ns31989";

            });
            LoginCommand = new Command(OnLoginClicked);
            ValidateCodeOnClickCommand = new Command(ValidateCodeClicked);
        }
        #endregion
        #region Private properties  
        private ValidatableObject<string> code;
        private string codeError;
        private IUserService userService;
        IDevice device;
        private string loginError;
        #endregion

        #region Public properties

        public ValidatableObject<string> Code
        {
            get => code;
            set => SetProperty(ref code, value);
        }
        public string LoginError
        {
            get => loginError;
            set => SetProperty(ref loginError, value);
        }

        public string CodeError
        {
            get => codeError;
            set => SetProperty(ref codeError, value);
        }
        #endregion


        #region Commands
        public ICommand ValidateCodeCommand => new Command(() =>
        {
            CodeError = !code.HasValidData() ? code.Errors.FirstOrDefault() : "";
        });
        public Command LoginCommand { get; }
        public Command ValidateCodeOnClickCommand { get; }

        #endregion

        #region Protected methods
        protected void AddValidations()
        {
            code.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "El código es requerido"
            });

        }
        private bool Validate()
        {

            bool isValidCode = ValidateCode();
            if (!isValidCode)
            {
                CodeError = !code.HasValidData() ? code.Errors.FirstOrDefault() : "";

            }

            return isValidCode;

        }

        private bool ValidateCode()
        {
            return code.HasValidData();
        }
        private async void ValidateCodeClicked(object obj)
        {
            await base.ExecuteMethodAsync("OnLoginClicked", async delegate ()
            {
                if (Validate())
                {
                    device = DependencyService.Get<IDevice>();
                    string Identifier = device.GetIdentifier();
                    var Email = Xamarin.Essentials.SecureStorage.GetAsync("emailOTP").Result;

                    LoginResponse response = await userService.ConfirmOTPAsync(Email, device, code.Value);

                    if (response.message == "OK")
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "1");
                        Application.Current.MainPage = new AppShell();
                        await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                    }
                    else if (response.message == "CODIGO_NO_VALIDO")
                    { 
                        LoginError = "El codigo ha expirado, por favor intente iniciar sesión de nuevo.";

                    }
                    else
                    {
                        LoginError = "Hubo un error, por favor inténtelo más tarde";
                    }
                }
            });
        }

        private async void OnLoginClicked(object obj)
        {
            App.Current.MainPage = new LoginPage();

        }
        #endregion
    }
}