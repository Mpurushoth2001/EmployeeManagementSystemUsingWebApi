using EmployeeManagement.CQRS.Command;
using EmployeeManagement.CQRS.Query;
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
        /// <param name="emp"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployee emp) 
        {
            return Ok(await _mediator.Send(emp));
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
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteEmployee { EmpId=id}));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetEmployee()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int ID)
        {
            return Ok(await _mediator.Send(new GetEmployeeByID()));
        }
    }
}
