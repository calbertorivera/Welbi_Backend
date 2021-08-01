using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelbiBackend.Entites;
using WelbiBackend.Proxy;
using System.Linq;

namespace WelbiBackend.Business
{
    public class ProgramsManager
    {
      
        public bool registerProgram(RegisterProgram registerProgram, string url)
        {
            ProgramsService residentServiceCall = new ProgramsService();
            return residentServiceCall.registerProgram(registerProgram, url);

        }
    }
}
