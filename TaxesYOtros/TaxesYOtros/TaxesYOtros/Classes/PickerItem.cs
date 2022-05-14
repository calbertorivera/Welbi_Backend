using System;
using System.Collections.Generic;
using System.Text;

namespace TaxesYOtros.Classes
{
    public  class PickerItem
    {
        public PickerItem(string id, string description)
        {
            Id = id;
            Description = description;
        }


        public string Id { get; set; }
        public string Description { get; set; }
    }
}
