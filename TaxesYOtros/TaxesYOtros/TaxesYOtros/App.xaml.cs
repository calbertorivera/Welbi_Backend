using RestSharp;
using System;
using TaxesYOtros.Core;
using TaxesYOtros.Services;
using TaxesYOtros.Services.RequestProvider;
using TaxesYOtros.Services.Texts;
using TaxesYOtros.Services.User;
using TaxesYOtros.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UserService = TaxesYOtros.Services.User.UserService;

namespace TaxesYOtros
{
    public partial class App : Application
    {
        private static RestClient restClient;
        private ITextService textsService;
  
        public static RestClient ServiceClient
        {
            get
            {
                if (restClient == null)
                {
                    return new RestClient(GlobalSetting.Instance.BaseEndpoint);
                }
                else { return restClient; }
            }
        }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<IUserService, UserService>();
            DependencyService.Register<ITextService, TextService>();
            DependencyService.Register<IRequestProvider, RequestProvider>();

            Application.Current.UserAppTheme = OSAppTheme.Light;

            var isLoogged = Xamarin.Essentials.SecureStorage.GetAsync("isLogged").Result;
            if (isLoogged == "1")
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new LoginPage();
            }

            //check language
            CheckLanguage();
        }

        private async void CheckLanguage()
        {
            try
            {
                String lang = await Xamarin.Essentials.SecureStorage.GetAsync("lan");
                if (lang != null)
                {
                    //String texts = await Xamarin.Essentials.SecureStorage.GetAsync($"{lang}_TEXTS");
                    //if (texts == null)
                    //{
                        this.textsService = DependencyService.Get<ITextService>();
                        String response = await textsService.getAppTexts(lang);
                        await Xamarin.Essentials.SecureStorage.SetAsync($"{lang}_TEXTS", response);
                    //}
                }
            }
            catch (Exception)
            {

            }   
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
