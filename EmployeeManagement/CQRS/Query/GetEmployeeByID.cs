using EmployeeManagement.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.CQRS.Query
{
    public class GetEmployeeByID :IRequest<EmployeeModel>
    {
        public int EmpID { get; set; }
        public class GetEmployeeByIDHandler : IRequestHandler<GetEmployeeByID,EmployeeModel>
        {
            private readonly EmployeeDbcontext _context;
            public GetEmployeeByIDHandler(EmployeeDbcontext context) => _context = context;

            public async Task<EmployeeModel> Handle(GetEmployeeByID query,CancellationToken cancellationToken)
            {
                var employees =  _context.Employees.Where(a=>a.EmpId==query.EmpID).FirstOrDefault();
                if (employees == null)return null;
                return employees;
                
            }
        }
    }
}
