using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelbiBackend.Entites
{
    public class Resident
    {
        public String id { get; set; }
        public String name { get; set; }
      
        public String firstName { get; set; }
     
        public String lastName { get; set; }
     
        public String preferredName { get; set; }
       
        public String status { get; set; }

        public String eventStatus { get; set; }
        public String room { get; set; }
        
        public String levelOfCare { get; set; }
       
        public String ambulation { get; set; }
        
        public WelbiTime birthDate { get; set; }
        
        public WelbiTime moveInDate { get; set; }
        [JsonIgnore]
        public WelbiTime createdAt { get; set; }
        [JsonIgnore]
        public WelbiTime updatedAt { get; set; }


        [JsonIgnore]
        public Attendance[] attendance { get; set; }

    }
}
