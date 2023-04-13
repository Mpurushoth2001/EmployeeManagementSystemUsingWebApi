using FluentValidation.Validators;

namespace EmployeeManagement.Validator
{
    public class DateOfBirthValitator : PropertyValidator
    {
        public DateOfBirthValitator():base("Invalid Date Of Birth")
        {
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            DateTime date = (DateTime)context.PropertyValue;
            int currentYear = DateTime.Now.Year;
            int dobYear = date.Year;
            if (context.PropertyValue!=null)
            {
                if (dobYear <= currentYear && dobYear > currentYear - 60)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
