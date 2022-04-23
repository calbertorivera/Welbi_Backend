using Configuration;
using System;
using System.Collections.Generic;
using TaxesYOtros.Services.Texts;
using TaxesYOtros.ViewModels;
using TaxesYOtros.Views;
using Xamarin.Forms;

namespace TaxesYOtros
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public ITextService textsService;
       

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(LoginPageOTPValidation), typeof(LoginPageOTPValidation));
            Routing.RegisterRoute(nameof(PersonalInformation), typeof(PersonalInformation));

            this.textsService = DependencyService.Get<ITextService>();

            //texts About Label
            AboutLabel.Title = BaseViewModel.GetLocalizedTextStatic(LanguageToken.AMIDONE, "¿Está todo Completo?");
            PersonalInfo.Title = BaseViewModel.GetLocalizedTextStatic(LanguageToken.SECTION1, "Información Personal");
            Logout.Text = BaseViewModel.GetLocalizedTextStatic(LanguageToken.LOGOUT, "Cerrar Sesión");
            
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "0");
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private async void btnSpanish_Clicked(object sender, EventArgs e)
        {
            this.IsBusy = true;
            await Xamarin.Essentials.SecureStorage.SetAsync("lan", "ES");

            String response = await textsService.getAppTexts("ES");
            await Xamarin.Essentials.SecureStorage.SetAsync("ES_TEXTS", response);
 

            try
            {
                string route = Shell.Current.CurrentItem.CurrentItem.Route;

                if (route.StartsWith("IMPL_"))
                {
                    route = $"//{route.Replace("IMPL_", "")}";
                }
                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception)
            {
                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }

            this.IsBusy = false;
        }

        private async void btnEnglish_Clicked(object sender, EventArgs e)
        {
            this.IsBusy = true;
            await Xamarin.Essentials.SecureStorage.SetAsync("lan", "EN");
            this.textsService = DependencyService.Get<ITextService>();
            String response = await textsService.getAppTexts("EN");
            await Xamarin.Essentials.SecureStorage.SetAsync("EN_TEXTS", response);
         
            var current = Shell.Current.CurrentItem.CurrentItem.Route;
            try
            {
                string route = Shell.Current.CurrentItem.CurrentItem.Route;

                if (route.StartsWith("IMPL_"))
                {
                    route = $"//{route.Replace("IMPL_", "")}";
                }
                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception)
            {
                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            this.IsBusy = false;
        }      
    }
}
