using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace EmployeeManagement.Validator
{
    public class NameValidator : PropertyValidator
    {
        public NameValidator() : base("Invalid {PropertyName}") { }
        protected override bool IsValid(PropertyValidatorContext contect)
        {
            //Allows only Alphabets
            string name = (string)contect.PropertyValue;
            Regex regex = new Regex(@"^[a-z]+$", RegexOptions.IgnoreCase);
            
            //Checks The Value Is null
            if (contect.PropertyValue != null)
            {
                return regex.IsMatch(name);
            }
            else
            {
                return false;
            }
        }
    }
}


