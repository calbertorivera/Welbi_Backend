using System;
using System.Collections.Generic;
using System.Text;

namespace TaxesYOtros.Classes
{
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }


            if (typeof(T) == typeof(String))
            {
                var str = value as string;
                return !string.IsNullOrWhiteSpace(str);
            }

            if (typeof(T) == typeof(DateTime?) || typeof(T) == typeof(DateTime))
            {
                return value != null;
            }

            throw new Exception("Type not supported");



        }
    }
}
