using EmployeeManagement.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.CQRS.Query
{
    public class GetEmployeeByID :IRequest<EmployeeModel>
    {
        public int EmployeeID { get; set; }
        public class GetEmployeeByIDHandler : IRequestHandler<GetEmployeeByID,EmployeeModel>
        {
            private readonly EmployeeDbcontext _context;
            public GetEmployeeByIDHandler(EmployeeDbcontext context) => _context = context;

            public async Task<EmployeeModel> Handle(GetEmployeeByID query,CancellationToken cancellationToken)
            {
                var employees = await _context.Employees.Where(a=>a.EmpId==query.EmployeeID).FirstOrDefaultAsync();
                if (employees == null)
                {
                    return null;
                }
                Console.WriteLine("Employee Record For the ID "+query.EmployeeID);
                return employees;
                
            }
        }
    }
}
