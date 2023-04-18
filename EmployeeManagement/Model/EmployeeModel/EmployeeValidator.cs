using EmployeeManagement.Validator;
using FluentValidation;

namespace EmployeeManagement.Model.EmployeeModel
{

    public class EmployeeValidator : AbstractValidator<EmployeeModel>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.FirstName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(3, 40).WithMessage("{PropertyName} must contain valid Name")
                .SetValidator(new NameValidator());

            RuleFor(x => x.Lastname).SetValidator(new NameValidator())
                .When(y => !string.IsNullOrEmpty(y.Lastname));

            RuleFor(x => x.Gender).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("The Field is empty")
                .SetValidator(new GenderValidator());

            RuleFor(x => x.DOB).NotEmpty().NotNull().SetValidator(new DateOfBirthValitator());

            RuleFor(x => x.Designation).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("The Field is empty")
                .Length(3, 25)
                .SetValidator(new NameValidator());
        }
    }
}
