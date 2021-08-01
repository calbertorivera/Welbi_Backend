using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelbiBackend.Proxy.ResponseEntities
{
    [Serializable]
    public class StartResponse
    {
        public String email { get; set; }
        public String token { get; set; }
    }
}
