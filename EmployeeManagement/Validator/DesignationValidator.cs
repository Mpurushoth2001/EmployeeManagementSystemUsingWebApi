using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace EmployeeManagement.Validator
{
    public class DesignationValidator:PropertyValidator
    {
        public DesignationValidator():base("Invalid Designation")
        { }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            string designation = (string)context.PropertyValue;
            Regex regex = new Regex(@"^[a-z\s]+$", RegexOptions.IgnoreCase);
            if (context.PropertyValue != null)
            {
                return regex.IsMatch(designation);
            }
            else
            {
                return false;
            }
        }
        
    }
}
