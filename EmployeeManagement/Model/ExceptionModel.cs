using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Web.Http.Filters;
using ExceptionFilterAttribute = System.Web.Http.Filters.ExceptionFilterAttribute;

namespace EmployeeManagement.Model
{
    public class ExceptionModel
    {
        //[Serializable]
        public class EmployeeNotFoundException : Exception
        {

            public EmployeeNotFoundException() : base(message: "Employee Table is Empty") { }

            public override string StackTrace { get { return ""; } }
        }
        public class InvalidIDException : Exception
        {
            public InvalidIDException() : base(message: "Invalid Employee ID") { }
            public override string StackTrace { get { return string.Empty; } }
        }

    }
}
