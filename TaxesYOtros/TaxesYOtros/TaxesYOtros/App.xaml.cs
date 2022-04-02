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
        public static RestClient ServiceClient
        {
            get {
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

            var isLoogged = Xamarin.Essentials.SecureStorage.GetAsync("isLogged").Result;
            if (isLoogged == "1")
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new LoginPage();
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
