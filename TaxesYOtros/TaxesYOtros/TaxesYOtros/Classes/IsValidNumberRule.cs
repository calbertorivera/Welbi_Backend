using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TaxesYOtros.Classes
{
    internal class IsValidNumberRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }


        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            try
            {
                if (Regex.IsMatch(str, @"[0-9]+$"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}