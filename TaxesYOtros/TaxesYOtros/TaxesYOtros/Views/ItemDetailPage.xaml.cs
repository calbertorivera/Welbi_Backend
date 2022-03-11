using System.ComponentModel;
using TaxesYOtros.ViewModels;
using Xamarin.Forms;

namespace TaxesYOtros.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}