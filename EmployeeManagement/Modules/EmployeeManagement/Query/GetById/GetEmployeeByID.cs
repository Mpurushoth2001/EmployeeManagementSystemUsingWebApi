using EmployeeManagement.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Modules.EmployeeManagement.Query.GetById
{
    public class GetEmployeeByID : IRequest<EmployeeModel>
    {
        public int EmpID { get; set; }
        public class GetEmployeeByIDHandler : IRequestHandler<GetEmployeeByID, EmployeeModel>
        {
            private readonly EmployeeDbcontext _context;
            public GetEmployeeByIDHandler(EmployeeDbcontext context) => _context = context;

            public async Task<EmployeeModel> Handle(GetEmployeeByID query, CancellationToken cancellationToken)
            {
                var employees = _context.Employees.Where(a => a.EmpId == query.EmpID).FirstOrDefault();
                //Fetch Employee Details By Id 
                if (employees != null)
                {
                    return employees;
                }
                else
                {
                    throw new NullReferenceException("Invalid Employee ID");
                }
            }
        }
    }
}
