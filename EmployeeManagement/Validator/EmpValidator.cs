using EmployeeManagement.Model;
using FluentValidation;

namespace EmployeeManagement.Validator
{
   
    public class EmpValidator : AbstractValidator<EmployeeModel>
    {
        public EmpValidator() 
        {
            RuleFor(x => x.Name).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} must not be empty")
                .Length(3,40).WithMessage("{PropertyName} must contain valid Name")
                .Must(IsValidName).WithMessage("{PropertyName} should be Letters");
            RuleFor(x => x.Sex).NotEmpty().Must(IsValidName).WithMessage("{PropertyName} can't be digits");
            RuleFor(x => x.DOB).Must(BeAValidAge).WithMessage("Invalid Date Of Birth");
            RuleFor(x => x.Designation).NotEmpty().Length(5,60);
        }
        private bool IsValidName(string name)
        {
            return name.All(char.IsLetter);
        }
        private bool IsValidName(char name)
        {
            return (char.IsLetter(name));
        }
        protected bool BeAValidAge(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int dobYear = date.Year;

            if (dobYear <= currentYear && dobYear > (currentYear - 120))
            {
                return true;
            }

            return false;
        }
    }
}
