using EmployeeManagement.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.CQRS.Command
{
    public class DeleteEmployee :IRequest<string>
    {
        public int EmpId { get; set; }
        public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployee, string>
        {
            private readonly EmployeeDbcontext _context;
            public DeleteEmployeeHandler(EmployeeDbcontext context) => _context = context;
            public async Task<string> Handle(DeleteEmployee command,CancellationToken cancellationToken)
            {
                var employees=await _context.Employees.Where(a=>a.EmpId==command.EmpId).FirstOrDefaultAsync();
                
                
                if (employees == null) return null;
                _context.Employees.Remove(employees);
                await _context.SaveChangesAsync();
                string output = "Record is Deleted";
                return output;
            }
        }
    }
}
