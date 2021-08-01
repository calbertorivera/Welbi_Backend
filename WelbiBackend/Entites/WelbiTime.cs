using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelbiBackend.Entites
{
    public class WelbiTime
    {
        private String datetime;
        public String time
        {
            get
            {
                return datetime;
            }
            set
            {
                this.datetime = Convert.ToDateTime(value).ToString();
            }
        }
    }
}
