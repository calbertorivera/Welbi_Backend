using System;
using System.Collections.Generic;
using System.Text;
using TaxesYOtros.Views;
using Xamarin.Forms;

namespace TaxesYOtros.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "1");
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
              
    }
}
