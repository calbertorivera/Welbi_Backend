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
                this.phone = new ValidatableObject<string>();
                this.phoneCarrier = new ValidatableObject<string>();
                this.maritalStatus = new ValidatableObject<PickerItem>();
                this.hasDependents = new ValidatableObject<PickerItem>();

                ListMaritalStatus = new List<PickerItem>();
                ListMaritalStatus.Add(new PickerItem("SIN",Text_Single));
                ListMaritalStatus.Add(new PickerItem("MAR", Text_Married));

                YesNoList = new List<PickerItem>();
                YesNoList.Add(new PickerItem("YES", Text_YES));
                YesNoList.Add(new PickerItem("NO", Text_NO));

                //Fill Form
                FillData();

                AddValidations();

                //Password.Value = "Ju4ns31989";

            });
            LoginCommand = new Command(OnLoginClicked);
            SaveOnClickCommand = new Command(RegisterClicked);

        }

        private async void FillData()
        {
            await LoadUserData(false);

            try
            {
                if (this.UserData["user"] != null)
                {
                    this.FirstName.Value = this.UserData["user"]?["first_name"]?.ToString();
                    this.LastName.Value = this.UserData["user"]?["last_name"]?.ToString();
                    this.Email.Value = this.UserData["user"]?["username"]?.ToString();
                    this.Sufix.Value = this.UserData["user"]?["sufijo"]?.ToString();

                    if (this.UserData["user"]?["SSN"] != null)
                    {
                        String ssnFull = this.UserData["user"]?["SSN"]?.ToString();
                        this.SSN.Value = $"###-##-#{ssnFull.Substring(ssnFull.Length-3)}" ;
                    }
                   
                    if (this.UserData["user"]?["DOB"] != null)
                    {
                        this.DOB.Value = Convert.ToDateTime(this.UserData["user"]?["DOB"].ToString());
                    }

                    this.Ocupation.Value = this.UserData["user"]?["ocupation"]?.ToString();
                    if (this.UserData["user"]?["phone"] != null)
                    {
                        this.Phone.Value = ofuscatePhone((this.UserData["user"]?["phone"].ToString()));
                        ;
                    }
                    if (this.UserData["user"]?["spouse_first_name"] != null)
                    {
                        var itm = this.listMaritalStatus.FirstOrDefault(a => a.Id == "MAR");
                        this.maritalStatus.Value = itm;
                    }

                    if (this.UserData["user"]?["marital_status"] != null)
                    {
                       var itm = this.listMaritalStatus.FirstOrDefault(a => a.Id == this.UserData["user"]?["marital_status"]?.ToString());
                        this.maritalStatus.Value = itm;
                    }

                    if (this.UserData["dependents"] != null)
                    {
                        var itm = this.yesNoList.FirstOrDefault(a => a.Id == "YES");
                        this.hasDependents.Value = itm;
                    }

                    this.PhoneCarrier.Value = this.UserData["user"]?["carrier"]?.ToString();
                }
            }
            catch (Exception exc)
            {
                await App.Current.MainPage.DisplayAlert("Taxes y Otros", Text_General_Error, "Ok");
            }
           

        }

        private string ofuscatePhone(string phone)
        {
            if (phone.Length == 1)
            {
                return "####";
            }
            else
            {
                  var new_phone = "";
                  var substringNum = phone.Length > 3 ? 3 : phone.Length - 1;

                for (int i = 0; i < phone.Length - substringNum; i++) {
                  new_phone += "#";
                }

                return (new_phone + phone.Substring(phone.Length - substringNum)).Trim();
            }
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
        private ValidatableObject<string> phone;
        private ValidatableObject<string> phoneCarrier;
        private ValidatableObject<PickerItem> maritalStatus;
        private ValidatableObject<PickerItem> hasDependents;
        private List<PickerItem> listMaritalStatus;
        private List<PickerItem> yesNoList;


        private string firstNameError;
        private string lastNameError;
        private string emailError;
        private string sufixError;
        private string ssnError;
        private string hasDependentsError;
        private string maritalStatusError;
        private string dobError;
        private string ocupationError;
        private string phoneError;
        private string phoneCarrierError;
        private string savingError;

        private IUserService userService;
        IDevice device;
        #endregion

        #region Public properties
        public List<PickerItem> ListMaritalStatus
        {
            get => listMaritalStatus;
            set => SetProperty(ref listMaritalStatus, value);
        }
        public List<PickerItem> YesNoList
        {
            get => yesNoList;
            set => SetProperty(ref yesNoList, value);
        }
        

        public ValidatableObject<string> FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }
        public ValidatableObject<PickerItem> MaritalStatus
        {
            get => maritalStatus;
            set => SetProperty(ref maritalStatus, value);
        }

        public ValidatableObject<PickerItem> HasDependents
        {
            get => hasDependents;
            set => SetProperty(ref hasDependents, value);
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

        public ValidatableObject<string> Phone
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

        public string MaritalStatusError
        {
            get => maritalStatusError;
            set => SetProperty(ref maritalStatusError, value);
        }

        public string HasDependentsError
        {
            get => hasDependentsError;
            set => SetProperty(ref hasDependentsError, value);
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
        public ICommand MaritalStatusCommand => new Command(() =>
        {
            MaritalStatusError = !MaritalStatus.HasValidData() ? MaritalStatus.Errors.FirstOrDefault() : "";
        });

        public ICommand HasDependentsCommand => new Command(() =>
        {
            hasDependentsError = !HasDependents.HasValidData() ? HasDependents.Errors.FirstOrDefault() : "";
        });

        

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

            phone.Validations.Add(new IsValidNumberRule<string>
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
           
            maritalStatus.Validations.Add(new IsNotNullOrEmptyRule<PickerItem>
            {
                ValidationMessage = Text_MaritalStatus_Is_Required
            });

            hasDependents.Validations.Add(new IsNotNullOrEmptyRule<PickerItem>
            {
                ValidationMessage = Text_GENERIC_REQUIRED_TEXT
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

            bool isValidMaritalStatus = maritalStatus.HasValidData();
            if (!isValidMaritalStatus)
            {
                MaritalStatusError = !maritalStatus.HasValidData() ? maritalStatus.Errors.FirstOrDefault() : "";
            }

            bool isValidHasDependents = HasDependents.HasValidData();
            if (!isValidHasDependents)
            {
                HasDependentsError = !HasDependents.HasValidData() ? HasDependents.Errors.FirstOrDefault() : "";
            }

            return isValidHasDependents && isValidMaritalStatus && isValidPhone && isValidPhoneCarrier && isValidEmail && isValidFirstName && isValidLastName && isValidSufix && isValidSSN && isValidDOB && isValidOcupation;

        }

        private async void RegisterClicked(object obj)
        {
            this.IsBusy = true;
            await base.ExecuteMethodAsync("OnLoginClicked", async delegate ()
            {
                if (Validate())
                {

                    await App.Current.MainPage.DisplayAlert("Taxes y Otros", Text_Saved, "Ok");
                    
                    
                    //load user data
                    await LoadUserData(true);
                    Application.Current.MainPage = new AppShell();
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");

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

        public string Text_MaritalStatus { get { return GetLocalizedText(LanguageToken.TAXPAYER29, "Estado Civil"); } }

        public string Text_Single { get { return GetLocalizedText(LanguageToken.TAXPAYER30, "Single"); } }
        public string Text_Married { get { return GetLocalizedText(LanguageToken.TAXPAYER31, "Married"); } }
        public string Text_HasDependents { get { return GetLocalizedText(LanguageToken.TAXPAYER33, "Tiene personas a cargo?"); } }

        public string Text_MaritalStatus_Is_Required{ get { return GetLocalizedText(LanguageToken.TAXPAYER32, "El estado civil es requerido");

            }
}
public string Text_Save { get { return GetLocalizedText(LanguageToken.SAVE, "Guardar"); } }

        #endregion

    }
}
