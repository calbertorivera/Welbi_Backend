using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelbiBackend.Entites
{
    [Serializable]
    public class Attendance
    {
        public String status { get; set; }
        public String programId { get; set; }
        public String author { get; set; }
        public String residentId { get; set; }
    }
}
