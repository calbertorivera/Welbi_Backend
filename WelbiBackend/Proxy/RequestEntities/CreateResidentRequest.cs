using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelbiBackend.Proxy.RequestEntities
{
    [Serializable]
    public class CreateResidentRequest
    {       
        public String name { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String preferredName { get; set; }
        public String status { get; set; }
        public String room { get; set; }
        public String levelOfCare { get; set; }
        public String ambulation { get; set; }
        public String birthDate { get; set; }
        public String moveInDate { get; set; }
        public String[] attendance { get; set; }
    }

}
