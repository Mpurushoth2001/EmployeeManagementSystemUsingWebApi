using EmployeeManagement.Model;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Modules.EmployeeManagement.Command.Create
{
    public class CreateEmployee : IRequest<string>
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public char Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Designation { get; set; }
        public class CreateEmployeeHandler : IRequestHandler<CreateEmployee, string>
        {
            private readonly EmployeeDbcontext _context;
            public CreateEmployeeHandler(EmployeeDbcontext context) => _context = context;
            public async Task<string> Handle(CreateEmployee command, CancellationToken cancellationToken)
            {
                try
                {
                    var employees = new EmployeeModel();
                    employees.Name = command.FirstName;
                    employees.Lastname = command.Lastname;
                    employees.Sex = command.Gender;
                    employees.DOB = command.DOB;
                    employees.Designation = command.Designation;
                    _context.Employees.Add(employees);
                    await _context.SaveChangesAsync();
                    string output = "New Employee Data Created ID is " + employees.EmpId;
                }
                catch (Exception e)
                {
                    return "Invalid Identity";
                }
                return "Success";
            }
        }
    }
}
