using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelbiBackend.Entites
{
    public class Program
    {
        public String id { get; set; }
        public String name { get; set; }
        public String location { get; set; }
        public bool allDay { get; set; }
        public String start { get; set; }
        public String end { get; set; }

        public String[] tags { get; set; }
        public Attendance[] attendance { get; set; }
    }
}