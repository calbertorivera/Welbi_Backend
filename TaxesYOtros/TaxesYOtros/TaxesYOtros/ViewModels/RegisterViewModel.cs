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
using System.Reflection;
using TaxesYOtros.Models.Responses.ResposneDTO;
using Configuration;

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
                ValidationMessage = Text_Email_is_Required
            });
            email.Validations.Add(new EmailRule<string>
            {
                ValidationMessage = Text_Email_is_not_valid
            });

            firstName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Text_FirstName_Required
            });

            lastName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Text_LastName_Is_Required
            });

            address.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Text_Address_Is_Required
            });

            phone.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Text_Phone_Is_Required
            });

            phone.Validations.Add(new IsValidNumberRule<string>
            {
                ValidationMessage = Text_Phone_Must_Be_Numbers
            });

            phone.Validations.Add(new IsValidLengthRule<string>
            {
                MaximumLength = 10,
                ValidationMessage = Text_Phone_Number_Is_Required
            });

            password.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Text_Password_Is_Required
            });
            password.Validations.Add(new IsValidLengthRule<string>
            {
                MinimumLength = 6,
                ValidationMessage = Text_Minimum_Length_6
            });

            confirmPassword.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Text_Confirm_Password_Is_Required
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
                ConfirmPasswordError = Text_Passwords_Dont_Match;
                passwordMatch = false;
            }

            return isValidEmail && isValidFirstName && isValidLastName && isValidAddress && isValidPhone && isValidPassword && isValidConfirmPassword && passwordMatch;

        }

        private async void RegisterClicked(object obj)
        {
            this.IsBusy = true;
            await base.ExecuteMethodAsync("OnLoginClicked", async delegate ()
            {
                if (Validate())
                {
                    RegistrationResponse response = await userService.RegisterAsync(email.Value, firstName.Value, lastName.Value, address.Value, phone.Value, password.Value, confirmPassword.Value

                        );

                    if (response.message == "OK")
                    {

                        await App.Current.MainPage.DisplayAlert("Taxes y Otros", Text_Successfullty_Registered, "Ok");
                        App.Current.MainPage = new LoginPage();

                    }
                    else if (response.message == "VALIDATION_ERRORS")
                    {
                        if (response.errors != null)
                        {
                            PropertyInfo[] myPropertyInfo;
                            // Get the properties of 'Type' class object.
                            myPropertyInfo = (response.errors.GetType()).GetProperties();
                            for (int i = 0; i < myPropertyInfo.Length; i++)
                            {
                                var PropValue = response.errors.GetType().GetProperty(myPropertyInfo[i].Name).GetValue(response.errors, null);

                                if (PropValue != null)
                                {
                                    await App.Current.MainPage.DisplayAlert("Taxes y Otros", PropValue?.ToString(), "Ok");
                                    break;
                                }

                            }
                        }
                        else
                        {
                            RegisterError = Text_Form_Is_Not_Complete;
                        }
                    }
                    else if (response.message == "EMAIL_ALREADY_EXIST")
                    {
                        RegisterError = Text_Email_Is_Already_Registered;

                    }
                    else
                    {
                        RegisterError = Text_General_Error;
                    }
                }
            });

            this.IsBusy = false;
        }

        private async void OnLoginClicked(object obj)
        {
            App.Current.MainPage = new LoginPage();

        }
        #endregion

        #region Screen text



        public string Text_Register { get { return GetLocalizedText(LanguageToken.REGISTRATION1, "Crear Cuenta"); } }
        public string Text_Email { get { return GetLocalizedText(LanguageToken.REGISTRATION2, "Correo Electrónico:"); } }
        public string Text_Email_PlaceHolder { get { return GetLocalizedText(LanguageToken.REGISTRATION3, "Ingrese aquí su corre electronico"); } }
        public string Text_FirstName { get { return GetLocalizedText(LanguageToken.REGISTRATION4, "Primer Nombre:"); } }
        public string Text_FirstName_PlaceHolder { get { return GetLocalizedText(LanguageToken.REGISTRATION5, "Ingrese aquí su primer nombre"); } }
        public string Text_LastName { get { return GetLocalizedText(LanguageToken.REGISTRATION6, "Apellido:"); } }
        public string Text_LastName_PlaceHolder { get { return GetLocalizedText(LanguageToken.REGISTRATION7, "Ingrese aquí su apellido"); } }
        public string Text_Address { get { return GetLocalizedText(LanguageToken.REGISTRATION8, "Dirección:"); } }
        public string Text_Address_PlaceHolder { get { return GetLocalizedText(LanguageToken.REGISTRATION9, "Ingrese aquí su dirección de residencia"); } }
        public string Text_Phone { get { return GetLocalizedText(LanguageToken.REGISTRATION10, "Celular:"); } }
        public string Text_Phone_PlaceHolder { get { return GetLocalizedText(LanguageToken.REGISTRATION11, "Ingrese aquí su número de contacto"); } }
        public string Text_Password { get { return GetLocalizedText(LanguageToken.REGISTRATION12, "Contraseña:"); } }
        public string Text_Password_PlaceHolder { get { return GetLocalizedText(LanguageToken.REGISTRATION13, "Ingrese aquí su Contraseña"); } }
        public string Text_ConfirmPassword { get { return GetLocalizedText(LanguageToken.REGISTRATION14, "Confirme su Contraseña:"); } }
        public string Text_ConfirmPassword_PlaceHolder { get { return GetLocalizedText(LanguageToken.REGISTRATION15, "Confirme su Contraseña"); } }
        public string Text_Email_is_Required { get { return GetLocalizedText(LanguageToken.REGISTRATION16, "El correo electronico es requerido"); } }
        public string Text_Email_is_not_valid { get { return GetLocalizedText(LanguageToken.REGISTRATION17, "El correo electronico no es valido"); } }
        public string Text_FirstName_Required { get { return GetLocalizedText(LanguageToken.REGISTRATION18, "El Primer Nombre es requerido"); } }
        public string Text_LastName_Is_Required { get { return GetLocalizedText(LanguageToken.REGISTRATION19, "El Apellido es requerido"); } }
        public string Text_Address_Is_Required { get { return GetLocalizedText(LanguageToken.REGISTRATION20, "La dirección es requerida"); } }
        public string Text_Phone_Is_Required { get { return GetLocalizedText(LanguageToken.REGISTRATION21, "El teléfono es requerido"); } }
        public string Text_Phone_Must_Be_Numbers { get { return GetLocalizedText(LanguageToken.REGISTRATION22, "El teléfono debe ser solo numeros"); } }
        public string Text_Phone_Number_Is_Required { get { return GetLocalizedText(LanguageToken.REGISTRATION23, "El teléfono es requerido y no debe contener mas de 10 números"); } }
        public string Text_Password_Is_Required { get { return GetLocalizedText(LanguageToken.REGISTRATION24, "La contraseña es requerida."); } }
        public string Text_Minimum_Length_6 { get { return GetLocalizedText(LanguageToken.REGISTRATION25, "La longitud minima es 6 letras"); } }
        public string Text_Confirm_Password_Is_Required { get { return GetLocalizedText(LanguageToken.REGISTRATION26, "La confirmación de contraseña es requerida."); } }
        public string Text_Passwords_Dont_Match { get { return GetLocalizedText(LanguageToken.REGISTRATION27, "Las contraseñas no coinciden"); } }
        public string Text_Successfullty_Registered { get { return GetLocalizedText(LanguageToken.REGISTRATION28, "Registro Exitoso! por favor ingrese con su email y contraseña."); } }
        public string Text_Form_Is_Not_Complete { get { return GetLocalizedText(LanguageToken.REGISTRATION29, "El formulario se encuentra incompleto"); } }
        public string Text_Email_Is_Already_Registered { get { return GetLocalizedText(LanguageToken.REGISTRATION30, "El email ya se encuentra registrado."); } }
        public string Text_Submit { get { return GetLocalizedText(LanguageToken.REGISTRATION31, "Crear Cuenta"); } }

        #endregion

    }
}
