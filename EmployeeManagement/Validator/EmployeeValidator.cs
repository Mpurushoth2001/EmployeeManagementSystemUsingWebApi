﻿using EmployeeManagement.Model;
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
                .SetValidator(new NameValidator());

            RuleFor(x => x.Lastname).SetValidator(new NameValidator())
                .When(y => !string.IsNullOrEmpty(y.Lastname));

            RuleFor(x => x.Sex).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("The Field is empty")
                .SetValidator(new GenderValidator())
                .SetValidator(new NameValidator());

            RuleFor(x => x.DOB).NotEmpty().NotNull().Must(BeAValidAge).WithMessage("Invalid Date Of Birth");

            RuleFor(x => x.Designation).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("The Field is empty")
                .Length(3, 25)
                .SetValidator(new NameValidator());
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
