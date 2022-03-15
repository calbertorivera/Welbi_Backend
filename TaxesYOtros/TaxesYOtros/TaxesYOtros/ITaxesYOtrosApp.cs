using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using RestSharp;

namespace TaxesYOtros
{
    public interface ITaxesYOtrosApp
    {
        string CurrentViewModel { get; set; }
        Page CurrentPage { get; set; }
        RestClient ServiceClient { get; set; }
       
    }
}
