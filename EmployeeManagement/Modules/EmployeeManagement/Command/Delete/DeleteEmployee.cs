﻿using Azure;
using EmployeeManagement.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Modules.EmployeeManagement.Command.Delete
{
    public class DeleteEmployee : IRequest<EntityResponse>
    {
        public int EmployeeId { get; set; }
        public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployee, EntityResponse>
        {
            private readonly EmployeeDbcontext _context;
            public DeleteEmployeeHandler(EmployeeDbcontext context) => _context = context;
            public async Task<EntityResponse> Handle(DeleteEmployee command, CancellationToken cancellationToken)
            {
                var employees = await _context.Employees.Where(a => a.EmpId == command.EmployeeId).FirstOrDefaultAsync();
                EntityResponse response=new EntityResponse();

                //Delete Employee Record When Given Id is Not Empty.
                if (employees != null) 
                {
                    _context.Employees.Remove(employees);
                    response.ResponseId = await _context.SaveChangesAsync();
                    response.AdditionalInfo = "1 Row Affected";
                    response.AdditionalInfo = "Employee Record is Deleted";
                    return response;
                }
                else
                {
                    throw new Exception("Invalid Employee ID");
                }
            }
        }
    }
}
