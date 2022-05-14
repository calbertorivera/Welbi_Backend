using Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TaxesYOtros.Classes;
using TaxesYOtros.Models;
using TaxesYOtros.Services;
using TaxesYOtros.Services.Texts;
using TaxesYOtros.Services.User;
using Xamarin.Forms;

namespace TaxesYOtros.ViewModels
{
    public class BaseViewModel : ExtendedBindableObject, INotifyPropertyChanged
    {
        public ITextService textsService;
        public IUserService userService;
        private JObject userData;
        public BaseViewModel(string trackPrefix = null)
            : base(trackPrefix)
        {
            this.textsService = DependencyService.Get<ITextService>();
            this.userService = DependencyService.Get<IUserService>();
        }

        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public JObject UserData
        {
            get
            {
                if (Xamarin.Essentials.SecureStorage.GetAsync("data").Result != null)
                {
                    return JObject.Parse(Xamarin.Essentials.SecureStorage.GetAsync("data").Result);
                }
                else { return null; }
            }
            set
            {
                if (value != null)
                {
                    Xamarin.Essentials.SecureStorage.SetAsync("data", value.ToString());
                }
                else
                {
                    Xamarin.Essentials.SecureStorage.SetAsync("data", null);
                }
            }
        }

        public async Task<bool> LoadUserData(bool forceRefresh = false)
        {

            var token = Xamarin.Essentials.SecureStorage.GetAsync("token").Result;
            var sessionId = Xamarin.Essentials.SecureStorage.GetAsync("user_id").Result;

            if (forceRefresh || UserData == null)
            {
                var Data = await userService.GetUser(token, sessionId);

                if (Data != null)
                {
                    UserData = Data;
                }

            }
            else if (UserData == null)
            {
                var Data = await userService.GetUser(token, sessionId);

                if (Data != null)
                {
                    UserData = Data;
                }
            }

            return true;
        }


        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region Protected Methods



        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public static string GetLocalizedTextStatic(LanguageToken token, string defaultText)
        {
            var lan = Xamarin.Essentials.SecureStorage.GetAsync("lan").Result;
            if (lan != null)
            {
                var Texts = Xamarin.Essentials.SecureStorage.GetAsync($"{lan}_TEXTS").Result;
                if (Texts != null)
                {

                    JObject texts = JObject.Parse(Texts);
                    try
                    {
                        string key = token.ToString();
                        var text = texts[key].ToString();
                        return text;
                    }
                    catch (Exception)
                    {
                        return defaultText;
                    }
                }
            }
            return defaultText;
        }

        public string GetLocalizedText(LanguageToken token, string defaultText)
        {

            return GetLocalizedTextStatic(token, defaultText);

        }

        #region Screen text


        public string Text_General_Error
        {
            get
            {
                return GetLocalizedText(LanguageToken.GENERAL_ERROR, "Hubo un error, por favor inténtelo más tarde");
            }
        }

        public string Text_TryToLoginAgain
        {
            get
            {
                return GetLocalizedText(LanguageToken.OTP5, "Intenta ingresar sesión de nuevo");
            }
        }

        public string Text_Email_Not_Valid
        {
            get
            {
                return GetLocalizedText(LanguageToken.LOGIN10, "El correo electronico no es valido");
            }
        }
        public string Text_Email_Required
        {
            get
            {
                return GetLocalizedText(LanguageToken.LOGIN4, "El correo electrónico es requerido");
            }
        }

        public string TextPersonalInformationTitle
        {
            get
            {
                return GetLocalizedText(LanguageToken.SECTION1, "Información Personal");
            }
        }
        public string TextSpouseInformationTitle
        {
            get
            {
                return GetLocalizedText(LanguageToken.SECTION2, "Información del Cónyugue");
            }
        }
        public string TextHomeAddressInformationTitle
        {
            get
            {
                return GetLocalizedText(LanguageToken.SECTION3, "Información de Residencia");
            }
        }
        public string TextBankInformationTitle
        {
            get
            {
                return GetLocalizedText(LanguageToken.SECTION4, "Información Bancaria");
            }
        }
        public string TextDependentsTitle
        {
            get
            {
                return GetLocalizedText(LanguageToken.SECTION5, "Dependientes");
            }
        }
        public string TextDocumentsTitle
        {
            get
            {
                return GetLocalizedText(LanguageToken.SECTION6, "Documentos");
            }
        }

        public string Text_Select { get { return GetLocalizedText(LanguageToken.SELECT, "--Seleccione--"); } }

        public string Text_YES { get { return GetLocalizedText(LanguageToken.YES, "Sí"); } }
        public string Text_NO { get { return GetLocalizedText(LanguageToken.NO, "No"); } }
        public string Text_GENERIC_REQUIRED_TEXT
        {
            get
            {
                return GetLocalizedText(LanguageToken.GENERIC_REQUIRED_TEXT, "Este campo/pregunta es requerido");
            }
        }

        #endregion

    }
}
