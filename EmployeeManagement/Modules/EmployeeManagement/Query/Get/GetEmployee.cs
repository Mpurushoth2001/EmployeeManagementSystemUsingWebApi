using Azure.Core;
using EmployeeManagement.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
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
                try
                {
                    if (employee.Count != 0)
                    {
                        return employee.AsReadOnly();
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
                    throw new EmployeeNotFoundException(500);

                }
            }
        }
    }
}
