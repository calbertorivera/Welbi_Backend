using System;
using System.Collections.Generic;
using System.Text;

namespace TaxesYOtros.Classes
{
    public class IsValidLengthRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public int MinimumLength { get; set; }
        public int MaximumLength { get; set; }


        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            if (MaximumLength == 0)
            {
                MaximumLength = 200;
            }

            return str.Length>=MinimumLength && str.Length <= MaximumLength;
        }
    }
}
