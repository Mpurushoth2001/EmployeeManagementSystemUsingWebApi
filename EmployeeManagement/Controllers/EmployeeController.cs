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
        public async Task<IActionResult> Create(CreateEmployee Create) 
        {
            return Ok(await _mediator.Send(Create));
        }

        /// <summary>
        /// Employee Update
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>

        [HttpPut]

        public async Task<IActionResult> Update(UpdateEmployee update)
        {
            return Ok(await _mediator.Send(update));
        }

        /// <summary>
        /// Employee Delete by Employee ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int ID)
        {
            return Ok(await _mediator.Send(new DeleteEmployee { EmpId=ID}));
        }

        /// <summary>
        /// Get All The Employee Records
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetEmployee()));
        }

        /// <summary>
        /// Get An Employee Record By Mentioning ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetByID(int ID)
        {
            return Ok(await _mediator.Send(new GetEmployeeByID {EmpID=ID}));
        }
    }
}
