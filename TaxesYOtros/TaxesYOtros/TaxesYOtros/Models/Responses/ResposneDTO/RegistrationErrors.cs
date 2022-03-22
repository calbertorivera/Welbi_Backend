using System;
using System.Collections.Generic;
using System.Text;

namespace TaxesYOtros.Models.Responses.ResposneDTO
{
    public class RegistrationErrors
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public string confirm { get; set; }

    }
}
