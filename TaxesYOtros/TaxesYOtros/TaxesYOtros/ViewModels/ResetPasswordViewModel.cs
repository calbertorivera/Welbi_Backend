using Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TaxesYOtros.CustomClasses;
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
                ConfirmPasswordError = TEXT_PASSWORDS_DONT_MATCH;

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
                ValidationMessage = TEXT_PASSWORD_REQUIRED
            });
            newPassword.Validations.Add(new IsValidLengthRule<string>
            {
                MinimumLength = 6,
                ValidationMessage = TEXT_MINIMUM_LENGTH
            });

            confirmPassword.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = TEXT_CONFIRM_PASSWORD_REQUIRED
            });
            otp.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = TEXT_CODE_IS_REQUIRED
            });
        }

        private async void OnResetPasswordClicked(object obj)
        {
            this.IsBusy = true;
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
                        await App.Current.MainPage.DisplayAlert("Taxes y Otros", TEXT_PASSWORD_HAS_BEEN_UPDATED, "Ok");
                        App.Current.MainPage = new LoginPage();
                    }
                    else if (response.message == "OTP_CODIGO_INCORRECTO")
                    {
                        ConfirmPasswordError = TEXT_CODE_IS_NOT_CORRECT;
                    }
                    else
                    {
                        ConfirmPasswordError = Text_General_Error;

                    }
                }
            });
            this.IsBusy = false;

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
                    ConfirmPasswordError = TEXT_PASSWORDS_DONT_MATCH;
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

        #region Screen text




        public string TEXT_RESET_PASSWORD
        {
            get
            {
                return GetLocalizedText(LanguageToken.FORGOT5, "Restablezca su contraseña aquí");
            }
        }
        public string TEXT_ENTER_CODE
        {
            get
            {
                return GetLocalizedText(LanguageToken.FORGOT6, "Ingrese el código que recibió en su correo electrónico");
            }
        }
        public string TEXT_ENTER_NEW_PASSWORD
        {
            get
            {
                return GetLocalizedText(LanguageToken.FORGOT7, "Ingrese su nueva contraseña");
            }
        }
        public string TEXT_CONFIRM_NEW_PASSWORD
        {
            get
            {
                return GetLocalizedText(LanguageToken.FORGOT8, "Confirme su nueva contraseña");
            }
        }
        public string TEXT_RESET_PASSWORD_BUTTON
        {
            get
            {
                return GetLocalizedText(LanguageToken.FORGOT9, "RESTABLECER CONTRASEÑA");
            }
        }
        public string TEXT_PASSWORDS_DONT_MATCH
        {
            get
            {
                return GetLocalizedText(LanguageToken.FORGOT10, "Las contraseñas no coinciden");
            }
        }
        public string TEXT_PASSWORD_REQUIRED
        {
            get
            {
                return GetLocalizedText(LanguageToken.FORGOT11, "La contraseña es requerida.");
            }
        }
        public string TEXT_MINIMUM_LENGTH
        {
            get
            {
                return GetLocalizedText(LanguageToken.FORGOT12, "La longitud minima es 6 letras");
            }
        }
        public string TEXT_CONFIRM_PASSWORD_REQUIRED
        {
            get
            {
                return GetLocalizedText(LanguageToken.FORGOT13, "La confirmación de contraseña es requerida.");
            }
        }
        public string TEXT_CODE_IS_REQUIRED
        {
            get
            {
                return GetLocalizedText(LanguageToken.FORGOT14, "El código es requerido. (Por favor revise su bandeja de correo electronico, incluyendo bandeja de correos no deseados o spam).");
            }
        }
        public string TEXT_PASSWORD_HAS_BEEN_UPDATED
        {
            get
            {
                return GetLocalizedText(LanguageToken.FORGOT15, "Su contraseña ha sido actualizada, por favor inicie sesión nuevamente");
            }
        }
        public string TEXT_CODE_IS_NOT_CORRECT
        {
            get
            {
                return GetLocalizedText(LanguageToken.FORGOT16, "El código no es correcto");
            }
        }

        #endregion

    }
}
