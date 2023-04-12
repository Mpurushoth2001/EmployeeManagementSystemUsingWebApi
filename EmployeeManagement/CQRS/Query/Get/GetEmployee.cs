using EmployeeManagement.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.CQRS.Query.Get
{
    public class GetEmployee : IRequest<IEnumerable<EmployeeModel>>
    {
        public class GetEmployeeHandler : IRequestHandler<GetEmployee, IEnumerable<EmployeeModel>>
        {
            private readonly EmployeeDbcontext _context;
            public GetEmployeeHandler(EmployeeDbcontext context) => _context = context;
            public async Task<IEnumerable<EmployeeModel>> Handle(GetEmployee query, CancellationToken cancellationToken)
            {
                var employee = await _context.Employees.ToListAsync();
                if (employee == null)
                {
                    return null;
                }
                return employee.AsReadOnly();
            }
        }
    }
}
