using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxesYOtros.Services.RequestProvider;
using Xamarin.Forms;

namespace TaxesYOtros.Services.Texts
{
    public class TextService : ITextService
    {
        public async Task<string> getAppTexts(string language)
        {
               

            var response = await DependencyService.Get<IRequestProvider>().GetJsonAsync(String.Format(GlobalSetting.Instance.TextsEndpoint, language));

            return response;
        }
    }
}
