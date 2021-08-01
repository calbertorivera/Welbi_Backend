using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelbiBackend.Entites
{
    public class ProgramAttendanceResident
    {
        public ProgramAttendanceResident(string id, string name, string room)
        {
            this.id = id;
            this.name = name;
            this.room = room;
        }

        public String id { get; set; }
        public String name { get; set; }
        public String room { get; set; }
        public String eventStatus { get; set; }

    }
}
