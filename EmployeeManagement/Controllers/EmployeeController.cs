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
        public EmployeeController(IMediator mediator) => _mediator = mediator;



        /// <summary>
        /// Employee Create
        /// </summary>
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
        /// Employee Update
        /// </summary>
        /// <param name="Update"></param>
        /// <returns></returns>

        [HttpPut]

        public async Task<EntityResponse> UpdateEmployeeRecord(UpdateEmployee Update)
        {
            var result = await _mediator.Send(Update);
            return result;
        }

        /// <summary>
        /// Employee Delete by Employee ID
        /// </summary>
        /// <param name="Delete"></param>
        /// <returns></returns>

        [HttpDelete]
        public async Task<EntityResponse> DeleteEmployeeRecord(DeleteEmployee Delete)
        {
            var result = await _mediator.Send(Delete);
            return result;
        }

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
        #region
        //public async Task<IHttpActionResult> GetAllEmployeeRecords()
        //{
        //    try
        //    {
        //        return (IHttpActionResult)Ok(await _mediator.Send(new GetEmployee()));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
        //        {
        //            Content = new StringContent(ex.Message)
        //        });
        //    }
        //}
        #endregion


        /// <summary>
        /// Get An Employee Record By Mentioning ID
        /// </summary>
        /// <param name="GetEmployeeByID"></param>
        /// <returns></returns>

        [HttpGet("{id}")]

        public async Task<IActionResult> GetByEmployeeID(GetEmployeeByID GetEmployeeByID)
        {
            return Ok(await _mediator.Send(GetEmployeeByID));
        }
    }
}