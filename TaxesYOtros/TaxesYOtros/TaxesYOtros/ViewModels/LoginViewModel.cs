using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TaxesYOtros.Classes;
using TaxesYOtros.Views;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using TaxesYOtros.Services.User;
using TaxesYOtros.Models;
using TaxesYOtros.Models.Responses;

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
                this.userService = DependencyService.Get<IUserService>();
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
        private IUserService userService;
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
                ValidationMessage = "El correo electronico es requerido"
            });
            email.Validations.Add(new EmailRule<string>
            {
                ValidationMessage = "El correo electronico no es valido"
            });
            password.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "La contraseña es requerida"
            });
        }

        private async void OnRegisterClicked()
        {
            App.Current.MainPage = new Register();
        }

        private async void OnLoginClicked(object obj)
        {
            await base.ExecuteMethodAsync("OnLoginClicked", async delegate ()
            {
                if (Validate())
                {

                    device = DependencyService.Get<IDevice>();
                    string Identifier = device.GetIdentifier();
                    LoginResponse response = await userService.LoginAsync(email.Value, password.Value, Identifier);

                    if (response.message == "OK")
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "1");
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
                        LoginError = "El email no es correcto, por favor revise.";

                    }
                    else
                    {
                        LoginError = "Hubo un error, por favor inténtelo más tarde";

                    }
                }
            });

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

    }
}
