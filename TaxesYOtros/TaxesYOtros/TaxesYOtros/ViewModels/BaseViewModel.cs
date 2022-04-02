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
using Xamarin.Forms;

namespace TaxesYOtros.ViewModels
{
    public class BaseViewModel : ExtendedBindableObject, INotifyPropertyChanged
    {
        public ITextService textsService;
        public BaseViewModel(string trackPrefix = null)
            : base(trackPrefix)
        {
            this.textsService = DependencyService.Get<ITextService>();
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


        public string GetLocalizedText(LanguageToken token, string defaultText)
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

        #endregion

    }
}
