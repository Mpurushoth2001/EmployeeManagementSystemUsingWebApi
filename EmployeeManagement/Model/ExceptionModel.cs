namespace EmployeeManagement.Model
{
    public class ExceptionModel
    {
        public int Code { get; set; }
        public string Information { get; set; }
        //[Serializable]
        public class EmployeeNotFoundException : Exception
        {            
            public EmployeeNotFoundException() : base(string.Format("Employee Table is Empty")) {  }
            //public EmployeeNotFoundException(int code) : base(string.Format("Employee Table is Empty")) { }

            public int code { get { return 404; } }
            public string Information { get { return "Employee Table is Empty"; } }
        }
    }
}
