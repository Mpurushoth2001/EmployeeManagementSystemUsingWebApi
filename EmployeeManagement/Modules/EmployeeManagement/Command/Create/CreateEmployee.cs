using EmployeeManagement.Model.EmployeeModel;
using EmployeeManagement.Model.ResponseModel;
using MediatR;
using static EmployeeManagement.Model.ResponseModel.ExceptionModel;

namespace EmployeeManagement.Modules.EmployeeManagement.Command.Create
{
    public class CreateEmployee : IRequest<EntityResponse>
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public char Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Designation { get; set; }


        public class CreateEmployeeHandler : IRequestHandler<CreateEmployee, EntityResponse>
        {
            private readonly EmployeeDbcontext _context;
            public CreateEmployeeHandler(EmployeeDbcontext context) => _context = context;
            public async Task<EntityResponse> Handle(CreateEmployee command, CancellationToken cancellationToken)
            {
                EntityResponse response = new EntityResponse();

                var employees = new EmployeeModel();
                employees.FirstName = command.FirstName;
                employees.Lastname = command.Lastname;
                employees.Gender = command.Gender;
                employees.DOB = command.DOB;
                employees.Designation = command.Designation;
                _context.Employees.Add(employees);

                response.ResponseId = await _context.SaveChangesAsync();
                response.AdditionalInfo = "New Employee Details Added Successfully" +
                "Your Employee ID is " + employees.EmployeeId;
                return response;
            }
        }
    }
}
