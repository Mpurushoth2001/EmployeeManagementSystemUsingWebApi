using EmployeeManagement.Model;
using FluentValidation;
using System.Text.RegularExpressions;

namespace EmployeeManagement.Validator
{

    public class EmployeeValidator : AbstractValidator<EmployeeModel>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Name).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(3, 40).WithMessage("{PropertyName} must contain valid Name")
                .Must(IsValidName).WithMessage("{PropertyName} should be Letters");

            RuleFor(x => x.Lastname).Must(IsValidName).WithMessage("Digits are not valid");

            RuleFor(x => x.Sex).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("The Field is empty")
                .Must(Test).WithMessage("Invalid Character")
                .Must(IsValidName).WithMessage("{PropertyName} can't be digits");

            RuleFor(x => x.DOB).NotEmpty().NotNull().Must(BeAValidAge).WithMessage("Invalid Date Of Birth");

            RuleFor(x => x.Designation).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("The Field is empty")
                .Length(3, 25)
                .Must(ValidDesignation);
        }
        private bool ValidDesignation(string Designation)
        {
            var Digits = new Regex("[\\W]");
            return Digits.IsMatch(Designation);
        }
        private bool Test(char name)
        {
            return ((name == 'M')||(name == 'm')||(name == 'F')||(name == 'f'));
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
