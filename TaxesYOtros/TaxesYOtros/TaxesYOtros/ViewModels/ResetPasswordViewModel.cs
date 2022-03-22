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
    internal class ResetPasswordViewModel : BaseViewModel
    {
        #region Constructor
        public ResetPasswordViewModel()
          : base("ResetPasswordViewModel")
        {
            base.ExecuteMethod("ResetPasswordViewModel", delegate ()
            {
                this.userService = DependencyService.Get<IUserService>();
                this.newPassword = new ValidatableObject<string>();
                this.confirmPassword = new ValidatableObject<string>();
                this.otp = new ValidatableObject<string>();
                AddValidations();

            });
            LoginCommand = new Command(OnLoginClicked);
            ResetPasswordCommand = new Command(OnResetPasswordClicked);
        }


        #endregion

        #region Private properties  
        private ValidatableObject<string> newPassword;
        private ValidatableObject<string> confirmPassword;
        private ValidatableObject<string> otp;

        private string passwordError;
        private string confirmPasswordError;
        private string otpError;

        private IUserService userService;
        IDevice device;
        #endregion

        #region Public properties
        public ValidatableObject<string> Otp
        {
            get => otp;
            set => SetProperty(ref otp, value);
        }

        public ValidatableObject<string> NewPassword
        {
            get => newPassword;
            set => SetProperty(ref newPassword, value);
        }

        public ValidatableObject<string> ConfirmPassword
        {
            get => confirmPassword;
            set => SetProperty(ref confirmPassword, value);
        }

        public string OtpError
        {
            get => otpError;
            set => SetProperty(ref otpError, value);
        }
        public string PasswordError
        {
            get => passwordError;
            set => SetProperty(ref passwordError, value);
        }

        public string ConfirmPasswordError
        {
            get => confirmPasswordError;
            set => SetProperty(ref confirmPasswordError, value);
        }

        #endregion

        #region Commands
        public Command LoginCommand { get; }
        public Command ResetPasswordCommand { get; }
        public ICommand ValidatePasswordCommand => new Command(() =>
            {
                PasswordError = !newPassword.HasValidData() ? newPassword.Errors.FirstOrDefault() : "";
            });

        public ICommand ValidateConfirmPasswordCommand => new Command(() =>
        {
            ConfirmPasswordError = !confirmPassword.HasValidData() ? confirmPassword.Errors.FirstOrDefault() : "";

            if (newPassword.HasValidData() && confirmPassword.HasValidData() && newPassword.Value != confirmPassword.Value)
            {
                ConfirmPasswordError = "Las contraseñas no coinciden";

            }
        });

        public ICommand ValidateOtpCommand => new Command(() =>
        {
            OtpError = !otp.HasValidData() ? otp.Errors.FirstOrDefault() : "";
        });

        public Command SendRecoveryEmailCommand { get; }
        #endregion

        #region Protected methods
        protected void AddValidations()
        {
            newPassword.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "La contraseña es requerida."
            });
            newPassword.Validations.Add(new IsValidLengthRule<string>
            {
                MinimumLength=6,
                ValidationMessage = "La longitud minima es 6 letras"
            });

            confirmPassword.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "La confirmación de contraseña es requerida."
            });
            otp.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "El código es requerido. (Por favor revise su bandeja de correo electronico, incluyendo bandeja de correos no deseados o spam)."
            });
        }

        private async void OnResetPasswordClicked(object obj)
        {
            await base.ExecuteMethodAsync("OnLoginClicked", async delegate ()
            {
                if (Validate())
                {

                    device = DependencyService.Get<IDevice>();
                    string Identifier = device.GetIdentifier();

                    var Email = Xamarin.Essentials.SecureStorage.GetAsync("emailOTP").Result;

                    ServiceStatusResponse response = await userService.ResetPasswordAsync(Email, newPassword.Value, otp.Value);

                    if (response.message == "OK")
                    {
                        await App.Current.MainPage.DisplayAlert("Taxes y Otros", "Su contraseña ha sido actualizada, por favor inicie sesión nuevamente", "Ok");
                        App.Current.MainPage = new LoginPage();
                    }
                    else if (response.message == "OTP_CODIGO_INCORRECTO")
                    {
                        ConfirmPasswordError = "El código no es correcto";
                    }
                    else
                    {
                        ConfirmPasswordError = "Hubo un error, por favor inténtelo más tarde";

                    }
                }
            });

        }

        private bool Validate()
        {
            try
            {

                bool isOtpValid = otp.HasValidData();
                if (!isOtpValid)
                {
                    OtpError = !otp.HasValidData() ? otp.Errors.FirstOrDefault() : "";
                }

                bool isValidPassword = newPassword.HasValidData();
                if (!isValidPassword)
                {
                    PasswordError = !newPassword.HasValidData() ? newPassword.Errors.FirstOrDefault() : "";

                }

                bool isValidConfirmPassword = confirmPassword.HasValidData();
                if (!isValidConfirmPassword)
                {
                    ConfirmPasswordError = !confirmPassword.HasValidData() ? confirmPassword.Errors.FirstOrDefault() : "";
                }

                bool passwordMatch = true;
                if (newPassword.Value != confirmPassword.Value)
                {
                    ConfirmPasswordError = "Las contraseñas no coinciden";
                    passwordMatch = false;
                }

                return isOtpValid && isValidPassword && isValidPassword && passwordMatch;
            }
            catch (Exception exc)
            {
                return false;
            }
            

        }


        private async void OnLoginClicked(object obj)
        {
            App.Current.MainPage = new LoginPage();

        }

        #endregion
    }
}
