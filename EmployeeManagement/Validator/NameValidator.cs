using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace EmployeeManagement.Validator
{
    public class NameValidator : PropertyValidator
    {
        public NameValidator() : base("WhiteSpace,Digits and Special Characters are Not Valid") { }
        protected override bool IsValid(PropertyValidatorContext contect)
        {
            Regex regex = new Regex(@"[\p{L} ]+$", RegexOptions.IgnoreCase);
            if (contect.PropertyValue != null)
            {
                return regex.IsMatch(contect.PropertyName);
            }
            else
            {
                return false;
            }

        }
    }
}


