using Configuration;
using System;
using System.Windows.Input;
using TaxesYOtros.Services.Texts;
using TaxesYOtros.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TaxesYOtros.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }

        public ICommand SpanishCommand => new Command(async () =>
        {
            this.IsBusy = true;
            await Xamarin.Essentials.SecureStorage.SetAsync("lan", "ES");

            String response = await textsService.getAppTexts("ES");
            await Xamarin.Essentials.SecureStorage.SetAsync("ES_TEXTS", response);
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            this.IsBusy = false;
        });

        public ICommand EnglishCommand => new Command(async () =>
        {
            this.IsBusy = true;
            await Xamarin.Essentials.SecureStorage.SetAsync("lan", "EN");
            this.textsService = DependencyService.Get<ITextService>();
            String response = await textsService.getAppTexts("EN");
            await Xamarin.Essentials.SecureStorage.SetAsync("EN_TEXTS", response);
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            this.IsBusy = false;

        });
        #region Screen text

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