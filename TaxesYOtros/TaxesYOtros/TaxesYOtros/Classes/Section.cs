using System;
using System.Collections.Generic;
using System.Text;

namespace TaxesYOtros.Classes
{
    public class Section
    {
      
        public Section(Constants.Sections sct, string name, bool status)
        {
            this.name = name;
            this.status = status;
            this.sect = sct;
        }

        public string name { get; set; }
        public bool status { get; set; }
        public Constants.Sections sect { get;  set; }
    }
}
