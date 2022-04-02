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
using System.Reflection;
using TaxesYOtros.Models.Responses.ResposneDTO;

namespace TaxesYOtros.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Constructor
        public RegisterViewModel()
          : base("RegisterViewModel")
        {
            base.ExecuteMethod("RegisterViewModel", delegate ()
            {
                this.userService = DependencyService.Get<IUserService>();
                this.email = new ValidatableObject<string>();
                this.firstName = new ValidatableObject<string>();
                this.lastName = new ValidatableObject<string>();
                this.address = new ValidatableObject<string>();
                this.phone = new ValidatableObject<string>();
                this.password = new ValidatableObject<string>();
                this.confirmPassword = new ValidatableObject<string>();
                AddValidations();


                //Password.Value = "Ju4ns31989";

            });
            LoginCommand = new Command(OnLoginClicked);
            RegisterOnClickCommand = new Command(RegisterClicked);

        }
        #endregion
        #region Private properties  
        private ValidatableObject<string> email;
        private ValidatableObject<string> firstName;
        private ValidatableObject<string> lastName;
        private ValidatableObject<string> address;
        private ValidatableObject<string> phone;
        private ValidatableObject<string> password;
        private ValidatableObject<string> confirmPassword;

        private string emailError;
        private string firstNameError;
        private string lastNameError;
        private string addressError;
        private string phoneError;
        private string passwordError;
        private string confirmPasswordError;
        private string registerError;

        private IUserService userService;
        IDevice device;
        #endregion

        #region Public properties

        public ValidatableObject<string> Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        public ValidatableObject<string> FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }
        public ValidatableObject<string> LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }
        public ValidatableObject<string> Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }
        public ValidatableObject<string> Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }
        public ValidatableObject<string> Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        public ValidatableObject<string> ConfirmPassword
        {
            get => confirmPassword;
            set => SetProperty(ref confirmPassword, value);
        }

        public string RegisterError
        {
            get => registerError;
            set => SetProperty(ref registerError, value);
        }



        public string EmailError
        {
            get => emailError;
            set => SetProperty(ref emailError, value);
        }

        public string FirstNameError
        {
            get => firstNameError;
            set => SetProperty(ref firstNameError, value);
        }
        public string LastNameError
        {
            get => lastNameError;
            set => SetProperty(ref lastNameError, value);
        }
        public string AddressError
        {
            get => addressError;
            set => SetProperty(ref addressError, value);
        }
        public string PhoneError
        {
            get => phoneError;
            set => SetProperty(ref phoneError, value);
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
        public ICommand ValidateEmailCodeCommand => new Command(() =>
        {
            EmailError = !email.HasValidData() ? email.Errors.FirstOrDefault() : "";
        });
        public ICommand ValidateFirstNameCodeCommand => new Command(() =>
        {
            FirstNameError = !firstName.HasValidData() ? firstName.Errors.FirstOrDefault() : "";
        });
        public ICommand ValidateLastNameCodeCommand => new Command(() =>
        {
            LastNameError = !lastName.HasValidData() ? lastName.Errors.FirstOrDefault() : "";
        });
        public ICommand ValidateAddressCodeCommand => new Command(() =>
        {
            AddressError = !address.HasValidData() ? address.Errors.FirstOrDefault() : "";
        });
        public ICommand ValidatePhoneCodeCommand => new Command(() =>
        {
            PhoneError = !phone.HasValidData() ? phone.Errors.FirstOrDefault() : "";
        });
        public ICommand ValidatePasswordCodeCommand => new Command(() =>
        {
            PasswordError = !password.HasValidData() ? password.Errors.FirstOrDefault() : "";

        });
        public ICommand ValidateConfirmPasswordCodeCommand => new Command(() =>
        {
            ConfirmPasswordError = !confirmPassword.HasValidData() ? confirmPassword.Errors.FirstOrDefault() : "";

            if (password.HasValidData() && confirmPassword.HasValidData() && password.Value != confirmPassword.Value)
            {
                ConfirmPasswordError = "Las contraseñas no coinciden";

            }
        });
        public Command LoginCommand { get; }
        public Command RegisterOnClickCommand { get; }

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

            firstName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "El Primer Nombre es requerido"
            });

            lastName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "El Apellido es requerido"
            });

            address.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "La dirección es requerida"
            });

            phone.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "El teléfono es requerido"
            });

            phone.Validations.Add(new IsValidNumberRule<string>
            {
                ValidationMessage = "El teléfono debe ser solo numeros"
            });

            phone.Validations.Add(new IsValidLengthRule<string>
            {
                MaximumLength = 10,
                ValidationMessage = "El teléfono es requerido y no debe contener mas de 10 numeros"
            });

            password.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "La contraseña es requerida."
            });
            password.Validations.Add(new IsValidLengthRule<string>
            {
                MinimumLength = 6,
                ValidationMessage = "La longitud minima es 6 letras"
            });

            confirmPassword.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "La confirmación de contraseña es requerida."
            });

        }
        private bool Validate()
        {
            bool isValidEmail = email.HasValidData();
            if (!isValidEmail)
            {
                EmailError = !email.HasValidData() ? email.Errors.FirstOrDefault() : "";

            }
            bool isValidFirstName = firstName.HasValidData();
            if (!isValidFirstName)
            {
                FirstNameError = !firstName.HasValidData() ? firstName.Errors.FirstOrDefault() : "";
            }
            bool isValidLastName = lastName.HasValidData();
            if (!isValidLastName)
            {
                LastNameError = !lastName.HasValidData() ? lastName.Errors.FirstOrDefault() : "";

            }
            bool isValidAddress = address.HasValidData();
            if (!isValidAddress)
            {
                AddressError = !address.HasValidData() ? address.Errors.FirstOrDefault() : "";

            }
            bool isValidPhone = phone.HasValidData();
            if (!isValidPhone)
            {
                PhoneError = !phone.HasValidData() ? phone.Errors.FirstOrDefault() : "";

            }


            bool isValidPassword = password.HasValidData();
            if (!isValidPassword)
            {
                PasswordError = !password.HasValidData() ? password.Errors.FirstOrDefault() : "";

            }

            bool isValidConfirmPassword = confirmPassword.HasValidData();
            if (!isValidConfirmPassword)
            {
                ConfirmPasswordError = !confirmPassword.HasValidData() ? confirmPassword.Errors.FirstOrDefault() : "";
            }

            bool passwordMatch = true;
            if (password.Value != confirmPassword.Value)
            {
                ConfirmPasswordError = "Las contraseñas no coinciden";
                passwordMatch = false;
            }

            return isValidEmail && isValidFirstName && isValidLastName && isValidAddress && isValidPhone  && isValidPassword && isValidConfirmPassword && passwordMatch;

        }

        private async void RegisterClicked(object obj)
        {
            await base.ExecuteMethodAsync("OnLoginClicked", async delegate ()
            {
                if (Validate())
                {
                    RegistrationResponse response = await userService.RegisterAsync(email.Value, firstName.Value, lastName.Value, address.Value, phone.Value, password.Value, confirmPassword.Value

                        );

                    if (response.message == "OK")
                    {

                        await App.Current.MainPage.DisplayAlert("Taxes y Otros", "Registro Exitoso! por favor ingrese con su email y contraseña.", "Ok");
                        App.Current.MainPage = new LoginPage();

                    }
                    else if (response.message == "VALIDATION_ERRORS")
                    {
                        if (response.errors!=null)
                        {
                            PropertyInfo[] myPropertyInfo;
                            // Get the properties of 'Type' class object.
                            myPropertyInfo = (response.errors.GetType()).GetProperties();
                            for (int i = 0; i < myPropertyInfo.Length; i++)
                            {
                                var PropValue = response.errors.GetType().GetProperty(myPropertyInfo[i].Name).GetValue(response.errors, null);

                                if (PropValue!=null)
                                {
                                    await App.Current.MainPage.DisplayAlert("Taxes y Otros", PropValue?.ToString(), "Ok");                              
                                    break;
                                }
                               
                            }
                        }
                        else
                        {
                            RegisterError = "El formulario se encuentra incompleto";
                        }
                    }                    
                    else if (response.message == "EMAIL_ALREADY_EXIST")
                    {
                        RegisterError = "El email ya se encuentra registrado.";

                    }
                    else
                    {
                        RegisterError = "Hubo un error, por favor inténtelo más tarde";
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
