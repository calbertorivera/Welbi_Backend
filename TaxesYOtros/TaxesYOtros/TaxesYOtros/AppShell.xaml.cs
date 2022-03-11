using System;
using System.Collections.Generic;
using TaxesYOtros.ViewModels;
using TaxesYOtros.Views;
using Xamarin.Forms;

namespace TaxesYOtros
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "0");       
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
