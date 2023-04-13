using EmployeeManagement.Model;
using EmployeeManagement.Modules.EmployeeManagement.Command.Create;
using EmployeeManagement.Modules.EmployeeManagement.Command.Delete;
using EmployeeManagement.Modules.EmployeeManagement.Command.Update;
using EmployeeManagement.Modules.EmployeeManagement.Query.Get;
using EmployeeManagement.Modules.EmployeeManagement.Query.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public EmployeeController(IMediator mediator)=>_mediator=mediator;
        /// <summary>
        /// Employee Create
        /// </summary>
        /// <param name="Create"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<EntityResponse> CreateNewEmployee(CreateEmployee Create) 
        {
            var result = await _mediator.Send(Create);
            return result;
        }

        /// <summary>
        /// Employee Update
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>

        [HttpPut]

        public async Task<EntityResponse> UpdateEmployeeRecord(UpdateEmployee update)
        {
            var result=await _mediator.Send(update);
            return result;
        }

        /// <summary>
        /// Employee Delete by Employee ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpDelete]
        public async Task<EntityResponse> DeleteEmployeeRecord(int ID)
        {
            var result=await _mediator.Send(new DeleteEmployee { EmployeeId =ID});
            return result;
        }

        /// <summary>
        /// Get All The Employee Records
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeRecords()
        {
            return Ok(await _mediator.Send(new GetEmployee()));
        }

        /// <summary>
        /// Get An Employee Record By Mentioning ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetByEmployeeID(int ID)
        {
            return Ok(await _mediator.Send(new GetEmployeeByID {EmpID=ID}));
        }
    }
}
