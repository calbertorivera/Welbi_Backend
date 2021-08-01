using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelbiBackend.Proxy.RequestEntities
{
    [Serializable]
    public class StartRequest
    {
        public StartRequest(String email)
        {
            this.email = email;
        }

        public String email { get; set; }
    }
}
