namespace EmployeeManagement.Model.ResponseModel
{
    public class ExceptionModel
    {
        //[Serializable]
        public class    NoDataFoundException : Exception
        {
            public NoDataFoundException() : base(message: "Employee Table is Empty") { }            
        }

        public class InvalidIDException : Exception
        {
            public InvalidIDException() : base(message: "Invalid Employee ID") { }
           
        }
    }
}
