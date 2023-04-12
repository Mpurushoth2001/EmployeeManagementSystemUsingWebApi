using EmployeeManagement.Validator;
using FluentValidation;
using System.Text.RegularExpressions;

namespace EmployeeManagement.Modules.EmployeeManagement.Command.Update
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployee>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(x => x.EmpId).NotEmpty().NotNull().OverridePropertyName("Employee ID");

            RuleFor(x => x.FirstName).Cascade(CascadeMode.StopOnFirstFailure)
                 .NotEmpty().WithMessage("{PropertyName} is empty")
                 .Length(3, 25).WithMessage("{PropertyName} must contain valid Name")
                 .SetValidator(new NameValidator()).WithMessage("{PropertyName} should be Letters");

            RuleFor(x => x.Lastname).SetValidator(new NameValidator()).WithMessage("WhiteSpace,digits and special characters are Not Valid")
                .When(y => !string.IsNullOrEmpty(y.Lastname));

            RuleFor(x => x.Gender).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("The Field is empty")
                .SetValidator(new GenderValidator()).WithMessage("Invalid Character")
                .SetValidator(new NameValidator()).WithMessage("Digits and Special Characters are Not Valid");

            RuleFor(x => x.DOB).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Date of Birth can't be empty")
                .NotNull().Must(BeAValidAge).WithMessage("Invalid Date Of Birth");

            RuleFor(x => x.Designation).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("The Field is empty")
                .Length(3, 25)
                .SetValidator(new NameValidator());

        }
        protected bool BeAValidAge(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int dobYear = date.Year;

            if (dobYear <= currentYear && dobYear > currentYear - 120)
            {
                return true;
            }

            return false;
        }
    }
}
