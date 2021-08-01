using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelbiBackend.Entites
{
    public class RegisterProgram
    {
        public string name { get; set; }
        public string location { get; set; }
        public string allDay { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string tags { get; set; }


        public string hobbies { get; set; }
        public string facilitators { get; set; }
        public string isRepeated { get; set; }
        public string[]  levelOfCare { get; set; }
        public string  dimension { get; set; }
    }
}
