using System;
using System.Collections.Generic;
using System.Text;

namespace TaxesYOtros.Models.Responses
{
    public class ServiceStatusResponse
    {
        public string message { get; set; }
        public bool status { get; set; }
        public string token { get; set; } 
    }
}
