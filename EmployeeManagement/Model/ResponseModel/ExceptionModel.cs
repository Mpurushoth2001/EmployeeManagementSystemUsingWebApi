namespace EmployeeManagement.Model.ResponseModel
{
    public class ExceptionModel
    {
        //[Serializable]
        public class EmployeeNotFoundException : Exception
        {

            public EmployeeNotFoundException() : base(message: "Employee Table is Empty") { }

            
        }
        public class InvalidIDException : Exception
        {
            public InvalidIDException() : base(message: "Invalid Employee ID") { }
           
        }
        public class EmployeeNullException : Exception 
        {
            public EmployeeNullException():base(message:"Invalid Entity"){ }
        }

    }
}
