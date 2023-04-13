using FluentValidation;

namespace EmployeeManagement.Modules.EmployeeManagement.Command.Delete
{
    public class DeleteEmployeeValidator : AbstractValidator<DeleteEmployee>
    {
        public DeleteEmployeeValidator()
        {
            RuleFor(x => x.EmployeeId).NotEmpty().NotNull();
        }
    }
}
