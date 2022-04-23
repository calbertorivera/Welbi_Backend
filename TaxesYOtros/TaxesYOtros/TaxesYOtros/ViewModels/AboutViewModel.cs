using Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TaxesYOtros.Classes;
using TaxesYOtros.Services.Texts;
using TaxesYOtros.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TaxesYOtros.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        #region Constructor


        public AboutViewModel()
       : base("AboutViewModel")
        {

            base.ExecuteMethod("LoginViewModel", delegate ()
            {
                Title = "";

                sections = new ObservableCollection<Section>();
                sections.Add(new Section(Constants.Sections.PERSONAL_INFO, TextPersonalInformationTitle, false));
                sections.Add(new Section(Constants.Sections.SPOUSE_INFO, TextSpouseInformationTitle, false));
                sections.Add(new Section(Constants.Sections.ADDRESS_INFO, TextHomeAddressInformationTitle, false));
                sections.Add(new Section(Constants.Sections.BANK_INFO, TextBankInformationTitle, false));
                sections.Add(new Section(Constants.Sections.DEPENDENTS_INFO, TextDependentsTitle, false));
                sections.Add(new Section(Constants.Sections.DOCUMENTS, TextDocumentsTitle, false));

                SetProperty(ref sections, sections);


            });

            ItemTapped = new Command<Section>(OnItemSelected);
            ImageTapped = new Command<String>(OnImageTapped);
        }
        #endregion

        #region Private properties       
        private ObservableCollection<Section> sections;

        #endregion

        #region Public properties


        public ObservableCollection<Section> Sections
        {
            get => sections;
            set => SetProperty(ref sections, value);
        }

        #endregion

        #region Commands

        async void OnImageTapped(String image)
        {
            switch (image)
            {
                case "3":
                    await Browser.OpenAsync("https://www.irs.gov/es/refunds", new BrowserLaunchOptions
                    {
                        LaunchMode = BrowserLaunchMode.SystemPreferred,
                        TitleMode = BrowserTitleMode.Show,
                        PreferredToolbarColor = (Color)App.Current.Resources["TaxesYOtrosRedColor"],
                        PreferredControlColor = (Color)App.Current.Resources["TaxesYOtrosRedColor"]
                    });
                    break;
                case "4":
                    await Browser.OpenAsync("https://www.ftb.ca.gov/refund/status-es.asp", new BrowserLaunchOptions
                    {
                        LaunchMode = BrowserLaunchMode.SystemPreferred,
                        TitleMode = BrowserTitleMode.Show,
                        PreferredToolbarColor = (Color)App.Current.Resources["TaxesYOtrosRedColor"],
                        PreferredControlColor = (Color)App.Current.Resources["TaxesYOtrosRedColor"]
                    });
                    break;
                default:
                    break;
            }

        }
        async void OnItemSelected(Section item)
        {
            if (item == null)
                return;


            switch (item.sect)
            {
                case Constants.Sections.PERSONAL_INFO:
                    Application.Current.MainPage = new AppShell();
                    await Shell.Current.GoToAsync($"//{nameof(PersonalInformation)}");
                    break;
                case Constants.Sections.SPOUSE_INFO:
                    break;
                case Constants.Sections.ADDRESS_INFO:
                    break;
                case Constants.Sections.BANK_INFO:
                    break;
                case Constants.Sections.DEPENDENTS_INFO:
                    break;
                case Constants.Sections.DOCUMENTS:
                    break;
                default:
                    break;
            }

        }
        public Command<Section> ItemTapped { get; }
        public Command<String> ImageTapped { get; }
        

        public ICommand OpenWebCommand { get; }

        public ICommand SpanishCommand => new Command(async () =>
        {
            this.IsBusy = true;
            await Xamarin.Essentials.SecureStorage.SetAsync("lan", "ES");

            String response = await textsService.getAppTexts("ES");
            await Xamarin.Essentials.SecureStorage.SetAsync("ES_TEXTS", response);
            Application.Current.MainPage = new AppShell();
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
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            this.IsBusy = false;

        });
        #endregion



        #region Screen text

        public string TextCambiarIdioma
        {
            get
            {
                return GetLocalizedText(LanguageToken.LOGIN9, "Change Language?");
            }
        }


        public string TextWelcomeTitle
        {
            get
            {
                return GetLocalizedText(LanguageToken.WELCOMETITLE, "Bienvenido a Taxes y Otros");
            }
        }
        public string TextWelcomeMessage
        {
            get
            {
                return GetLocalizedText(LanguageToken.WELCOMEMESSAGE, "Por favor revise cada una de las secciones listadas a continuación:");
            }
        }

        public string TextTitle
        {
            get
            {
                return GetLocalizedTextStatic(LanguageToken.AMIDONE, "¿Está todo Completo?");
            }
        }


        #endregion
    }
}