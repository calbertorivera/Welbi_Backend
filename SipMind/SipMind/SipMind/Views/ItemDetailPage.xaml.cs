using SipMind.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace SipMind.Views
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