using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TaxesYOtros.Classes;
using TaxesYOtros.CustomClasses;
using TaxesYOtros.Views;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using TaxesYOtros.Services.User;
using TaxesYOtros.Models;
using TaxesYOtros.Models.Responses;
using TaxesYOtros.Core;
using Configuration;
using TaxesYOtros.Services.Texts;

namespace TaxesYOtros.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Constructor
        public LoginViewModel()
          : base("LoginViewModel")
        {
            base.ExecuteMethod("LoginViewModel", delegate ()
            {
              
                this.email = new ValidatableObject<string>();
                this.password = new ValidatableObject<string>();
                AddValidations();

                Email.Value = "calbertorivera@hotmail.com";
                Password.Value = "Ju4ns31989";

            });



            LoginCommand = new Command(OnLoginClicked);
            RegisterCommand = new Command(OnRegisterClicked);
        }


        #endregion

        #region Private properties  
        private ValidatableObject<string> email;
        private ValidatableObject<string> password;
        private string emailError;
        private string loginError;
        private string passwordError;
      
   
        IDevice device;
        #endregion

        #region Public properties


        public ValidatableObject<string> Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public ValidatableObject<string> Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public string EmailError
        {
            get => emailError;
            set => SetProperty(ref emailError, value);
        }

        public string LoginError
        {
            get => loginError;
            set => SetProperty(ref loginError, value);
        }


        public string PasswordError
        {
            get => passwordError;
            set => SetProperty(ref passwordError, value);
        }
        #endregion

        #region Commands
        public ICommand ValidateEmailCommand => new Command(() =>
        {
            EmailError = !email.HasValidData() ? email.Errors.FirstOrDefault() : "";
        });

        public ICommand ForgotPasswordCommand => new Command(async () => await ForgotPasswordAsync());

        public ICommand SpanishCommand => new Command(async () =>
        {
            this.IsBusy = true;
            await Xamarin.Essentials.SecureStorage.SetAsync("lan", "ES");          
            String response = await textsService.getAppTexts("ES");
            await Xamarin.Essentials.SecureStorage.SetAsync("ES_TEXTS", response);
            App.Current.MainPage = new LoginPage();
            this.IsBusy = false;
        });

        public ICommand EnglishCommand => new Command(async () =>
        {
            this.IsBusy = true;
            await Xamarin.Essentials.SecureStorage.SetAsync("lan", "EN");
            this.textsService = DependencyService.Get<ITextService>();
            String response = await textsService.getAppTexts("EN");
            await Xamarin.Essentials.SecureStorage.SetAsync("EN_TEXTS", response);
            App.Current.MainPage = new LoginPage();
            this.IsBusy = false;
                        
        });

        public ICommand ValidatePasswordCommand => new Command(() =>
        {
            PasswordError = !password.HasValidData() ? password.Errors.FirstOrDefault() : "";

        });
        public Command LoginCommand { get; }
        public ICommand TapCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        #endregion

        #region Protected methods

        protected async Task ForgotPasswordAsync()
        {
            App.Current.MainPage = new ForgotPassword();
        }


        protected void AddValidations()
        {
            email.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Text_Email_Required
            });
            email.Validations.Add(new EmailRule<string>
            {
                ValidationMessage = Text_Email_Not_Valid
            });
            password.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Text_Password_Required
            });
        }

        private async void OnRegisterClicked()
        {
            App.Current.MainPage = new Register();
        }

        private async void OnLoginClicked(object obj)
        {
            this.IsBusy = true;
            await base.ExecuteMethodAsync("OnLoginClicked", async delegate ()
            {
              
                if (Validate())
                {
                   
                    device = DependencyService.Get<IDevice>();
                    string Identifier = device.GetIdentifier();
                    LoginResponse response = await userService.LoginAsync(email.Value, password.Value, Identifier);

                    if (response.message == "OK")
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("LoggedInEmail", Email.Value);
                        await Xamarin.Essentials.SecureStorage.SetAsync("token", response.token);
                        await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "1");
                        await Xamarin.Essentials.SecureStorage.SetAsync("user_id", response.user_id);
                        await LoadUserData(true);

                        Application.Current.MainPage = new AppShell();
                        await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                    }
                    else if (response.message == "OTP_SENT")
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("emailOTP", Email.Value);

                        App.Current.MainPage = new LoginPageOTPValidation();
                    }
                    else if (response.message == "CREDENCIALES_INCORRECTAS")
                    {
                        LoginError = Text_Credenciales_Incorrectas;

                    }
                    else
                    {
                        LoginError = Text_General_Error;

                    }
                }
              
            });

            this.IsBusy = false;

        }

        private bool Validate()
        {

            bool isValidUser = ValidateUserName();
            bool isValidPassword = ValidatePassword();

            return isValidUser && isValidPassword;

        }

        private bool ValidateUserName()
        {
            return email.HasValidData();
        }

        private bool ValidatePassword()
        {
            return password.HasValidData();
        }
        #endregion

        #region Screen text
      

        public string Text_Credenciales_Incorrectas
        {
            get
            {
                return GetLocalizedText(LanguageToken.LOGIN11, "El email no es correcto, por favor revise.");
            }
        }
        public string Text_Title
        {
            get
            {
                return GetLocalizedText(LanguageToken.LOGIN1, "Ingrese con su correo electrónico");
            }
        }
        public string Text_PlaceHolder_Email
        {
            get
            {
                return GetLocalizedText(LanguageToken.LOGIN2, "Ingrese aquí su correo");
            }
        }
        public string Text_PlaceHolder_Password
        {
            get
            {
                return GetLocalizedText(LanguageToken.LOGIN3, "Ingrese aquí su contraseña");
            }
        }
      
        public string Text_Password_Required
        {
            get
            {
                return GetLocalizedText(LanguageToken.LOGIN5, "La contraseña es requerida");
            }
        }
        public string Text_Login_Button
        {
            get
            {
                return GetLocalizedText(LanguageToken.LOGIN6, "Ingresar");
            }
        }
        public string Text_ForgotPassword
        {
            get
            {
                return GetLocalizedText(LanguageToken.LOGIN7, "Olvidó su contraseña, haga click aquí");
            }
        }
        public string Text_SignUp
        {
            get
            {
                return GetLocalizedText(LanguageToken.LOGIN8, "Desea Registrarse? Registrese auí");
            }
        }

        public string TextCambiarIdioma
        {
            get
            {
                return GetLocalizedText(LanguageToken.LOGIN9, "Change Language?");
            }
        }


        #endregion
    }
}
