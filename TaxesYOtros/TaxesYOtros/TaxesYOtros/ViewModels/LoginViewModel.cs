using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TaxesYOtros.Classes;
using TaxesYOtros.Views;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace TaxesYOtros.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private ValidatableObject<string> _email;
        private ValidatableObject<string> _password;
        private string _emailError;
        private string _passwordError;
        public ValidatableObject<string> Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        public ValidatableObject<string> Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public string EmailError
        {
            get => _emailError;
            set => SetProperty(ref _emailError, value);
        }

        public string PasswordError
        {
            get => _passwordError;
            set => SetProperty(ref _passwordError, value);
        }

        public ICommand ValidateEmailCommand => new Command(() =>
        {           
            EmailError = !_email.HasValidData()?_email.Errors.FirstOrDefault():"";            
        });

        public ICommand ValidatePasswordCommand => new Command(() =>
        {
            PasswordError = !_password.HasValidData()? _password.Errors.FirstOrDefault():"";
           
        });
        public Command LoginCommand { get; }

        public LoginViewModel()
            : base("LoginViewModel")
        {
            base.ExecuteMethod("LoginViewModel", delegate ()
            {

                this._email = new ValidatableObject<string>();
                this._password = new ValidatableObject<string>();
                AddValidations();

                EmailError = "Fuck OFF!";
            });

            LoginCommand = new Command(OnLoginClicked);

        }

        private void AddValidations()
        {
            _email.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "El correo electronico es requerido"
            });
            _email.Validations.Add(new EmailRule<string>
            {
                ValidationMessage = "El correo electronico no es valido"
            });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "La contraseña es requerida"
            });
        }


        private async void OnLoginClicked(object obj)
        {
            await base.ExecuteMethodAsync("OnLoginClicked", async delegate ()
            {
                if (Validate())
                {
                    await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "1");
                    Application.Current.MainPage = new AppShell();
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
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
            return _email.HasValidData();
        }

        private bool ValidatePassword()
        {
            return _password.HasValidData();
        }

    }
}
