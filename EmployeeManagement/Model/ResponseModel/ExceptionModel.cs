namespace EmployeeManagement.Model.ResponseModel
{
    public class ExceptionModel
    {
        
        public class    NoDataFoundException : Exception
        {
            public NoDataFoundException() : base(message: "No Records Found") { }            
        }

        public class EmployeeIDNotFoundException : Exception
        {
            public EmployeeIDNotFoundException() : base(message: "Employee ID Not Found") { }
           
        }
    }
}
