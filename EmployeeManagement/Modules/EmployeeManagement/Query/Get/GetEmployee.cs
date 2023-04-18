using EmployeeManagement.Model.EmployeeModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EmployeeManagement.Model.ResponseModel.ExceptionModel;

namespace EmployeeManagement.Modules.EmployeeManagement.Query.Get
{
    public class GetEmployee : IRequest<List<EmployeeModel>>
    {
        public class GetEmployeeHandler : IRequestHandler<GetEmployee, List<EmployeeModel>>
        {
            private readonly EmployeeDbcontext _context;
            public GetEmployeeHandler(EmployeeDbcontext context) => _context = context;
            public async Task<List<EmployeeModel>> Handle(GetEmployee query, CancellationToken cancellationToken)
            {
                var employee = await _context.Employees.ToListAsync();
                //Fetch Employee Details If Employee Table(Database) Is Not Empty.
                try
                {
                    if (employee.Count != 0)
                    {
                        return employee;
                    }
                    else
                    {
                        //return exception;
                        //throw new EmployeeNotFoundException();
                        //throw new EmployeeNotFoundException(500);
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    throw new EmployeeNotFoundException();

                }
            }
        }
    }
}
