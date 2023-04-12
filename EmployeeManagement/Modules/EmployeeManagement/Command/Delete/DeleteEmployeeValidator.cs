using FluentValidation;

namespace EmployeeManagement.Modules.EmployeeManagement.Command.Delete
{
    public class DeleteEmployeeValidator : AbstractValidator<DeleteEmployee>
    {
        public DeleteEmployeeValidator()
        {
            RuleFor(x => x.EmpId).NotEmpty().NotNull().OverridePropertyName("Employee ID");
        }
    }
}
