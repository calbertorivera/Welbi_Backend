using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelbiBackend.Proxy.RequestEntities
{
    public class RegisterProgramRequest
    {     
        public String name { get; set; }
        public String location { get; set; }
        public bool allDay { get; set; }
        public String start { get; set; }
        public String end { get; set; }
        public String[] tags { get; set; }

        public string[] hobbies { get; set; }
        public string[] facilitators { get; set; }
        public bool isRepeated { get; set; }
        public string[] levelOfCare { get; set; }
        public string dimension { get; set; }
    }
}
