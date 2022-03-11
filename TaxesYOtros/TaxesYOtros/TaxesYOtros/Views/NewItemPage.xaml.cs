using System;
using System.Collections.Generic;
using System.ComponentModel;
using TaxesYOtros.Models;
using TaxesYOtros.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxesYOtros.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}