using EmployeeManagement.Model;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.CQRS.Command
{
    public class CreateEmployee :IRequest<EntityResponse>
    {
        public string Name { get; set; }     
        public string Lastname { get; set; }
        public char Sex { get; set; }
        public DateTime DOB { get; set; }
        public string Designation { get; set; }
        public class CreateEmployeeHandler : IRequestHandler<CreateEmployee, EntityResponse>
        {
            private readonly EmployeeDbcontext _context;
            public CreateEmployeeHandler(EmployeeDbcontext context) => _context = context;
            public async Task<EntityResponse> Handle(CreateEmployee command,CancellationToken cancellationToken)
            {
                try
                {
                    var employees = new EmployeeModel();
                    EntityResponse result = new EntityResponse();
                    employees.Name = command.Name;
                    employees.Lastname = command.Lastname;
                    employees.Sex = command.Sex;
                    employees.DOB = command.DOB;
                    employees.Designation = command.Designation;
                    _context.Employees.Add(employees);
                    await _context.SaveChangesAsync();
                    string output = "New Employee Data Created ID is " + employees.EmpId;
                    result.id = employees.EmpId;
                }
                catch (Exception ex) { 
                }
                return default;
            }
        }
    }
}
