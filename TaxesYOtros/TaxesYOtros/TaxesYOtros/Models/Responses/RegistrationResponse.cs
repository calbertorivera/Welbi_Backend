using System;
using System.Collections.Generic;
using System.Text;
using TaxesYOtros.Models.Responses.ResposneDTO;

namespace TaxesYOtros.Models.Responses
{
    public class RegistrationResponse:ServiceStatusResponse
    {
        public RegistrationErrors errors { get; set; }
    }
}
