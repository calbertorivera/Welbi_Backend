using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelbiBackend.Entites
{
    public class ProgramAttendance
    {
        public ProgramAttendance(Program program)
        {
            this.id = program.id;
            this.name = program.name;
            this.location = program.location;
            this.allDay = program.allDay;
            this.start = Convert.ToDateTime(program.start).ToString("dddd, dd MMMM yyyy");
            this.end = Convert.ToDateTime(program.end).ToString("dddd, dd MMMM yyyy");
            this.startTime = Convert.ToDateTime(program.start);
            this.tags = program.tags;

            attendees = new List<ProgramAttendanceResident>();
        }

        public String id { get; set; }
        public String name { get; set; }
        public String location { get; set; }
        public bool allDay { get; set; }
        public String start { get; set; }

        [JsonIgnore]
        public DateTime startTime { get; set; }
        public String end { get; set; }
        public String[] tags { get; set; }

        public List<ProgramAttendanceResident> attendees { get; set; }

        public int? attendeesCount { get { return attendees?.Count(); } }

        public String tagsSplitted { get { return tags != null? String.Join(", ", tags.Select(a=>"#"+a)): ""; } }
    }
}
