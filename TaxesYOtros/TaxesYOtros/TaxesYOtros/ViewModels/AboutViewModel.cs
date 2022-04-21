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
                sections.Add(new Section(Constants.Sections.PERSONAL_INFO, "Información Personal", false));
                sections.Add(new Section(Constants.Sections.SPOUSE_INFO, "Información del Cónyuge", false));
                sections.Add(new Section(Constants.Sections.ADDRESS_INFO, "Información de Residencia", true));
                sections.Add(new Section(Constants.Sections.BANK_INFO, "Información Bancaria", false));
                sections.Add(new Section(Constants.Sections.DEPENDENTS_INFO, "Dependientes", false));
                sections.Add(new Section(Constants.Sections.DOCUMENTS, "Documentos", false));

                SetProperty(ref sections, sections);

                
            });

            ItemTapped = new Command<Section>(OnItemSelected);
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
        async void OnItemSelected(Section item)
        {
            if (item == null)
                return;

         
            await App.Current.MainPage.DisplayAlert("Taxes y Otros", item.name, "Ok");
            //// This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
        public Command<Section> ItemTapped { get; }

        public ICommand OpenWebCommand { get; }

        public ICommand SpanishCommand => new Command(async () =>
        {
            this.IsBusy = true;
            await Xamarin.Essentials.SecureStorage.SetAsync("lan", "ES");

            String response = await textsService.getAppTexts("ES");
            await Xamarin.Essentials.SecureStorage.SetAsync("ES_TEXTS", response);
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

        #endregion
    }
}