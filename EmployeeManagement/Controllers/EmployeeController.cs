using EmployeeManagement.Model.ResponseModel;
using EmployeeManagement.Modules.EmployeeManagement.Command.Create;
using EmployeeManagement.Modules.EmployeeManagement.Command.Delete;
using EmployeeManagement.Modules.EmployeeManagement.Command.Update;
using EmployeeManagement.Modules.EmployeeManagement.Query.Get;
using EmployeeManagement.Modules.EmployeeManagement.Query.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag]
    [ApiVersion("1")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)=>_mediator = mediator;


        /// <summary>
        /// Used to Create New Employee
        /// </summary>
        /// <remarks>
        /// {
        /// 
        ///     "FirstName":"Vishnu",
        ///     
        ///     "Lastname":"Palani",
        ///     
        ///     "Designation":"Developer",
        ///     
        ///     "DOB":"2001/10/23",
        ///     
        ///     "Gender":'M'
        ///     
        /// }
        /// </remarks>
        /// <param name="Create"></param>
        /// <returns>Employee Record Creation</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<EntityResponse> CreateNewEmployee(CreateEmployee Create)
        {

            var result = await _mediator.Send(Create);
            HttpContext.Response.Clear();
            return result;

        }

        /// <summary>
        /// Update Employee Details
        /// </summary>
        ///<remarks>
        ///{
        ///
        ///     "EmployeeID":2,
        ///     
        ///     "FirstName":"Vishnu",
        ///     
        ///     "Lastname":"Palani",
        ///     
        ///     "Designation":"Developer",
        ///     
        ///     "DOB":"2001/10/23",
        ///     
        ///     "Gender":'M'
        ///     
        /// }
        /// </remarks> 
        /// <param name="Update"></param>
        /// <returns></returns>

        [HttpPut]

        public async Task<EntityResponse> UpdateEmployeeRecord(UpdateEmployee Update)
        {
            var result = await _mediator.Send(Update);
            return result;
        }

        /// <summary>
        /// Delete Employee Record by Employee ID
        /// </summary>
        /// <remarks>
        /// {
        /// 
        ///    "EmployeeID":2 
        ///    
        /// }
        /// </remarks>
        /// <param name="Delete"></param>
        /// <returns></returns>

        [HttpDelete]
        public async Task<EntityResponse> DeleteEmployeeRecord(DeleteEmployee Delete)
        {
            var result = await _mediator.Send(Delete);
            return result;
        }
        #region Queries
        /// <summary>
        /// Get All The Employee Records
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllEmployeeRecords()
        {
            HttpContext.Response.Clear();
            return Ok(await _mediator.Send(new GetEmployee()));
        }


        /// <summary>
        /// Get An Employee Record By Employee ID
        /// </summary>
        /// <remarks>
        /// {
        /// 
        ///    "EmployeeID":2
        ///    
        /// }
        /// </remarks>
        /// <param name="ID"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("GetByEmployeeID")]

        public async Task<IActionResult> GetByEmployeeID(int ID)
        {
            return Ok(await _mediator.Send(new GetEmployeeByID { EmployeeId=ID}));
        }
        #endregion
    }
}