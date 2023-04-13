using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace EmployeeManagement.Validator
{
    public class NameValidator : PropertyValidator
    {
        public NameValidator() : base("{PropertyName} should be Letters") { }
        protected override bool IsValid(PropertyValidatorContext contect)
        {
            string name = (string)contect.PropertyValue;
            Regex regex = new Regex(@"^[a-z]+$", RegexOptions.IgnoreCase);
            if (contect.PropertyValue != null)
            {
                return regex.IsMatch(name);
            }
            else
            {
                return false;
            }

            //string data = (string)contect.PropertyValue;
            //if (contect.PropertyValue != null)
            //{
            //    return data.All(char.IsLetter);
            //}
            //else
            //{
            //    return false;
            //}

        }
    }
}


