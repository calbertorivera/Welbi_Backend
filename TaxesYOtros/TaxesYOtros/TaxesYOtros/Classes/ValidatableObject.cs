using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TaxesYOtros.Classes
{

    public class ValidatableObject<T> : ExtendedBindableObject, IValidity
    {
        public ValidatableObject()
            : base("ValidatableObject")
        {
            _isValid = false;
            _erros = new List<string>();
            _validations = new List<IValidationRule<T>>();
        }

        private readonly List<IValidationRule<T>> _validations;
        private List<string> _erros;
        private T _value;
        private bool _isValid;

        public List<IValidationRule<T>> Validations => _validations;
        public List<string> Errors
        {
            get
            {
                return _erros;
            }
            set
            {
                _erros = value;
                RaisePropertyChanged(() => Errors);
            }
        }
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                RaisePropertyChanged(() => Value);
            }
        }
        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }



        public bool HasValidData()
        {
            Errors.Clear();

            IEnumerable<string> erros = _validations.Where(v => !v.Check(Value))
                                                    .Select(v => v.ValidationMessage);
            Errors = erros.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }
    }
}
