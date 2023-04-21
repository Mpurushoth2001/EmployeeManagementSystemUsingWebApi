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

        #region Command

        /// <summary>
        /// Used to Create New Employee
        /// </summary>
        /// <remarks>
        /// 
        /// Example Value
        /// ------- -----
        /// 
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<EntityResponse> CreateNewEmployee(CreateEmployee Create)
        {

            var result = await _mediator.Send(Create);
            return result;

        }

        /// <summary>
        /// Update Employee Details
        /// </summary>
        ///<remarks>
        ///
        /// Example Value
        /// ------- -----
        /// 
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<EntityResponse> UpdateEmployeeRecord(UpdateEmployee Update)
        {
            var result = await _mediator.Send(Update);
            return result;
        }

        /// <summary>
        /// Delete Employee Record by Employee ID
        /// </summary>
        /// <remarks>
        /// 
        /// Example Value
        /// ------- -----
        ///
        /// {
        /// 
        ///    "EmployeeID":2 
        ///    
        /// }
        /// </remarks>
        /// <param name="Delete"></param>
        /// <returns></returns>

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<EntityResponse> DeleteEmployeeRecord(DeleteEmployee Delete)
        {
            var result = await _mediator.Send(Delete);
            return result;
        }
        #endregion


        #region Queries
        /// <summary>
        /// Get All The Employee Records
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEmployeeRecords()
        {            
            return Ok(await _mediator.Send(new GetEmployee()));
        }


        /// <summary>
        /// Get An Employee Record By Employee ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("GetByEmployeeID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByEmployeeID(int ID)
        {
            return Ok(await _mediator.Send(new GetEmployeeByID { EmployeeId=ID}));
        }
        #endregion
    }
}