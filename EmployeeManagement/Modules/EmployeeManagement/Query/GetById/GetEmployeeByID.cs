using EmployeeManagement.Model.EmployeeModel;
using MediatR;
using static EmployeeManagement.Model.ResponseModel.ExceptionModel;

namespace EmployeeManagement.Modules.EmployeeManagement.Query.GetById
{
    public class GetEmployeeByID : IRequest<EmployeeModel>
    {
        public int EmployeeId { get; set; }
        public class GetEmployeeByIDHandler : IRequestHandler<GetEmployeeByID, EmployeeModel>
        {
            private readonly EmployeeDbcontext _context;
            public GetEmployeeByIDHandler(EmployeeDbcontext context) => _context = context;

            public async Task<EmployeeModel> Handle(GetEmployeeByID query, CancellationToken cancellationToken)
            {
                var employees = _context.Employees.Where(a => a.EmployeeId == query.EmployeeId).FirstOrDefault();
                
                //Fetch Employee Details
                if (employees != null)
                {
                    return employees;
                }
                else
                {
                    throw new InvalidIDException();
                    
                }
            }
        }
    }
}
