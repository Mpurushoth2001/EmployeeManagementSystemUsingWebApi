using EmployeeManagement.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EmployeeManagement.Model.ExceptionModel;

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
                //Fetch Employee Details If Employee Table(Database) Is Not Empty.
                if (employee.Count != 0)
                {
                    return employee.AsReadOnly();
                }
                else
                {
                    ExceptionModel exception = new ExceptionModel();
                    exception.Code = 500;
                    exception.Information = "Table is empty";

                    //return exception;
                    throw new EmployeeNotFoundException();
                }
            }
        }
    }
}
