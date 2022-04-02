using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaxesYOtros.Services.Texts
{

    public interface ITextService
    {
        Task<string> getAppTexts(string language);
    }

}
