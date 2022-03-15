using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using RestSharp;

namespace TaxesYOtros
{
    public interface ITaxesYOtrosApp
    {
      
        RestClient ServiceClient { get; set; }
       
    }
}
