using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxesYOtros.Classes;
using TaxesYOtros.Droid.CustomRenders;
using static Android.Provider.Settings;

[assembly: Xamarin.Forms.Dependency(typeof(GetInfoImplement))]
namespace TaxesYOtros.Droid.CustomRenders
{
    internal class GetInfoImplement : IDevice
    {
       
        public string GetIdentifier()
        {
            var context = Android.App.Application.Context;
            string id = Android.Provider.Settings.Secure.GetString(context.ContentResolver, Secure.AndroidId);
            return id;
        }
    }
  
}