using FluentValidation.Validators;

namespace EmployeeManagement.Validator
{
    public class GenderValidator : PropertyValidator
    {
        public GenderValidator() : base("WhiteSpace,Digits and Special Characters are Not Valid")
        {
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue != null)
            {
                return context.PropertyValue is (object)'M' or (object)'m' or (object)'F' or (object)'f';
            }
            return false;
        }
    }
}
