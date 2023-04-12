using EmployeeManagement.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Modules.EmployeeManagement.Query.Get
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
                if (employee != null)
                {
                    return employee.AsReadOnly();
                }
                else
                {
                    throw new NullReferenceException("Invalid Entity");
                }
            }
        }
    }
}
