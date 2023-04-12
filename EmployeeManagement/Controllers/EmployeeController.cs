using EmployeeManagement.CQRS.Command;
using EmployeeManagement.CQRS.Command.Create;
using EmployeeManagement.CQRS.Command.Delete;
using EmployeeManagement.CQRS.Command.Update;
using EmployeeManagement.CQRS.Query;
using EmployeeManagement.CQRS.Query.Get;
using EmployeeManagement.CQRS.Query.GetById;
using EmployeeManagement.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        
        [HttpDelete("{ID}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteEmployee { EmpId=id}));
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

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetByID(int ID)
        {
            return Ok(await _mediator.Send(new GetEmployeeByID {EmpID=ID}));
        }
    }
}
