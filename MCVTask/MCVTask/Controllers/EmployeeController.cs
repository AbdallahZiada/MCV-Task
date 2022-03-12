using MCVTask.AppService.AppService.Employee.AddEmployee;
using MCVTask.AppService.AppService.Employee.DeleteEmployee;
using MCVTask.AppService.AppService.Employee.EmployeeList;
using MCVTask.AppService.AppService.Employee.GetEmployee;
using MCVTask.AppService.AppService.Employee.UpdateEmployee;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MCVTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IMediator Mediator { get; }

        public EmployeeController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(int pageIndex = 0)
        {
            return Ok(await Mediator.Send(new EmployeeListCommand(pageIndex)));
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetEmployeeCommand(id)));
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(AddEmployeeCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateEmployeeCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(DeleteEmployeeCommand request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}