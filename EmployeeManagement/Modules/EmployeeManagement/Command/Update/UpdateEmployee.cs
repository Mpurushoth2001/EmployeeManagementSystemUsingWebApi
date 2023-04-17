﻿using EmployeeManagement.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static EmployeeManagement.Model.ExceptionModel;
using static EmployeeManagement.Model.Globals;

namespace EmployeeManagement.Modules.EmployeeManagement.Command.Update
{
    public class UpdateEmployee : IRequest<EntityResponse>
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public char Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Designation { get; set; }

        

        public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployee, EntityResponse>
        {
            private readonly EmployeeDbcontext _context;
            public UpdateEmployeeHandler(EmployeeDbcontext context) => _context = context;

            public async Task<EntityResponse> Handle(UpdateEmployee command, CancellationToken cancellationToken)
            {
                var employees = await _context.Employees.Where(a => a.EmployeeId == command.EmployeeId).FirstOrDefaultAsync();
                EntityResponse response=new EntityResponse();
                //Updates Existing Employee Record 
                try
                {
                    if (employees != null)
                    {
                        employees.FirstName = command.FirstName;
                        employees.Lastname = command.Lastname;
                        employees.Gender = command.Gender;
                        employees.DOB = command.DOB;
                        employees.Designation = command.Designation;
                        response.ResponseId = await _context.SaveChangesAsync();
                        response.AdditionalInfo = "1 Row Affected";
                        response.AdditionalInfo = "Employee Details Are Updated";
                        return response;
                    }
                    else
                    {
                        throw new Exception("Invalid  Employee ID");
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidIDException();
                }

            }
        }
    }
}
