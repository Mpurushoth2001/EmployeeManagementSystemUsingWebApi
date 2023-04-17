using Azure;
using EmployeeManagement.Model;
using MediatR;
using System.ComponentModel.DataAnnotations;
using static EmployeeManagement.Model.Globals;

namespace EmployeeManagement.Modules.EmployeeManagement.Command.Create
{
    public class CreateEmployee : IRequest<EntityResponse>
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public char  Gender{ get; set; }
        public DateTime DOB { get; set; }
        public string Designation { get; set; }
        //public enum _Gender
        //{
        //    M,
        //    F
        //};
        
        public class CreateEmployeeHandler : IRequestHandler<CreateEmployee, EntityResponse>
        {
            private readonly EmployeeDbcontext _context;
            public CreateEmployeeHandler(EmployeeDbcontext context) => _context = context;
            public async Task<EntityResponse> Handle(CreateEmployee command, CancellationToken cancellationToken)
            {
                EntityResponse response = new EntityResponse();
                try
                {                    
                    var employees = new EmployeeModel();
                    employees.FirstName = command.FirstName;
                    employees.Lastname = command.Lastname;
                    employees.Gender = command.Gender;
                    employees.DOB = command.DOB;
                    employees.Designation = command.Designation;
                    _context.Employees.Add(employees);
                    await _context.SaveChangesAsync();
                    response.ResponseId = employees.EmployeeId;
                    response.AdditionalInfo= "New Employee Details Added Successfully";
                    return response;
                }
                catch (NullReferenceException e)
                {
                    response.AdditionalInfo = "Invalid Entity";
                }
                return response; 
            }
        }
    }
}
