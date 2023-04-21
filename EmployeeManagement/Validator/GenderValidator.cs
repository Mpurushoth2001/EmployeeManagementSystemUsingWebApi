using FluentValidation.Validators;

namespace EmployeeManagement.Validator
{
    public class GenderValidator : PropertyValidator
    {
        public GenderValidator() : base("Invalid Gender")
        {
        }
        //Allows Char Represent Male and Female
        protected override bool IsValid(PropertyValidatorContext context)
        {
            
             return context.PropertyValue is (object)'M' or (object)'m' or (object)'F' or (object)'f';
            
        }
    }
}
