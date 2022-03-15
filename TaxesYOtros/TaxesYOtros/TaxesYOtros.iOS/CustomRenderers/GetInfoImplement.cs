using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxesYOtros.Classes;
using TaxesYOtros.iOS.CustomRenderers;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(GetInfoImplement))]
namespace TaxesYOtros.iOS.CustomRenderers
{
    internal class GetInfoImplement : IDevice
    {

        public string GetIdentifier()
        {        
            return UIDevice.CurrentDevice.IdentifierForVendor.AsString(); ;
        }
    }

}