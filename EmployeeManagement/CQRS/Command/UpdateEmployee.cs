using EmployeeManagement.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.CQRS.Command
{
    public class UpdateEmployee:IRequest<string>
    {
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public char Sex { get; set; }
        public DateTime DOB { get; set; }
        public string Designation { get; set; }

        public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployee, string>
        {
            private readonly EmployeeDbcontext _context;
            public UpdateEmployeeHandler(EmployeeDbcontext context) => _context = context;
            
            public async Task<string> Handle(UpdateEmployee command,CancellationToken cancellationToken)
            {
                var employees = await _context.Employees.Where(a => a.EmpId == command.EmpId).FirstOrDefaultAsync();

                if (employees == null)
                {
                    string op = "Employee ID is Null";
                    return op;
                }
                else
                {
                    employees.Name = command.Name;
                    employees.Lastname = command.Lastname;
                    employees.Sex = command.Sex;
                    employees.DOB = command.DOB;
                    employees.Designation = command.Designation;
                    await _context.SaveChangesAsync();
                    string Op = "Record is Updated";
                    return Op;
                }
            }
        }
    }
}
