using Configuration;
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
        private bool addTrustedDevice;
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

        public bool AddTrustedDevice
        {
            get => addTrustedDevice;
            set => SetProperty(ref addTrustedDevice, value);
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
                ValidationMessage = Text_CodeisRequired
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
                    if (!AddTrustedDevice)
                    {
                        Identifier = "";
                    }
                    LoginResponse response = await userService.ConfirmOTPAsync(Email, Identifier, code.Value);

                    if (response.message == "OK")
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "1");
                        Application.Current.MainPage = new AppShell();
                        await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                    }
                    else if (response.message == "CODIGO_NO_VALIDO")
                    { 
                        LoginError = Text_CodeHasExpired;

                    }
                    else
                    {
                        LoginError = Text_General_Error;
                    }
                }
            });
        }

        private async void OnLoginClicked(object obj)
        {
            App.Current.MainPage = new LoginPage();

        }
        #endregion

        #region Screen text
        public string Text_Title
        {
            get
            {
                return GetLocalizedText(LanguageToken.OTP1, "Hemos enviado un código a su correo electrónico y celular. Por favor, ingréselo a continuación para acceder a su información:");
            }
        }

        public string Text_Code_PlaceHolder
        {
            get
            {
                return GetLocalizedText(LanguageToken.OTP2, "Ingrese aquí el código");
            }
        }

        public string Text_SaveDevice_asTrusted
        {
            get
            {
                return GetLocalizedText(LanguageToken.OTP3, "Guardar este dispositivo como confiable");
            }
        }

        public string Text_Login
        {
            get
            {
                return GetLocalizedText(LanguageToken.OTP4, "Ingresar");
            }
        }

     

        public string Text_CodeHasExpired
        {
            get
            {
                return GetLocalizedText(LanguageToken.OTP6, "El codigo ha expirado, por favor intente iniciar sesión de nuevo.");
            }
        }

        public string Text_CodeisRequired
        {
            get
            {
                return GetLocalizedText(LanguageToken.OTP7, "El código es requerido");
            }
        }

    
       
        #endregion
    }
}