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
    public class PersonalInformationViewModel : BaseViewModel
    {
        #region Constructor
        public PersonalInformationViewModel()
          : base("PersonalInformationViewModel")
        {
            base.ExecuteMethod("PersonalInformationViewModel", delegate ()
            {
                this.firstName = new ValidatableObject<string>();
                this.lastName = new ValidatableObject<string>();
                this.userService = DependencyService.Get<IUserService>();
                this.email = new ValidatableObject<string>();
                this.sufix = new ValidatableObject<string>();
                this.ssn = new ValidatableObject<string>();
                this.dob = new ValidatableObject<DateTime?>();
                this.ocupation = new ValidatableObject<string>();
                this.phone = new ValidatableObject<int?>();
                this.phoneCarrier = new ValidatableObject<string>();


                AddValidations();


                //Password.Value = "Ju4ns31989";

            });
            LoginCommand = new Command(OnLoginClicked);
            SaveOnClickCommand = new Command(RegisterClicked);

        }
        #endregion
        #region Private properties  
        private ValidatableObject<string> firstName;
        private ValidatableObject<string> lastName;
        private ValidatableObject<string> email;
        private ValidatableObject<string> sufix;
        private ValidatableObject<string> ssn;
        private ValidatableObject<DateTime?> dob;
        private ValidatableObject<string> ocupation;
        private ValidatableObject<int?> phone;
        private ValidatableObject<string> phoneCarrier;

        private string firstNameError;
        private string lastNameError;
        private string emailError;
        private string sufixError;
        private string ssnError;
        private string dobError;
        private string ocupationError;
        private string phoneError;
        private string phoneCarrierError;
        private string savingError;

        private IUserService userService;
        IDevice device;
        #endregion

        #region Public properties


        public ValidatableObject<string> FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }
        public ValidatableObject<string> Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        public ValidatableObject<string> LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }
        public ValidatableObject<string> Sufix
        {
            get => sufix;
            set => SetProperty(ref sufix, value);
        }
        public ValidatableObject<string> SSN
        {
            get => ssn;
            set => SetProperty(ref ssn, value);
        }
        public ValidatableObject<DateTime?> DOB
        {
            get => dob;
            set => SetProperty(ref dob, value);
        }
        public ValidatableObject<string> Ocupation
        {
            get => ocupation;
            set => SetProperty(ref ocupation, value);
        }

        public ValidatableObject<int?> Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }
        public ValidatableObject<string> PhoneCarrier
        {
            get => phoneCarrier;
            set => SetProperty(ref phoneCarrier, value);
        }
        public string SavingError
        {
            get => savingError;
            set => SetProperty(ref savingError, value);
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

        public string EmailError
        {
            get => emailError;
            set => SetProperty(ref emailError, value);
        }
        public string SufixError
        {
            get => sufixError;
            set => SetProperty(ref sufixError, value);
        }
        public string SSNError
        {
            get => ssnError;
            set => SetProperty(ref ssnError, value);
        }
        public string DOBError
        {
            get => dobError;
            set => SetProperty(ref dobError, value);
        }
        public string OcupationError
        {
            get => ocupationError;
            set => SetProperty(ref ocupationError, value);
        }

        public string PhoneError
        {
            get => phoneError;
            set => SetProperty(ref phoneError, value);
        }

        public string PhoneCarrierError
        {
            get => phoneCarrierError;
            set => SetProperty(ref phoneCarrierError, value);
        }

        #endregion


        #region Commands
        public ICommand ValidateFirstNameCodeCommand => new Command(() =>
        {
            FirstNameError = !firstName.HasValidData() ? firstName.Errors.FirstOrDefault() : "";
        });

        public ICommand ValidateLastNameCodeCommand => new Command(() =>
        {
            LastNameError = !lastName.HasValidData() ? lastName.Errors.FirstOrDefault() : "";
        });

        public ICommand ValidateEmailCodeCommand => new Command(() =>
        {
            EmailError = !email.HasValidData() ? email.Errors.FirstOrDefault() : "";
        });

        public ICommand ValidateSufixCodeCommand => new Command(() =>
        {
            SufixError = !sufix.HasValidData() ? sufix.Errors.FirstOrDefault() : "";
        });

        public ICommand ValidateSSNCodeCommand => new Command(() =>
        {
            SSNError = !ssn.HasValidData() ? ssn.Errors.FirstOrDefault() : "";
        });

        public ICommand ValidateDOBCodeCommand => new Command(() =>
        {
            DOBError = !DOB.HasValidData() ? DOB.Errors.FirstOrDefault() : "";
        });

        public ICommand ValidateOcupationCodeCommand => new Command(() =>
        {
            OcupationError = !ocupation.HasValidData() ? ocupation.Errors.FirstOrDefault() : "";
        });

        public ICommand ValidatePhoneCodeCommand => new Command(() =>
        {
            PhoneError = !phone.HasValidData() ? phone.Errors.FirstOrDefault() : "";
        });


        public ICommand ValidatePhoneCarrierCodeCommand => new Command(() =>
        {
            PhoneCarrierError = !phoneCarrier.HasValidData() ? phoneCarrier.Errors.FirstOrDefault() : "";
        });


        public Command LoginCommand { get; }
        public Command SaveOnClickCommand { get; }

        #endregion

        #region Protected methods
        protected void AddValidations()
        {


            firstName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Text_FirstName_Required
            });

            lastName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Text_LastName_Is_Required
            });

            email.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Text_Email_is_Required
            });
            email.Validations.Add(new EmailRule<string>
            {
                ValidationMessage = Text_Email_is_not_valid
            });

        
            ssn.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Text_SSN_Is_Required
            });

            phone.Validations.Add(new IsValidNumberRule<int?>
            {
                ValidationMessage = Text_Phone_Must_Be_Numbers
            });

            ssn.Validations.Add(new IsValidLengthRule<string>
            {
                MaximumLength = 11,
                MinimumLength = 11,
                ValidationMessage = Text_SSN_minimun_Length
            });

            dob.Validations.Add(new IsNotNullOrEmptyRule<DateTime?>
            {
                ValidationMessage = Text_DOB_Is_Required
            });
           
           

        }
        private bool Validate()
        {

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
            bool isValidEmail = email.HasValidData();
            if (!isValidEmail)
            {
                EmailError = !email.HasValidData() ? email.Errors.FirstOrDefault() : "";

            }

            bool isValidSufix = sufix.HasValidData();
            if (!isValidSufix)
            {
                SufixError = !sufix.HasValidData() ? sufix.Errors.FirstOrDefault() : "";

            }
            bool isValidSSN = ssn.HasValidData();
            if (!isValidSSN)
            {
                SSNError = !ssn.HasValidData() ? ssn.Errors.FirstOrDefault() : "";

            }


            bool isValidDOB = dob.HasValidData();
            if (!isValidDOB)
            {
                dobError = !dob.HasValidData() ? dob.Errors.FirstOrDefault() : "";

            }

            bool isValidOcupation = ocupation.HasValidData();
            if (!isValidOcupation)
            {
                OcupationError = !ocupation.HasValidData() ? ocupation.Errors.FirstOrDefault() : "";
            }

            bool isValidPhone = phone.HasValidData();
            if (!isValidPhone)
            {
                PhoneError = !phone.HasValidData() ? phone.Errors.FirstOrDefault() : "";
            }


            bool isValidPhoneCarrier = phoneCarrier.HasValidData();
            if (!isValidPhoneCarrier)
            {
                PhoneCarrierError = !phoneCarrier.HasValidData() ? phoneCarrier.Errors.FirstOrDefault() : "";
            }



            return isValidPhone && isValidPhoneCarrier && isValidEmail && isValidFirstName && isValidLastName && isValidSufix && isValidSSN && isValidDOB && isValidOcupation;

        }

        private async void RegisterClicked(object obj)
        {
            this.IsBusy = true;
            await base.ExecuteMethodAsync("OnLoginClicked", async delegate ()
            {
                if (Validate())
                {

                    await App.Current.MainPage.DisplayAlert("Taxes y Otros", Text_Saved, "Ok");
                    //RegistrationResponse response = await userService.RegisterAsync(email.Value, firstName.Value, lastName.Value, sufix.Value, ssn.Value, dob.Value, ocupation.Value

                    //    );

                    //if (response.message == "OK")
                    //{

                    //    await App.Current.MainPage.DisplayAlert("Taxes y Otros", Text_Successfullty_Registered, "Ok");
                    //    App.Current.MainPage = new LoginPage();

                    //}
                    //else if (response.message == "VALIDATION_ERRORS")
                    //{
                    //    if (response.errors != null)
                    //    {
                    //        PropertyInfo[] myPropertyInfo;
                    //        // Get the properties of 'Type' class object.
                    //        myPropertyInfo = (response.errors.GetType()).GetProperties();
                    //        for (int i = 0; i < myPropertyInfo.Length; i++)
                    //        {
                    //            var PropValue = response.errors.GetType().GetProperty(myPropertyInfo[i].Name).GetValue(response.errors, null);

                    //            if (PropValue != null)
                    //            {
                    //                await App.Current.MainPage.DisplayAlert("Taxes y Otros", PropValue?.ToString(), "Ok");
                    //                break;
                    //            }

                    //        }
                    //    }
                    //    else
                    //    {
                    //        SavingError = Text_Form_Is_Not_Complete;
                    //    }
                    //}
                    //else if (response.message == "EMAIL_ALREADY_EXIST")
                    //{
                    //    SavingError = Text_Email_Is_Already_Registered;

                    //}
                    //else
                    //{
                    //    SavingError = Text_General_Error;
                    //}
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

        public string Text_FirstName { get { return GetLocalizedText(LanguageToken.TAXPAYER2, "First Name"); } }
        public string Text_LastName { get { return GetLocalizedText(LanguageToken.TAXPAYER3, "Last Name"); } }
        public string Text_Sufix { get { return GetLocalizedText(LanguageToken.TAXPAYER4, "Sufix"); } }
        public string Text_EmailAddress { get { return GetLocalizedText(LanguageToken.TAXPAYER5, "Email Address"); } }
        public string Text_SSN { get { return GetLocalizedText(LanguageToken.TAXPAYER6, "SSN"); } }
        public string Text_DOB { get { return GetLocalizedText(LanguageToken.TAXPAYER7, "Date of Birth"); } }
        public string Text_Ocupation { get { return GetLocalizedText(LanguageToken.TAXPAYER8, "Ocupation"); } }
        public string Text_Cellphone { get { return GetLocalizedText(LanguageToken.TAXPAYER9, "Cellphone Number"); } }
        public string Text_Cellphone_Carrier { get { return GetLocalizedText(LanguageToken.TAXPAYER10, "Cellphone Carrier:"); } }
        public string Text_FirstName_PlaceHolder { get { return GetLocalizedText(LanguageToken.TAXPAYER11, "Enter your First Name"); } }
        public string Text_LastName_PlaceHolder { get { return GetLocalizedText(LanguageToken.TAXPAYER12, "Enter your Last Name"); } }
        public string Text_Sufix_PlaceHolder { get { return GetLocalizedText(LanguageToken.TAXPAYER13, "Enter your Sufix"); } }
        public string Text_EmailAddress_PlaceHolder { get { return GetLocalizedText(LanguageToken.TAXPAYER14, "Enter your Email Address"); } }
        public string Text_SSN_PlaceHolder { get { return GetLocalizedText(LanguageToken.TAXPAYER15, "Enter the 9 digits of your SSN"); } }
        public string Text_DOB_PlaceHolder { get { return GetLocalizedText(LanguageToken.TAXPAYER16, "Enter your Date of Birth"); } }
        public string Text_Ocupation_PlaceHolder { get { return GetLocalizedText(LanguageToken.TAXPAYER17, "Enter your Ocupation"); } }
        public string Text_Cellphone_PlaceHolder { get { return GetLocalizedText(LanguageToken.TAXPAYER18, "Enter your Cellphone Number"); } }
        public string Text_Cellphone_Carrier_PlaceHolder { get { return GetLocalizedText(LanguageToken.TAXPAYER19, "Enter your Cellphone Carrier:"); } }

        public string Text_FirstName_Required { get { return GetLocalizedText(LanguageToken.TAXPAYER20, "El nombre es requerido"); } }
        public string Text_LastName_Is_Required { get { return GetLocalizedText(LanguageToken.TAXPAYER21, "El apellido es requerido"); } }
        public string Text_Email_is_Required { get { return GetLocalizedText(LanguageToken.TAXPAYER22, "Email es requerido"); } }
        public string Text_Email_is_not_valid { get { return GetLocalizedText(LanguageToken.TAXPAYER23, "Email no es válido"); } }
        public string Text_SSN_Is_Required { get { return GetLocalizedText(LanguageToken.TAXPAYER24, "SSN es requerido"); } }
        public string Text_Phone_Must_Be_Numbers { get { return GetLocalizedText(LanguageToken.TAXPAYER25, "El número celular deben ser solo números"); } }
        public string Text_SSN_minimun_Length { get { return GetLocalizedText(LanguageToken.TAXPAYER26, "La longitud debe ser 9 números"); } }
        public string Text_DOB_Is_Required { get { return GetLocalizedText(LanguageToken.TAXPAYER27, "La fecha de nacimiento es requerido"); } }

        public string Text_Saved { get { return GetLocalizedText(LanguageToken.TAXPAYER28, "Su información has sido guardada exitosamente"); } }


        public string Text_Save { get { return GetLocalizedText(LanguageToken.SAVE, "Guardar"); } }

        #endregion

    }
}
